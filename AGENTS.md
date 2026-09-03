# Roguelike Autobattler — Repository Guide

This document describes the current implementation and architecture of the project. Keep it updated whenever files, responsibilities, dependencies, or runtime flow change.

Game rules, feature direction, and unresolved design questions live in [`GAME_DESIGN.md`](GAME_DESIGN.md). Read that document before making gameplay decisions, and ask before implementing anything marked as unresolved.

**REMINDER** - The Unity CLI is available to be used to aid in development/testing/troublshooting

## Project Structure

The current gameplay code lives under `Assets/_Game/`.

### Core (`Assets/_Game/Core/`)

- **`GameStateMachine`** — Holds the current `IGameState`. `ChangeState()` calls `Exit()` on the previous state, raises state events, and calls `Enter()` on the new state. `Tick()` delegates to the current state.
- **`IGameState`** — Interface defining `Enter(Action<IGameState> onStateChange)`, `Exit()`, and `Tick()`.
- **`GameEvents`** — Static event bus for state transitions, unit damage and defeat, and combat start and end.

### States (`Assets/_Game/States/`)

- **`PlanningState`** — Current entry state. It hardcodes an enemy team, adds a test player unit, and immediately transitions to `CombatState`.
- **`CombatState`** — Creates a `BattleSimulation`, advances it using `Time.deltaTime`, and transitions to `ShopState` when combat finishes.
- **`ShopState`** — Stub. It logs when entered and currently has no implemented shop flow or outgoing transition.

### Models (`Assets/_Game/Models/`)

- **`UnitModel`** — Holds base stats, current-combat stat variants, current health and mana, attack timing, team ownership, and targeting state. It also applies healing and damage and raises unit events.
- **`RunModel`** — Holds the run ID, player health, player money, player units, and enemy units. `AddUnitToTeam()` currently enforces a hardcoded limit of five units.
- **`Attack`** — Describes an attack by name, `DamageType` (`Physical`, `Magic`, or `True`), and `AttackType` (`InstantDamage` or `Dot`).

### Combat (`Assets/_Game/Combat/`)

- **`BattleSimulation`** — Builds the combat-unit list from `RunModel`, resets combat stats, advances unit attack timers, selects targets, resolves attacks, and determines the winner.
- **`AttackResolver`** — Calculates base damage, critical hits, and armor or magic-resistance mitigation. True damage ignores mitigation.
- **`UnitTargeting`** — Currently selects the first living unit from the supplied target list.

### Run (`Assets/_Game/Run/`)

- **`RunManager`** — Static global holder for the current `RunModel`, exposed through `SetRun()` and `GetRun()`.

### Presentation (`Assets/_Game/Presentation/`)

- **`RunController` (`MonoBehaviour`)** — Creates a run with 100 health and 50 gold in `Awake()`, stores it in `RunManager`, enters `PlanningState` in `Start()`, and ticks the state machine in `Update()`.

### Units (`Assets/_Game/Units/`)

- **`AddUnitToTeamResult`** — Result enum with `Success` and `TeamFull` values.

## Current Runtime Flow

```text
RunController
    -> PlanningState
        -> CombatState
            -> ShopState
```

`PlanningState` currently creates all test combat data during `Enter()` and transitions immediately, so there is not yet an interactive planning phase. `ShopState` is currently the end of the implemented flow.

## Current Architectural Characteristics

- Game states construct their successor states directly.
- `RunManager` and `GameEvents` are static global dependencies.
- Unit definition data, persistent unit state, and temporary combat state currently share `UnitModel`.
- Enemy setup is hardcoded in `PlanningState`.
- Combat is advanced from Unity's frame delta and uses Unity's global random source for critical hits.
- Runtime code currently compiles into Unity's default `Assembly-CSharp` assembly; there are no gameplay assembly definitions or gameplay tests yet.
- The enabled build scene is `Assets/Scenes/SampleScene.unity`. At the time of writing, `RunController` is referenced by `Assets/_Recovery/0.unity`, not the checked-in build scene.

## Documentation Responsibilities

Update this file when implementation details change, including:

- File and folder structure
- Class responsibilities
- Dependencies and ownership
- Runtime and state flow
- Persistence and serialization architecture
- Testing and assembly structure

Update `GAME_DESIGN.md` instead when changing intended rules, balance, progression, shops, units, traits, artifacts, items, encounters, or other player-facing features.
