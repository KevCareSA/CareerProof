namespace CareerProof.Api.Domain;

public class Skill
{
    public Skill(string id, string displayName)
    {
        Id = id;
        DisplayName = displayName;
    }

    public string Id { get; }

    public string DisplayName { get; }
}
