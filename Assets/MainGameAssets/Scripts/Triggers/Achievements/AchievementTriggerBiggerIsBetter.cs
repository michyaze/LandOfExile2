using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementTriggerBiggerIsBetter : TriggerAchievement
{

    public int deckSizeMin = 30;

    public override void GameEnded(bool victory)
    {
        if (victory)
        {
            AdventureItemEncounter encounter = MenuControl.Instance.adventureMenu.GetCurrentAdventureItemEncounter();
            if (encounter != null && encounter.isBoss && MenuControl.Instance.areaMenu.areasVisited == 3)
            {

                if (MenuControl.Instance.heroMenu.cardsOwned.Count >= deckSizeMin + 1)
                {
                    MarkAchievementCompleted();

                }
            }

        }
    }
}
