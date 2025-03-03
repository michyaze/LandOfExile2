using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoicePanel : MonoBehaviour
{
    public Card cardToShow;
    public Image image;
    public Text buttonLabel;
    public Button button;
    public Text choiceText;
    public Text descriptionText;
    public Image descriptionImage;

    public float battleWidth = 620;
  
    public void RenderEventChoice(EventChoice eventChoice, Action actionToPerform, string choiceString, Card cardToShow = null)
    {
        if (cardToShow != null)
        {
            this.cardToShow = cardToShow;
            VisibleCard vc = Instantiate(MenuControl.Instance.visibleCardPrefab, image.transform);
            vc.RenderCardForMenu(cardToShow);
        }
        if (choiceString != null) choiceText.text = choiceString;
        else image.sprite = eventChoice.GetSprite();

        buttonLabel.text = eventChoice.GetName();
        if (eventChoice is EventChoiceFight)
        {
            descriptionText.gameObject.SetActive(false);
            descriptionImage.gameObject.SetActive(true);
            //change recttransform's width to battleWidth
            RectTransform rectTransform = GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(battleWidth, rectTransform.sizeDelta.y);
        }
        else
        {
            descriptionText.gameObject.SetActive(true);
            descriptionImage.gameObject.SetActive(false);
            descriptionText.text = eventChoice.GetDescription();
        }

        if (I2.Loc.LocalizationManager.CurrentLanguage != MenuControl.Languages.English.ToString())
        {
            descriptionText.font = MenuControl.Instance.GetSafeFont();
            buttonLabel.font = MenuControl.Instance.GetSafeFont();
        }

        button.onClick.AddListener(() =>
        {
            actionToPerform();
        });
    }
}
