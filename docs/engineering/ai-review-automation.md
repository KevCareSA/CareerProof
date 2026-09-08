# CareerProof — AI Review Automation

**Purpose:** Define how automated invocation of the existing independent AI reviewer may be used during CareerProof development.

## 1. Role Boundary

Automated review invokes the existing single reviewer role defined in:

`docs/agents/reviewer.md`

It does not create an additional agent role.

The reviewer remains advisory and independent from the implementer.

Advisory means the automation cannot mechanically gate a merge. A BLOCKER finding remains binding on the human merge decision under `docs/agents/reviewer.md`, Section 7.

## 2. Human Authority

Automated review may inspect a pull request and publish findings, but it may not:

- merge a pull request;
- bypass branch protection;
- change repository protection rules;
- become the final acceptance authority.

The human project owner retains final merge authority.

## 3. Required Checks and Workflow Permissions

CareerProof continues to require exactly three merge-gating status checks:

- Build
- Unit Tests
- Format

Automated AI review is not a required CI check.

An advisory review workflow may exist under `.github/workflows/`.

It must not be added to the required-checks list in `docs/engineering/repository-settings.md`.

The advisory workflow may be granted permission to write pull-request comments where required for publishing review findings.

It must not be granted permission to modify repository contents, protection rules, workflow files, or merge pull requests.

## 4. Review Credentials

Automated review may require a repository secret such as an API key or subscription token.

Any such credential must be used only for the review workflow and must not be committed to the repository.

## 5. Review Contract

Automated review must follow:

`docs/agents/reviewer.md`

The existing reviewer permissions, governing precedence, review questions, severity levels, output requirements, and no-courtesy-approval rule remain unchanged.

## 6. Independence

Automated invocation must run the reviewer in a fresh review context.

This satisfies the separate-context independence requirement defined in `docs/agents/reviewer.md`, Section 2.

Where a different model from the implementer is available and practical, the reviewer contract's different-model preference still applies.

## 7. Review Cycle Limit

The existing automated review-cycle limit remains unchanged.

Only two automated review cycles are permitted for the same ticket.

After cycle two, unresolved findings must be escalated to the human architect.

This rule is inherited from `docs/agents/reviewer.md`, Section 10.

## 8. Invocation

Review may be invoked manually or automatically.

Automation changes only the invocation mechanism.

It does not change:

- reviewer authority;
- reviewer behaviour;
- review standards;
- merge authority;
- required CI checks;
- or the review-cycle limit.

## 9. Scope

This document governs CareerProof's engineering process.

It does not add a CareerProof product feature or alter the V1 product scope.
