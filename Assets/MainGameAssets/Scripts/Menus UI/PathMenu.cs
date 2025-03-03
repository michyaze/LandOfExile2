using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Linq;
using UnityEngine.Serialization;


public class PathMenu : BasicMenu
{


    private HeroMenu heroMenu;
    [HideInInspector]public List<HeroPath> heroPaths =>heroMenu.heroPaths;
    public List<UnlockableStartingCards> unlockableStartingCardLists = new List<UnlockableStartingCards>();

    public Text deckNameText;
    public bool selectedPath = false;

    //Variables
    private HeroClass heroClass=>heroMenu.heroClass;
    private HeroPath heroPath{
        get { return heroMenu.heroPath; }
        set { heroMenu.heroPath = value; }}



    private VisibleCard selectedVisibleCard;

    public Transform decklistVCs;
    

    public Text pathNameText;
    public Text pathDescText;
    public Image pathIcon;
    public Image largePathImage;


    public void backButtonPressed()
    {
        
        
        MenuControl.Instance.dataControl.ResetData();
        CloseMenu();

        MenuControl.Instance.heroMenu.ShowMenu();
    }
    public int startingDeckInt
    {
        get { return heroMenu.startingDeckInt; }
        set { heroMenu.startingDeckInt = value; }
    }

    private List<Card> originalStartingCards = new List<Card>();


    public void Setup()
    {
        heroMenu = MenuControl.Instance.heroMenu;
    }
    public override void ShowMenu()
    {
        base.ShowMenu();
        selectedPath = false;
        
        heroMenu = MenuControl.Instance.heroMenu;
        


        // if (heroPath == null)
        //     heroPath = heroMenu.heroPaths[0];

        SelectPath(heroPath);
    }

    public UnlockableStartingCards GetUnlockbleCardList()
    {
        foreach (UnlockableStartingCards cardsList in unlockableStartingCardLists)
        {
            if (cardsList.heroPath ==  MenuControl.Instance.heroMenu.heroPath && cardsList.heroClass == MenuControl.Instance.heroMenu.heroClass)
            {
                return cardsList;
            }
        }

        return null;
    }


    public override void CloseMenu()
    {
        base.CloseMenu();
        MenuControl.Instance.infoMenu.HideMenu();
        //heroNamePanelView.Hide(true);
    }


    void SelectPath(HeroPath heroPath)
    {
        if (this.heroPath != heroPath) startingDeckInt = 0;
        this.heroPath = heroPath;

        largePathImage.sprite = heroPath.GetSprite();
        pathIcon.sprite = heroPath.icon;
        pathNameText.text = heroPath.GetName();
        pathDescText.text = heroPath.GetDescription();
        
        //update left hero info
        GetComponentInChildren<HeroInfoPanel>().updateHeroInfo();

        LoadStartingCardsInDeck();
        RenderDeck();
    }


    void LoadStartingCardsInDeck()
    {
        originalStartingCards.Clear();
        foreach (Card card in GetUnlockbleCardList().startingDecks[startingDeckInt].startingCards)
        {
                originalStartingCards.Add(card);
        }
    }


    public void CreateHeroAndClose()
    {


        // foreach (Card card in originalStartingCards)
        // {
        //     if (!MenuControl.Instance.progressMenu.cardsDiscovered.Contains(card.UniqueID) && !(card is Hero))
        //     {
        //         MenuControl.Instance.progressMenu.cardsDiscovered.Add(card.UniqueID);
        //     }
        //
        //     if (card is Hero)
        //     {
        //         heroMenu. cardsOwned.Add(card);
        //     }
        //     else
        //     {
        //         CreateCardToOwn(card);
        //     }
        // }

        selectedPath = true;
        MenuControl.Instance.dataControl.SaveData();

        CloseMenu();

        MenuControl.Instance.adventureMenu.ContinueAdventure();

        MenuControl.Instance.LogEvent("NewHero_" + GetLevelClassPathString());
        MenuControl.Instance.LogEvent("StartNewGame");
    }

    public string GetLevelClassPathString()
    {
        return heroMenu.currentLevel.ToString() + "_" + heroClass.UniqueID + "_" + heroPath.UniqueID;
    }


    public Card CreateCardToOwn(Card cardTemplate)
    {
        Card card = Instantiate(cardTemplate, transform);
        card.player = MenuControl.Instance.battleMenu.player1;
        heroMenu.cardsOwned.Add(card);
        return card;
    }

    public void RemoveCardToOwn(Card card)
    {
        heroMenu.cardsOwned.Remove(card);
        Destroy(card.gameObject);
    }
    public void DeckLeftPressed()
    {
        startingDeckInt -= 1;
        if (startingDeckInt < 0) startingDeckInt = GetUnlockbleCardList().startingDecks.Count - 1;
        SelectPath(heroPath);
    }

    public void DeckRightPressed()
    {
        startingDeckInt += 1;
        if (startingDeckInt == GetUnlockbleCardList().startingDecks.Count) startingDeckInt = 0;
        SelectPath(heroPath);
    }

    public void PathLeftPressed()
    {
        
        MenuControl.Instance.indicatorMenu.ShowIndicator(MenuControl.Instance.GetLocalizedString("DemoSelectLockedHero"));
        // int nextIndex = heroPaths.IndexOf(heroPath) - 1;
        // if (nextIndex <= -1) nextIndex = heroPaths.Count - 1;
        //
        //
        // SelectPath(heroPaths[nextIndex]);
    }
    
    public void PathRightPressed()
    {
        MenuControl.Instance.indicatorMenu.ShowIndicator(MenuControl.Instance.GetLocalizedString("DemoSelectLockedHero"));
        // int nextIndex = heroPaths.IndexOf(heroPath) + 1;
        // if (nextIndex >= heroPaths.Count) nextIndex = 0;
        //
        //
        // SelectPath(heroPaths[nextIndex]);
    }

    

    public override void SelectVisibleCard(VisibleCard vc, bool withClick = true)
    {
        base.SelectVisibleCard(vc, withClick);
        selectedVisibleCard = vc;
        if (!withClick)
        {
            MenuControl.Instance.infoMenu.ShowInfo(vc);
        }
    }

    public override void DeSelectVisibleCard(VisibleCard vc, bool withClick = true)
    {
        base.DeSelectVisibleCard(vc, withClick);
        MenuControl.Instance.infoMenu.HideMenu();
    }


    public void RenderDeck()
    {
        
        
        int ii = 0;
        for (; ii < originalStartingCards.Count; ii += 1)
        {
            {
                VisibleCard vc = decklistVCs.GetComponentsInChildren<VisibleCard>()[ii];
                vc.RenderCardForMenu(originalStartingCards[ii]);
                vc.Show();
            }
        }

        for (; ii < decklistVCs.GetComponentsInChildren<VisibleCard>().Length; ii++)
        {
            VisibleCard vc = decklistVCs.GetComponentsInChildren<VisibleCard>()[ii];
            vc.Hide();
        }
        deckNameText.text = GetUnlockbleCardList().startingDecks[startingDeckInt].GetName();
    }

}