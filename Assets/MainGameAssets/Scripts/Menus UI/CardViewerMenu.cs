using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardViewerMenu : BasicMenu
{

    public Transform cardHolderParent;
    public Text flavourText;
    public GameObject[] arrowObjects;
    public void ShowCard(Card card)
    {
        ShowMenu();

        
        var cardHolders = cardHolderParent.GetComponentsInChildren<UpgradeCardHolder>(true);
        var rootCard = card.ancestorCard;

        RenderCard(cardHolders[0], rootCard,card);
        int arrowIndex = 0;
        int cardIndex = 1;
        //var cards = new List<Card>() { rootCard };
        while (rootCard.upgradeCards.Count>0)
        {
            arrowObjects[arrowIndex] .SetActive(true);
            arrowIndex++;
            foreach (var upgradeCard in rootCard.upgradeCards)
            {
                RenderCard(cardHolders[cardIndex], upgradeCard,card);
                cardIndex++;
            }

            rootCard = rootCard.upgradeCards[0];
        }

        for (; cardIndex < cardHolders.Length; cardIndex++)
        {
            cardHolders[cardIndex].gameObject.SetActive(false);
        }
        for (; arrowIndex < arrowObjects.Length; arrowIndex++)
        {
            arrowObjects[arrowIndex].gameObject.SetActive(false);
        }

        

        flavourText.text = MenuControl.Instance.GetLocalizedString(card.UniqueID + "FlavourText","");

        MenuControl.Instance.infoMenu.HideMenu();
    }

    void RenderCard(UpgradeCardHolder cardHolder,  Card card,Card currentCard)
    {
        
        cardHolder.visibleCard.RenderCardForMenu(card);
        cardHolder.visibleCard.showCardView = false;
        if (!card.IsDiscovered())
        {
            cardHolder.visibleCard.RenderCardBackOnly(null);
        }
        cardHolder.gameObject.SetActive(true);
        cardHolder.currentObj.SetActive(card.UniqueID == currentCard.UniqueID);
    }

    //public override void CloseMenu()
    //{
    //    base.CloseMenu();
    //    MenuControl.Instance.infoMenu.HideMenu();
    //}

    public override void SelectVisibleCard(VisibleCard vc, bool withClick = true)
    {
        base.SelectVisibleCard(vc, withClick);
        if (GetComponent<Doozy.Engine.UI.UIView>().IsHiding) return;
        if (vc.card != null)
            MenuControl.Instance.infoMenu.ShowInfo(vc,false);
    }

    public override void DeSelectVisibleCard(VisibleCard vc, bool withClick = true)
    {
        base.DeSelectVisibleCard(vc, withClick);
        MenuControl.Instance.infoMenu.HideMenu();
    }
}
