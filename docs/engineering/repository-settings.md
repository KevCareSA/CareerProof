# CareerProof — Repository Settings

**Status:** Verified for the solo-repository phase on 2026-08-27.

These controls live in GitHub repository settings and therefore cannot be guaranteed by files alone. The human project owner must enable them, and the repository state must be verified before Step 2 is frozen.

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
- Block direct pushes to `main` through the pull-request requirement with no bypass actors.
- Block branch deletion and force pushes.
- Do not grant implementation/reviewer agents bypass permission for branch protection.
- Human project owner retains final merge authority.

## Review-enforcement gap

GitHub does not allow the author of a pull request to approve that same pull request. On a solo repository, requiring one approving review would make the owner's own PRs unmergeable unless the protection were disabled or bypassed.

Therefore required-review enforcement is intentionally deferred until a second reviewer identity exists, such as a trusted collaborator or approved review-bot account.

Until then:

- human review remains a process requirement under the reviewer contract;
- GitHub enforces PR-only changes, green status checks, up-to-date branches, and no bypass actors;
- CODEOWNERS remains an auditable ownership artifact but is not a required-review gate.

This is a deliberate configuration, not a missing protection. A future reviewer must not increase the required approval count or enable required CODEOWNER review until a second reviewer identity exists.

## Claude review environment

The `claude-review` GitHub environment controls credential access for the advisory Claude review workflow. It is not an application deployment environment.

Configured protection settings:

- Required reviewers: `ON`.
- Required reviewer: `KevCareSA`.
- Prevent self-review: `OFF`.
- Allow administrators to bypass configured protection rules: `OFF`.
- Credential scope: the Claude review credential is stored as an environment-scoped secret rather than a repository-wide secret.

`KevCareSA` is currently the only authorised approver during the solo-repository phase. Prevent self-review is deliberately disabled because enabling it would make approval impossible and leave review workflow runs blocked.

Environment approval gates access to the credential. It does not create a required pull-request review or add a required merge check. The required merge checks remain exactly `Build`, `Unit Tests`, and `Format`.

The approval gate is provisional. After several successful and audited automated review runs, the human project owner may reassess whether required environment approval should remain enabled. Any change must be deliberate and documented.

## CODEOWNERS

`.github/CODEOWNERS` assigns repository ownership to `@KevCareSA`.

Required CODEOWNER review must not be enabled during the solo-repository phase. Enable it only when a second reviewer identity exists and can provide an independent approval.

## Verification record

### 2026-08-27 — Step 2 repository protections

- Configured: `2026-08-27`
- Verified by: `KevCare`
- Checked with: `ChatGPT (GPT-5.6 Sol)`
- Ruleset: `Protect main` — `ACTIVE`
- Target: default branch (`main`) — `VERIFIED`
- Require pull request before merging: `VERIFIED`
- Required approving reviews: `0` — `VERIFIED`
- Require branches up to date: `VERIFIED`
- Bypass actors: `NONE` — `VERIFIED`
- Branch deletion blocked: `VERIFIED`
- Force pushes blocked: `VERIFIED`
- Required checks observed on PR #1: `Build`, `Unit Tests`, `Format` — `PASS`
- CODEOWNERS file: `* @KevCareSA` — `VERIFIED`
- CODEOWNERS review enforcement: `DEFERRED — second reviewer identity required`

Step 2 was frozen after PR #1 was merged into `main`.

### 2026-09-08 — Claude review environment

- Configured: `2026-09-08`
- Verified by: `KevCare`
- Checked with: `ChatGPT (GPT-5.6 Sol)`
- Claude review environment: `claude-review` — `VERIFIED`
- Claude review required reviewers: `ON` — `VERIFIED`
- Claude review required reviewer: `KevCareSA` — `VERIFIED`
- Claude review prevent self-review: `OFF — deliberate solo-repository configuration`
- Claude review administrator bypass: `OFF` — `VERIFIED`
- Claude review credential scope: environment secret — `VERIFIED`
