# Post-Generation Validation

After modifying any C# file, verify:

1. **Space before `()`** — scan for `\w(` (missing space)
2. **Space before `[]`** — scan for `\w[` (missing space)
3. **Allman braces** — scan for `) {` or `= {` (same-line brace)
4. **No `var`** for non-built-in types
5. **Target-typed `new ()`** not `new TypeName()`
6. **Collection expressions** `[...]` not `new () { ... }`
7. **Lambda discards** `(_, _)` for unused params

## Quick Fix Commands

```bash
# Check formatting
dotnet format Examples.sln --verify-no-changes

# Auto-fix formatting
dotnet format Examples.sln

# ReSharper full cleanup
dotnet tool restore
dotnet jb cleanupcode Examples.sln --profile="Built-in: Full Cleanup" --no-build
```
