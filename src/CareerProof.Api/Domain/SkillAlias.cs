namespace CareerProof.Api.Domain;

public class SkillAlias
{
    public SkillAlias(string alias, string targetSkillId)
    {
        Alias = alias;
        TargetSkillId = targetSkillId;
    }

    public string Alias { get; }

    public string TargetSkillId { get; }
}
