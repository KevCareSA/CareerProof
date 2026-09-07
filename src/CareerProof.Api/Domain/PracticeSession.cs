namespace CareerProof.Api.Domain;

public class PracticeSession
{
    public PracticeSession(Guid id, Guid parsedActivityId, string skillId, DateTimeOffset occurredAt)
    {
        Id = id;
        ParsedActivityId = parsedActivityId;
        SkillId = skillId;
        OccurredAt = occurredAt;
    }

    public Guid Id { get; }

    public Guid ParsedActivityId { get; }

    public string SkillId { get; }

    public DateTimeOffset OccurredAt { get; }
}
