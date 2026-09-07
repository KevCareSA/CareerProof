namespace CareerProof.Api.Domain;

public class SyncRun
{
    public SyncRun(Guid id, Guid notionSourceId, DateTimeOffset startedAt, DateTimeOffset? completedAt)
    {
        Id = id;
        NotionSourceId = notionSourceId;
        StartedAt = startedAt;
        CompletedAt = completedAt;
    }

    public Guid Id { get; }

    public Guid NotionSourceId { get; }

    public DateTimeOffset StartedAt { get; }

    public DateTimeOffset? CompletedAt { get; }
}
