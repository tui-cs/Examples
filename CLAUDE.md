# CLAUDE.md

See [AGENTS.md](AGENTS.md) for AI agent instructions.

## Before Every File Edit

Read `.claude/POST-GENERATION-VALIDATION.md` to prevent common style violations.

## After Writing/Modifying Code

Use `.claude/agents/cleanup-agent.md` to validate and clean all code.

## Quick Rules

1. **Space before `()` and `[]`** — `Method ()`, `array [i]`
2. **Braces on next line** (Allman style)
3. **No `var`** except built-in types
4. **Use `new ()`** not `new TypeName()`
5. **Use `[...]`** collection expressions
6. **SubView/SuperView** terminology
7. **Early return / guard clauses**
