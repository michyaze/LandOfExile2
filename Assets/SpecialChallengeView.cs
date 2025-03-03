using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialChallengeView : MonoBehaviour
{
    public Image rewardImage;
    public Text rewardCount;
    public VisibleCard card;
    public Text rewardDesc;
    public Text rewardTitle;

    public void Show()
    {
        
        gameObject.SetActive(true);
        card.isEnemy = true;
        card.RenderCard(MenuControl.Instance.eventMenu. specialChallengeVcCard.card);
        rewardImage.sprite = MenuControl.Instance.eventMenu.specialChallengeRewardImage.sprite;
        rewardCount.text = MenuControl.Instance.eventMenu.specialChallengeRewardCountText.text;
        rewardDesc.text = MenuControl.Instance.eventMenu.specialChallengeRewardDesc.text;
        rewardTitle.text = MenuControl.Instance.eventMenu.specialChallengeRewardText.text;

    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    
}
