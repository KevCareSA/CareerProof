namespace CareerProof.Api.Domain;

public class ClassificationReview
{
    public ClassificationReview(Guid id, Guid parsedActivityId, DateTimeOffset createdAt, DateTimeOffset? resolvedAt)
    {
        Id = id;
        ParsedActivityId = parsedActivityId;
        CreatedAt = createdAt;
        ResolvedAt = resolvedAt;
    }

    public Guid Id { get; }

    public Guid ParsedActivityId { get; }

    public DateTimeOffset CreatedAt { get; }

    public DateTimeOffset? ResolvedAt { get; }
}
