---
name: project-memory
description: Persistent project context and architectural memory manager. Automatically reads, distills, and updates architectural decisions, conventions, and invariants in the repository.
---

# Project Memory Manager

Maintains project continuity across long coding sessions, subagents, and chat boundaries.

## Core Files
- `.agent/memory.json` or `PROJECT_MEMORY.md` in workspace root.

## Memory Structure
1. **Architectural Invariants**: Core rules that must never be violated (e.g., target frameworks, separation of concerns).
2. **Key Decisions Log**: Why specific technical choices were made (ADR format).
3. **Known Gotchas & Pitfalls**: Bugs encountered and verified workarounds.
4. **Active Workspace State**: Current sub-systems under modification.

## Workflow
- **On Task Start**: Check `.agent/memory.json` or `PROJECT_MEMORY.md` to ground context before proposing solutions.
- **On Milestone Completion**: Distill durable lessons and append to the project memory file without bloating the file.
