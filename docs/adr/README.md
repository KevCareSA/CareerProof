# Architecture Decision Records

Decisions here are binding. An agent or a future session that wants to reverse one must supersede it with a new ADR, not quietly implement the alternative.

Status values: Proposed, Accepted, Superseded by ADR-NNNN.

---

## ADR-0001 — RawActivity is immutable

**Status:** Accepted

**Decision.** `RawActivity` is append-only. Nothing in the system updates a row. If Notion's copy changes, a new row is inserted. `RawPayload` stores the complete JSON response, not only the fields currently parsed.

**Reason.** Classifier and taxonomy versions will change. Reclassification must be reproducible against exactly what was received. A field nobody parses today will be needed in six months, and re-fetching two years of Notion is not something to do twice.

**Alternatives rejected.** Store only the latest parse. Overwrite on source edit. Both destroy the ability to prove the classifier improved rather than merely feeling improved.

---

## ADR-0002 — Interpretation is versioned separately from source

**Status:** Accepted

**Decision.** `ParsedActivity` is a separate entity with a foreign key to `RawActivity`. One raw record has many parses over time. Every `ParsedActivity` stores `classifier_version` and `taxonomy_version`. Every `MasterySnapshot` stores `score_version`, `classifier_version` and `taxonomy_version`.

**Reason.** When a number on screen changes, the system must be able to say whether the classifier changed, the taxonomy changed, or the scoring changed. Without all three versions recorded, that question is unanswerable.

---

## ADR-0003 — A Notion row is not an activity

**Status:** Accepted

**Decision.** A `SessionExtractor` stage sits between `RawActivity` and classification, splitting one row into N sessions. The row header's `N/4` count is a required checksum: extracted session count must equal N, and a mismatch raises a hard, logged extraction failure.

**Reason.** Observed directly in the Days 71-100 spike. Rows carry up to four independent sessions across different skills, projects and courses. Persisting one row as one activity would have destroyed the primary unit of practice.

**Consequence.** The checksum is how the "zero silent failures" requirement is met — extraction cannot quietly lose a session.

---

## ADR-0004 — Practice is measured in sessions, not minutes

**Status:** Accepted

**Decision.** The Practice component of Mastery v1 is derived from session count. Day-level `Time (mins)` is stored on `RawActivity` as a separate field and is not used for scoring in v1. Duration is never divided across sessions and never inferred.

**Reason.** `Time (mins)` is null for every row from Day 87 through Day 100 and is day-level where it exists. Its semantics are inconsistent across row types. Session count, by contrast, is reliably extractable and independently validated by the header checksum (ADR-0003).

**Consequence.** Any displayed hour figure is nominal and must be labelled as derived, not measured. Revisit in Mastery v2 if per-session timing becomes available.

---

## ADR-0005 — Two classifiability rates, and the gate applies to one

**Status:** Accepted

**Decision.** The system reports `interpretable_rate` and `skill_attributable_rate` separately. The Milestone 1 acceptance gate applies to `skill_attributable_rate`. Administrative sessions — submissions, assignment prep, quarterly planning — resolve with `skill_id: null` and are recorded as successful classifications, not failures.

**Reason.** Two independent spikes on the same 30 rows produced 94% and 76%. The difference was definitional, not empirical. Roughly 18% of sessions are fully readable and carry no skill. Conflating "unreadable" with "no skill attached" would either inflate the accuracy figure or wrongly condemn the classifier.

---

## ADR-0006 — Activity type is not a taxonomy node

**Status:** Accepted

**Decision.** Learning, implementation, review, assignment-work, submission, planning and rest are values of an `activity_type` enum. They are not siblings of C# or SQL in the skill tree. Each declares whether it contributes to Practice and Application scoring.

**Reason.** A review session is *about* something. Modelling "Review" as a taxonomy node alongside "Databases" would let Mastery attribute practice hours to an activity type as though it were a skill, and would make the tree structurally inconsistent.

---

## ADR-0007 — Taxonomy is a versioned data artifact with stable IDs

**Status:** Accepted

**Decision.** The taxonomy lives in `taxonomy/` under source control and changes through pull requests. IDs are permanent; display names may change freely. Maximum depth is three segments (`skill.topic.subtopic`). Changes are recorded as migrations with an explicit operation:

| Operation | Effect on existing ParsedActivity rows |
|---|---|
| `rename` | None. ID unchanged, scores unchanged. |
| `merge` | Both source IDs map to the target. Practice combines. Recorded, not silent. |
| `split` | Affected rows drop to the review queue. Never auto-assigned. |
| `reparent` | ID changes; old ID retained as a permanent alias. |

**Reason.** Every classification resolves against the taxonomy and every mastery score inherits its shape. Ad-hoc edits made while fixing individual misclassifications produce duplicate nodes and inconsistent depth, after which scores cannot be compared across time.

**Consequence.** "Should this be a sibling or a child?" is a pull request, not an in-the-moment judgement.

---

## ADR-0008 — DevUniverse is canonical; UniversalDev is permanent

**Status:** Accepted

**Decision.** `project_id: devuniverse`, display name **DevUniverse**. `UniversalDev` and its compound forms are permanent entries in the alias table, not legacy entries scheduled for removal.

**Reason.** The historical log writes `UniversalDev` throughout Days 71-100. That text is immutable under ADR-0001, so the alias must resolve indefinitely regardless of what the canonical display name is or how future entries are written.
