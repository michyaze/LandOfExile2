using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementEvaluationReachAscensionLevel : AchievementEvaluation
{

    public int level = 10;

    public override bool EvaluateAchievementCompleted()
    {
        var classId = MenuControl.Instance.heroMenu.heroClasses.IndexOf(MenuControl.Instance.heroMenu.heroClass);
        if (classId >= MenuControl.Instance.heroMenu.ascensionUnlocks.Count)
        {
            return true;
        }
        return MenuControl.Instance.heroMenu.ascensionUnlocks[classId] >= level;
    }

}
