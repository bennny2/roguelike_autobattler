# Roguelike Autobattler — Game Design

This document is the source of truth for the intended game rules, systems, progression, and player experience. Keep implementation details in [`AGENTS.md`](AGENTS.md).

Anything listed under **Open Design Questions** must be resolved with Ben before it is implemented.

## Concept

The game combines a Balatro-style roguelike structure and collection-building economy with PvE TFT-style autobattler combat. The player builds a persistent roster, forms and positions a team, fights automatic battles, and develops the run through a large unified shop.

The player's **Roster**—all fielded and benched units—is the loose equivalent of a Balatro deck, while the active formation is the part played in combat. There is no player-owned Unit Bag or roster draw. The distinctive interactions should come from levelling, rerolling, unit upgrades, artifacts, traits, packs, consumables, and a visible Recruitment Pool all competing for the same resources.

The project is intended for eventual release on Steam and/or mobile storefronts, but its scope must remain realistic for Ben and Codex to complete, with specialist art help eventually.

## Core Loop

```text
Shop Phase
    - Buy, sell, reroll, lock, level, and manage the roster
    - Broad encounter intent may be visible, but exact enemy composition and formation are not
        -> Finish Shopping
            -> Planning Phase
                - Enemy information is revealed according to fog of war
                - Finalise formation, bench, and equipment
                    -> Combat
                        -> Encounter rewards
                            -> Shop Phase
                                -> ...
```

The fielded team and bench persist across the run. The whole shop can initially be locked between rounds, but leaving the Shop Phase is a one-way transition until after combat.

## Unified Shop

The shop is intended to be a large, pivotal, screen-filling part of the game rather than a small row of units. There is **one shop and one reroll action**. Rerolling refreshes every eligible, unpurchased offer in the shop at the same time.

The initial structure to prototype is:

- **Five Unit slots** that always offer units.
- **Three Curio slots** that can offer artifacts, items, consumables, tarot/planet equivalents, specialist services, or other non-unit effects.
- **Two Pack slots** that offer packs for units, artifacts, items, consumables, tarot/planet equivalents, or mixed rewards.
- **One Voucher-equivalent slot** for permanent run infrastructure. The initial intent is that this can be purchased once per chapter or stage; until purchased, unified rerolls may change its offer.
- XP and level controls, selling, shop odds, the reroll action, and the shop lock are presented as part of the same screen.

Purchased units go directly to the bench. There is no owned-unit storage beyond the field and bench, so a full bench forces the player to field, combine, sell, or decline a unit.

During the Shop Phase, the player may buy and sell, move units between the field and bench, arrange the board, equip units, reroll, lock, and buy XP. Broad encounter intent, such as an archetype or special rule, may be shown, but the next enemy's exact composition, formation, and fogged board are not. Selecting **Finish Shopping** closes all commerce and economy actions until after combat, preventing the player from selling and rebuilding the roster specifically to counter a revealed opponent.

The initial intent is for the unified reroll price to remain flat rather than escalating, preserving TFT-style low-cost reroll strategies. The exact reroll price is a balance value. Rerolling, buying XP, purchasing offers, and saving for interest must compete for the same gold.

### Shop Level

Player level governs team size at specific thresholds and influences the shop's offer distributions:

- Higher levels improve the odds of finding higher-cost units.
- Higher levels improve the odds of finding rarer or more powerful artifacts and other curios.
- Run stage may also influence rarity odds and shop quality.
- Level and stage influence probabilities rather than acting as absolute rarity gates.

Every unlocked rarity has a small non-zero chance to appear from the start, allowing a legendary first-shop high-roll to define a run. Level and stage make rare offers more likely rather than unlocking them absolutely. Because levelling already improves team capacity and unit odds, rarity should also reflect flexibility, complexity, cost, ceiling, or drawbacks—not only numerical strength.

### Shop Composition and Variance

Shop generation uses soft composition rules and an internal market-power budget to control average quality. These are guardrails, not a promise of evenly matched shops:

- A player can high-roll an unusually powerful early artifact or an exceptional collection of complementary offers.
- A player can low-roll an unattractive or poorly matched shop.
- Some shops create pivots; others test adaptation and economy discipline.
- Repetition and extreme-outlier protection should not smooth away meaningful highs and lows.

Low-level reroll builds may gain broad selection among common or specialised effects, while high-level builds gain a higher average rarity ceiling. Artifact frequency, composition, rarity, prices, level and stage scaling, pack frequency, and the market budget all require playtesting. Skipped unique artifacts should generally be protected from immediately repeating.

### Unit Shop Pool

The five Unit slots are supplied by a finite, player-visible TFT-style **Recruitment Pool**. This is a shop-generation and scarcity system rather than an owned collection or a source from which the player's combat team is drawn:

