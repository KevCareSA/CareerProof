namespace CareerProof.Api.Domain;

public class MasterySnapshot
{
    public MasterySnapshot(Guid id, string skillId, decimal score, string scoreVersion, string classifierVersion, string taxonomyVersion, DateTimeOffset calculatedAt)
    {
        Id = id;
        SkillId = skillId;
        Score = score;
        ScoreVersion = scoreVersion;
        ClassifierVersion = classifierVersion;
        TaxonomyVersion = taxonomyVersion;
        CalculatedAt = calculatedAt;
    }

    public Guid Id { get; }

    public string SkillId { get; }

    public decimal Score { get; }

    public string ScoreVersion { get; }

    public string ClassifierVersion { get; }

    public string TaxonomyVersion { get; }

    public DateTimeOffset CalculatedAt { get; }
}
