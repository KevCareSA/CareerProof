# CareerProof — Target Architecture

## Status

Target architecture.

This document describes the long-term destination of CareerProof.

It is NOT the current implementation plan and must not be used by AI agents as permission to introduce future features into V1.

Current implementation scope is governed by `docs/product/v1-scope.md`.

---

## Precedence

```text
ADRs                    bind everything, at every stage
        │
v1-scope.md             decides what may be built now
        │
target-architecture.md  describes where this is going
```

`v1-scope.md` takes precedence over this document when deciding whether work may proceed.

**ADRs take precedence over both.** An ADR is a decision, not a scope boundary. ADR-0001 (raw data is immutable), ADR-0002 (interpretation is versioned separately), ADR-0007 (taxonomy IDs are stable, depth capped at three) and the rest apply to every layer described below, including layers that arrive years after V1. Reversing an ADR requires a superseding ADR, never a scope change.

---

# 1. Real-World Activity

CareerProof begins with things the user actually does.

Sources eventually include:

- learning
- coding
- projects
- professional work
- university
- certifications
- job applications
- employment
- business activity
- production engineering
- deliberate practice

---

# 2. External Data Sources

## Notion

Primary source for:

- daily activity
- study sessions
- learning records
- roadmaps
- project notes
- career planning
- historical logs

## GitHub

Technical evidence source for:

- repositories
- commits
- pull requests
- issues
- code reviews
- releases
- project history

## DevUniverse

Structured engineering-experience source for:

- tickets
- sprints
- attempts
- hints
- reviews
- QA
- bugs
- engineering difficulty
- role expectations

## Additional Future Sources

- certification providers
- cloud platforms
- deployment platforms
- job specifications
- employment records
- business records

---

# 3. Integration Layer

```text
External Source
      ↓
Connector
      ↓
Source-specific synchronisation
      ↓
Raw source record
```

Future integrations may include:

* Notion
* GitHub
* DevUniverse
* certification systems
* deployment systems
* market/job sources

---

# 4. Raw Data Layer

Raw source information must remain preserved.

```text
Source
   ↓
Raw Record
   ↓
Never rewritten by classification
```

Examples:

* RawActivity
* RawGitHubActivity
* RawJobSpecification
* RawCertificationRecord

Raw data exists separately from interpretation. This is ADR-0001 and it holds for every source added later, not only for Notion.

---

# 5. Session / Activity Extraction

Some source records contain multiple activities.

```text
Notion Daily Log
      ↓
Session 1
Session 2
Session 3
Session 4
      ↓
CareerProof Activities
```

Source record identity and CareerProof activity identity are therefore separate concepts. See ADR-0003.

---

# 6. Classification Layer

```text
RawActivity
      ↓
Source Quality
      ↓
Normalisation
      ↓
Alias Resolution
      ↓
Project Resolution
      ↓
Skill Resolution
      ↓
Topic Resolution
      ↓
Activity Classification
      ↓
LLM Fallback
      ↓
Validation
      ↓
Confidence Gate
      ↓
ParsedActivity
```

Classification is versioned. Low-confidence output may require human review.

---

# 7. Taxonomy

CareerProof maintains a versioned taxonomy.

```text
Skill
  ↓
Topic
  ↓
Subtopic
```

**Domain is a grouping attribute on the skill, not a hierarchy level and not an ID segment.** IDs are capped at three segments per ADR-0007.

```text
id:     csharp.generics.constraints
domain: software-engineering        (attribute, not part of the ID)
```

Further examples:

```text
csharp
└── csharp.generics
    └── csharp.generics.constraints

aspnet-core
└── aspnet-core.controllers
    └── aspnet-core.controllers.action-results
```

Taxonomy IDs remain stable. Display names may evolve.

---

# 8. Career Graph

The long-term Career Graph connects:

```text
User
│
├── Skills
├── Topics
├── Practice
├── Projects
├── Evidence
├── Certifications
├── Employment
├── Education
├── Business Activity
├── Career Goals
└── Market Requirements
```

This becomes the structured representation of the user's career history.

---

# 9. Activity Intelligence

CareerProof eventually analyses:

* practice sessions
* consistency
* repetition
* learning activity
* implementation activity
* reviews
* project work
* production work
* career activity

Activity volume alone must never equal mastery.

---

