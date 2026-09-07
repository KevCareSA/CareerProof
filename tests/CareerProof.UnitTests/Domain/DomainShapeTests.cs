using System.Reflection;
using CareerProof.Api.Domain;
using Xunit;

namespace CareerProof.UnitTests.Domain;

public sealed class DomainShapeTests
{
    private static readonly Dictionary<Type, (string Name, Type ClrType, bool Nullable)[]> Shapes = new()
    {
        [typeof(NotionSource)] =
        [
            ("Id", typeof(Guid), false),
            ("ExternalSourceId", typeof(string), false),
            ("DisplayName", typeof(string), false),
        ],
        [typeof(SyncRun)] =
        [
            ("Id", typeof(Guid), false),
            ("NotionSourceId", typeof(Guid), false),
            ("StartedAt", typeof(DateTimeOffset), false),
            ("CompletedAt", typeof(DateTimeOffset?), true),
        ],
        [typeof(RawActivity)] =
        [
            ("Id", typeof(Guid), false),
            ("SyncRunId", typeof(Guid), false),
            ("ExternalRecordId", typeof(string), false),
            ("RawPayload", typeof(string), false),
            ("DayLevelMinutes", typeof(int?), true),
            ("DeclaredSessionCount", typeof(int?), true),
        ],
        [typeof(ParsedActivity)] =
        [
            ("Id", typeof(Guid), false),
            ("RawActivityId", typeof(Guid), false),
            ("SessionOrdinal", typeof(int), false),
            ("ActivityType", typeof(ActivityType), false),
            ("SkillId", typeof(string), true),
            ("ClassifierVersion", typeof(string), false),
            ("TaxonomyVersion", typeof(string), false),
        ],
        [typeof(Skill)] =
        [
            ("Id", typeof(string), false),
            ("DisplayName", typeof(string), false),
        ],
        [typeof(SkillAlias)] =
        [
            ("Alias", typeof(string), false),
            ("TargetSkillId", typeof(string), false),
        ],
        [typeof(PracticeSession)] =
        [
            ("Id", typeof(Guid), false),
            ("ParsedActivityId", typeof(Guid), false),
            ("SkillId", typeof(string), false),
            ("OccurredAt", typeof(DateTimeOffset), false),
        ],
        [typeof(Evidence)] =
        [
            ("Id", typeof(Guid), false),
            ("EvidenceType", typeof(string), false),
            ("Reference", typeof(string), false),
            ("SkillId", typeof(string), true),
        ],
        [typeof(MasterySnapshot)] =
        [
            ("Id", typeof(Guid), false),
            ("SkillId", typeof(string), false),
            ("Score", typeof(decimal), false),
            ("ScoreVersion", typeof(string), false),
            ("ClassifierVersion", typeof(string), false),
            ("TaxonomyVersion", typeof(string), false),
            ("CalculatedAt", typeof(DateTimeOffset), false),
        ],
        [typeof(ClassificationReview)] =
        [
            ("Id", typeof(Guid), false),
            ("ParsedActivityId", typeof(Guid), false),
            ("CreatedAt", typeof(DateTimeOffset), false),
            ("ResolvedAt", typeof(DateTimeOffset?), true),
        ],
    };

    [Fact]
    public void Domain_ContainsExactlyApprovedClassesAndEnum()
    {
        var actual = typeof(RawActivity).Assembly.GetTypes()
            .Where(type => type.Namespace == typeof(RawActivity).Namespace)
            .OrderBy(type => type.Name, StringComparer.Ordinal).ToArray();
        var expected = Shapes.Keys.Append(typeof(ActivityType))
            .OrderBy(type => type.Name, StringComparer.Ordinal).ToArray();

        Assert.Equal(expected, actual);
        Assert.True(typeof(ActivityType).IsEnum);
    }

    [Fact]
    public void Entities_HaveExactGetterOnlyShapeAndPreserveConstructorValues()
    {
        var nullability = new NullabilityInfoContext();
        foreach (var (type, shape) in Shapes)
        {
            Assert.True(type.IsClass);
            Assert.True(type.IsPublic);
            Assert.Equal(typeof(object), type.BaseType);
            Assert.Empty(type.GetInterfaces());
            Assert.Empty(type.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly));
            Assert.DoesNotContain(type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly),
                method => !method.IsSpecialName);

            var properties = type.GetProperties();
            Assert.Equal(shape.Select(field => field.Name).OrderBy(name => name, StringComparer.Ordinal),
                properties.Select(property => property.Name).OrderBy(name => name, StringComparer.Ordinal));

            var constructor = Assert.Single(type.GetConstructors());
            var parameters = constructor.GetParameters();
            Assert.Equal(shape.Select(field => field.ClrType), parameters.Select(parameter => parameter.ParameterType));
            Assert.All(parameters, parameter => Assert.False(parameter.HasDefaultValue));

            var values = shape.Select(field => SampleValue(field.ClrType)).ToArray();
            var entity = constructor.Invoke(values);
            for (var index = 0; index < shape.Length; index++)
            {
                var field = shape[index];
                var property = type.GetProperty(field.Name)!;
                Assert.Equal(field.ClrType, property.PropertyType);
                Assert.True(property.GetMethod!.IsPublic);
                Assert.Null(property.GetSetMethod(true));
                Assert.Equal(values[index], property.GetValue(entity));
                if (field.ClrType == typeof(string))
                {
                    var expectedState = field.Nullable ? NullabilityState.Nullable : NullabilityState.NotNull;
                    Assert.Equal(expectedState, nullability.Create(property).ReadState);
                    Assert.Equal(expectedState, nullability.Create(parameters[index]).ReadState);
                }
            }

            var absentValues = shape.Select(field => field.Nullable ? null : SampleValue(field.ClrType)).ToArray();
            var withAbsentValues = constructor.Invoke(absentValues);
            foreach (var field in shape.Where(field => field.Nullable))
            {
                Assert.Null(type.GetProperty(field.Name)!.GetValue(withAbsentValues));
            }
        }
    }

    private static object SampleValue(Type type)
    {
        if (type == typeof(Guid))
        {
            return Guid.NewGuid();
        }

        if (type == typeof(string))
        {
            return "csharp";
        }

        if (type == typeof(int) || type == typeof(int?))
        {
            return 2;
        }

        if (type == typeof(DateTimeOffset) || type == typeof(DateTimeOffset?))
        {
            return new DateTimeOffset(2026, 1, 2, 10, 0, 0, TimeSpan.FromHours(2));
        }

        if (type == typeof(decimal))
        {
            return 12.345m;
        }

        if (type == typeof(ActivityType))
        {
            return ActivityType.Learning;
        }

        throw new InvalidOperationException("Unexpected type in approved shape.");
    }
}
