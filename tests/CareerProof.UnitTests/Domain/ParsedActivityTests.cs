using CareerProof.Api.Domain;
using Xunit;

namespace CareerProof.UnitTests.Domain;

public sealed class ParsedActivityTests
{
    [Fact]
    public void Sessions_ShareRawIdentityButHaveDistinctOrdinals()
    {
        var rawId = Guid.NewGuid();
        var first = new ParsedActivity(Guid.NewGuid(), rawId, 1, ActivityType.Learning, "csharp", "classifier-v1", "v1");
        var second = new ParsedActivity(Guid.NewGuid(), rawId, 2, ActivityType.Review, "sql", "classifier-v1", "v1");

        Assert.Equal(rawId, first.RawActivityId);
        Assert.Equal(rawId, second.RawActivityId);
        Assert.Equal(1, first.SessionOrdinal);
        Assert.Equal(2, second.SessionOrdinal);
        Assert.NotEqual((first.RawActivityId, first.SessionOrdinal), (second.RawActivityId, second.SessionOrdinal));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_RejectsNonPositiveOrdinal(int ordinal)
    {
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            new ParsedActivity(Guid.NewGuid(), Guid.NewGuid(), ordinal, ActivityType.Learning, "csharp", "classifier-v1", "v1"));
        Assert.Equal("sessionOrdinal", exception.ParamName);
    }

    [Fact]
    public void Interpretations_PreserveSessionIdentityAndEarlierVersions()
    {
        var rawId = Guid.NewGuid();
        var firstId = Guid.NewGuid();
        var secondId = Guid.NewGuid();
        var first = new ParsedActivity(firstId, rawId, 2, ActivityType.Learning, "csharp", "classifier-v1", "v1");
        var second = new ParsedActivity(secondId, rawId, 2, ActivityType.Review, "csharp", "classifier-v2", "synthetic-taxonomy-v2");

        Assert.Equal((first.RawActivityId, first.SessionOrdinal), (second.RawActivityId, second.SessionOrdinal));
        Assert.Equal(firstId, first.Id);
        Assert.Equal(secondId, second.Id);
        Assert.NotEqual(first.Id, second.Id);
        Assert.Equal("classifier-v1", first.ClassifierVersion);
        Assert.Equal("v1", first.TaxonomyVersion);
        Assert.Equal(ActivityType.Learning, first.ActivityType);
        Assert.Equal("classifier-v2", second.ClassifierVersion);
        Assert.Equal("synthetic-taxonomy-v2", second.TaxonomyVersion);
    }

    [Fact]
    public void AdministrativeInterpretation_PermitsNullSkill()
    {
        var parsed = new ParsedActivity(Guid.NewGuid(), Guid.NewGuid(), 1, ActivityType.Submission, null, "classifier-v1", "v1");

        Assert.Null(parsed.SkillId);
        Assert.Equal(ActivityType.Submission, parsed.ActivityType);
        Assert.Equal("classifier-v1", parsed.ClassifierVersion);
        Assert.Equal("v1", parsed.TaxonomyVersion);
    }
}
