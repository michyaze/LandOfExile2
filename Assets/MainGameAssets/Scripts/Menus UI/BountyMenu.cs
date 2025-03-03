using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BountyMenu : BasicMenu
{

    public List<Achievement> achievementsToShow = new List<Achievement>();
    public Transform grid;

    public void ShowNewBounties()
    {
        achievementsToShow.Clear();
        for (int ii = 0; ii < 4; ii += 1)
        {
            foreach (Achievement achievement in MenuControl.Instance.achievementsMenu.achievements)
            {
                // if (achievement.goldReward > 0)
                // {
                //     if (!achievement.IsCompleted())
                //     {
                //         if (!achievementsToShow.Contains(achievement))
                //         {
                //             achievementsToShow.Add(achievement);
                //             break;
                //         }
                //     }
                // }
            }
        }

        for (int ii = 0; ii < 2; ii += 1)
        {
            foreach (Achievement achievement in MenuControl.Instance.achievementsMenu.achievements)
            {
                if (achievement.cardReward != null)
                {
                    if (!achievement.IsCompleted())
                    {
                        if (!achievementsToShow.Contains(achievement))
                        {
                            achievementsToShow.Add(achievement);
                            break;
                        }
                    }
                }
            }
        }

        ShowMenu();

    }

    public void ShowBountiesCollected()
    {
        achievementsToShow.Clear();
        foreach (string uniqueID in MenuControl.Instance.heroMenu.achievementStringsCompletedThisRun)
        {
            Achievement achievement = MenuControl.Instance.achievementsMenu.GetAchievementByID(uniqueID);
            if (achievement != null)
            {
                achievementsToShow.Add(achievement);
            }
        }
        ShowMenu();

    }

    public override void ShowMenu()
    {
        base.ShowMenu();

        foreach (Transform child in grid)
        {
            Destroy(child.gameObject);
        }

        foreach (Achievement achievement in achievementsToShow)
        {
            AchievementPanel label = Instantiate(MenuControl.Instance.achievementsMenu.achievementPanelPrefab, grid);
            label.RenderAchievement(achievement);
        }
    }
}
