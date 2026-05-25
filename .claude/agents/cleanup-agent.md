# Code Cleanup Agent

## Purpose

Automated code cleanup and formatting enforcement for Terminal.Gui Examples.

## Usage

Invoke by telling your AI agent "cleanup these files" or "run code cleanup".

## Steps

1. **Build** the solution to ensure a clean starting state:
   ```bash
   dotnet build Examples.sln
   ```

2. **Run dotnet format** to fix .editorconfig violations:
   ```bash
   dotnet format Examples.sln
   ```

3. **Run ReSharper cleanupcode** for deeper style fixes:
   ```bash
   dotnet tool restore
   dotnet jb cleanupcode Examples.sln --profile="Built-in: Full Cleanup" --no-build
   ```

4. **Verify** no new build warnings:
   ```bash
   dotnet build Examples.sln --no-restore
   ```

5. **Run smoke tests** to confirm nothing broke:
   ```bash
   dotnet run --project tests/Examples.SmokeTests
   ```

## Acceptance Criteria

Successfully cleaned files meet:
- ✓ No build warnings (TreatWarningsAsErrors is enabled)
- ✓ `dotnet format --verify-no-changes` passes
- ✓ `jb cleanupcode` produces no diff
- ✓ All smoke tests pass

## Style Quick Reference

| Rule | Correct | Wrong |
|------|---------|-------|
| Space before `()` | `Method ()` | `Method()` |
| Space before `[]` | `array [i]` | `array[i]` |
| Allman braces | `)\n{` | `) {` |
| No var (non-built-in) | `Label lbl = new ();` | `var lbl = new Label ();` |
| Target-typed new | `Button btn = new ();` | `Button btn = new Button ();` |
| Collection expressions | `[1, 2, 3]` | `new () { 1, 2, 3 }` |
| Lambda discards | `(_, _) => { }` | `(sender, e) => { }` |

## ReSharper Command Line Usage

```bash
# Apply cleanup to all files
dotnet jb cleanupcode Examples.sln --profile="Built-in: Full Cleanup" --no-build

# Apply cleanup to a single file
dotnet jb cleanupcode Examples.sln --profile="Built-in: Full Cleanup" --include="Example/Example.cs" --no-build

# Inspect for code issues
dotnet jb inspectcode Examples.sln --output=report.xml --severity=WARNING --no-build
```
