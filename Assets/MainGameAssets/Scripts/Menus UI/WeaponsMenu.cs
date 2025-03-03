using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class WeaponsMenu : BasicMenu
{
    public enum Mode
    {
        Weapons, Artifacts
    }

    public Text titleText;
    public Text promptText;
    public List<Card> cardsToShow = new List<Card>();

    public Transform cardGrid;
    public Toggle autoEquipNewWeaponsToggle;
    public Toggle stackDuplicatesToggle;

    public Mode menuMode;
    public float cellScale = 0.65f;

    public void ShowDeck(List<Card> cardsToBeShown, string titleString)
    {
        cardsToShow.Clear();
        cardsToShow.AddRange(cardsToBeShown);
        ShowMenu();
        titleText.text = titleString + " ("+cardsToShow.Count+")";
    }

    public void ShowMyWeapons()
    {
        menuMode = Mode.Weapons;
        cardsToShow.Clear();
        cardsToShow.AddRange(MenuControl.Instance.heroMenu.weaponsOwned);
        ShowMenu();
        titleText.text = MenuControl.Instance.GetLocalizedString("Weapons") + " (" + cardsToShow.Count + ")";
        promptText.text = MenuControl.Instance.GetLocalizedString("WeaponsMenuPrompt");
        autoEquipNewWeaponsToggle.gameObject.SetActive(true);
    }

    public void ShowMyArtifacts()
    {
        menuMode = Mode.Artifacts;
        cardsToShow.Clear();
        cardsToShow.AddRange(MenuControl.Instance.heroMenu.artifactsOwned);
        cardsToShow.AddRange(MenuControl.Instance.heroMenu.GetHealingPotionsInDeck());
        ShowMenu();
        titleText.text = MenuControl.Instance.GetLocalizedString("Artifacts") + " " + MenuControl.Instance.heroMenu.artifactsEquipped.Count + " / " + MenuControl.Instance.heroMenu.artifactSlots;
        promptText.text = MenuControl.Instance.GetLocalizedString("ArtifactsMenuPrompt").Replace("XX", MenuControl.Instance.heroMenu.artifactSlots.ToString());
        autoEquipNewWeaponsToggle.gameObject.SetActive(false);
    }

    public override void ShowMenu()
    {
        base.ShowMenu();

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
            cardsToShow2.AddRange(uniqueCards.OrderBy(x => (x.GetName())));
        }
        else
        {
            cardsToShow2.AddRange(cardsToShow.OrderBy(x => (x.GetName())));
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

            if (menuMode == Mode.Weapons)
            {
                if (card == MenuControl.Instance.heroMenu.hero.weapon)
                {
                    if (cardsToShow2.IndexOf(card) == ii)
                        vc.HighlightGreen();
                }
            }

            if (menuMode == Mode.Artifacts)
            {
                if (MenuControl.Instance.heroMenu.artifactsEquipped.Contains(card))
                {
                    if (stackDuplicatesToggle.isOn && cardsToShow2.IndexOf(card) == ii)
                    {
                        //vc.HighlightGreen();
                        vc.HighlightCheck();
                    }
                    else if (!stackDuplicatesToggle.isOn) {

                        int totalOfThisCardEquipped = MenuControl.Instance.CountOfCardsInList(card, MenuControl.Instance.heroMenu.artifactsEquipped);

                        List<int> duplicateIndexes = new List<int>();
                        for (int xx =0; xx <cardsToShow2.Count; xx+=1)
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
        cardGrid.GetComponent<RectTransform>().sizeDelta = new Vector2(cardGrid.GetComponent<RectTransform>().sizeDelta.x, Mathf.CeilToInt(cardsToShow.Count / 6f) * (cardGrid.GetComponent<GridLayoutGroup>().cellSize.y + cardGrid.GetComponent<GridLayoutGroup>().spacing.y));


    }

    public override void SelectVisibleCard(VisibleCard vc, bool withClick = true)
    {
        base.SelectVisibleCard(vc, withClick);
        if (GetComponent<Doozy.Engine.UI.UIView>().IsHiding) return;
        MenuControl.Instance.infoMenu.ShowInfo(vc);
    }

    public override void DeSelectVisibleCard(VisibleCard vc, bool withClick = true)
    {
        base.DeSelectVisibleCard(vc, withClick);
        MenuControl.Instance.infoMenu.HideMenu();
    }

    public override void ClickVisibleCard(VisibleCard vc)
    {
        base.ClickVisibleCard(vc);
        // if (menuMode == Mode.Weapons)
        // {
        //     if (MenuControl.Instance.heroMenu.hero.weapon != vc.card)
        //     {
        //         MenuControl.Instance.heroMenu.hero.weapon = (Deprecated Weapon)vc.card;
        //
        //         if (MenuControl.Instance.battleMenu.inBattle)
        //         {
        //             MenuControl.Instance.battleMenu.player1.GetHero().ChangeWeapon((Deprecated Weapon)vc.card);
        //             MenuControl.Instance.battleMenu.player1.RenderCards();
        //         }
        //         MenuControl.Instance.dataControl.SaveData();
        //         ShowMenu();
        //         MenuControl.Instance.adventureMenu.RenderPlayerIcon();
        //     }
        // }

        if (menuMode == Mode.Artifacts)
        {
            if (!vc.highlightCheckBoard.activeInHierarchy && MenuControl.Instance.heroMenu.artifactsEquipped.Count < MenuControl.Instance.heroMenu.artifactSlots) //!MenuControl.Instance.heroMenu.artifactsEquipped.Contains(vc.card) && MenuControl.Instance.heroMenu.artifactSlots > MenuControl.Instance.heroMenu.artifactsEquipped.Count)
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
        }
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
        if (MenuControl.Instance.levelUpMenu.gameObject.activeInHierarchy)
            MenuControl.Instance.levelUpMenu.ShowCharacterSheet();
        base.CloseMenu();
    }
}
