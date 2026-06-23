# Contributing to Terminal.Gui Examples

Thank you for your interest in contributing examples!

## Guidelines

1. **One example per directory** — each example should be self-contained
2. **Use NuGet references** — reference Terminal.Gui via `<PackageReference Include="Terminal.Gui" />`, not ProjectReference
3. **Follow coding conventions** — see the `.editorconfig` and [Terminal.Gui CONTRIBUTING.md](https://github.com/tui-cs/Terminal.Gui/blob/develop/CONTRIBUTING.md)
4. **Include a README** — explain what the example demonstrates and how to run it
5. **Keep it focused** — each example should demonstrate one concept or integration pattern
6. **Ensure it builds** — run `dotnet build` before submitting

## Code Style Quick Reference

- Space before `()` and `[]`: `Method ()`, `array [i]`
- Allman braces (opening brace on next line)
- No `var` except built-in types (`int`, `string`, `bool`, etc.)
- Target-typed new: `Button btn = new () { Text = "OK" };`
- Collection expressions: `List<View> views = [view1, view2];`

## Submitting

1. Fork this repository
2. Create a feature branch
3. Add your example
4. Run `dotnet build` and `dotnet format --verify-no-changes`
5. Submit a pull request
