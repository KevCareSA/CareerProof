# CareerProof

CareerProof V1 ingests the real Daily Code Log, classifies its sessions reliably, and computes a defensible mastery score.

## Current build position

This README is the authoritative marker for the current CareerProof build step.

- Step -1 — 100-record feasibility spike: CLOSED
- Step 0 — taxonomy, scope, target architecture, ADRs: FROZEN
- Step 1 — implementer/reviewer contracts: FROZEN
- Step 2 — repository + three CI checks: FROZEN
- Step 3 — Domain Entities: CURRENT

## Repository structure

```text
CareerProof/
├── .github/
│   ├── CODEOWNERS
│   ├── pull_request_template.md
│   └── workflows/ci.yml
├── docs/
│   ├── adr/README.md
│   ├── agents/
│   │   ├── implementer.md
│   │   └── reviewer.md
│   ├── architecture/target-architecture.md
│   ├── engineering/repository-settings.md
│   └── product/v1-scope.md
├── taxonomy/
│   ├── taxonomy-v1.json
│   └── aliases-v1.json
├── src/CareerProof.Api/
├── tests/CareerProof.UnitTests/
├── CareerProof.sln
├── Directory.Build.props
├── global.json
├── .gitattributes
└── .editorconfig
```

## CI — exactly three checks

1. Build
2. Unit Tests
3. Format

No additional CI gate is part of V1 Step 2.

## Local commands

```bash
dotnet restore CareerProof.sln
dotnet build CareerProof.sln --configuration Release
dotnet test CareerProof.sln --configuration Release
dotnet format CareerProof.sln --verify-no-changes
```

## Scope

`docs/product/v1-scope.md` governs what may be built in V1. Accepted ADRs remain binding across stages.


## Step 2 status

Step 2 is frozen. Repository protections are configured, PR #1 is merged, and the required Build, Unit Tests, and Format checks have passed.
