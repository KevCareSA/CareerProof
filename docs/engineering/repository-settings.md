# CareerProof — Repository Settings

**Status:** Required before Step 2 is frozen on GitHub.

These controls live in GitHub repository settings and therefore cannot be guaranteed by files alone. The human project owner must enable and verify them after the repository is created remotely.

## Protected branch

Branch: `main`

Required settings for the current solo-repository phase:

- Require a pull request before merging.
- Required approving reviews: `0`.
- CODEOWNERS review requirement: `off` until a second reviewer identity exists.
- Require branches to be up to date before merging.
- Require all three status checks before merge:
  - `Build`
  - `Unit Tests`
  - `Format`
- Block direct pushes to `main`.
- Do not grant implementation/reviewer agents bypass permission for branch protection.
- Human project owner retains final merge authority.

## Review-enforcement gap

GitHub does not allow the author of a pull request to approve that same pull request. On a solo repository, requiring one approving review would make the owner's own PRs unmergeable unless the protection were disabled or bypassed.

Therefore required-review enforcement is intentionally deferred until a second reviewer identity exists, such as a trusted collaborator or approved review-bot account.

Until then:

- human review remains a process requirement under the reviewer contract;
- GitHub enforces PR-only changes, green status checks, up-to-date branches, and no direct pushes to `main`;
- CODEOWNERS remains an auditable ownership artifact but is not a required-review gate.

## CODEOWNERS

`.github/CODEOWNERS` assigns repository ownership to `@KevCareSA`.

Required CODEOWNER review must not be enabled during the solo-repository phase. Enable it only when a second reviewer identity exists and can provide an independent approval.

## Verification record

After configuring GitHub, record the date and verifier here:

- Configured: `PENDING`
- Verified by: `PENDING`
- Require pull request before merging: `PENDING`
- Required approving reviews: `0`
- Require branches up to date: `PENDING`
- Direct pushes to `main` blocked: `PENDING`
- Required checks observed on first PR: `Build`, `Unit Tests`, `Format` — `PASS`
- CODEOWNERS review enforcement: `DEFERRED — second reviewer identity required`

Step 2 is not fully frozen until the remaining `PENDING` repository controls are completed and verified.
