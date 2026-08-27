# CareerProof — Repository Settings

**Status:** Required before Step 2 is frozen on GitHub.

These controls live in GitHub repository settings and therefore cannot be guaranteed by files alone. The human project owner must enable and verify them after the repository is created remotely.

## Protected branch

Branch: `main`

Required settings:

- Require a pull request before merging.
- Require at least one approving review.
- Require review from CODEOWNERS once `.github/CODEOWNERS` contains the real human owner's GitHub username.
- Dismiss stale approvals when new commits are pushed.
- Require all three status checks before merge:
  - `Build`
  - `Unit Tests`
  - `Format`
- Do not allow direct pushes to `main` by implementation agents.
- Do not grant implementation/reviewer agents bypass permission for branch protection.
- Human project owner retains final merge authority.

## CODEOWNERS activation

`.github/CODEOWNERS` assigns repository ownership to `@KevCareSA`.

Before Step 2 is frozen, enable required CODEOWNER review in GitHub settings and verify it on the first pull request.

Until the GitHub repository setting is enabled, CODEOWNERS is an auditable configuration artifact but not yet an enforced ownership control.

## Verification record

After configuring GitHub, record the date and verifier here:

- Configured: `PENDING`
- Verified by: `PENDING`
- Required checks observed on first PR: `PENDING`

Step 2 is not fully frozen until these values are completed and the first CI run passes.
