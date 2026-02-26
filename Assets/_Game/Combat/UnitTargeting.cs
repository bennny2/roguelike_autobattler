using System.Collections.Generic;
using System.Linq;

public class UnitTargeting
{
    public UnitModel SelectTarget(UnitModel attacker, List<UnitModel> potentialTargets)
    {
        // Simple targeting logic: select the first alive target
        return potentialTargets.FirstOrDefault(t => t.Health > 0);
    }
}