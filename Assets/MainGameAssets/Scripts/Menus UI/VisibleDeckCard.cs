using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VisibleDeckCard : VisibleCard
{
    public GameObject naughtyBorder;
    public GameObject niceBorder;
    public Image cardImage;
    public Text cardNameText;
    public Text copiesText;

    public void RenderDeckCard(Card cardToShow, int copies)
    {
        disableInteraction = true;
        this.card = cardToShow;
        cardImage.sprite = cardToShow.GetSprite();
        cardNameText.text = cardToShow.GetName();
        if (I2.Loc.LocalizationManager.CurrentLanguage != MenuControl.Languages.English.ToString())
        {
            cardNameText.font = MenuControl.Instance.GetSafeFont();
        }

        costText.text = cardToShow.GetCost().ToString();
        costText.transform.parent.gameObject.SetActive(cardToShow.GetCost() > 0);
        copiesText.text = "x" + copies.ToString();
        
        naughtyBorder.SetActive(cardToShow.cardTags.Contains(MenuControl.Instance.naughtyTag));
        niceBorder.SetActive(cardToShow.cardTags.Contains(MenuControl.Instance.niceTag));

        RenderStars();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Middle)
            return;
        else if (eventData.button == PointerEventData.InputButton.Right)
            return;

        MenuControl.Instance.infoMenu.ShowInfo(this);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Middle)
            return;
        else if (eventData.button == PointerEventData.InputButton.Right)
            return;

        MenuControl.Instance.infoMenu.HideMenu();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Middle)
            return;
        else if (eventData.button == PointerEventData.InputButton.Right)
        {

            MenuControl.Instance.battleMenu.CancelSelectVisibleCard();
            MenuControl.Instance.cardViewerMenu.ShowCard(card);
            return;

        }
    }
}