- Shop offers temporarily reserve copies from the pool.
- Purchased units remain removed from the pool.
- Selling returns their underlying copies.
- Combining units does not return copies; an upgraded unit still represents all copies used to create it.
- Player level influences cost-tier odds.
- Generated copies and effects that break normal supply rules must be clearly identified and tracked separately where necessary.
- Traits, artifacts, vouchers, tarot/planet equivalents, and other effects may manipulate supply, alter odds, guarantee categories of units, or otherwise bend the shop rules.

For example, a high Earthborn trait breakpoint might make every third paid reroll guarantee some number of Earthborn offers. Such guarantees should normally respect actual remaining pool availability unless an effect explicitly creates new copies.

The player can open a detailed Recruitment Pool view from the shop. It should show each unit's total supply, copies owned on the field or bench, copies reserved by the current shop, and remaining available copies. It should support grouping or filtering by cost, trait, and role, while the normal shop uses compact scarcity and upgrade indicators.

The broader unit catalogue should also show locked units and allow the player to inspect their unlock challenges. Locked units are not part of the current run's supply; unlocks earned during a run should normally apply from the next run.

Player modification of the Recruitment Pool is an intentional but carefully controlled strategy. Expensive and relatively uncommon effects may add or remove available copies, guarantee or filter future offers, introduce a unit, or cause an upgraded unit to appear. Pool modification should compete economically with levelling and conventional rerolling: a player may remain at a low level and reroll less, but spend heavily to make the smaller number of shops much more likely to contain desired units. The pool must remain substantially less editable than a Balatro deck, and all such effects require extensive balance testing.

Unlocking additional units may eventually dilute future shop odds; record this as a content-scaling risk, but do not add a rotation or other mitigation system until the roster is large enough to require one.

Exact pool sizes, the detailed presentation, transformation accounting, and whether any enemy or rival systems interact with the player's pool remain unresolved.

### Packs and Voucher Equivalents

Packs make the shop larger and provide controlled bursts of choice without filling every visible slot with direct permanent power. Potential categories include unit packs organised by cost, trait, role, or upgrade relevance; artifact, item, consumable, or tactic packs; and mixed or mystery packs. Their exact contents and choice rules remain unresolved.

Voucher equivalents provide permanent infrastructure for the current run rather than ordinary unit combat stats. Potential effects include increasing bench or artifact capacity, raising the interest cap, improving packs, adding or modifying shop slots, discounting rerolls, improving scouting, increasing sell value, or allowing an individual offer to be pinned. Their exact name, cadence, costs, and pool remain unresolved.

## Planning Phase

The Planning Phase begins only after the player selects **Finish Shopping**. At that point the shop is closed and the upcoming enemy board is shown according to the current fog-of-war and scouting rules.

The player may reposition owned units, swap units between the field and bench, and equip, remove, or transfer items before starting combat. The player cannot buy, sell, reroll, lock, or purchase XP. Planning is therefore about adapting the already-owned roster to available enemy information, not rebuilding the roster after seeing the matchup.

## Balatro-to-Autobattler Mappings

| Balatro | Roguelike Autobattler |
|---|---|
| Poker cards | **Units**, which are either fielded or benched |
| Deck | **Roster**, the persistent collection of fielded and benched units |
| Full deck inspection | **Recruitment Pool view**, showing the run's unit supply, owned copies, current offers, remaining copies, and locked catalogue entries |
| Played poker hand | **Active formation**, selected and positioned from the roster |
| Jokers | **Artifacts**, providing modifiers, combinations, economy effects, and rule-breaking interactions |
| Joker, planet, and tarot packs | Packs for artifacts, consumables, units, items, and related rewards |
| Tarot cards | **Tarot equivalents** that modify units or game state; exact rules are unresolved |
| Planet cards | **Planet equivalents** that provide lasting run progression; exact rules are unresolved |
| Hands per round | **Team size**, governed primarily by the level system |
| Interest and economy | TFT/Balatro-style gold management |
| Shop refresh and lock | One unified shop refresh and an initially whole-shop lock |

These mappings are inspirations rather than requirements for exact one-to-one mechanical equivalents.

## Run and Progression

- The target run duration remains unresolved and should be set only after a complete run can be playtested. Early, deliberative runs may be much longer than experienced runs using faster battle speeds.
- Player health is lost according to the number of surviving enemy units after a defeat.
- An initial pacing hypothesis is three chapters, each containing four encounters followed by a boss, with shallow branching and optional elites. This is not yet a confirmed structure.
- Bosses should each centre on one prominent, clearly communicated gameplay rule rather than relying only on stronger statistics.
- Enemy encounters should be based on handcrafted choices with added randomness. Broad encounter intent may be visible during shopping; actionable board information is revealed during Planning, after commerce has closed, so the player can adapt positioning without rebuilding the roster for the matchup.
- The run economy and shop should allow both high-rolls and low-rolls. Rare early offers can define a build, but the player must also be able to adapt when shops do not support the intended plan.
- An endless mode is a possible later addition, but normal-run content must not rely on endless mode to provide enough time for late-game artifacts to matter.

