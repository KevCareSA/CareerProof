using CareerProof.Api.Domain;
using Xunit;

namespace CareerProof.UnitTests.Domain;

public sealed class RawActivityTests
{
    [Fact]
    public void Constructor_PreservesCompleteSourceRepresentation()
    {
        var id = Guid.NewGuid();
        var syncRunId = Guid.NewGuid();
        const string payload = " {\n  \"unused\": \"synthetic\\nvalue\", \"sessions\": []\n } ";
        var raw = new RawActivity(id, syncRunId, "synthetic-row", payload, 125, 2);

        Assert.Equal(id, raw.Id);
        Assert.Equal(syncRunId, raw.SyncRunId);
        Assert.Equal("synthetic-row", raw.ExternalRecordId);
        Assert.Equal(payload, raw.RawPayload);
        Assert.Equal(125, raw.DayLevelMinutes);
        Assert.Equal(2, raw.DeclaredSessionCount);
    }

    [Fact]
    public void Constructor_PermitsAbsentSourceMetadata()
    {
        var raw = new RawActivity(Guid.NewGuid(), Guid.NewGuid(), "synthetic-row", "{}", null, null);

        Assert.Null(raw.DayLevelMinutes);
        Assert.Null(raw.DeclaredSessionCount);
    }

    [Fact]
    public void SourceRepresentation_HasNoSettersOrPublicMutationMethods()
    {
        var type = typeof(RawActivity);
        Assert.All(type.GetProperties(), property => Assert.Null(property.GetSetMethod(true)));
        Assert.DoesNotContain(type.GetMethods(System.Reflection.BindingFlags.Public |
            System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly),
            method => !method.IsSpecialName);
    }
}
