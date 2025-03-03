using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LevelUpMenu : BasicMenu
{

    public List<Card> variableTalentsAcquired = new List<Card>();

    public Card extraTalentChoicesTalent;
    public Card extraXPTalent;
    public Card extraSneakingTalent;
    public Card extraTreasureTalent;
    public Card extraFreeBarterTalent;
    public Card extraFlareStoneEOBTalent;
    public Card extraDropRate23CardsTalent;
    public Card extraDropRate23MinionsTalent;
    public Card extraDropRate23SpellsTalent;
    public Card extraSpellDropTalent;
    public Card extraMinionDropTalent;
    public Card extraHPPerLevelTalent;
    public Card extra3DropNoPickTalent;
    public Card lessUpgradeCostTalent;
    public Card strangeKeyForFloor4;

    public Text heroNameText;
    public Text heroClassText;
    public Image heroClassIcon;
    public Image heroImage;
    public SkeletonGraphic largeHeroAnimation;
    public Slider heroXPSlider;
    public Text heroXPLabel;
    public Text heroLevelLable;
    public Transform talentsHolder;
    public Transform statsTextHolder;
    public VisibleCard weaponVC;
    public List<VisibleArtifactSlot> artifactSlots = new List<VisibleArtifactSlot>();

    public Text cardListText;
    public Transform deckCardHolder;
    public RectTransform deckListPanel;
    public VisibleDeckCard deckCardPrefab;
    public Image deckToggleButtonImage;
    public Sprite deckListShowSprite;
    public Sprite deckListHideSprite;
    public bool showDeckList;

    public void ShowLevelUp()
    {
        GenerateVariableTalents();
    }

    public void ContinueLevelUp()
    {
        MenuControl.Instance.heroMenu.LevelUp(); //Saves

        MenuControl.Instance.infoMenu.HideMenu();
        HideMenu();

        Doozy.Engine.Soundy.SoundyManager.Play(MenuControl.Instance.buffActionSound);
        MenuControl.Instance. StartCoroutine(ContinueLevelUpEnumerator());
    }

    private IEnumerator ContinueLevelUpEnumerator()
    {
        yield return new WaitForSeconds(0.1f);
        MenuControl.Instance.adventureMenu.ContinueAdventure();
    }

    private void Awake()
    {
        List<Card> talents = new List<Card>
        {
            extraTalentChoicesTalent,
            extraXPTalent,
            extraSneakingTalent,
            extraTreasureTalent,
            extraFreeBarterTalent,
            extraFlareStoneEOBTalent,
            extraDropRate23CardsTalent,
            extraDropRate23MinionsTalent,
            extraDropRate23SpellsTalent,
            extraSpellDropTalent,
            extraMinionDropTalent,
            extraHPPerLevelTalent,
            extra3DropNoPickTalent,
            lessUpgradeCostTalent,
            strangeKeyForFloor4
        };

        foreach (var talent in talents)
        {
            if (talent == null)
            {
                Debug.LogError($"One of the talents is null in level up manager");
            }
        }
    }

    void GenerateVariableTalents()
    {
        int seed = MenuControl.Instance.ApplySeed();
        Random.InitState(seed + MenuControl.Instance.heroMenu.currentLevel);

        int totalToChooseFrom = 3 + MenuControl.Instance.CountOfCardsInList(extraTalentChoicesTalent, MenuControl.Instance.levelUpMenu.variableTalentsAcquired);

        List<Card> talentsToShow = new List<Card>();

        // var allTalents = MenuControl.Instance.heroMenu.heroClass.midpointTalents;
        // allTalents.AddRange(MenuControl.Instance.heroMenu.heroClass.advancedTalents);
        // allTalents.AddRange(MenuControl.Instance.heroMenu.heroClass.basicTalents);
        // foreach (var talent in allTalents)
        // {
        //     if (talent.UniqueID == "MageBasic02")
        //     {
        //         talentsToShow.Add(talent);
        //         break;
        //     }
        // }
        
        if (MenuControl.Instance.heroMenu.currentLevel == 4 - 1 || MenuControl.Instance.heroMenu.currentLevel == 8 - 1 || MenuControl.Instance.heroMenu.currentLevel == 12 - 1)
        {
            var potentialMidCards = new List<Card>();
            foreach (Card card in MenuControl.Instance.heroMenu.heroClass.midpointTalents)
            {
                if (((Skill)card).GetMaximumPickAllowed() == 0 || ((Skill)card).GetMaximumPickAllowed() > MenuControl.Instance.CountOfCardsInList(card, MenuControl.Instance.levelUpMenu.variableTalentsAcquired))
                {
                    potentialMidCards.Add(card);
                }
            }

            if (potentialMidCards.Count <= totalToChooseFrom)
            {
                talentsToShow.AddRange(potentialMidCards);
            }
            else
            {

                while (talentsToShow.Count < totalToChooseFrom)
                {
                    Card talent = potentialMidCards[Random.Range(0, potentialMidCards.Count)];
                    if (!talentsToShow.Contains(talent))
                    {
                        //if (((Skill)talent).GetMaximumPickAllowed() == 0 || ((Skill)talent).GetMaximumPickAllowed() > MenuControl.Instance.CountOfCardsInList(talent, MenuControl.Instance.levelUpMenu.variableTalentsAcquired))
                        {
                            talentsToShow.Add(talent);
                        }
                    }
                }
            }
        }
        else
        {
           // bool hasAdvancedTalent = (Random.Range(0, 3) == 0 );
            //if (hasAdvancedTalent)
            {
                int tried = 100;
                while (tried>=0)
                {
                    tried -= 1;
                    Card talent = MenuControl.Instance.heroMenu.heroClass.advancedTalents[Random.Range(0, MenuControl.Instance.heroMenu.heroClass.advancedTalents.Count)];
                    if (!talentsToShow.Contains(talent))
                    {
                        if (((Skill)talent).GetMaximumPickAllowed() == 0 || ((Skill)talent).GetMaximumPickAllowed() > MenuControl.Instance.CountOfCardsInList(talent, MenuControl.Instance.levelUpMenu.variableTalentsAcquired))
                        {
                            talentsToShow.Add(talent);
                            break;
                        }
                    }
                }
            }
            while (talentsToShow.Count < totalToChooseFrom)
            {
                Card talent = MenuControl.Instance.heroMenu.heroClass.basicTalents[Random.Range(0, MenuControl.Instance.heroMenu.heroClass.basicTalents.Count)];
                if (!talentsToShow.Contains(talent))
                {
                    if (((Skill)talent).GetMaximumPickAllowed() == 0 || ((Skill)talent).GetMaximumPickAllowed() > MenuControl.Instance.CountOfCardsInList(talent, MenuControl.Instance.levelUpMenu.variableTalentsAcquired))
                    {
                        talentsToShow.Add(talent);
                    }
                }
            }
        }

        List<string> buttonLabels = new List<string>();
        buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Confirm"));

        List<System.Action> actions = new List<System.Action>();
        actions.Add(() =>
        {

            Card selectedSkill = MenuControl.Instance.lootMenu.cardsToShow[MenuControl.Instance.lootMenu.selectedVisibleCardInts[0]];
            variableTalentsAcquired.Add(selectedSkill);
            if (selectedSkill is Skill)
            {
                if (((Skill)selectedSkill).abilityToPerform != null)
                    ((Skill)selectedSkill).abilityToPerform.PerformAbility(null, null, 0);
            }
            else
            {
                MenuControl.Instance.heroMenu.AddCardToDeck(selectedSkill);
            }


            //if (!((Skill)selectedSkill).requiresChoice)
            {
                ContinueLevelUp();
            }
            MenuControl.Instance.LogEvent("ChooseTalent_" + selectedSkill.UniqueID);// + MenuControl.Instance.heroMenu.GetLevelClassPathString());
        });

        // MenuControl.Instance.cardChoiceMenu.ShowChoice(talentsToShow, buttonLabels, actions, MenuControl.Instance.GetLocalizedString("Select a talent").Replace("XX", MenuControl.Instance.heroMenu.heroClass.GetHPGainPerLevel().ToString()), 
        //     1, 1, false, false, true/*,MenuControl.Instance.GetLocalizedString("PickTalentTitle")*/);
        MenuControl.Instance.lootMenu.ShowChoice(talentsToShow, buttonLabels, actions,
            MenuControl.Instance.GetLocalizedString("Select a talent").Replace("XX",
                MenuControl.Instance.heroMenu.heroClass.GetHPGainPerLevel().ToString()),
            1, 1, false, false, true, false,MenuControl.Instance.GetLocalizedString("PickTalentTitle"));
    }

    public void ShowCharacterSheet()
    {
        ShowMenu();

        //Name and Image
        heroNameText.text = MenuControl.Instance.heroMenu.hero.GetName();
        heroClassText.text = MenuControl.Instance.heroMenu.heroClass.GetName();
        heroClassIcon.sprite = MenuControl.Instance.heroMenu.heroClass.iconSprite;
        //heroImage.sprite = MenuControl.Instance.heroMenu.heroClass.largeHeroSelectSprite;
        
        largeHeroAnimation.skeletonDataAsset = MenuControl.Instance.heroMenu.heroClass.spineAsset;
        // Reinitialize the skeleton with the new data asset
        largeHeroAnimation.Initialize(true);
        largeHeroAnimation.AnimationState.SetAnimation(0, "idle", true);

        heroXPLabel.text = MenuControl.Instance.heroMenu.currentXPForLevel().ToString() + " / " +
            MenuControl.Instance.heroMenu.xPForNextLevel();
        heroXPSlider.value = MenuControl.Instance.heroMenu.currentXPForLevel() /(float) MenuControl.Instance.heroMenu.xPForNextLevel();
        heroLevelLable.text = MenuControl.Instance.heroMenu.currentLevel.ToString();
        

        //Draw Talents
        List<Card> talentsToShow = new List<Card>();
        if (MenuControl.Instance.heroMenu.gameObject.activeInHierarchy)
        {
            talentsToShow.AddRange(MenuControl.Instance.heroMenu.heroClass.basicTalents);
        }
        else
        {
            talentsToShow.AddRange(variableTalentsAcquired);
        }
        /*if (talentsToShow.Count > 15)
        {
            talentsToShow.RemoveRange(15, talentsToShow.Count - 15);
        }*/

        foreach (Transform child in talentsHolder)
        {
            Destroy(child.gameObject);
        }

        Dictionary<string, int> talentsDictToShow = new Dictionary<string, int>();
        Dictionary<string, Card> talentsDictToCard = new Dictionary<string, Card>();
        
        foreach (Card talent in talentsToShow)
        {
            if (!talentsDictToCard.ContainsKey(talent.UniqueID))
            {
                talentsDictToCard[talent.UniqueID] = talent;
                talentsDictToShow[talent.UniqueID] = 0;
            }

            talentsDictToShow[talent.UniqueID]++; 
        }

        foreach (var pair in talentsDictToShow)
        {
            var talentName = pair.Key;
            var talent = talentsDictToCard[talentName];
            VisibleCard vc = Instantiate(MenuControl.Instance.visibleCardPrefab, talentsHolder);
            vc.RenderCardForMenu(talent);
            if (pair.Value > 1)
            {
                vc.SetTalentCardCount(pair.Value);
            }
        }
        // Size talent holder container
        RectTransform holderTransform = talentsHolder.GetComponent<RectTransform>();
        GridLayoutGroup layoutGroup = talentsHolder.GetComponent<GridLayoutGroup>();
        float height = layoutGroup.cellSize.y * (holderTransform.childCount / layoutGroup.constraintCount);

        holderTransform.rect.Set(holderTransform.rect.x, holderTransform.rect.y, holderTransform.rect.width, height);

        //STATS
        statsTextHolder.GetChild(0).GetComponent<Text>().text = MenuControl.Instance.heroMenu.currentLevel.ToString();
        statsTextHolder.GetChild(2).GetComponent<Text>().text = MenuControl.Instance.heroMenu.initialMana.ToString();
        statsTextHolder.GetChild(3).GetComponent<Text>().text = MenuControl.Instance.heroMenu.hero.GetInitialPower().ToString();
        statsTextHolder.GetChild(4).GetComponent<Text>().text = MenuControl.Instance.heroMenu.hero.GetHP().ToString() + " / " + MenuControl.Instance.heroMenu.hero.GetInitialHP().ToString();
        statsTextHolder.GetChild(1).GetComponent<Text>().text = MenuControl.Instance.heroMenu.drawsPerTurn.ToString();
        statsTextHolder.GetChild(5).GetComponent<Text>().text = MenuControl.Instance.heroMenu.heroClass.GetHPGainPerLevel().ToString();

        //Weapon
        if (MenuControl.Instance.heroMenu.hero.weapon != null)
        {
            weaponVC.RenderCardForMenu(MenuControl.Instance.heroMenu.hero.weapon);
        }

        //Artifacts
        for (int ii = 0; ii < artifactSlots.Count; ii += 1)
        {
            artifactSlots[ii].gameObject.SetActive(MenuControl.Instance.heroMenu.artifactsEquipped.Count > ii);
            artifactSlots[ii].transform.parent.gameObject.SetActive(MenuControl.Instance.heroMenu.artifactSlots > ii);

            if (MenuControl.Instance.heroMenu.artifactsEquipped.Count > ii)
            {
                artifactSlots[ii].RenderArtifact(MenuControl.Instance.heroMenu.artifactsEquipped[ii]);
            }
        }

        RenderDeckList();
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

        if (!MenuControl.Instance.heroMenu.gameObject.activeInHierarchy)
        {
            if (vc.card.isItem)
            {
                MenuControl.Instance.itemsMenu.ShowMyArtifacts();
            }
            // else if (vc.card is Deprecated Weapon)
            // {
            //     MenuControl.Instance.weaponsMenu.ShowMyWeapons();
            // }
        }
    }
//this is used in inspector
    public void EditArtifacts()
    {
        if (!MenuControl.Instance.heroMenu.gameObject.activeInHierarchy)
        {
    
            MenuControl.Instance.itemsMenu.ShowMyArtifacts();
    
        }
    }

    public void ToggleDeckList()
    {
        showDeckList = !showDeckList;

        float yPos = -1140f;

#if !UNITY_STANDALONE
        yPos = -1380f;
#endif

        LeanTween.moveY(deckListPanel, showDeckList ? 0f : yPos, 0.35f).setEaseInOutQuad();

        deckToggleButtonImage.sprite = showDeckList ? deckListShowSprite : deckListHideSprite;

    }

    public void RenderDeckList()
    {

        foreach (Transform child in deckCardHolder)
        {
            Destroy(child.gameObject);
        }
        List<Card> cardsToShow = new List<Card>();
        cardsToShow.AddRange(MenuControl.Instance.heroMenu.cardsOwned);

        List<Card> uniqueCards = new List<Card>();
        foreach (Card card in cardsToShow)
        {
            int count = MenuControl.Instance.CountOfCardsInList(card, uniqueCards);
            if (count == 0)
            {
                uniqueCards.Add(card);
            }
        }

        List<Card> cardsToShow2 = new List<Card>();
        cardsToShow2.AddRange(uniqueCards);

        foreach (Card card in cardsToShow2)
        {
            VisibleDeckCard deckCard = Instantiate(deckCardPrefab, deckCardHolder);

            int count = MenuControl.Instance.CountOfCardsInList(card, cardsToShow);
            deckCard.RenderDeckCard(card, count);
        }
        cardListText.text = MenuControl.Instance.heroMenu.cardsOwned.Count + " " + MenuControl.Instance.GetLocalizedString("Cards");

    }

}