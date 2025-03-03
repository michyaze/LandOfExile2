using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementEvaluationCompleteAscensionLevel : AchievementEvaluation
{

    public int level = 15;

    public override bool EvaluateAchievementCompleted()
    {
        return MenuControl.Instance.heroMenu.ascensionMode >= level && MenuControl.Instance.heroMenu.isAlive == false;
    }

}
