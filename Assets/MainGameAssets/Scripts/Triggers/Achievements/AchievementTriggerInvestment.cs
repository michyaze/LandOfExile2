using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementTriggerInvestment : TriggerAchievement
{
    public int minThreeStarCards = 5;
    public override void GameStarted()
    {
        int count = 0;
        foreach (Card card in MenuControl.Instance.heroMenu.cardsOwned)
        {
            if (card.level == 3)
            {
                count += 1;
            }
        }

        if (count >= minThreeStarCards)
        {
            MarkAchievementCompleted();
        }
    }
}
