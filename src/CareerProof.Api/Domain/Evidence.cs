namespace CareerProof.Api.Domain;

public class Evidence
{
    public Evidence(Guid id, string evidenceType, string reference, string? skillId)
    {
        Id = id;
        EvidenceType = evidenceType;
        Reference = reference;
        SkillId = skillId;
    }

    public Guid Id { get; }

    public string EvidenceType { get; }

    public string Reference { get; }

    public string? SkillId { get; }
}
