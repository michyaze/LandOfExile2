using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardChoiceMenu : BasicMenu
{
    public Text promptText;
    public Transform cardHolder;
    public List<GameObject> buttonObjects = new List<GameObject>();

    public List<Card> cardsToShow = new List<Card>();
    public List<VisibleCard> visibleCardsShown = new List<VisibleCard>();
    public List<System.Action> actionsToPerform = new List<System.Action>();

    public Text titleLabel;
    public HeroInfoPanel infoPanel;

    public int minSelectedCards;
    public int maxSelectedCards;
    public bool firstButtonNoCheck;
    public bool combineButtonsIntoOne;
    public List<string> buttonLabels;

    public List<int> selectedVisibleCardInts = new List<int>();

    public List<GameObject> vfxCardShowingPrefabs = new List<GameObject>();
    public List<GameObject> vfxObjects = new List<GameObject>();
    public Doozy.Engine.Soundy.SoundyData cardRevealSound;

    public GameObject myDeckButton;
    public GameObject myWeaponsButton;

    public List<Spine.Unity.SkeletonGraphic> skeletonGraphics = new List<Spine.Unity.SkeletonGraphic>();
    public List<string> animationStrings = new List<string>();
    public List<Vector2> animationOffsets = new List<Vector2>();

    public Button hideShowButton;
    public GameObject mainPanel; 

    public enum HighlightColor
    {
        Blue, Red, Green,Cross
    }

    public HighlightColor highlightColor = HighlightColor.Blue;

    public override void HideMenu(bool instantly = false)
    {
        base.HideMenu(instantly);
        foreach (GameObject go in vfxObjects)
        {
            if (go != null)
            {
                Destroy(go);
            }
        }
        vfxObjects.Clear();
    }

    public bool noChoice = false;

    public bool isRewardAfterBattle;

    void ShowParticle(int ii, int showRevealVFXType)
    {
        VisibleCard vc = visibleCardsShown[ii];
        vc.Hide();
        LeanTween.delayedCall((ii * 0.15f) + 0.4f, () =>
        {
            GameObject vfx = Instantiate(vfxCardShowingPrefabs[showRevealVFXType], MenuControl.Instance.transform) as GameObject;
            vfx.transform.position = vc.transform.position + (Vector3.back * 10f);
            vfxObjects.Add(vfx);
            Destroy(vfx, 3f);

            LeanTween.delayedCall(0.7f, () =>
            {
                vc.Show();
                LeanTween.rotateAroundLocal(vc.gameObject, Vector3.up, 360f, 1f).setEaseOutSine();
                Doozy.Engine.Soundy.SoundyManager.Play(cardRevealSound);
            });
        });
    }
    //combineButtonsIntoOne: buttonLabels would not shown as multiple button, but one combined button, and change text with different conditions
    public void ShowChoice(List<Card> cardsToShow, List<string> buttonLabels, List<System.Action> actionsToPerform, string textToShow,int minSelectedCards, int maxSelectedCards, bool firstButtonNoCheck, int showRevealVFXType, bool showMyDeckButton, string titleToShow = "", bool combineButtonsIntoOne = false,bool isupgrading = false)
    {
        ShowMenu();
        noChoice = false;
        infoPanel = GetComponentInChildren<HeroInfoPanel>();
        if (infoPanel)
        {
            if (showMyDeckButton)
            {
                infoPanel.gameObject.SetActive(true);
                infoPanel.updateHeroInfo();
            }
            else
            {
                infoPanel.gameObject.SetActive(false);
            
            }
        }

        mainPanel.SetActive(true);
        hideShowButton.gameObject.SetActive(false);

        highlightColor = HighlightColor.Blue;

        selectedVisibleCardInts.Clear();
        visibleCardsShown.Clear();
        if (titleToShow == "")
        {
            titleLabel.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            
            titleLabel.transform.parent.gameObject.SetActive(true);
            titleLabel.text = titleToShow;
        }
        promptText.text = textToShow;
        this.cardsToShow = cardsToShow;
        this.actionsToPerform = actionsToPerform;
        this.minSelectedCards = minSelectedCards;
        this.maxSelectedCards = maxSelectedCards;
        this.firstButtonNoCheck = firstButtonNoCheck;
        this.combineButtonsIntoOne = combineButtonsIntoOne;
        this.buttonLabels = buttonLabels;

        foreach (Transform child in cardHolder)
        {
            Destroy(child.gameObject);
        }

        foreach (Card card in cardsToShow)
        {
            VisibleCard vc = Instantiate(MenuControl.Instance.visibleCardPrefab, cardHolder);
            
            vc.isUpgrading = isupgrading;
            
            vc.RenderCardForMenu(card);
            visibleCardsShown.Add(vc);
        }

        var buttonCount = combineButtonsIntoOne ? 1 : actionsToPerform.Count;
        for (int ii = 0; ii < buttonObjects.Count; ii += 1)
        {
            buttonObjects[ii].SetActive(ii < buttonCount);
            buttonObjects[ii].transform.GetComponentInChildren<Text>().text = ii < buttonLabels.Count ? buttonLabels[ii] : "";
        }

        RenderButtons();

        //todo 这是啥
        if (cardsToShow.Count == minSelectedCards && cardsToShow.Count == maxSelectedCards)
        {
            foreach(VisibleCard vc in visibleCardsShown)
            {
                ClickVisibleCard(vc);
            }
        }

        cardHolder.parent.parent.gameObject.SetActive(cardsToShow.Count > 0);

        if (showRevealVFXType>=0)
        {
            for (int ii = 0; ii < visibleCardsShown.Count; ii += 1)
            {

                ShowParticle(ii, showRevealVFXType);
            }
        }
        else
        {
            if (isRewardAfterBattle)
            {
                for (int ii = 0; ii < visibleCardsShown.Count; ii += 1)
                {
                    if (cardsToShow[ii].IsTreasure())
                    {
                        ShowParticle(ii, 1);
                    }
                }
            }
        }

        myDeckButton.SetActive(false);
        myWeaponsButton.SetActive(false);

    }

    public override void SelectVisibleCard(VisibleCard vc, bool withClick = true)
    {
        // if (noChoice)
        // {
        //     return;
        // }
        base.SelectVisibleCard(vc, withClick);
        if (GetComponent<Doozy.Engine.UI.UIView>().IsHiding) return;
        //这个是为了修选卡的时候显示卡的信息
      //  if (GetComponent<Doozy.Engine.UI.UIView>().IsShowing) return;

        if (MenuControl.Instance.heroMenu.seasonsMode && visibleCardsShown[0]== vc && vc.niceCard.activeInHierarchy) return;
        
        MenuControl.Instance.infoMenu.ShowInfo(vc);

        //RenderButtons();
    }

    public override void DeSelectVisibleCard(VisibleCard vc, bool withClick = true)
    {
        //base.DeSelectVisibleCard(vc, withClick);
        MenuControl.Instance.infoMenu.HideMenu();

        //RenderButtons();
    }

    public override void ClickVisibleCard(VisibleCard vc)
    {if (noChoice)
        {
            return;
        }
        base.ClickVisibleCard(vc);
        Doozy.Engine.Soundy.SoundyManager.Play(MenuControl.Instance.cardLiftSound);
        if (selectedVisibleCardInts.Contains(visibleCardsShown.IndexOf(vc)))
        {
            selectedVisibleCardInts.Remove(visibleCardsShown.IndexOf(vc));
            vc.StopHighlight();
        }
        else
        {
            if (selectedVisibleCardInts.Count < maxSelectedCards)
            {
                selectedVisibleCardInts.Add(visibleCardsShown.IndexOf(vc));
                if (highlightColor == HighlightColor.Blue)
                    vc.HighlightCheck();
                else if (highlightColor == HighlightColor.Red)
                    vc.HighlightRed();
                else if (highlightColor == HighlightColor.Green)
                    vc.HighlightGreen();
                else if (highlightColor == HighlightColor.Cross)
                    vc.HighlightCross();
                

                LeanTween.scale(vc.gameObject, Vector3.one * 1.0f, 0.15f).setLoopPingPong(1).setEaseInSine();

            }
            else if (maxSelectedCards == 1)
            {
                ClickVisibleCard(visibleCardsShown[selectedVisibleCardInts[0]]);
                selectedVisibleCardInts.Add(visibleCardsShown.IndexOf(vc));
                if (highlightColor == HighlightColor.Blue)
                    vc.HighlightCheck();
                    //vc.HighlightBlue();
                else if (highlightColor == HighlightColor.Red)
                    vc.HighlightRed();
                else if (highlightColor == HighlightColor.Green)
                    vc.HighlightGreen();
                else if (highlightColor == HighlightColor.Cross)
                    vc.HighlightCross();

                LeanTween.scale(vc.gameObject, Vector3.one * 1.0f, 0.15f).setLoopPingPong(1).setEaseInSine();
            }
        }

        RenderButtons();

    }

    public void RenderButtons()
    {
        for (int ii = 0; ii < buttonObjects.Count; ii += 1)
        {
            buttonObjects[ii].GetComponent<Button>().interactable = false;
            if (ii == 0 && combineButtonsIntoOne)
            {
                buttonObjects[ii].GetComponent<Button>().GetComponentInChildren<Text>().text = buttonLabels[ii];
            }
            if (ii == 0 && firstButtonNoCheck)
            {
                buttonObjects[ii].GetComponent<Button>().interactable = true;
            }
            else if(selectedVisibleCardInts.Count >= minSelectedCards && selectedVisibleCardInts.Count <= maxSelectedCards)
            {
                buttonObjects[ii].GetComponent<Button>().interactable = true;
                if (combineButtonsIntoOne && ii<buttonLabels.Count)
                {
                    // if combine buttons, and later button should show, update first label text
                    buttonObjects[0].GetComponent<Button>().GetComponentInChildren<Text>().text = buttonLabels[ii];
                }
            }
            //buttonObjects[ii].GetComponent<Button>().interactable = ((firstButtonNoCheck && ii == 0) || (selectedVisibleCardInts.Count >= minSelectedCards && selectedVisibleCardInts.Count <= maxSelectedCards && !combineButtonsIntoOne));
           
        }
    }

    public void PressedButton(int buttonInt)
    {
        for (int ii = 0; ii < buttonObjects.Count; ii += 1)
        {
            buttonObjects[ii].GetComponent<Button>().interactable = false;
        }
        CloseMenu();
        //HideMenu();
        if (buttonInt == 0 && combineButtonsIntoOne && selectedVisibleCardInts.Count >= minSelectedCards &&
            selectedVisibleCardInts.Count <= maxSelectedCards )
        {
            actionsToPerform[actionsToPerform.Count-1]();
        }
        else
        {
            actionsToPerform[buttonInt]();
        }
        MenuControl.Instance.adventureMenu.UpdateScreenIfOnTop();
        //CloseMenu();
    }

    public void ShowPayTreasure(System.Action actionWhenConfirmed)
    {
        Debug.LogError("should not get into ShowPayTreasure");
        // List<Card> cards = MenuControl.Instance.heroMenu.GetTreasureCardsOwned();
        //
        // List<string> buttonLabels = new List<string>();
        // buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Cancel"));
        // buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Confirm"));
        //
        // List<System.Action> actions = new List<System.Action>();
        // actions.Add(() => { });
        // actions.Add(() => { 
        //     foreach (int integer in selectedVisibleCardInts)
        //     {
        //         MenuControl.Instance.heroMenu.RemoveCardFromDeck(visibleCardsShown[integer].card);
        //     }
        //     actionWhenConfirmed();
        // });
        //
        // ShowChoice(cards, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("TreasurePaymentPrompt", "Choose a treasure card as payment (it will be removed from your deck):"), 1, 1, true, false, true);

    }

    public void ShowNotifcation(Card card, System.Action actionWhenConfirmed, string notificationString,bool isUpgrading = false)
    {
        List<Card> cards = new List<Card>();
        if (card != null)
            cards.Add(card);

        List<string> buttonLabels = new List<string>();

        buttonLabels.Add(MenuControl.Instance.GetLocalizedString("OK"));

        List<System.Action> actions = new List<System.Action>();

        actions.Add(() => {
            actionWhenConfirmed();
        });

        ShowChoice(cards, buttonLabels, actions, notificationString, 0, 0, true, -1, false,"",false,isUpgrading);
    }

    public void ShowNotifcation(List<Card> cardsToShow, System.Action actionWhenConfirmed, string notificationString)
    {
        List<Card> cards = new List<Card>();
        cards.AddRange(cardsToShow);

        List<string> buttonLabels = new List<string>();

        buttonLabels.Add(MenuControl.Instance.GetLocalizedString("OK"));

        List<System.Action> actions = new List<System.Action>();

        actions.Add(() => {
            actionWhenConfirmed();
        });

        ShowChoice(cards, buttonLabels, actions, notificationString, 0, 0, true, -1, false);
    }

    public void HideShowButtonPressed()
    {
        mainPanel.SetActive(!mainPanel.activeInHierarchy);
        if (infoPanel && infoPanel.gameObject)
        {
            
            infoPanel.gameObject.SetActive(mainPanel.activeInHierarchy);
        }
    }
}
