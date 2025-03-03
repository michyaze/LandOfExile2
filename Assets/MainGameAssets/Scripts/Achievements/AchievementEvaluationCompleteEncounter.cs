using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementEvaluationCompleteEncounter : AchievementEvaluation
{
    public int requiredCount = 1;
    public HeroClass requiredClass;
    public HeroPath requiredPath;
    public int level;
    public bool isBoss;

    public override bool EvaluateAchievementCompleted(AdventureItemEncounter encounter)
    {
//Debug.Log($"evaluating  {encounter.GetName()} level {level} {encounter.level} isBoss {isBoss} {encounter.isBoss} path {requiredPath != null && MenuControl.Instance.heroMenu.heroPath != requiredPath} class {requiredClass != null && MenuControl.Instance.heroMenu.heroClass != requiredClass} completeCount {MenuControl.Instance.achievementsMenu.achievementProgressCount[MenuControl.Instance.achievementsMenu.achievements.IndexOf(GetComponent<Achievement>())]}");
        if (level > 0 && encounter.level != level) return false;
        if (isBoss != encounter.isBoss) return false;
        if (requiredPath != null && MenuControl.Instance.heroMenu.heroPath != requiredPath) return false;
        if (requiredClass != null && MenuControl.Instance.heroMenu.heroClass != requiredClass) return false;

        var achievementIndex = MenuControl.Instance.achievementsMenu.achievements.IndexOf(GetComponent<Achievement>());
        if (achievementIndex == -1)
        {
            Debug.LogError("why achievementProgressCount is wrong ");
            return false;
        }
        if(
            achievementIndex >= MenuControl.Instance.achievementsMenu.achievementProgressCount.Count)
        {
            for (int i = MenuControl.Instance.achievementsMenu.achievementProgressCount.Count; i <= achievementIndex; i++)
            {
                MenuControl.Instance.achievementsMenu.achievementProgressCount.Add(0);
            }
        }
        {

        MenuControl.Instance.achievementsMenu.achievementProgressCount[MenuControl.Instance.achievementsMenu.achievements.IndexOf(GetComponent<Achievement>())] += 1;
            int completedCount = MenuControl.Instance.achievementsMenu.achievementProgressCount[MenuControl.Instance.achievementsMenu.achievements.IndexOf(GetComponent<Achievement>())];
            
            
            return completedCount >= requiredCount;
        }
        

    }

}
