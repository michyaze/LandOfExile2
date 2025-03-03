using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetOtherUnitsWithTag : Ability
{
    public Ability otherAbility;
    public CardTag cardTag;
    public bool enemyUnits;
    public bool friendlyUnits;
    public bool minionsOnly;
    public int upToMaxCount;
    public bool notThisUnit;
    public TargetOtherUnitType targetOtherUnitType;
public enum TargetOtherUnitType {None, DamagedUnit}
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        int count = 0;
        var list = MenuControl.Instance.battleMenu.GetAllUnitsOnBoard();
        list.Shuffle();
        foreach (Unit unit in list)
        {
            if (unit != null)
            {
                if (cardTag == null || (unit.cardTags!=null && unit.cardTags.Contains(cardTag)))
                {
                    if ((enemyUnits && unit.player == GetCard().player.GetOpponent()) || (friendlyUnits && unit.player == GetCard().player))
                    {
                        if (!minionsOnly || (minionsOnly && unit is Minion))
                        {
                            if (!notThisUnit || unit != GetCard())
                            {
                                if (upToMaxCount > 0 && count >= upToMaxCount) return;

                                switch (targetOtherUnitType)
                                {
                                    case TargetOtherUnitType.DamagedUnit:
                                        if (unit.currentHP >= unit.GetInitialHP())
                                        {
                                            continue;
                                        }
                                        break;
                                }
                                
                                otherAbility.PerformAbility(sourceCard, unit.GetTile(), amount);
                                count += 1;
                            }
                        }
                    }
                        

                }
            }

        }
    }

}
