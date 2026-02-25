using System;
using System.Collections.Generic;

public class RunModel
{
    public Guid Id { get; set; }
    public int PlayerHealth { get; set; }
    public int PlayerMoney { get; set; }
    public List<UnitModel> PlayerUnits { get; set; }

    public RunModel(){}

    public AddUnitToTeamResult AddUnitToTeam(UnitModel unit)
    {
        PlayerUnits ??= new List<UnitModel>();
        
        if (PlayerUnits.Count >= 5)
        {
            return AddUnitToTeamResult.TeamFull;
        }
        
        PlayerUnits.Add(unit);
        return AddUnitToTeamResult.Success;
    }
        
}