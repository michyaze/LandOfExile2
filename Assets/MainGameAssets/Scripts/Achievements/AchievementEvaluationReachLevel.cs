using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementEvaluationReachLevel : AchievementEvaluation
{

    public int level = 15;

    public override bool EvaluateAchievementCompleted()
    {
        return MenuControl.Instance.heroMenu.currentLevel >= level;
    }

}
