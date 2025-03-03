using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementTriggerWhatIsCounterPlay : TriggerAchievement
{
    public override void GameEnded(bool victory)
    {
        if (victory)
        {
            if (MenuControl.Instance.battleMenu.currentRound == 1 && !MenuControl.Instance.battleMenu.tutorialMode)
            {
                MarkAchievementCompleted();
            }
        }
    }
}
