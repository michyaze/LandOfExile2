using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.UI;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsMenu : BasicMenu {

    public List<Achievement> achievements = new List<Achievement>();
    public List<int> achievementProgressCount = new List<int>();
    public List<string> achievementStringsCompleted = new List<string>();
    public AchievementPanel achievementPanelPrefab;
    public Transform grid;

    public override void ShowMenu()
    {
        base.ShowMenu();

        foreach (Transform child in grid)
        {
            Destroy(child.gameObject);
        }

        foreach (Achievement achievement in achievements)
        {
            if (!achievement.isHidden || achievement.IsCompleted())
            {
                AchievementPanel label = Instantiate(achievementPanelPrefab, grid);
                label.RenderAchievement(achievement);
            }
        }


    }

    public bool HasCompletedAchievement(Achievement achievement)
    {
        return achievementStringsCompleted.Contains(achievement.UniqueID);
    }

    public void CheckAchievements()
    {

        foreach (Achievement achievement in achievements)
        {
            if (!achievementStringsCompleted.Contains(achievement.UniqueID) && achievement.EvaluateAchievementCompleted())
            {
                achievement.Complete();
            }
        }
    }



    public void CheckAchievements(AdventureItemEncounter encounter)
    {
        foreach (Achievement achievement in achievements)
        {
            if (!achievementStringsCompleted.Contains(achievement.UniqueID) && achievement.EvaluateAchievementCompleted(encounter))
            {
                achievement.Complete();
            }
        }
    }


    public void ShowPopupAchievementCompleted(Achievement achievement)
    {

        UIPopup popup = UIPopup.GetPopup("AchivementCompleted");
        if (popup == null) return;

        popup.Data.Images[0].sprite = achievement.GetSprite();
        popup.Data.SetLabelsTexts(MenuControl.Instance.GetLocalizedString("Achievement Completed"),"<color=white>" + achievement.GetName() + "</color>",  achievement.GetDescription());

        // if (I2.Loc.LocalizationManager.CurrentLanguage != MenuControl.Languages.English.ToString())
        // {
        //     popup.Data.Labels[0].GetComponent<Text>().font = MenuControl.Instance.GetSafeFont();
        // }
        // if (I2.Loc.LocalizationManager.CurrentLanguage == MenuControl.Languages.Chinese.ToString() || I2.Loc.LocalizationManager.CurrentLanguage == MenuControl.Languages.ChineseTraditional.ToString() || I2.Loc.LocalizationManager.CurrentLanguage == MenuControl.Languages.Japanese.ToString())
        // {
        //     popup.Data.Labels[1].GetComponent<Text>().resizeTextForBestFit = false;
        // }

        UIPopupManager.AddToQueue(popup);
    }

    private void Update()
    {
        if (MenuControl.Instance.testMode && Input.GetKeyDown(KeyCode.K))
        {
            ShowPopupAchievementCompleted(GetAchievementByID("Achievement03"));
        }
    }

    public Achievement GetAchievementByID(string uniqueID)
    {
        foreach (Achievement achievement in achievements)
        {
            if (achievement.UniqueID == uniqueID) return achievement;
        }

        return null;
      
    }

    public override void SelectVisibleCard(VisibleCard vc, bool withClick = true)
    {

        MenuControl.Instance.infoMenu.ShowInfo(vc);
    }

    public override void DeSelectVisibleCard(VisibleCard vc, bool withClick = true)
    {
        MenuControl.Instance.infoMenu.HideMenu();
    }

    public void ShowMyAchievementsThisRun()
    {
        if (UIPopupManager.CurrentVisibleQueuePopup == null)
        {
            foreach (string achievementString in MenuControl.Instance.heroMenu.achievementStringsCompletedThisRun)
            {
                Achievement achievement = GetAchievementByID(achievementString);
                if (achievement)
                    ShowPopupAchievementCompleted(achievement);
            }
        }
    }
}
