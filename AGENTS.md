# Roguelike Autobattler — Project Overview
This is the main guide for the game. please keep this updated.

## Concept
Balatro-style roguelike structure + PVE TFT autobattler combat. You build a bag of units over a run, draft from it each planning phase to form a persistent team, fight enemy teams automatically, then shop for upgrades.

---

## Current - not necessarily ideal - Architecture (Assets/_Game/) "UPDATE WHEN CHANGED"

### Core (`_Game/Core/`)
- **`GameStateMachine`** — Holds `IGameState`. `ChangeState()` calls `Exit()` on old, `Enter()` on new. `Tick()` delegates.
- **`IGameState`** — Interface: `Enter(Action<IGameState> onStateChange)`, `Exit()`, `Tick()`.
- **`GameEvents`** — Static event bus. Events: state transitions, unit damaged/defeated, combat start/end.

### States (`_Game/States/`)
- **`PlanningState`** — Entry state. Hardcodes enemies, adds test unit, transitions to CombatState.
- **`CombatState`** — Creates `BattleSimulation`, calls `Step(deltaTime)` each tick. Transitions to ShopState when finished.
- **`ShopState`** — Stub. Logs on enter, awaits implementation.

### Models (`_Game/Models/`)
- **`UnitModel`** — Data class. Health, Mana, AttackDamage/Speed, Crit, AbilityPower, Armor, MR, GoldCost, IsPlayerUnit, Target. Has ForCurrentCombat stat variants. Methods: `TakeDamage()`, `Heal()`.
- **`RunModel`** — Run state: Id, PlayerHealth, PlayerMoney, PlayerUnits, EnemyUnits. `AddUnitToTeam()` caps at 5.
- **`Attack`** — Name, DamageType (Physical/Magic/True), AttackType (InstantDamage/Dot).

### Combat (`_Game/Combat/`)
- **`BattleSimulation`** — Orchestrates combat. `StartBattle()` resets stats. `Step(deltaTime)` ticks timers, picks targets, resolves attacks. Ends when one side is dead.
- **`AttackResolver`** — Damage calc: base → crit check → armor/MR mitigation (100/(100+stat)). True damage ignores mitigation.
- **`UnitTargeting`** — Returns first alive enemy target.

### Run (`_Game/Run/`)
- **`RunManager`** — Static singleton: `SetRun()` / `GetRun()`.

### Presentation (`_Game/Presentation/`)
- **`RunController` (MonoBehaviour)** — `Awake()` creates `RunModel` (100 HP, 50 gold), stores via RunManager. `Start()` enters PlanningState. `Update()` calls `_stateMachine.Tick()`.

### Units (`_Game/Units/`)
- **`AddUnitToTeamResult`** — Enum: Success, TeamFull.

---

## Game Design

### Core Loop
```
Planning Phase (TFT unit shop + formation) → Combat → Shop Phase (Balatro shop) → Planning Phase → ...
```
Persistent team + bench across the entire run (like TFT). Units survive between rounds. Bench is permanent — you can save units for 3-star upgrades. Shop can be locked between rounds.#

Run length (successful run) will be longer than balatro but shorter than something like slay the spire, maybe 1 hour ish

### Two Separate Shops
| Phase | Type | What You Buy | Currency |
|-------|------|-------------|----------|
| **Planning** (TFT-style) | Unit draft from your bag | Units to field/bench | Same gold pool |
| **Shop** (Balatro-style) | Collection building | Artifacts (jokers), tarot equivalents, unit packs, items | Same gold pool |

Same gold pool. Tension: *"Buy team size/units/reroll shop for this battle? or for my bag? or save for an artifact or packs after the fight?"*

### Concept Mappings (Balatro → This Game)
| Balatro | This Game |
|---------|-----------|
| Poker cards | **Units** (fielded on board, benched, or in bag) |
| Jokers (multiplicative, combos) | **Artifacts** (modifiers, crazy combos, gold/strength/synergy) |
| Deck | **Unit Bag** (pool of units you've collected over the run) |
| Joker/planet/tarot packs | **Pack equivalents** for units, artifacts, tarots |
| Tarot cards | **Tarot equivalents** (modify units temporarily/permanently) |
| Hands (count per round) | **Team size** (grows via augments, gold, time — persistent across run) |
| Interest / economy | Same — Balatro-style gold management |
| Shop refresh/lock | Same — both shops can be refreshed/locked |

### Unit Systems
- **Stars**: 1–5 star (TFT-style, combine 3 of same for upgrade)
- **Cost**: 1–10 gold cost tiers
- **EXP** (exploratory): Units might gain exp over time to unlock item slots, ultimate upgrades, or character augments (like TFT hero augments)

### Artifacts (Joker Equivalents)
- Multiplicative modifiers, crazy combos, synergy enablers
- Affect gold, strength, bag depth, traits, synergies
- Stack and multiply like Balatro jokers

### Other Systems
- **Traits & Synergies** — Standard autobattler trait system
- **Items** — TFT-style persistent equippable items for units
- **Fog of War** — only shows in planning phase, starts full blind to enemy layout and comp - fog gets revealed through some mechanic (could be an artifact, certain level (like tft level, related mainly to team size) bonus, something similar to vouchers (probs best)?)
- **Art Style** — Cartoony, polished. Heavily inspired by Balatro UI + Dota Underlords aesthetics

### Open Design Questions (ask me before implementing)
Unknown:
- How exactly do unit packs work for the bag? (Random pull vs choose from options vs ?)
- Tarot equivalents — what do they actually do? (Buff a unit for one fight? Permanently modify? Reroll shop?)
- Unit EXP system — should this exist, and if so, what gates does it unlock?

Known:
- Trait system — standard TFT breakpoints (2/4/6) or something different?
ANSWER: mostly standard tft
- Team size — exact scaling mechanism (every X rounds? gold purchase? artifact effect?)
ANSWER: same as tft, level system  
- Enemy generation — randomly generated each round vs handcrafted stages vs ?
ANSWER: handcrafted choices but with randomness added (player should have an idea but still be somewhat suprised) 
- How does losing work? (HP loss per surviving enemy? Run over at 0? Balatro-style antes?)
ANSWER: hp loss per surviving enemy
- Bench size limit? Or unlimited bench (bag-like)?
ANSWER: starts at 10, like tft. something similar to negatives exist to bypass it. it can be insreased through artifacts/vouchers/unit traits 
- How many artifact slots? Do they grow over time too?
ANSWER: starts at 5, like balatro. something similar to negatives exist to bypass it. it can be insreased through artifacts/vouchers/unit traits 