## Roster and Team Rules

- The player's **Roster** consists of every owned unit on the field or bench.
- There is no separate player-facing Unit Bag or draw step for owned units.
- Buying a unit moves it directly from the shop to the bench.
- The fielded team and bench persist between rounds.
- Team size scales through a TFT-style level system.
- The bench starts with approximately ten slots; the exact number requires testing.
- Bench capacity can be increased through artifacts, voucher-like upgrades, or unit traits.
- A negative-like modifier may allow specific units to bypass normal bench-capacity limits.

## Difficulty
- There will be a difficulty system similar to Balatro's stakes.
- Difficulty should primarily introduce systemic rules and constraints rather than rely on numerical stat increases or longer runs.
- Exactly what this means is an open design question.

## Units

### Stars

- Units range from 1-star to 5-star. **Three matching units combine into the next star level** at every tier: a 4-star represents 27 base copies and a 5-star represents 81.
- Four-star units should be achievable in exceptional normal strategies. Five-star units are deliberately extreme, run-defining outcomes enabled by heavy investment, pool modification, copy generation, favourable rerolling, and luck.
- Natural supply need not support five stars by itself, but created or added copies count toward upgrades. Ordinary encounters never require a 5-star unit, and their power should reward the extraordinary commitment needed to make one.

### Cost

- Unit costs range from 1 to 10 gold, although the vast majority live in the 1–5 or 1–6 range. More expensive units should be very rare, unlockable, transformed, or require particular mechanics or artifacts.

### Traits and Synergies

- Traits use a mostly standard TFT-style breakpoint system, such as 2/4/6 thresholds, but remain flexible.
- Traits may interact with artifacts, shop odds and guarantees, team-size rules, bench capacity, and other systems.

### Items

- Items are persistent TFT-style equipment assigned to units.
- Units initially have two item slots, and items are acquired complete rather than assembled from components.
- Items can be freely removed or transferred during Shop and Planning. If combining units would exceed the surviving unit's capacity, excess items enter a temporary tray that must be resolved before combat.
- Items may appear directly in Curio slots, inside packs, or as encounter rewards.

## Artifacts

Artifacts are the equivalent of Balatro's Jokers. They are intended to enable multiplicative scaling, unusual combinations, economy strategies, and rule-breaking builds.

Artifacts may affect:

- Gold and economy
- Unit strength and combat calculations
- Shop composition, odds, rerolls, or prices
- Traits and synergy thresholds
- Team or bench capacity
- Artifact capacity
- Packs, items, consumables, or voucher equivalents
- Other game rules

Artifacts have rarity or power classifications that influence their shop odds, prices, complexity, and potential ceiling. Level and stage should improve the odds of higher-rarity artifacts, but all normal rarities remain technically obtainable early at appropriately small probabilities.

The player starts with five artifact slots. Artifact capacity can grow through artifacts, voucher-like upgrades, or unit traits. A negative-like modifier may allow individual artifacts to bypass normal capacity limits.

## Consumables

- The exact consumable system is unresolved.
- The game should include some form of one-use potions, tactics, shop manipulation, scouting, and/or temporary unit bonuses.
- Consumables may drop from enemies, appear in Curio slots or packs, or be generated by artifacts and other effects.
- Targeted combat tactics, such as molotovs, healing areas, walls, or temporary structures, are placed or configured during Planning and resolve automatically during combat. They may be one-use or recurring depending on their source.
- The player does not directly aim abilities or use consumables during combat.

## Fog of War

- Broad encounter intent may be visible during the Shop Phase, but no exact enemy composition, formation, or fogged board is shown.
- Fog of war applies only after **Finish Shopping**, when the Planning Phase begins, and initially hides some enemy layout and composition information.
- Scouting and reveal effects may expose information during Planning, when commerce is already closed. Possible sources include artifacts, level bonuses, items, unit abilities, and voucher equivalents; the exact baseline information and reveal rules remain unresolved.

## Combat Feedback

- Combat results should include a clear recap attributing damage, healing, shielding, ability use, and relevant artifact or trait contributions.
- The exact recap presentation and whether it includes replay or timeline tools remain unresolved.

## Art Direction

The intended style is cartoony and polished, strongly inspired by Balatro's UI and the aesthetics of Dota Underlords.

## Open Design Questions

