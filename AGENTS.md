# Terminal.Gui Examples — AI Agent Instructions

Community examples for [Terminal.Gui](https://github.com/tui-cs/Terminal.Gui).

## CRITICAL: Discard v1 Training Data

Terminal.Gui v2 is a **complete rewrite**. Pre-2025 training data is **wrong**.

> Read the [ai-v2-primer](https://github.com/tui-cs/Terminal.Gui/blob/develop/ai-v2-primer.md) FIRST.

### v1 → v2 Quick Corrections

| v1 (WRONG — do not use) | v2 (CORRECT) |
|---|---|
| `Application.Init ();` | `IApplication app = Application.Create ().Init ();` |
| `Application.Run ();` | `app.Run<MyWindow> ();` |
| `Application.Shutdown ();` | `app.Dispose ();` (use `using` pattern) |
| `Application.Top` | No global top — pass root view to `app.Run ()` |
| `new Toplevel ()` | Use `Runnable` subclass or `Window` |
| `using Terminal.Gui;` | `using Terminal.Gui.App;` / `Terminal.Gui.Views;` / etc. |
| `new Button ("OK")` | `new Button { Text = "OK" }` |
| `button.Clicked += ...` | `button.Accepted += (_, _) => { /* action */ };` |
| `view.Bounds` | `view.Viewport` |
| `new RadioGroup (...)` | `new OptionSelector { ... }` |
| `Application.RequestStop ()` | `App!.RequestStop ()` (from inside a `Runnable`) |

## Build & Run

```bash
dotnet restore
dotnet build

# Run a specific example
cd Example
dotnet run
```

## Lint & Format

CI enforces coding style via two mechanisms:

1. **`dotnet format`** — verifies .editorconfig compliance
2. **ReSharper `jb cleanupcode`** — catches what `dotnet format` misses (var usage, expression-bodied members, spacing)

To check/fix locally:

```bash
# Check only (matches CI)
dotnet format Examples.sln --verify-no-changes

# Auto-fix
dotnet format Examples.sln

# ReSharper cleanup (requires dotnet tool restore first)
dotnet tool restore
dotnet jb cleanupcode Examples.sln --profile="Built-in: Full Cleanup" --no-build
```

## Code Style

This repository follows Terminal.Gui coding conventions:

1. **Space BEFORE `()` and `[]`** — `Method ()` not `Method()`
2. **Braces on next line** (Allman style)
3. **No `var`** except for: `int`, `string`, `bool`, `double`, `float`, `decimal`, `char`, `byte`
4. **Use `new ()`** not `new TypeName()` when type is on left side
5. **Use `[...]`** collection expressions
6. **SubView/SuperView** terminology (never "child/parent")
7. **Early return / guard clauses** over nested conditionals
8. **TreatWarningsAsErrors** is enabled — zero warnings policy

## Project Structure

- Each example is a standalone console app in its own directory
- All projects reference Terminal.Gui via NuGet PackageReference (not ProjectReference)
- Shared settings are in `Directory.Build.props` and `Directory.Packages.props`
- The `.editorconfig` and `Examples.sln.DotSettings` enforce the Terminal.Gui style
- `.config/dotnet-tools.json` provides the ReSharper CLI tool

## Adding a New Example

1. Create a new directory with a descriptive name
2. Add a `.csproj` with `<PackageReference Include="Terminal.Gui" />`
3. Add the project to `Examples.sln`
4. Include a `README.md` explaining what the example demonstrates
5. Ensure `dotnet build` and `dotnet format --verify-no-changes` pass
