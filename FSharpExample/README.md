# F# Example

A simple Terminal.Gui v2 login window written in F#, mirroring the C# `Example` project.

```bash
cd FSharpExample
dotnet run
```

Notes for F# consumers of Terminal.Gui v2:

- Open the v2 namespaces (`Terminal.Gui.App`, `Terminal.Gui.Input`, `Terminal.Gui.ViewBase`, `Terminal.Gui.Views`) — there is no flat `Terminal.Gui` namespace.
- `Pos` arithmetic with integers needs an explicit conversion: `Pos.Right (label) + Pos.op_Implicit (1)`.
- With `<Nullable>enable</Nullable>`, F# 9 nullness checking applies to Terminal.Gui's annotations — match nullable members like `View.App` against `null` before use.
- Configuration (themes, schemes, settings) is applied automatically at assembly load.
