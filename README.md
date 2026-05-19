# dungeon-anticheat

A small 2D top-down Unity project that demonstrates data integrity checks for save files. The game saves player position and health to JSON and uses SHA-256 with a per-session seed to detect tampering.

---

## Overview

The project includes:
- Basic player movement and camera follow.
- Health system with a UI slider.
- Health and speed potions.
- Trap damage over time.
- Pause menu with Save and Quit.
- Save/load system with integrity verification.

---

## Anticheat Mechanism

When saving, the game computes a hash over the critical values and a session-only seed:

- Data: `health`, `x`, `y`
- Hash: `SHA-256(health:x:y:sessionSeed)`

On load, the hash is recomputed and compared. If it does not match, the save is rejected and a warning is logged.

---

## How To Run

1. Open the project in Unity 2022.3 LTS or later.
2. Open the main scene (your project uses the scene name `MainScene`).
3. Press Play.

---

## Controls

- `W A S D` or Arrow keys: Move
- `Esc`: Toggle pause menu

---

## Save File

The file is stored at `Application.persistentDataPath` as `savegame.json`. The full path is printed to the console on startup.

Example format:

```json
{
  "x": 1.5,
  "y": -3.25,
  "health": 80,
  "hash": "...",
  "savedAt": "2026-05-18 13:42:10"
}
```

If you edit `x`, `y`, or `health` without updating `hash`, the integrity check will fail on the next load.

---

## Scripts Reference

- `SaveSystem.cs`: JSON save/load and SHA-256 integrity check.
- `PauseBehaviour.cs`: Pause menu logic and Save trigger.
- `PlayerMovement.cs`: Movement and animation parameters.
- `PlayerHealth.cs`: Health logic and UI slider updates.
- `HealthPotion.cs`: Restores health and destroys itself.
- `SpeedPotion.cs`: Temporary speed boost.
- `TrampsDamage.cs`: Trap damage with cooldown.
- `CameraBehaivour.cs`: Camera follow.
- `UIcontroller.cs`: Scene start and quit buttons.

---

## Demo: Tamper Detection

1. Play the scene.
2. Pause and click Save (or call `SaveSystem.SaveGame()` from UI).
3. Open the `savegame.json` file and change `health`, `x`, or `y`.
4. Return to Unity and reload the game (SaveSystem loads on Start).
5. The console will log an anticheat error and the save will be rejected.