# 10. Knowledge Intelligence

CareerProof eventually understands:

```text
What has been studied?
What concepts are connected?
What has been revisited?
What is incomplete?
What knowledge gaps remain?
```

Notion learning material may contribute to this layer.

---

# 11. Evidence Engine

Career claims should be connected to evidence.

```text
Claim
  ↓
Supporting Activity
  ↓
Project
  ↓
Code / Review / Deployment / Certification
  ↓
Evidence Strength
```

Evidence may eventually come from Notion, GitHub, DevUniverse, deployed systems, employment, certifications and client work.

---

# 12. Project Intelligence

Projects eventually capture technologies, skills, features, bugs, tests, architecture decisions, commits, reviews, deployment, production experience, users and business outcomes.

---

# 13. Mastery Engine

CareerProof calculates mastery from observable career activity.

Initial versions remain simple. Future versions may incorporate practice, application, evidence, repetition, recency, difficulty, independence, production exposure and responsibility.

Mastery calculations must always be versioned (ADR-0002). A component enters the score only when the data behind it is observable rather than self-declared.

---

# 14. Market Intelligence

CareerProof eventually understands external demand.

Sources may include user-provided job descriptions, approved job data sources, employer requirements and target roles.

```text
Market Requirement
       ↓
Skill Demand
       ↓
Evidence Expectation
```

---

# 15. Gap Analysis

```text
Current Career State
        +
Target Role
        +
External Requirements
        ↓
Gap Analysis
```

Gaps may include knowledge, skill, evidence, project, production and certification gaps.

---

# 16. Next Best Action

CareerProof eventually recommends the highest-value next activity.

```text
Gap
 ↓
Opportunity
 ↓
Recommended Action
 ↓
Expected Evidence
```

Example:

```text
Weak Docker evidence
        ↓
Containerise an existing API
        ↓
Build
Test
CI
Deploy
        ↓
New Evidence
```

---

# 17. AI Mentor

The AI Mentor sits above structured CareerProof intelligence.

It must not simply encourage the user. Its responsibility is to identify:

* weaknesses
* missing experiences
* unproven claims
* valuable next challenges
* evidence gaps

AI recommendations remain advisory.

---

# 18. Career Journey

CareerProof eventually represents progression over time.

```text
Education
   ↓
Learning
   ↓
Projects
   ↓
First professional work
   ↓
Increasing responsibility
   ↓
Specialisation
   ↓
Leadership / Business
```

Career history becomes evidence-backed rather than CV-only.

---

# 19. Product Experience

Future primary product areas may include:

```text
Overview
Career Journey
Practice
Knowledge
Mastery
Skills & Evidence
Projects
Job Market
Job Search
Business
Career Profile
Integrations
```

These are target product areas, not V1 commitments.

---

# 20. Engineering Platform

Long-term infrastructure may include Next.js, ASP.NET Core, PostgreSQL, object storage, background processing, authentication, authorisation, logging, monitoring, analytics, CI/CD, Docker, Azure and AI providers.

Infrastructure is introduced only when a product requirement justifies it.

---

# 21. How CareerProof Is Built

CareerProof is built through a multi-AI engineering process in which no AI holds final authority over architecture or production.

That process — agent roles, ticket flow, review gates, merge authority, tool permissions and model routing — is defined in the CareerProof AI-Native Engineering Guideline, which is maintained outside this repository and is not restated here. Duplicating it would allow the two copies to drift, and this document is the less likely of the two to be kept current.

The in-repository agent contracts are maintained under `docs/agents/`.

---

# Governing Target Loop

```text
USER DOES REAL WORK
        ↓
NOTION / GITHUB / DEVUNIVERSE
        ↓
INGESTION
        ↓
CLASSIFICATION
        ↓
CAREER GRAPH
        ↓
EVIDENCE
        ↓
MASTERY
        ↓
MARKET COMPARISON
        ↓
GAP
        ↓
NEXT BEST ACTION
        ↓
MORE VALUABLE EXPERIENCE
        ↓
        ↺
```

---

# Scope Boundary

This document protects the long-term direction.

It must NEVER be interpreted as:

> Build all of this now.

Every implementation ticket must first be allowed by the current scope document. For V1 that is `docs/product/v1-scope.md`, which takes precedence over this Target Architecture. ADRs take precedence over both.
