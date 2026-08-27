# CareerProof V1 — Scope Lock

**Status:** Locked at Stage 0, 2026-08-27
**Supersedes:** nothing. Sits beneath `docs/architecture/target-architecture.md`, which is the destination and NOT the build order.

Any feature not named in "In Scope" requires a written justification for why V1 cannot proceed without it.

---

## Milestone 1

> CareerProof ingests the real Daily Code Log and understands it reliably enough to compute a defensible mastery score.

Nothing beyond that is V1.

---

## In Scope

```
Notion Daily Code Log (one database)
        v
NotionSource / SyncRun / RawActivity
        v
SessionExtractor          (one row -> many sessions)
        v
SourceQualityEvaluator
        v
Normaliser -> AliasResolver -> ProjectResolver
        -> SkillResolver -> TopicResolver
        -> ActivityTypeClassifier
        v
LLM fallback (schema-constrained)
        v
SchemaValidator -> ConfidenceGate
        v
ParsedActivity (versioned)
        v
ClassificationReview  (human correction UI)
        v
PracticeSession
        v
MasterySnapshot v1
        v
Five real screens
```

**Entities:** NotionSource, SyncRun, RawActivity, ParsedActivity, Skill, SkillAlias, PracticeSession, Evidence, MasterySnapshot, ClassificationReview.

**Screens:** Overview, Practice, Skills, Classification Review, Integrations.

**CI:** build, unit tests, format. Three checks. No more.

**Agent roles:** human architect, one implementer, one reviewer. Three. No more.

---

## Explicitly Out of Scope

GitHub integration. DevUniverse integration. Wider Notion knowledge crawl. Job market intelligence. Gap analysis. Next Best Action. AI mentor. Recruiter views. Business and employment records. Certifications. Notifications. Redis. Agent orchestration. Mastery v2. Any custom CI check beyond the three above.

These live in the Target Architecture. They are not deleted, deprioritised, or in doubt. They are simply not V1.

---

## Decisions Inherited From the Spike

The Days 71-100 spike (~99 sessions) settled four things that are now design constraints, not open questions:

**1. A Notion row is not an activity.** Rows contain up to four sessions. `SessionExtractor` sits between `RawActivity` ingestion and classification.

**2. The header line is a checksum.** Every row opens with `N/4 DONE` or `N/4 sessions`. Parsed session count must equal N. A mismatch is a hard extraction failure, logged and surfaced — never a silent undercount. This is how "zero silent classification failures" is satisfied.

**3. Duration is not usable as minutes.** `Time (mins)` is null for every row from Day 87 to Day 100, and where present it is day-level, never per session. Its semantics are inconsistent (a rest day carries 200 minutes; other rest days carry 0 or null).

Therefore the Practice component of Mastery v1 is **session-count based**. A session is one defined unit of focused work. Nominal duration may be attached for display, flagged as derived rather than measured. Day-level minutes are stored as a separate field for later reconciliation. Time is never divided across sessions and never invented. See ADR-0004.

**4. Two rates, not one.** The spike produced 94% and 76% for the same data because they measured different things.

- `interpretable_rate` — the entry is readable and its activity is clear
- `skill_attributable_rate` — the entry yields a `skill_id` that can feed Mastery

Roughly 18% of sessions are administrative (submissions, prep, quarterly planning). They are fully interpretable and carry no skill. They are **not** classification failures.

---

## Acceptance Gate

Run against 500 historical activities, with corrections supplied by the human through the Classification Review screen.

| Metric | Target |
|---|---|
| Skill-family accuracy, of skill-attributable records | >= 95% |
| Overall classification accuracy, of skill-attributable records | >= 90% |
| Raw source preservation | 100% |
| Silent classification failures | 0 |
| Session-count checksum mismatches | 0 unhandled |

**The gate applies to `skill_attributable_rate`**, since that is the population Mastery depends on. `interpretable_rate` is reported alongside but is not gated.

### On failure

A missed gate triggers the failure split before any threshold is discussed:

```
CLASSIFIER FAILURE     -> more work. The bar does not move.
SOURCE FAILURE         -> the log lacked the information. Threshold may move.
TAXONOMY FAILURE       -> node missing or wrongly shaped. Fix taxonomy, re-run.
ALIAS FAILURE          -> add alias, re-run.
```

Only a documented source-failure finding justifies changing a threshold, and that change is recorded as an ADR. A classifier failure never lowers the bar.

---

## Build Order

```
STEP -1   100-record spike                                    CLOSED
STEP  0   Taxonomy v1, scope lock, target architecture, ADRs  CLOSED / FROZEN
STEP  1   Implementer and reviewer contracts                  <- current
STEP  2   Repository, three CI checks
STEP  3   Domain entities
STEP  4   Notion sync (one database)
STEP  5   Classification pipeline
STEP  6   Classification Review UI
STEP  7   500-record validation gate
STEP  8   Mastery v1
STEP  9   Real dashboard on real data
```
