using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement : CollectibleItem
{
    //public int goldReward;
    public Card cardReward;
    public bool isHidden;

    public bool EvaluateAchievementCompleted()
    {
        if (GetComponent<AchievementEvaluation>() != null)
            return GetComponent<AchievementEvaluation>().EvaluateAchievementCompleted();

        return false;
    }

    public override Sprite GetSprite()
    {
        return MenuControl.Instance.csvLoader.achievementSprite(GetChineseName());
    }

    public bool EvaluateAchievementCompleted(AdventureItemEncounter encounter)
    {
        if (GetComponent<AchievementEvaluation>() != null)
            return GetComponent<AchievementEvaluation>().EvaluateAchievementCompleted(encounter);

        return false;
    }

    public List<int> EvaluateAchievementProgress()
    {
        if (GetComponent<AchievementEvaluation>() != null)
            return GetComponent<AchievementEvaluation>().EvaluateAchievementProgress();

        if (IsCompleted())
        {
            List<int> ints = new List<int>();
            ints.Add(1);
            ints.Add(1);
            return ints;
        }

        List<int> ints2 = new List<int>();
        ints2.Add(0);
        ints2.Add(1);
        return ints2;
    }

    public bool IsCompleted()
    {
        return MenuControl.Instance.achievementsMenu.achievementStringsCompleted.Contains(UniqueID);
    }

    public void Complete()
    {
        MenuControl.Instance.achievementsMenu.achievementStringsCompleted.Add(UniqueID);
        MenuControl.Instance.heroMenu.achievementStringsCompletedThisRun.Add(UniqueID);

        // if (goldReward > 0)
        // {
        //     int goldToGive = goldReward;
        //     //MenuControl.Instance.heroMenu.accumulatedGold += goldToGive;
        // }
        if (!MenuControl.Instance.battleMenu.inBattle)
        {
            MenuControl.Instance.dataControl.SaveData();
        }

        MenuControl.Instance.achievementsMenu.ShowPopupAchievementCompleted(this);

        MenuControl.Instance.steamLogic.SetAchievementComplete(this);
    }
}