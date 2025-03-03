using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCursedFoes : Trigger
{
    public override void GameStarted()
    {
        if (MenuControl.Instance.adventureMenu.GetCurrentAdventureItemEncounter() != null && !MenuControl.Instance.adventureMenu.GetCurrentAdventureItemEncounter().isBoss)
        {
            int index = MenuControl.Instance.heroMenu.hero.startingEffects.IndexOf(GetEffect().originalTemplate);
            if (index > -1)
            {
                MenuControl.Instance.heroMenu.hero.startingEffectCharges[index] -= 1;
                if (MenuControl.Instance.heroMenu.hero.startingEffectCharges[index] == 0)
                {
                    MenuControl.Instance.heroMenu.hero.startingEffects.RemoveAt(index);
                    MenuControl.Instance.heroMenu.hero.startingEffectCharges.RemoveAt(index);
                    //MenuControl.Instance.dataControl.SaveData();
                }


                Hero enemyHero = GetCard().player.GetOpponent().GetHero();
                enemyHero.ChangeCurrentHP(this, Mathf.CeilToInt(enemyHero.currentHP / 2f));

                GetEffect().ConsumeCharges(this, 1);

            }
        }
    }
}