Resolve these questions before implementing their respective systems beyond basic prototypes.

### Shop Tuning

- What is the base unified reroll price, and should any effects or late-game rules alter it?
- What are the exact Curio category frequencies and rarity curves at each level and stage?
- How loose should the market-power budget be, and what protections prevent pathological shops without removing exciting high-rolls and low-rolls?
- What happens to each purchased shop slot on a subsequent reroll during the same visit?
- How are unique-offer repetition protections implemented and communicated?
- How should exact Recruitment Pool counts, generated copies, and temporarily reserved copies be presented without cluttering the normal shop?
- Do enemies, rivals, or encounters ever remove units from the player's internal shop pool?

### Packs

- What pack categories exist, how many choices do they reveal, and how many rewards may be taken?
- Do packs use the player's level when offered, when opened, or a special pack-specific rarity table?
- How do unit packs differ sufficiently from buying direct units in the five Unit slots?
- What happens when a pack grants a unit while the bench is full?

### Tarot/Planet Equivalents

- What are the names and exact identities of the tarot and planet equivalents?
- Are tarot equivalents temporary combat effects, permanent unit modifications, shop manipulation, scouting, Recruitment Pool manipulation, transformations, or a mixture?
- Do planet equivalents improve traits, roles, unit categories, abilities, or another form of lasting run progression?
- Which effects appear directly in Curio slots and which primarily appear through packs or rewards?
- Which system owns expensive pool-editing effects such as adding or removing copies, filtering offers, or causing an upgraded unit to appear, and how rare should those effects be?
- How should pool-modification costs scale so that modifying supply is competitive with—but does not dominate—levelling and rerolling?

### Voucher Equivalents

- What are they called, how often can they be purchased, and when does their slot reopen?
- Are they tied to chapters, bosses, player level, or another progression system?
- How powerful can shop-size, reroll, capacity, interest, and scouting upgrades become?

### Items

- Can items be sold, dismantled, or transformed, and under what restrictions?

### Encounter Information

- What broad encounter intent is shown during the Shop Phase?
- What enemy information is visible by default during Planning, and what requires scouting?

### Run Structure

- Does the initial three-chapter, fifteen-encounter pacing hypothesis produce the right run arc?
- Is progression linear, branching, or based on encounter choices?
- What is the role and unlock condition of a possible endless mode?

### Difficulty

- Which systemic rules, enemy behaviours, and economy constraints should cumulative difficulties introduce?
- How many difficulties should there be?

### Time Modulation

- Can battles be paused, slowed down, or sped up?
- Are these standard interface and accessibility features, or unlockable effects?

### Permanent Unlocks

- Should units, artifacts, voucher equivalents, packs, and other systems unlock through challenges?
- Should permanent progression remain horizontal, or include permanent perks such as additional fog-of-war information?
- Which accessibility and pacing features must always be available rather than unlockable?

## Confirmed Design Decisions

- The persistent **Roster** contains fielded and benched units; purchases go to the bench, with no player-owned Unit Bag or roster draw.
- A finite, player-visible Recruitment Pool supplies units. Its detailed view shows total, owned, offered, and remaining copies, plus locked units and their challenges.
- Carefully controlled pool modification is an expensive alternative to XP and reroll spending. Unlock dilution is a future risk, but no mitigation system is currently planned.
- One large unified shop and one flat-cost reroll initially serve five Unit, three Curio, two Pack, and one Voucher-equivalent slot alongside XP and economy controls.
- Shop and Planning are separate phases. Broad encounter intent may be visible while shopping, but exact enemy information is not; **Finish Shopping** permanently closes commerce for that encounter before actionable board information is revealed.
- During Planning, the player may reposition, swap field and bench units, and transfer equipment, but cannot buy, sell, reroll, lock, or level.
- Level governs team size and unit-cost odds; level and stage improve curio rarity probabilities without absolute rarity gates.
- Legendary early offers remain possible, while soft composition guardrails preserve meaningful high-rolls and low-rolls.
- Three matching units combine into the next star level through 5-star; 5-star units are exceptional copy-economy outcomes rather than expected progression.
- Traits use mostly standard TFT-style breakpoints, and team size uses a TFT-style level system.
- Enemy encounters are handcrafted with added randomness.
- Bosses centre on a prominent, clearly communicated gameplay rule.
- Losing combat costs health based on surviving enemy combat power or something similar.
- The bench starts near ten slots and artifact capacity at five; both can be expanded or bypassed through special effects.
- Units do not have a separate EXP or hero-levelling progression system.
- Units initially have two slots for complete items, which may be freely transferred outside combat.
- Combat has no realtime player-controlled abilities; targeted tactics are configured during Planning and resolve automatically.
- Combat results include a clear performance recap.
