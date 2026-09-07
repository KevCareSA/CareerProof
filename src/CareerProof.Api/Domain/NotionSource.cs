namespace CareerProof.Api.Domain;

public class NotionSource
{
    public NotionSource(Guid id, string externalSourceId, string displayName)
    {
        Id = id;
        ExternalSourceId = externalSourceId;
        DisplayName = displayName;
    }

    public Guid Id { get; }

    public string ExternalSourceId { get; }

    public string DisplayName { get; }
}
