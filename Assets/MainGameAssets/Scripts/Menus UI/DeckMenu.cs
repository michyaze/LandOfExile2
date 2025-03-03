using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DeckMenu : BasicMenu
{
    public CanvasGroup blockerPanel;
    public Text titleText;
    public List<Card> cardsToShow = new List<Card>();

    public Transform cardGrid;
    public Button flareStones;
    public Text flareStonesText;
    public Toggle stackDuplicatesToggle;
    public float cellScale = 0.65f;
    public void ShowDeck(List<Card> cardsToBeShown, string titleString)
    {

        cardsToShow.Clear();
        cardsToShow.AddRange(cardsToBeShown);

        titleText.text = titleString + " (" + cardsToShow.Count + ")";
        ShowMenu();
    }
    public void ShowMyDeckRandomised()
    {
        //if (!MenuControl.Instance.cardChoiceMenu.gameObject.activeInHierarchy /*&& MenuControl.Instance.battleMenu.player1CanAct*/)
        {
            cardsToShow.Clear();
            foreach (Card card in MenuControl.Instance.battleMenu.player1.cardsInDeck)
            {
                 if (card is Hero) cardsToShow.Add(card);
            else
                //cardsToShow.Add(MenuControl.Instance.heroMenu.GetCardByID(card.UniqueID));
                cardsToShow.Add(card); //why use GetCardByID before, and why there is no ID for those cards before?
            }
            cardsToShow.Shuffle();

            titleText.text = MenuControl.Instance.GetLocalizedString("My Deck");
            ShowMenu();
        }
    }

    public void ShowMyDeck()
    {
        cardsToShow = MenuControl.Instance.heroMenu.cardsInDeck();
      
        titleText.text = MenuControl.Instance.GetLocalizedString("My Deck");
        flareStones.gameObject.SetActive(true);
        flareStonesText.text = "x" + MenuControl.Instance.heroMenu.flareStones;
        ShowMenu();
    }

    public void ShowMyTreasures()
    {
        cardsToShow.Clear();
        cardsToShow.AddRange(MenuControl.Instance.heroMenu.GetTreasureCardsOwned());
        titleText.text = MenuControl.Instance.GetLocalizedString("Treasures");
        ShowMenu();

    }

    public void ShowEnemyDeck()
    {

        cardsToShow.Clear();
        cardsToShow.AddRange(MenuControl.Instance.adventureMenu.GetCurrentAdventureItemEncounter().allOwnedCards);
        if (MenuControl.Instance.adventureMenu.GetCurrentAdventureItemEncounter().GetHero().GetIntentSystem() != null)
        {
            foreach (IntentSystemHand hand in MenuControl.Instance.adventureMenu.GetCurrentAdventureItemEncounter().GetHero().GetIntentSystem().hands)
            {
                if (hand.GetHandCards().Count > 0)
                {
                    cardsToShow.AddRange(hand.GetHandCards());
                }
                else
                {
                    cardsToShow.AddRange(hand.cards);
                }
            }
        }

        titleText.text = MenuControl.Instance.GetLocalizedString("Enemy Deck");
        ShowMenu();
    }

    public override void HideMenu(bool instantly = false)
    {
        base.HideMenu(instantly);
        LeanTween.alphaCanvas(blockerPanel, 0f, 0.2f);
    }

    public void UpdateMenuContent()
    {
        foreach (Transform child in cardGrid)
        {
            Destroy(child.gameObject);
        }

        List<Card> uniqueCards = new List<Card>();
        foreach (Card card in cardsToShow)
        {
            bool unique = true;
            foreach (Card cc in uniqueCards)
            {
                if (card.UniqueID == cc.UniqueID)
                {
                    unique = false;
                }
            }
            if (unique)
            {
                uniqueCards.Add(card);
            }
        }

        List<Card> cardsToShow2 = new List<Card>();
        if (stackDuplicatesToggle.isOn)
        {
            cardsToShow2.AddRange(uniqueCards);
        }
        else
        {
            cardsToShow2.AddRange(cardsToShow);
        }

        foreach (Card card in cardsToShow2)
        {
            
            if (card is Hero)
            {
                continue;
                //vc.HighlightGreen();
            }
            GameObject parentObj = new GameObject();
            parentObj.transform.parent = cardGrid;
            parentObj.transform.localScale = Vector3.one;
            parentObj.transform.localPosition = Vector3.zero;
            parentObj.AddComponent<RectTransform>();

            VisibleCard vc = Instantiate(MenuControl.Instance.visibleCardPrefab, parentObj.transform);
            vc.RenderCardForMenu(card);
            vc.transform.localScale = Vector3.one * cellScale;


            //Show multiplies
            if (stackDuplicatesToggle.isOn)
            {
                int count = MenuControl.Instance.CountOfCardsInList(card, cardsToShow);
                // for (int xx = 0; xx < count; xx += 1)
                // {
                //     if (xx > 0)
                //     {
                        // VisibleCard vc2 = Instantiate(MenuControl.Instance.visibleCardPrefab, parentObj.transform);
                        // vc2.RenderCardForMenu(card);
                        // vc2.transform.localScale = Vector3.one * 0.9f;
                        // vc2.transform.SetAsFirstSibling();
                        // vc2.GetComponent<RectTransform>().anchoredPosition += Vector2.down * 20f * xx + Vector2.right * 20f * xx;
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

        flareStones.gameObject.SetActive(false);
    }
    public override void ShowMenu()
    {
        base.ShowMenu();
        blockerPanel.alpha = 0f;
        LeanTween.alphaCanvas(blockerPanel, 1f, 0.3f).setDelay(0.4f);

        UpdateMenuContent();
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
}
