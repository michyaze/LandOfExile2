using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoAbilityMatchingTag : Ability
{
    
    public CardTag cardTag;
    public bool enemyUnits;
    public bool friendlyUnits;
    public Ability otherAbility;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        base.PerformAbility(sourceCard, targetTile, amount);
        foreach (Unit unit in MenuControl.Instance.battleMenu.GetAllUnitsOnBoard())
        {
            if (unit != null)
            {
                if (cardTag == null || unit.cardTags.Contains(cardTag))
                {
                    if ((enemyUnits && unit.player == GetCard().player.GetOpponent()) ||
                        (friendlyUnits && unit.player == GetCard().player))
                    {
                        if (GetCard() != null && GetCard().GetZone() == MenuControl.Instance.battleMenu.board)
                        {
                            otherAbility?.PerformAbility(sourceCard,unit.GetTile(),amount);
                        }
                    }
                }
            }
        }
    }
}
