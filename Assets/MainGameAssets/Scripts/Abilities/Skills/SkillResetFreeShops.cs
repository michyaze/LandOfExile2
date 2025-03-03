using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillResetFreeShops : Ability
{

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        // int reduction = MenuControl.Instance.CountOfCardsInList(MenuControl.Instance.levelUpMenu.extraFreeBarterTalent, MenuControl.Instance.levelUpMenu.variableTalentsAcquired);
        //
        // MenuControl.Instance.shopMenu.removals = Mathf.Min(reduction, MenuControl.Instance.shopMenu.removals);
        // MenuControl.Instance.shopMenu.upgrades = Mathf.Min(reduction, MenuControl.Instance.shopMenu.upgrades);
        // MenuControl.Instance.shopMenu.purchases = Mathf.Min(reduction, MenuControl.Instance.shopMenu.purchases);
    }
}
