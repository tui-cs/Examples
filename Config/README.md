# Terminal.Gui Key Binding Config Examples

This folder contains example `config.json` files that override Terminal.Gui's default key bindings to match platform conventions.

## How to Use

Copy the desired file to `~/.tui/config.json` (the global Terminal.Gui config location) or `./.tui/config.json` (resolved against the app's current directory).

| OS | Want macOS feel? | Want Windows feel? |
|----|------------------|--------------------|
| **Windows** | Copy `macos.json` → `~/.tui/config.json` | (already default) |
| **macOS**   | (already default) | Copy `windows.json` → `~/.tui/config.json` |

On Windows `~` expands to `C:\Users\<username>`. On macOS/Linux `~` expands to `/Users/<username>` / `/home/<username>`.

## JSON Shape (2.5+)

As of Terminal.Gui 2.5, configuration is loaded via Microsoft.Extensions.Configuration (`TuiConfigurationBuilder`) and uses **nested objects**, not dotted keys. Configuration is applied automatically at assembly load — `ConfigurationManager.Enable ()` no longer exists.

To convert a pre-2.5 config (dotted keys, `Themes`/`Schemes` arrays), run the migrator from the Terminal.Gui repo:

```bash
dotnet run --project Tools/MigrateConfig -- ./.tui/config.json ./.tui/config.json
```

See [Migrating ConfigurationManager to TuiConfigurationBuilder](https://github.com/tui-cs/Terminal.Gui/blob/develop/docfx/docs/migrate-cm-to-mec.md).

## What Each File Changes

### `macos.json` — macOS-style bindings (for Windows users)

| What changes | Default (Windows) | With `macos.json` |
|---|---|---|
| Quit app | `Esc` | `Esc` or `Ctrl+Q` |
| Suspend app to background | *(not available)* | `Ctrl+Z` |
| Undo | `Ctrl+Z` | `Ctrl+Z` or `Ctrl+/` |
| Redo | `Ctrl+Y` | `Ctrl+Y` or `Ctrl+Shift+Z` |
| Delete char right | `Delete` | `Delete` or `Ctrl+D` |

### `windows.json` — Windows-style bindings (for macOS users)

| What changes | Default (macOS) | With `windows.json` |
|---|---|---|
| Quit app | `Esc` or `Ctrl+Q` | `Esc` only |
| Suspend app to background | `Ctrl+Z` | *(disabled)* |
| Undo | `Ctrl+Z` or `Ctrl+/` | `Ctrl+Z` only |
| Redo | `Ctrl+Y` or `Ctrl+Shift+Z` | `Ctrl+Y` only |
| Delete char right | `Delete` or `Ctrl+D` | `Delete` only |

## How It Works

Terminal.Gui overlays three nested key-binding sections onto the hard-coded defaults:

- **`Application:DefaultKeyBindings`** — app-level commands (Quit, Suspend, Tab navigation)
- **`View:DefaultKeyBindings`** — shared commands across all views (navigation, clipboard, editing)
- **`View:ViewKeyBindings`** — per-view overrides (keyed by view type name, e.g. `"TextField"`)

The JSON format maps command names to `PlatformKeyBinding` objects:

```json
{
  "Application": {
    "DefaultKeyBindings": {
      "Quit": { "All": ["Esc", "Ctrl+Q"] }
    }
  },
  "View": {
    "DefaultKeyBindings": {
      "Undo": { "All": ["Ctrl+Z"], "Linux": ["Ctrl+/"], "Macos": ["Ctrl+/"] }
    },
    "ViewKeyBindings": {
      "TextField": {
        "WordLeft": { "All": ["Ctrl+CursorLeft"] }
      }
    }
  }
}
```

Each `PlatformKeyBinding` has four optional fields:

| Field | Applies to |
|-------|-----------|
| `All` | Every platform |
| `Windows` | Windows only (added to `All`) |
| `Linux` | Linux only (added to `All`) |
| `Macos` | macOS only (added to `All`) |

Bindings overlay **per command**: a command you set replaces that command's default binding entirely (include every key you want active for it), while commands you omit keep their compile-time defaults.

Key bindings can also be changed in code, before `Application.Create ()`:

```csharp
Application.SetDefaultKeyBinding (Command.Quit, Bind.All (Key.Esc, Key.Q.WithCtrl));
```

`example_config.json` is a fuller sample that also sets a custom theme, glyphs, and view defaults.
