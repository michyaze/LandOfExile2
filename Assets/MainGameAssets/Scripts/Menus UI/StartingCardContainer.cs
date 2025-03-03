using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartingCardContainer : MonoBehaviour
{
    public GameObject lockedImage;
    //public GameObject unlockedImage;
    public Transform cardHolder;
    public Button unlockButton;
    public VisibleCard vc;
    public Card card;
    public Image achievementImage;
    public GameObject achievementBG;

    public void RenderCard(Card card)
    {
        this.card = card;

        vc = Instantiate(MenuControl.Instance.visibleCardPrefab, cardHolder);
        vc.RenderCardForMenu(card);
        vc.transform.localScale = Vector3.one * 1f;

        bool locked = (MenuControl.Instance.heroMenu.unlockableCards.Contains(card) && !MenuControl.Instance.heroMenu.startingCardsUnlocked.Contains(card)) ||
            (card is Artifact && !MenuControl.Instance.heroMenu.artifactsUnlocked.Contains(card));
        lockedImage.SetActive(locked);
        unlockButton.gameObject.SetActive(locked);
        if (!locked)
        {
            vc.HighlightGreen();
        } else
        {
            vc.HighlightRed();
        }

        //vc.GetComponent<CanvasGroup>().alpha = !MenuControl.Instance.heroMenu.startingCardsUnlocked.Contains(card) ? 0.5f : 1f;

        //unlockButton.interactable = MenuControl.Instance.heroMenu.accumulatedGold >= MenuControl.Instance.heroMenu.StartingCardCost(card);
        unlockButton.GetComponentInChildren<Text>().text = MenuControl.Instance.heroMenu.StartingCardCost(card).ToString();

        Achievement achievement = MenuControl.Instance.heroMenu.StartingCardAchievement(card);
        achievementBG.SetActive(achievement != null && !MenuControl.Instance.achievementsMenu.HasCompletedAchievement(achievement));
        if (achievement != null)
        {
            achievementImage.sprite = achievement.GetSprite();
        }
    }

    public void UnlockStartingCard()
    {
        // Achievement achievement = MenuControl.Instance.heroMenu.StartingCardAchievement(card);
        // if (achievement != null && !MenuControl.Instance.achievementsMenu.HasCompletedAchievement(achievement))
        // {
        //     MenuControl.Instance.ShowNotification(null, MenuControl.Instance.GetLocalizedString("MustUnlockAchievementPrompt"), MenuControl.Instance.GetLocalizedString("MustUnlockAchievementText") + "\n\n<color=white>" + achievement.GetName() + "</color>\n" + achievement.GetDescription(), true, true, true);
        // }
        // else if (MenuControl.Instance.heroMenu.accumulatedGold >= MenuControl.Instance.heroMenu.StartingCardCost(card))
        // {
        //
        //     List<Card> cards = new List<Card>();
        //     cards.Add(card);
        //
        //     List<string> buttonLabels = new List<string>();
        //     buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Cancel"));
        //     buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Confirm"));
        //
        //     List<System.Action> actions = new List<System.Action>();
        //     actions.Add(() => { });
        //     actions.Add(() =>
        //     {
        //         MenuControl.Instance.heroMenu.PurchaseStartingCard(card);
        //     });
        //
        //     MenuControl.Instance.cardChoiceMenu.ShowChoice(cards, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("PurchaseStartingCardPrompt"), 0, 0, true, true, false);
        //
        // }
        // else
        // {
        //     MenuControl.Instance.ShowNotification(null, MenuControl.Instance.GetLocalizedString("NotEnoughGoldPrompt"), MenuControl.Instance.GetLocalizedString("NotEnoughGoldText"), true, true, true);
        // }

    }
}
