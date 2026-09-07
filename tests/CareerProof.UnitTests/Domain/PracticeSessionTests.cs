using CareerProof.Api.Domain;
using Xunit;

namespace CareerProof.UnitTests.Domain;

public sealed class PracticeSessionTests
{
    [Fact]
    public void Constructor_PreservesSuppliedIdentitySkillAndSourceTime()
    {
        var id = Guid.NewGuid();
        var parsedId = Guid.NewGuid();
        var occurredAt = new DateTimeOffset(2026, 1, 2, 9, 30, 0, TimeSpan.FromHours(2));
        var session = new PracticeSession(id, parsedId, "csharp", occurredAt);

        Assert.Equal(id, session.Id);
        Assert.Equal(parsedId, session.ParsedActivityId);
        Assert.Equal("csharp", session.SkillId);
        Assert.True(occurredAt.EqualsExact(session.OccurredAt));
    }

    [Fact]
    public void SkillId_HasNonNullableStringShape()
    {
        var property = typeof(PracticeSession).GetProperty(nameof(PracticeSession.SkillId))!;
        Assert.Equal(typeof(string), property.PropertyType);
        Assert.Equal(System.Reflection.NullabilityState.NotNull,
            new System.Reflection.NullabilityInfoContext().Create(property).ReadState);
    }
}
