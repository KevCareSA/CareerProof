namespace CareerProof.Api.Domain;

public class RawActivity
{
    public RawActivity(Guid id, Guid syncRunId, string externalRecordId, string rawPayload, int? dayLevelMinutes, int? declaredSessionCount)
    {
        Id = id;
        SyncRunId = syncRunId;
        ExternalRecordId = externalRecordId;
        RawPayload = rawPayload;
        DayLevelMinutes = dayLevelMinutes;
        DeclaredSessionCount = declaredSessionCount;
    }

    public Guid Id { get; }

    public Guid SyncRunId { get; }

    public string ExternalRecordId { get; }

    public string RawPayload { get; }

    public int? DayLevelMinutes { get; }

    public int? DeclaredSessionCount { get; }
}
