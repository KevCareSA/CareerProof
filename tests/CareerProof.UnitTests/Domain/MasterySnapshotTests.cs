using CareerProof.Api.Domain;
using Xunit;

namespace CareerProof.UnitTests.Domain;

public sealed class MasterySnapshotTests
{
    [Fact]
    public void Constructor_PreservesSuppliedResultAndAllVersions()
    {
        var id = Guid.NewGuid();
        var calculatedAt = new DateTimeOffset(2026, 1, 2, 10, 0, 0, TimeSpan.FromHours(2));
        var snapshot = new MasterySnapshot(id, "sql", 12.345m, "score-v1", "classifier-v1", "v1", calculatedAt);

        Assert.Equal(id, snapshot.Id);
        Assert.Equal("sql", snapshot.SkillId);
        Assert.Equal(12.345m, snapshot.Score);
        Assert.Equal("score-v1", snapshot.ScoreVersion);
        Assert.Equal("classifier-v1", snapshot.ClassifierVersion);
        Assert.Equal("v1", snapshot.TaxonomyVersion);
        Assert.True(calculatedAt.EqualsExact(snapshot.CalculatedAt));
    }
}
