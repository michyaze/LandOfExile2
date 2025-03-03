using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementEvaluation : MonoBehaviour
{
    public virtual bool EvaluateAchievementCompleted()
    {
        return false;
    }

    public virtual bool EvaluateAchievementCompleted(AdventureItemEncounter encounter)
    {
        return false;
    }

    public virtual List<int> EvaluateAchievementProgress()
    {
        int requiredCount = 1;
        int completedCount = MenuControl.Instance.achievementsMenu.achievementStringsCompleted.Contains(GetComponent<Achievement>().UniqueID) ? 1 : 0;

        List<int> integers = new List<int>();
        integers.Add(completedCount);
        integers.Add(requiredCount);

        return integers;
    }
}
