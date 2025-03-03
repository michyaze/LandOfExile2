using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementTriggerSlimTrim : TriggerAchievement
{

    public int deckSizeMax = 6;

    public override void GameEnded(bool victory)
    {
        if (victory)
        {
            AdventureItemEncounter encounter = MenuControl.Instance.adventureMenu.GetCurrentAdventureItemEncounter();
            if (encounter != null && encounter.isBoss && MenuControl.Instance.areaMenu.areasVisited == 3)
            {

                if (MenuControl.Instance.heroMenu.cardsOwned.Count <= deckSizeMax + 1)
                {
                    MarkAchievementCompleted();

                }
            }

        }
    }
}
