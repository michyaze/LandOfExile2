using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ItemMenu : BasicMenu
{
    public CanvasGroup blockerPanel;
    public Text titleText;
    public Text promptText;
    public List<Card> cardsToShow = new List<Card>();

    public Transform cardGrid;
    public Transform selectedCardGrid;
    public Toggle autoEquipNewWeaponsToggle;
    public Toggle stackDuplicatesToggle;

    public float cellScale = 0.65f;

    public RectTransform equipArea;

    public void ShowMyArtifacts()
    {
        cardsToShow.Clear();
        cardsToShow.AddRange(MenuControl.Instance.heroMenu.artifactsOwned);
        cardsToShow.AddRange(MenuControl.Instance.heroMenu.GetHealingPotionsInDeck());
        ShowMenu();
        //titleText.text = MenuControl.Instance.GetLocalizedString("itemTitle");
        //promptText.text = MenuControl.Instance.GetLocalizedString("ArtifactsMenuPrompt").Replace("XX", MenuControl.Instance.heroMenu.artifactSlots.ToString());
        //autoEquipNewWeaponsToggle.gameObject.SetActive(false);
    }
    public override void HideMenu(bool instantly = false)
    {
        base.HideMenu(instantly);
        LeanTween.alphaCanvas(blockerPanel, 0f, 0.2f);
    }
    public override void ShowMenu()
    {
        
        if (!gameObject.activeSelf)
        {
            
            blockerPanel.alpha = 0f;
            LeanTween.alphaCanvas(blockerPanel, 1f, 0.3f).setDelay(0.4f);
        }
        base.ShowMenu();

        //update selected cards
        
        var artifactSlots = selectedCardGrid.GetComponentsInChildren<VisibleArtifactSlot>(true);

        //Artifacts
        for (int ii = 0; ii < 4; ii += 1)
        {
            artifactSlots[ii].gameObject.SetActive(MenuControl.Instance.heroMenu.artifactsEquipped.Count > ii);
            artifactSlots[ii].transform.parent.gameObject.SetActive(MenuControl.Instance.heroMenu.artifactSlots > ii);

            if (MenuControl.Instance.heroMenu.artifactsEquipped.Count > ii)
            {
                artifactSlots[ii].RenderArtifact(MenuControl.Instance.heroMenu.artifactsEquipped[ii]);
            }
        }

        //update grid
        foreach (Transform child in cardGrid)
        {
            Destroy(child.gameObject);
        }

        List<Card> uniqueCards = new List<Card>();
        foreach (Card card in cardsToShow)
        {
            if (!uniqueCards.Contains(card))
            {
                uniqueCards.Add(card);
            }
        }

        List<Card> cardsToShow2 = new List<Card>();
        if (stackDuplicatesToggle.isOn)
        {
            //cardsToShow2.AddRange(uniqueCards.OrderBy(x => (x.GetName())));
            cardsToShow2.AddRange(uniqueCards);
        }
        else
        {
            //cardsToShow2.AddRange(cardsToShow.OrderBy(x => (x.GetName())));
            cardsToShow2.AddRange(cardsToShow);
        }

        for (int ii = 0; ii < cardsToShow2.Count; ii += 1)
        {
            Card card = cardsToShow2[ii];
            GameObject parentObj = new GameObject();
            parentObj.transform.parent = cardGrid;
            parentObj.transform.localScale = Vector3.one;
            parentObj.transform.localPosition = Vector3.zero;
            parentObj.AddComponent<RectTransform>();

            VisibleCard vc = Instantiate(MenuControl.Instance.visibleCardPrefab, parentObj.transform);
            vc.RenderCardForMenu(card);
            vc.transform.localScale = Vector3.one * cellScale;


            if (MenuControl.Instance.heroMenu.artifactsEquipped.Contains(card))
            {
                if ((stackDuplicatesToggle.isOn && cardsToShow2.IndexOf(card) == ii)||card.isPotion)
                {
                    //vc.HighlightGreen();
                    vc.HighlightCheck();
                }
                else if (!stackDuplicatesToggle.isOn)
                {
                    int totalOfThisCardEquipped =
                        MenuControl.Instance.CountOfCardsInList(card, MenuControl.Instance.heroMenu.artifactsEquipped);

                    List<int> duplicateIndexes = new List<int>();
                    for (int xx = 0; xx < cardsToShow2.Count; xx += 1)
                    {
                        if (cardsToShow2[xx] == card)
                        {
                            duplicateIndexes.Add(xx);
                        }
                    }

                    int thisCardIndex = duplicateIndexes.IndexOf(ii);

                    if (thisCardIndex < totalOfThisCardEquipped)
                    {
                        vc.HighlightCheck();
                        //vc.HighlightGreen();
                    }
                }
            }

            //Show multiplies
            if (stackDuplicatesToggle.isOn)
            {
                int count = MenuControl.Instance.CountOfCardsInList(card, cardsToShow);
                // for (int xx = 0; xx < count; xx += 1)
                // {
                //     if (xx > 0)
                //     {
                //         VisibleCard vc2 = Instantiate(MenuControl.Instance.visibleCardPrefab, parentObj.transform);
                //         vc2.RenderCardForMenu(card);
                //         vc2.transform.localScale = Vector3.one * 0.9f;
                //         vc2.transform.SetAsFirstSibling();
                //         vc2.GetComponent<RectTransform>().anchoredPosition += Vector2.down * 20f * xx + Vector2.right * 20f * xx;
                //     }
                // }

                //if (count > 1)
                {
                    vc.SetHandCardCount(count);
                    // GameObject textObj = new GameObject();
                    // textObj.transform.parent = parentObj.transform;
                    // textObj.transform.localScale = Vector3.one;
                    // textObj.transform.localPosition = Vector3.zero;
                    // RectTransform rectT = textObj.AddComponent<RectTransform>();
                    // rectT.anchoredPosition += Vector2.down * 170f;
                    //
                    // Text text = textObj.AddComponent<Text>();
                    // text.font = titleText.font;
                    // text.alignment = TextAnchor.UpperCenter;
                    // text.fontSize = 36;
                    // text.text = "x" + count;
                }
            }
        }

        cardGrid.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        cardGrid.GetComponent<RectTransform>().sizeDelta = new Vector2(
            cardGrid.GetComponent<RectTransform>().sizeDelta.x,
            Mathf.CeilToInt(cardsToShow.Count / 6f) * (cardGrid.GetComponent<GridLayoutGroup>().cellSize.y +
                                                       cardGrid.GetComponent<GridLayoutGroup>().spacing.y));
    }

    public override void SelectVisibleCard(VisibleCard vc, bool withClick = true)
    {
        base.SelectVisibleCard(vc, withClick);

        if (GetComponent<Doozy.Engine.UI.UIView>().IsHiding) return;
        if (!selectedVisibleCard)
        {
            MenuControl.Instance.infoMenu.ShowInfo(vc);
        }
        if (withClick)
        {
            startDraggingPosition = Input.mousePosition;
            selectedVisibleCard = vc;
            if (draggingVisibleCard == null)
            {
                draggingVisibleCard = Instantiate(MenuControl.Instance.visibleCardPrefab, vc.transform.position,vc.transform.rotation, transform);
                draggingVisibleCard.transform.localScale = Vector3.one * cellScale;
            }
            draggingVisibleCard.gameObject.SetActive(false);
            draggingVisibleCard.RenderCardForMenu(vc.card);
        
        
            if (GetComponent<Doozy.Engine.UI.UIView>().IsHiding) return;
            //MenuControl.Instance.infoMenu.ShowInfo(vc);
        }
    }

    public override void DeSelectVisibleCard(VisibleCard vc, bool withClick = true)
    {
        base.DeSelectVisibleCard(vc, withClick);
        MenuControl.Instance.infoMenu.HideMenu();
    }

    private bool wasClick = false;
    public override void ClickVisibleCard(VisibleCard vc)
    {
        base.ClickVisibleCard(vc);

        // if (vc.card.isConsumable)
        // {
        //     MenuControl.Instance.adventureMenu.ClickOnHealingPotions(vc.card);
        // }

        wasClick = true;
        // if (vc is VisibleArtifactSlot)
        // {
        //     return;
        // }
        
        Doozy.Engine.Soundy.SoundyManager.Play(MenuControl.Instance.cardLiftSound);
        if (!MenuControl.Instance.heroMenu.artifactsEquipped.Contains(vc.card)/* !vc.highlightCheckBoard.activeInHierarchy */&& MenuControl.Instance.heroMenu.artifactsEquipped.Count < MenuControl.Instance.heroMenu.artifactSlots) //!MenuControl.Instance.heroMenu.artifactsEquipped.Contains(vc.card) && MenuControl.Instance.heroMenu.artifactSlots > MenuControl.Instance.heroMenu.artifactsEquipped.Count)
        {
            MenuControl.Instance.heroMenu.artifactsEquipped.Add(vc.card);
            MenuControl.Instance.dataControl.SaveData();
            ShowMyArtifacts();
        }
        else //if (MenuControl.Instance.heroMenu.artifactsEquipped.Contains(vc.card))
        {
            MenuControl.Instance.heroMenu.artifactsEquipped.Remove(vc.card);
            MenuControl.Instance.dataControl.SaveData();
            ShowMyArtifacts();
        }
        CancelSelectVisibleCard();
    }
    void ClickVisibleCardInternal(VisibleCard vc)
    {
        base.ClickVisibleCard(vc);

        // if (vc.card.isConsumable)
        // {
        //     MenuControl.Instance.adventureMenu.ClickOnHealingPotions(vc.card);
        // }

        wasClick = true;
    }

    void TryEquipCard(VisibleCard vc)
    {
        if (!vc.highlightCheckBoard.activeInHierarchy && MenuControl.Instance.heroMenu.artifactsEquipped.Count <
            MenuControl.Instance.heroMenu
                .artifactSlots) //!MenuControl.Instance.heroMenu.artifactsEquipped.Contains(vc.card) && MenuControl.Instance.heroMenu.artifactSlots > MenuControl.Instance.heroMenu.artifactsEquipped.Count)
        {
            MenuControl.Instance.heroMenu.artifactsEquipped.Add(vc.card);
            MenuControl.Instance.dataControl.SaveData();
            ShowMyArtifacts();
        }
    }

    void TrySwapCards(VisibleCard vcEquipped, VisibleCard vcNew)
    {
        var oldTemp = vcEquipped.card;
        int oldIndex = MenuControl.Instance.heroMenu.artifactsEquipped.FindIndex((x) => x == vcEquipped.card);
        int newIndex = MenuControl.Instance.heroMenu.artifactsEquipped.FindIndex((x) => x == vcNew.card);
        MenuControl.Instance.heroMenu.artifactsEquipped[oldIndex] = vcNew.card;
        if (newIndex >= 0)
        {
            MenuControl.Instance.heroMenu.artifactsEquipped[newIndex] = oldTemp;
        }
        ShowMyArtifacts();
    }
    void TryUnequipCard(VisibleCard vc)
    {
        MenuControl.Instance.heroMenu.artifactsEquipped.Remove(vc.card);
        MenuControl.Instance.dataControl.SaveData();
        ShowMyArtifacts();
    }

    public override void CloseMenu()
    {
        //if (menuMode == Mode.Artifacts)
        //{
        //    if (MenuControl.Instance.heroMenu.artifactSlots > MenuControl.Instance.heroMenu.artifactsEquipped.Count && MenuControl.Instance.heroMenu.artifactsOwned.Count > MenuControl.Instance.heroMenu.artifactsEquipped.Count)
        //    {
        //        MenuControl.Instance.ShowBlockingNotification(null, MenuControl.Instance.GetLocalizedString("ArtifactsNotEquippedPrompt"), MenuControl.Instance.GetLocalizedString("ArtifactsNotEquippedDescription"), () =>
        //        {
        //            stackDuplicatesToggle.isOn = false;
        //            ShowMyArtifacts();
        //        });
        //        return;
        //    }
        //}
        CancelSelectVisibleCard();
        if (MenuControl.Instance.levelUpMenu.gameObject.activeInHierarchy)
            MenuControl.Instance.levelUpMenu.ShowCharacterSheet();
        base.CloseMenu();
    }

    private Vector2 startDraggingPosition;
    private VisibleCard selectedVisibleCard;
    private VisibleCard draggingVisibleCard;
    private void Update()
    {
        //if has selected card, and no draging card, show draging card
        //if mouse up, based on selected card and position, do
        //1. selected card is equipped
        //  position is another artifact slot, swap
        //  position is equip area, ignore
        //  otherwise, unequip
        //2. otherwise
        //  out of equip area, ignore
        //  position is an artifact slot with existed item, swap
        //  otherwise, equip, if has space
        
        if (Input.GetMouseButtonUp(1))
        {
            CancelSelectVisibleCard();
        }
var  pointerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            if (draggingVisibleCard && selectedVisibleCard)
            {
                if (new Vector2( Input.mousePosition.x, Input.mousePosition.y) != startDraggingPosition)
                {
                    draggingVisibleCard.gameObject.SetActive(true);
                }
                pointerPos.z = 0;
                draggingVisibleCard.transform.position = pointerPos;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            // if (wasClick)
            // {
            //     wasClick = false;
            //     return;
            // }
            
            if (selectedVisibleCard)
            {
                Vector2 mousePos = Input.mousePosition;
                if ((mousePos - startDraggingPosition).sqrMagnitude < 1)
                {
                    ClickVisibleCardInternal(selectedVisibleCard);
                    CancelSelectVisibleCard();
                    return;
                }
                Vector2 lp;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(equipArea, mousePos, Camera.main, out lp);
                if (MenuControl.Instance.heroMenu.artifactsEquipped.Contains(selectedVisibleCard.card))
                {
                
                    if (equipArea.GetComponent<RectTransform>().rect.Contains (lp))
                    {
                        foreach (var equippedArtifact in  selectedCardGrid.GetComponentsInChildren<VisibleArtifactSlot>())
                        {
                            if (equippedArtifact.card == selectedVisibleCard.card)
                            {
                                continue;
                            }
                            var rectTransform = equippedArtifact.GetComponent<RectTransform>();
                            Vector2 newLP;
                            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, mousePos, Camera.main, out newLP);
                            if (rectTransform.rect.Contains(newLP))
                            {
                                Doozy.Engine.Soundy.SoundyManager.Play(MenuControl.Instance.cardLiftSound);
                                TrySwapCards(equippedArtifact, selectedVisibleCard);
                                break;
                            }
                        }
                        
                        
                    }
                    else
                    {
                        Doozy.Engine.Soundy.SoundyManager.Play(MenuControl.Instance.cardLiftSound);
                        TryUnequipCard(selectedVisibleCard);
                    }
                }
                else
                {
                    if (equipArea.GetComponent<RectTransform>().rect.Contains (lp))
                    {
                        bool isChanged = false;
                        foreach (var equippedArtifact in  selectedCardGrid.GetComponentsInChildren<VisibleArtifactSlot>())
                        {
                            if (equippedArtifact.card == selectedVisibleCard.card)
                            {
                                continue;
                            }
                            var rectTransform = equippedArtifact.GetComponent<RectTransform>();
                            Vector2 newLP;
                            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, mousePos, Camera.main, out newLP);
                            if (rectTransform.rect.Contains(newLP))
                            {
                                Doozy.Engine.Soundy.SoundyManager.Play(MenuControl.Instance.cardLiftSound);
                                TrySwapCards(equippedArtifact, selectedVisibleCard);
                                isChanged = true;
                                break;
                            }
                        }

                        if (!isChanged)
                        {
                            
                            Doozy.Engine.Soundy.SoundyManager.Play(MenuControl.Instance.cardLiftSound);
                            TryEquipCard(selectedVisibleCard);
                        }
                    }
                    else
                    {
                    }
                }
            
            
                CancelSelectVisibleCard();
            }
            
        }
    }

        void CancelSelectVisibleCard()
        {
            selectedVisibleCard = null;
            if (draggingVisibleCard && draggingVisibleCard.gameObject.activeSelf)
            {
                draggingVisibleCard.gameObject.SetActive(false);

            }
            else
            {
                //Debug.LogError("no dragging visible card");
            }
            
            MenuControl.Instance.infoMenu.HideMenu();
        }
}