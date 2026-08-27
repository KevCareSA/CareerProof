using System.Text.Json;
using Xunit;

namespace CareerProof.UnitTests;

public sealed class TaxonomyIntegrityTests
{
    private static readonly string TaxonomyPath = Path.Combine(AppContext.BaseDirectory, "taxonomy", "taxonomy-v1.json");
    private static readonly string AliasesPath = Path.Combine(AppContext.BaseDirectory, "taxonomy", "aliases-v1.json");

    [Fact]
    public void Taxonomy_Ids_AreUnique()
    {
        using var taxonomy = JsonDocument.Parse(File.ReadAllText(TaxonomyPath));
        var ids = GetTaxonomyNodeIds(taxonomy.RootElement).ToList();

        Assert.Equal(ids.Count, ids.Distinct(StringComparer.Ordinal).Count());
    }

    [Fact]
    public void Taxonomy_Ids_DoNotExceedThreeSegments()
    {
        using var taxonomy = JsonDocument.Parse(File.ReadAllText(TaxonomyPath));
        var ids = GetTaxonomyNodeIds(taxonomy.RootElement);

        var tooDeep = ids.Where(id => id.Split('.', StringSplitOptions.RemoveEmptyEntries).Length > 3).ToArray();

        Assert.Empty(tooDeep);
    }

    [Fact]
    public void AliasTargets_ResolveToDeclaredTaxonomyOrProjectIds()
    {
        using var taxonomy = JsonDocument.Parse(File.ReadAllText(TaxonomyPath));
        using var aliases = JsonDocument.Parse(File.ReadAllText(AliasesPath));

        var validIds = GetTaxonomyNodeIds(taxonomy.RootElement)
            .Concat(GetObjectIds(taxonomy.RootElement, "projects"))
            .ToHashSet(StringComparer.Ordinal);

        var aliasTargets = GetPropertyNames(aliases.RootElement, "skill_aliases")
            .Concat(GetPropertyNames(aliases.RootElement, "topic_aliases"))
            .Concat(GetPropertyNames(aliases.RootElement, "project_aliases"))
            .Concat(GetPropertyNames(aliases.RootElement, "low_confidence_project_aliases")
                .Where(key => !string.Equals(key, "note", StringComparison.Ordinal)));

        var unresolved = aliasTargets.Where(id => !validIds.Contains(id)).Distinct(StringComparer.Ordinal).ToArray();

        Assert.Empty(unresolved);
    }

    [Fact]
    public void ActivityTypeMarkers_ResolveToDeclaredActivityTypes()
    {
        using var taxonomy = JsonDocument.Parse(File.ReadAllText(TaxonomyPath));
        using var aliases = JsonDocument.Parse(File.ReadAllText(AliasesPath));

        var activityTypes = GetObjectIds(taxonomy.RootElement, "activity_types").ToHashSet(StringComparer.Ordinal);
        var markerKeys = GetPropertyNames(aliases.RootElement, "activity_type_markers");

        var unresolved = markerKeys.Where(key => !activityTypes.Contains(key)).ToArray();

        Assert.Empty(unresolved);
    }

    private static IEnumerable<string> GetTaxonomyNodeIds(JsonElement root)
    {
        foreach (var skill in root.GetProperty("skills").EnumerateArray())
        {
            yield return skill.GetProperty("id").GetString()!;

            foreach (var topic in skill.GetProperty("topics").EnumerateArray())
            {
                yield return topic.GetProperty("id").GetString()!;

                foreach (var subtopic in topic.GetProperty("subtopics").EnumerateArray())
                {
                    yield return subtopic.GetProperty("id").GetString()!;
                }
            }
        }
    }

    private static IEnumerable<string> GetObjectIds(JsonElement root, string propertyName)
    {
        return root.GetProperty(propertyName)
            .EnumerateArray()
            .Select(element => element.GetProperty("id").GetString()!);
    }

    private static IEnumerable<string> GetPropertyNames(JsonElement root, string propertyName)
    {
        return root.GetProperty(propertyName)
            .EnumerateObject()
            .Select(property => property.Name);
    }
}
