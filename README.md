# SentryFM - WebGL ANR Filter

Package for filtering Sentry ANR errors when browser tab is inactive.

## Installation

### Via Git URL (Recommended)
Add this line to your `Packages/manifest.json`:
```json
{
  "dependencies": {
    "com.funmatica.sentry-fm": "https://github.com/khud-funmatica/sentryfm.git"
  }
}
```

### Via Local Package
The package is already installed via `Packages/manifest.json`.

## Usage

1. **Creating Sentry configuration:**
   - In Unity Editor, go to menu `Funmatica > Create > Sentry WebGL Options Configuration`
   - Select folder `Assets/Resources/Sentry/` and save the file
   - Open `Assets/Resources/Sentry/SentryOptions.asset`
   - Drag the created `SentryWebGLOptionsConfiguration` into the `OptionsConfiguration` field

2. **Adding visibility tracking component:**
   - Create an empty GameObject in the first scene
   - Name it `SentryWebGLFix`
   - Add `SentryWebGLFix` component
   - Done!

## Architecture

### Components

1. **SentryWebGLOptionsConfiguration** - ScriptableObject configuration for Sentry
   - Registers `WebGLAnrEventProcessor` through Sentry options configuration
   - Created in `Assets/Resources/Sentry/SentryWebGLOptionsConfiguration.asset`

2. **WebGLAnrEventProcessor** - Event processor for filtering ANR events
   - Implements `ISentryEventProcessor`
   - Filters ANR errors based on browser visibility state

3. **SentryWebGLFix** - MonoBehaviour for tracking tab visibility
   - Stores static visibility data (`IsVisible`, `LastVisibleAt`)
   - Initializes JavaScript integration for visibility tracking
   - Configurable grace period: `GracePeriodMs` (default 3000ms)

## How it works

1. When Sentry initializes, it loads configuration from `SentryOptions.asset`
2. `SentryWebGLOptionsConfiguration` adds `WebGLAnrEventProcessor` to event processing pipeline
3. `SentryWebGLFix` tracks browser tab visibility through JavaScript
4. When ANR event occurs, processor checks tab visibility:
   - If tab is invisible - event is filtered (not sent)
   - If less than `GracePeriodMs` has passed since return - event is filtered
   - Otherwise - event is sent to Sentry

## Settings

In `SentryWebGLFix` code you can change:
- `GracePeriodMs` (default 3000ms) - ANR ignore period after returning to tab

## Benefits of Options Configuration Architecture

- ✅ Configuration is applied at Sentry initialization stage
- ✅ No runtime code calls required
- ✅ Centralized configuration through Unity Inspector
- ✅ Follows Sentry Unity SDK best practices

## Requirements

- Unity 2021.3 or later
- Sentry Unity SDK
- WebGL platform target

## License

This package is part of the Funmatica project.