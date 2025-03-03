using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementPanel : MonoBehaviour
{

    public Image icon;
    public Text titleText;
    public Text descriptionText;
    public GameObject completedIcon;
    public Slider progressSlider;
    public Text progressText;
    //public Text goldText;
    public GameObject cardIcon;
    public Achievement achievementToShow;
    public Color finishedAchievementTitleColor;
    public Color unfinishedAchievementTitleColor;

    public void RenderAchievement(Achievement achievementToShow)
    {
        this.achievementToShow = achievementToShow;
        icon.sprite =  achievementToShow.GetSprite();
        titleText.text = achievementToShow.GetName();
        titleText.color = achievementToShow.IsCompleted()
            ? finishedAchievementTitleColor
            : unfinishedAchievementTitleColor;

        if (I2.Loc.LocalizationManager.CurrentLanguage != MenuControl.Languages.English.ToString())
        {
            titleText.font = MenuControl.Instance.GetSafeFont();
        }

        descriptionText.text = achievementToShow.GetDescription();

        completedIcon.SetActive(achievementToShow.IsCompleted());
        
        

        progressSlider.value = achievementToShow.EvaluateAchievementProgress()[0] / achievementToShow.EvaluateAchievementProgress()[1];
        progressText.text = achievementToShow.EvaluateAchievementProgress()[0] + "/" + achievementToShow.EvaluateAchievementProgress()[1];

        //goldText.text = achievementToShow.goldReward.ToString();
        //goldText.transform.parent.gameObject.SetActive(achievementToShow.goldReward > 0);

        //cardIcon.SetActive(achievementToShow.cardReward != null);
        cardIcon.SetActive(false);

        // if (achievementToShow.cardReward != null && achievementToShow.IsCompleted())
        // {
        //     VisibleCard vc = Instantiate(MenuControl.Instance.visibleCardPrefab, cardIcon.transform);
        //     vc.RenderCardForMenu(achievementToShow.cardReward);
        //     vc.transform.localScale = Vector3.one * 0.4f;
        // }


    }


}
