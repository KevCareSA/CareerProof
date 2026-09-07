using System.Text.Json;
using CareerProof.Api.Domain;
using Xunit;

namespace CareerProof.UnitTests.Domain;

public sealed class TaxonomyRepresentationTests
{
    [Fact]
    public void TaxonomyRepresentations_PreserveCommittedIdentifiersNamesAndAliases()
    {
        using var taxonomy = JsonDocument.Parse(File.ReadAllText(
            Path.Combine(AppContext.BaseDirectory, "taxonomy", "taxonomy-v1.json")));
        using var aliases = JsonDocument.Parse(File.ReadAllText(
            Path.Combine(AppContext.BaseDirectory, "taxonomy", "aliases-v1.json")));

        foreach (var node in taxonomy.RootElement.GetProperty("skills").EnumerateArray())
        {
            var id = node.GetProperty("id").GetString()!;
            var displayName = node.GetProperty("display_name").GetString()!;
            var skill = new Skill(id, displayName);
            Assert.Equal(id, skill.Id);
            Assert.Equal(displayName, skill.DisplayName);

            var parsed = new ParsedActivity(Guid.NewGuid(), Guid.NewGuid(), 1, ActivityType.Learning, id, "classifier-v1", "v1");
            var practice = new PracticeSession(Guid.NewGuid(), parsed.Id, id, DateTimeOffset.UnixEpoch);
            var evidence = new Evidence(Guid.NewGuid(), "synthetic", "synthetic-reference", id);
            var snapshot = new MasterySnapshot(Guid.NewGuid(), id, 1m, "score-v1", "classifier-v1", "v1", DateTimeOffset.UnixEpoch);
            Assert.Equal(id, parsed.SkillId);
            Assert.Equal(id, practice.SkillId);
            Assert.Equal(id, evidence.SkillId);
            Assert.Equal(id, snapshot.SkillId);

            foreach (var aliasValue in aliases.RootElement.GetProperty("skill_aliases").GetProperty(id).EnumerateArray())
            {
                var aliasText = aliasValue.GetString()!;
                var alias = new SkillAlias(aliasText, id);
                Assert.Equal(aliasText, alias.Alias);
                Assert.Equal(id, alias.TargetSkillId);
            }
        }
    }
}
