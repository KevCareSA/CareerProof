# CareerProof

> Turn real career activity into proof.

CareerProof is a career evidence system that transforms learning, practice, projects, and work into a structured record of skills, progress, and mastery.

It is designed to answer a simple question:

**What can you prove you know how to do?**

---

## What CareerProof Tracks

CareerProof turns real activity into evidence of:

- Learning
- Practice
- Projects
- Experience
- Skills
- Evidence
- Mastery
- Career progression

---

## How It Works
```text
ACTIVITY
   ↓
PRACTICE
   ↓
EVIDENCE
   ↓
SKILLS
   ↓
MASTERY
   ↓
CAREER PROOF
```

For V1:
```text
Notion Daily Code Log
        ↓
Raw Activity
        ↓
Session Extraction
        ↓
Classification
        ↓
Practice Sessions
        ↓
Mastery
        ↓
Dashboard
```

---

## V1 Goal

CareerProof V1 focuses on one thing:

> Ingest a real Daily Code Log, understand the sessions reliably, and calculate a defensible mastery score.

The goal is not to build every future CareerProof feature at once.

The goal is to prove the core system works with real data.

---

## Tech Stack

- ASP.NET Core
- C#
- .NET 8
- xUnit
- GitHub Actions

---

## Current Build
```text
✓ Step -1   100-record spike
✓ Step  0   Taxonomy, scope, architecture, ADRs
✓ Step  1   Implementer and reviewer contracts
✓ Step  2   Repository and CI
✓ Step  3   Domain entities

→ Step  4   Notion sync
```

---

## Engineering Approach

CareerProof is built with a human-controlled, AI-assisted workflow:
```text
Human Architect
      ↓
AI Implementer
      ↓
Independent AI Reviewer
      ↓
Automated Tests + CI
      ↓
Human Merge Authority
```

AI can plan, implement, and review.

**The human remains the final authority.**

---

## Repository
```text
CareerProof/
├── src/
│   └── CareerProof.Api/
├── tests/
│   └── CareerProof.UnitTests/
├── taxonomy/
├── docs/
├── .github/
└── CareerProof.sln
```

---

## Quality Gates

Every change must pass:

- Build
- Unit Tests
- Format

---

## Status

CareerProof is currently in active development.

**Current step: Step 4 — Notion sync**
