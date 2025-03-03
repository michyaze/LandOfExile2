using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardHistoryItem : MonoBehaviour, /*IPointerDownHandler, IPointerUpHandler,*/ IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    public Card cardToShow;
    public bool wasDiscarded;
    public bool wasRemoved;

    public Image bgImage;
    
    
    public Image cardImage;
    public Image spellCardImage;

    public Text tooltipText;

    public void RenderCardHistoryItem(Card card, bool discarded, bool removed)
    {
        tooltipText.gameObject.SetActive(false);
        cardToShow = card;
        wasDiscarded = discarded;
        wasRemoved = removed;

        cardImage.gameObject.SetActive(false);
        spellCardImage.gameObject.SetActive(false);
        if (card is Unit || card is NewWeapon)
        {
            cardImage.gameObject.SetActive(true);
            cardImage.sprite = MenuControl.Instance.csvLoader.playerCardSprite(  cardToShow);
        }
        else
        {
            
            spellCardImage.gameObject.SetActive(true);
            spellCardImage.sprite = MenuControl.Instance.csvLoader.playerCardSprite(  cardToShow);
        }
        // if (MenuControl.Instance.battleMenu.inBattle)
        //     bgImage.color = GetColorOfBorder();
        // else
        //     bgImage.color = new Color(82f / 255f, 48f / 255f, 0f);
     }

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    if (eventData.button == PointerEventData.InputButton.Middle)
    //        return;
    //    else if (eventData.button == PointerEventData.InputButton.Right)
    //        return;

    //    MenuControl.Instance.infoMenu.ShowInfo(cardToShow, transform.position);
    //}

    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    if (eventData.button == PointerEventData.InputButton.Middle)
    //        return;
    //    else if (eventData.button == PointerEventData.InputButton.Right)
    //        return;

    //    MenuControl.Instance.infoMenu.HideMenu();


    //}

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltipText.gameObject.SetActive(MenuControl.Instance.battleMenu.inBattle);
        tooltipText.text = MenuControl.Instance.GetLocalizedString("Played");
        if (wasDiscarded )
        {
            tooltipText.text = MenuControl.Instance.GetLocalizedString("Discarded");
        }
        if (wasRemoved)
        {
            tooltipText.text = MenuControl.Instance.GetLocalizedString("Removed");
        }

        if (eventData.button == PointerEventData.InputButton.Middle)
            return;
        else if (eventData.button == PointerEventData.InputButton.Right)
            return;

        //string startingText = cardToShow.player == MenuControl.Instance.battleMenu.player1 ? MenuControl.Instance.GetLocalizedString("You") : MenuControl.Instance.GetLocalizedString("Enemy");
        //startingText += " " + (wasDiscarded ? MenuControl.Instance.GetLocalizedString("discarded") : MenuControl.Instance.GetLocalizedString("played")) + ": ";

        MenuControl.Instance.infoMenu.ShowInfo(cardToShow, transform.position,false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipText.gameObject.SetActive(false);
        if (eventData.button == PointerEventData.InputButton.Middle)
            return;
        else if (eventData.button == PointerEventData.InputButton.Right)
            return;

        MenuControl.Instance.infoMenu.HideMenu();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Middle)
            return;
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            MenuControl.Instance.cardViewerMenu.ShowCard(cardToShow);
            return;
        }
    }

    int IndexForLevelOfCard()
    {
        return Mathf.Max(0, cardToShow.level - 1);
    }

    public Color GetColorOfBorder()
    {

        if (MenuControl.Instance.battleMenu.playerAI == cardToShow.player)
        {
            Color returnColor = Color.white;
            if (MenuControl.Instance.areaMenu.currentArea != null) returnColor = MenuControl.Instance.areaMenu.currentArea.cardHistoryBorderColor;

            if (returnColor == Color.white || MenuControl.Instance.areaMenu.currentArea.cardFrontSprites[IndexForLevelOfCard()] == MenuControl.Instance.heroMenu.heroPath.cardFrontSprites[IndexForLevelOfCard()])
            {
                int index = MenuControl.Instance.heroMenu.heroPaths.IndexOf(MenuControl.Instance.heroMenu.heroPath) + 1;
                if (index == MenuControl.Instance.heroMenu.heroPaths.Count) index = 0;
                return MenuControl.Instance.heroMenu.heroPaths[index].cardHistoryBorderColor;
            }
            return returnColor;
        }
        else
        {
            if (MenuControl.Instance.battleMenu.tutorialMode)
            {
                if (cardToShow is Hero)
                    return MenuControl.Instance.heroMenu.heroPaths[0].cardHistoryBorderColor;
                else
                    return MenuControl.Instance.heroMenu.heroPaths[0].cardHistoryBorderColor;
            }
        }

        return MenuControl.Instance.heroMenu.heroPath.cardHistoryBorderColor;

    }
}
