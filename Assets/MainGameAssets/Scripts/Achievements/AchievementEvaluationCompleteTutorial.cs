using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementEvaluationCompleteTutorial : AchievementEvaluation
{
    public override bool EvaluateAchievementCompleted()
    {
        return MenuControl.Instance.tutorialFinished;
    }

    public override List<int> EvaluateAchievementProgress()
    {
        int requiredCount = 1;
        int completedCount = MenuControl.Instance.tutorialFinished ? 1 : 0;

        List<int> integers = new List<int>
        {
            completedCount,
            requiredCount
        };

        return integers;
    }
}
