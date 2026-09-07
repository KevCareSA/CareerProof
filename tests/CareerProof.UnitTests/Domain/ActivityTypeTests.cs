using System.Text.Json;
using CareerProof.Api.Domain;
using Xunit;

namespace CareerProof.UnitTests.Domain;

public sealed class ActivityTypeTests
{
    [Fact]
    public void Enum_CorrespondsExactlyToCommittedActivityIdentifiers()
    {
        (ActivityType Member, string Id)[] correspondence =
        [
            (ActivityType.Learning, "learning"),
            (ActivityType.Implementation, "implementation"),
            (ActivityType.Review, "review"),
            (ActivityType.AssignmentWork, "assignment-work"),
            (ActivityType.Submission, "submission"),
            (ActivityType.Planning, "planning"),
            (ActivityType.Rest, "rest"),
        ];
        string[] names = ["Learning", "Implementation", "Review", "AssignmentWork", "Submission", "Planning", "Rest"];
        Assert.Equal(names, Enum.GetNames<ActivityType>());
        Assert.Equal(correspondence.Select(pair => pair.Member), Enum.GetValues<ActivityType>());
        Assert.Equal(7, correspondence.Select(pair => pair.Id).Distinct(StringComparer.Ordinal).Count());

        var path = Path.Combine(AppContext.BaseDirectory, "taxonomy", "taxonomy-v1.json");
        using var taxonomy = JsonDocument.Parse(File.ReadAllText(path));
        var ids = taxonomy.RootElement.GetProperty("activity_types").EnumerateArray()
            .Select(activity => activity.GetProperty("id").GetString()!).ToArray();
        Assert.Equal(7, ids.Length);
        Assert.Equal(7, ids.Distinct(StringComparer.Ordinal).Count());
        Assert.Equal(correspondence.Select(pair => pair.Id).OrderBy(id => id, StringComparer.Ordinal),
            ids.OrderBy(id => id, StringComparer.Ordinal));

        var skillNodeIds = new HashSet<string>(StringComparer.Ordinal);
        foreach (var skill in taxonomy.RootElement.GetProperty("skills").EnumerateArray())
        {
            skillNodeIds.Add(skill.GetProperty("id").GetString()!);
            foreach (var topic in skill.GetProperty("topics").EnumerateArray())
            {
                skillNodeIds.Add(topic.GetProperty("id").GetString()!);
                foreach (var subtopic in topic.GetProperty("subtopics").EnumerateArray())
                {
                    skillNodeIds.Add(subtopic.GetProperty("id").GetString()!);
                }
            }
        }

        Assert.DoesNotContain(ids, skillNodeIds.Contains);
    }

    [Fact]
    public void Enum_HasNoScoringPropertiesOrCustomMetadata()
    {
        var type = typeof(ActivityType);
        Assert.True(type.IsEnum);
        Assert.Empty(type.GetProperties());
        Assert.Empty(type.GetCustomAttributesData());
        Assert.All(type.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static),
            field => Assert.Empty(field.GetCustomAttributesData()));
    }
}
