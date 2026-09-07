namespace CareerProof.Api.Domain;

public class ParsedActivity
{
    public ParsedActivity(Guid id, Guid rawActivityId, int sessionOrdinal, ActivityType activityType, string? skillId, string classifierVersion, string taxonomyVersion)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(sessionOrdinal, 1);

        Id = id;
        RawActivityId = rawActivityId;
        SessionOrdinal = sessionOrdinal;
        ActivityType = activityType;
        SkillId = skillId;
        ClassifierVersion = classifierVersion;
        TaxonomyVersion = taxonomyVersion;
    }

    public Guid Id { get; }

    public Guid RawActivityId { get; }

    public int SessionOrdinal { get; }

    public ActivityType ActivityType { get; }

    public string? SkillId { get; }

    public string ClassifierVersion { get; }

    public string TaxonomyVersion { get; }
}
