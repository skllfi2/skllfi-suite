---
name: repo-roadmap
description: In-repo roadmap and backlog tracker. Manages feature tasks across Doing Now, Next, and Shipped stages, synchronizing progress with agent execution and commit logs.
---

# In-Repo Roadmap Tracker

Coordinates development flow directly inside the codebase using `ROADMAP.md`.

## Lifecycle Stages
- **Doing Now**: The single active initiative or feature being implemented.
- **Next**: Priority queue of upcoming tasks.
- **Shipped**: Completed features with completion date and commit/PR reference.
- **Backlog / Ideas**: Unscheduled ideas and feature wishes.

## Agent Behavior
1. Check `ROADMAP.md` at the start of a feature cycle to align on acceptance criteria.
2. Update the status of tasks dynamically as tests pass and verification concludes.
3. Automatically move finished tasks into `Shipped` with date tags.
