using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LootMenu : BasicMenu
{
    public GameObject unlootBK;
    public TMP_Text promptText;
    public TMP_Text extraPromptText;
    public Transform cardHolder;
    public List<GameObject> buttonObjects = new List<GameObject>();

    public List<Card> cardsToShow = new List<Card>();
    public List<VisibleCard> visibleCardsShown = new List<VisibleCard>();
    public List<System.Action> actionsToPerform = new List<System.Action>();

    public GameObject upgradeArrow;

    public int minSelectedCards;
    public int maxSelectedCards;
    public bool firstButtonNoCheck;
    public List<string> buttonLabels;

    public int freePicksTotal;
    public List<int> selectedVisibleCardInts = new List<int>();

    public GameObject myDeckButton;
    public GameObject myWeaponsButton;

    public Doozy.Engine.Soundy.SoundyData revealSound;
    public Doozy.Engine.Soundy.SoundyData flareStoneUsedSound;
    public Text flareStonesText;
    public Text flareStoneShardsText;
    public Text freeStonesText;
    public Text titleLabel;

    public float delayTime = 0.5f;

    public Transform promptHolder;

    public GameObject unlockPanel;
    public Image unlockPanelImage;

    int FreePicksRemaining()
    {
        return Mathf.Max(0, freePicksTotal - selectedVisibleCardInts.Count);
    }

    public void RenderFlareStones()
    {
        flareStonesText.text = "x" + (MenuControl.Instance.heroMenu.flareStones -
                                      Mathf.Max(0, selectedVisibleCardInts.Count - freePicksTotal)).ToString();
        flareStoneShardsText.text = "x" + MenuControl.Instance.heroMenu.flareStoneShards.ToString();
        freeStonesText.text = "x" + FreePicksRemaining().ToString();


        GetComponentInChildren<HeroInfoPanel>(true).parentTemporaryDiff = (selectedVisibleCardInts.Count < freePicksTotal)
            ? 0
            : -(selectedVisibleCardInts.Count - freePicksTotal) * 1;
        GetComponentInChildren<HeroInfoPanel>(true).updateHeroInfo();
    }

    private bool isLoot;

    static public int GetChallengeRewardValue(int[] list, AdventureItemEncounter encounter)
    {
        if (encounter == null)
        {
            Debug.LogError("Null encounter");
            return 1;
        }

        var id = encounter.isBoss ? 2 : (encounter.isElite ? 1 : 0);
        if (id >= list.Length)
        {
            id = list.Length - 1;
        }

        return list[id];
    }
    public List<GameObject> vfxObjects = new List<GameObject>();
    void ShowParticle(int ii, int showRevealVFXType)
    {
        VisibleCard vc = visibleCardsShown[ii];
       // vc.Hide();
        LeanTween.delayedCall(/*(ii * 0.15f) + 0.4f*/0.8f / (float)(cardsToShow.Count - ii), () =>
        {
            GameObject vfx = Instantiate(MenuControl.Instance.cardChoiceMenu. vfxCardShowingPrefabs[showRevealVFXType], MenuControl.Instance.transform) as GameObject;
            vfx.transform.position = vc.transform.position + (Vector3.back * 10f);
            vfxObjects.Add(vfx);
            Destroy(vfx, 3f);

        });
    }
    public void ShowChoice(List<Card> cardsToShow, List<string> buttonLabels, List<System.Action> actionsToPerform,
        string textToShow, int minSelectedCards, int maxSelectedCards, bool firstButtonNoCheck, bool showRevealVFX,
        bool showMyDeckButton, bool isLoot, string titleToShow, string extraText = null,bool isUnlockCard = false)
    {
        ShowMenu();

        
        GetComponentInChildren<HeroInfoPanel>(true).gameObject.SetActive(!isUnlockCard);
        unlockPanel.SetActive(isUnlockCard);
        unlockPanelImage.sprite = MenuControl.Instance.csvLoader.playerCardSprite(MenuControl.Instance.heroMenu.hero);
        GetComponentInChildren<HeroInfoPanel>(true).parentTemporaryDiff = 0;
        GetComponentInChildren<HeroInfoPanel>(true).updateHeroInfo();
        if (isLoot)
        {
            unlootBK.SetActive(false);
        }
        else
        {
            unlootBK.SetActive(true);
        }

        LeanTween.delayedCall(0.7f, () => //Cludge to fix view not showing
        {
            if (GetComponent<CanvasGroup>().alpha != 1f)
            {
                GetComponent<CanvasGroup>().alpha = 1f;
            }

            if (!GetComponent<Canvas>().enabled)
            {
                GetComponent<Canvas>().enabled = true;
            }

            if (!GetComponent<GraphicRaycaster>().enabled)
            {
                GetComponent<GraphicRaycaster>().enabled = true;
            }
        });

        //faceUpCards = 0;
        selectedVisibleCardInts.Clear();
        visibleCardsShown.Clear();

        promptText.text = textToShow;
        titleLabel.text = titleToShow;
        this.cardsToShow = cardsToShow;
        this.actionsToPerform = actionsToPerform;
        this.minSelectedCards = minSelectedCards;
        this.maxSelectedCards = maxSelectedCards;
        this.firstButtonNoCheck = firstButtonNoCheck;
        this.isLoot = isLoot;
        this.buttonLabels = buttonLabels;

        foreach (Transform child in cardHolder)
        {
            Destroy(child.gameObject);
        }

        for (int ii = 0; ii < promptHolder.childCount; ii += 1)
        {
            promptHolder.GetChild(ii).GetChild(0).GetChild(0).gameObject.SetActive(false);
        }

        for (int ii = 0; ii < cardsToShow.Count; ii += 1)
        {
            Card card = cardsToShow[ii];

            if (card == null)
            {
                Instantiate(upgradeArrow, cardHolder);
            }
            else
            {
                VisibleCard vc = Instantiate(MenuControl.Instance.visibleCardPrefab, cardHolder);

                vc.RenderCardForMenu(card);
                visibleCardsShown.Add(vc);
                AddNewCardIndicator(vc);
                vc.RenderCardForMenu(card, true);

                LeanTween.delayedCall((1.2f / (float)(cardsToShow.Count - ii)),
                    () =>
                    {
                        LeanTween.rotateAroundLocal(vc.gameObject, Vector3.up, 180, 0.35f).setLoopPingPong(1)
                            .setEaseInSine();
                    });
                LeanTween.delayedCall(0.35f + ((1.2f / (float)(cardsToShow.Count - ii))),
                    () => { vc.RenderCardForMenu(card); });

                if (card.IsTreasure())
                {
                    ShowParticle(ii, 1);
                }
            }
        }
        

        extraPromptText.gameObject.SetActive(false);
        if (isLoot)
        {
            //special challenge - extra stone
            if (MenuControl.Instance.eventMenu.isSpecialChallenge &&
                MenuControl.Instance.eventMenu.specialChallengeReward ==
                AdventureItemEncounter.EncounterSpeicallChallengeRewardType.ExtraStone)
            {
                extraPromptText.gameObject.SetActive(true);
                extraPromptText.text = extraText;
            }
        }


        if (isLoot)
        {
            StartCoroutine(RenderButtonsWithDelay());
        }
        else
        {
            for (int ii = 0; ii < buttonObjects.Count; ii += 1)
            {
                buttonObjects[ii].SetActive(ii < 1);
                buttonObjects[ii].transform.GetComponentInChildren<Text>().text =
                    ii < buttonLabels.Count ? buttonLabels[ii] : "";
            }

            RenderButtons();
        }

        cardHolder.gameObject.SetActive(cardsToShow.Count > 0);

        myDeckButton.SetActive(false);
        myWeaponsButton.SetActive(false);

        RenderFlareStones();

        for (int ii = 0; ii < promptHolder.childCount; ii += 1)
        {
            if (cardsToShow.Count > ii && cardsToShow[ii] != null)
            {
                promptHolder.GetChild(ii).gameObject.SetActive(true);

                promptHolder.GetChild(ii).GetComponentInChildren<Text>().text = "???";

                if (cardsToShow[ii].cardTags.Contains(MenuControl.Instance.treasureTag))
                {
                    promptHolder.GetChild(ii).GetComponentInChildren<Text>().text =
                        MenuControl.Instance.GetLocalizedString("Treasure");
                }


                else if (cardsToShow[ii].cardTags.Contains(MenuControl.Instance.artifactTag))
                    promptHolder.GetChild(ii).GetComponentInChildren<Text>().text =
                        MenuControl.Instance.GetLocalizedString("Artifact");

                else if (cardsToShow[ii].cardTags.Contains(MenuControl.Instance.potionTag))
                    promptHolder.GetChild(ii).GetComponentInChildren<Text>().text =
                        MenuControl.Instance.GetLocalizedString("Potion");

                else if (cardsToShow[ii] is NewWeapon)
                    promptHolder.GetChild(ii).GetComponentInChildren<Text>().text =
                        MenuControl.Instance.GetLocalizedString("Weapon");

                else if (cardsToShow[ii] is Minion)
                    promptHolder.GetChild(ii).GetComponentInChildren<Text>().text =
                        MenuControl.Instance.GetLocalizedString("Minion");

                else if (cardsToShow[ii].cardTags.Contains(MenuControl.Instance.spellTag))
                    promptHolder.GetChild(ii).GetComponentInChildren<Text>().text =
                        MenuControl.Instance.GetLocalizedString("Spell");
            }
            else
            {
                promptHolder.GetChild(ii).gameObject.SetActive(false);
            }
        }
    }

    public override void SelectVisibleCard(VisibleCard vc, bool withClick = true)
    {
        base.SelectVisibleCard(vc, withClick);
        if (GetComponent<Doozy.Engine.UI.UIView>().IsHiding) return;

        MenuControl.Instance.infoMenu.ShowInfo(vc);

        if (FreePicksRemaining() > 0)
        {
            freeStonesText.transform.parent.GetChild(0).transform.localRotation = Quaternion.Euler(Vector3.zero);
            LeanTween.rotateZ(freeStonesText.transform.parent.GetChild(0).gameObject, 15f, 0.3f).setEaseInOutSine()
                .setLoopPingPong(-1);
        }
        else
        {
            flareStonesText.transform.parent.GetChild(0).transform.localRotation = Quaternion.Euler(Vector3.zero);
            LeanTween.rotateZ(flareStonesText.transform.parent.GetChild(0).gameObject, 15f, 0.3f).setEaseInOutSine()
                .setLoopPingPong(-1);
        }
    }

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

    public override void DeSelectVisibleCard(VisibleCard vc, bool withClick = true)
    {
        //base.DeSelectVisibleCard(vc, withClick);
        MenuControl.Instance.infoMenu.HideMenu();

        if (FreePicksRemaining() > 0)
        {
            LeanTween.cancel(freeStonesText.transform.parent.GetChild(0).gameObject);
            freeStonesText.transform.parent.GetChild(0).transform.localRotation = Quaternion.Euler(Vector3.zero);
        }
        else
        {
            LeanTween.cancel(flareStonesText.transform.parent.GetChild(0).gameObject);
            flareStonesText.transform.parent.GetChild(0).transform.localRotation = Quaternion.Euler(Vector3.zero);
        }
    }

    void AddNewCardIndicator(VisibleCard vc)
    {
        if (!MenuControl.Instance.progressMenu.cardsDiscovered.Contains(vc.card.UniqueID) && !vc.card.temporaryOnly)
        {
            promptHolder.GetChild(visibleCardsShown.IndexOf(vc)).GetChild(0).GetChild(0).gameObject.SetActive(true);
            MenuControl.Instance.progressMenu.cardsDiscovered.Add(vc.card.UniqueID);
            MenuControl.Instance.heroMenu.cardsDiscoveredThisRun += 1;
        }
    }

    public override void ClickVisibleCard(VisibleCard vc)
    {
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
                vc.HighlightCheck();
                LeanTween.scale(vc.gameObject, Vector3.one * 1.0f, 0.15f).setLoopPingPong(1).setEaseInSine();
            }
            else if (maxSelectedCards == 1)
            {
                ClickVisibleCard(visibleCardsShown[selectedVisibleCardInts[0]]);
                selectedVisibleCardInts.Add(visibleCardsShown.IndexOf(vc));
                vc.HighlightCheck();
                LeanTween.scale(vc.gameObject, Vector3.one * 1.0f, 0.15f).setLoopPingPong(1).setEaseInSine();
            }

            if (FreePicksRemaining() > 0)
            {
                Doozy.Engine.Soundy.SoundyManager.Play(flareStoneUsedSound);
            }
            else
            {
                Doozy.Engine.Soundy.SoundyManager.Play(revealSound);
            }
        }

        RenderButtons();
        RenderFlareStones();
        //}
    }

    // public void RenderButtons()
    // {
    //     buttonObjects[0].GetComponent<Button>().interactable = true;
    //     //selectedVisibleCardInts.Count == 0;
    //     //buttonObjects[1].GetComponent<Button>().interactable = selectedVisibleCardInts.Count >= minSelectedCards &&
    //     //                                                       selectedVisibleCardInts.Count <= maxSelectedCards;
    //     if (selectedVisibleCardInts.Count >= minSelectedCards &&
    //         selectedVisibleCardInts.Count <= maxSelectedCards)
    //     {
    //         buttonObjects[0].GetComponentInChildren<Text>().text = buttonLabels[1];
    //     }
    //     else
    //     {
    //         buttonObjects[0].GetComponentInChildren<Text>().text = buttonLabels[0];
    //     }
    //     
    //     promptText.text = getLootPrompt();
    //     
    // }
    IEnumerator RenderButtonsWithDelay()
    {
        HideButtons();

        promptText.text = getLootPrompt();
        yield return new WaitForSeconds(delayTime);

        RenderButtons();
    }

    public void HideButtons()
    {
        foreach (var button in buttonObjects)
        {
            button.gameObject.SetActive(false);
        }
    }

    public void RenderButtons()
    {
        if (isRealLoot())
        {
            //buttonObjects[0].GetComponent<Button>().interactable = true;
            //selectedVisibleCardInts.Count == 0;
            //buttonObjects[1].GetComponent<Button>().interactable = selectedVisibleCardInts.Count >= minSelectedCards &&
            //                                                       selectedVisibleCardInts.Count <= maxSelectedCards;
            if (selectedVisibleCardInts.Count >= minSelectedCards &&
                selectedVisibleCardInts.Count <= maxSelectedCards)
            {
                if (selectedVisibleCardInts.Count >= freePicksTotal)
                {
                    //全选了
                    buttonObjects[0].SetActive(true);
                    buttonObjects[0].GetComponent<Button>().interactable = true;
                    buttonObjects[2].SetActive(false);
                    buttonObjects[0].GetComponentInChildren<Text>().text = buttonLabels[1];
                }
                else
                {
                    //选了，但没全选
                    //显示获得的flare数量
                    buttonObjects[0].SetActive(false);
                    buttonObjects[0].GetComponent<Button>().interactable = false;
                    buttonObjects[2].GetComponent<Button>().interactable = true;
                    buttonObjects[2].SetActive(true);
                    buttonObjects[2].GetComponentInChildren<Text>().text = buttonLabels[1];
                    buttonObjects[2].transform.Find("Loot").Find("LootCount").GetComponent<Text>().text = "+"+(freePicksTotal - selectedVisibleCardInts.Count);
                }
            }
            else
            {
                buttonObjects[2].SetActive(true);
                buttonObjects[2].GetComponent<Button>().interactable = true;
                buttonObjects[0].SetActive(false);
                buttonObjects[2].GetComponentInChildren<Text>().text = buttonLabels[0];
                buttonObjects[2].transform.Find("Loot").Find("LootCount").GetComponent<Text>().text = "+"+(freePicksTotal - selectedVisibleCardInts.Count);
            }

            promptText.text = getLootPrompt();
        }
        else
        {
            for (int ii = 0; ii < buttonObjects.Count; ii += 1)
            {
                buttonObjects[ii].SetActive(ii < 1);
                buttonObjects[ii].transform.GetComponentInChildren<Text>().text =
                    ii < buttonLabels.Count ? buttonLabels[ii] : "";
            }

            var combineButtonsIntoOne = false;
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
                else if (selectedVisibleCardInts.Count >= minSelectedCards &&
                         selectedVisibleCardInts.Count <= maxSelectedCards)
                {
                    buttonObjects[ii].GetComponent<Button>().interactable = true;
                    if (combineButtonsIntoOne && ii < buttonLabels.Count)
                    {
                        // if combine buttons, and later button should show, update first label text
                        buttonObjects[0].GetComponent<Button>().GetComponentInChildren<Text>().text = buttonLabels[ii];
                    }
                }
                //buttonObjects[ii].GetComponent<Button>().interactable = ((firstButtonNoCheck && ii == 0) || (selectedVisibleCardInts.Count >= minSelectedCards && selectedVisibleCardInts.Count <= maxSelectedCards && !combineButtonsIntoOne));
            }
        }
    }

    public void PressedButton(bool takeCards)
    {
        for (int ii = 0; ii < buttonObjects.Count; ii += 1)
        {
            buttonObjects[ii].GetComponent<Button>().interactable = false;
        }

        if (!isRealLoot())
        {
            CloseMenu();
            if (actionsToPerform.Count > 0)
            {
                actionsToPerform[0]();
            }

            return;
        }

        if (selectedVisibleCardInts.Count >= minSelectedCards &&
            selectedVisibleCardInts.Count <= maxSelectedCards)
        {
            takeCards = true;
        }

        // buttonObjects[0].GetComponent<Button>().interactable = false;
        // buttonObjects[1].GetComponent<Button>().interactable = false;

        CloseMenu();
        int remainingCards = cardsToShow.Count - selectedVisibleCardInts.Count;

        if (takeCards)
        {
            List<string> synergyTags = new List<string>();
            foreach (Card card in MenuControl.Instance.heroMenu.cardsOwned)
            {
                foreach (string synergyTag in card.GetSynergyTags())
                {
                    if (!synergyTags.Contains(synergyTag))
                    {
                        synergyTags.Add(synergyTag);
                    }
                }
            }

            for (int ii = 0; ii < cardsToShow.Count; ii += 1)
            {
                Card card = cardsToShow[ii];
                foreach (string synergyTag in card.GetSynergyTags())
                {
                    if (synergyTags.Contains(synergyTag))
                    {
                        if (!selectedVisibleCardInts.Contains(ii))
                        {
                            MenuControl.Instance.heroMenu.synergisticDropsSkipped += 1;
                        }

                        break;
                    }
                }
            }

            List<VisibleCard> vcsToAnimate = new List<VisibleCard>();
            foreach (int selectedCardInt in selectedVisibleCardInts)
            {
                Card card = visibleCardsShown[selectedCardInt].card;
                MenuControl.Instance.heroMenu.AddCardToDeck(card);
                vcsToAnimate.Add(visibleCardsShown[selectedCardInt]);
            }

            MenuControl.Instance.adventureMenu.AnimateVCsToDeck(vcsToAnimate);
            if (freePicksTotal - selectedVisibleCardInts.Count > 0)
            {
                MenuControl.Instance.heroMenu.addFlareStone(freePicksTotal - selectedVisibleCardInts.Count);
            }
        }
        else
        {
            remainingCards = cardsToShow.Count;
            MenuControl.Instance.heroMenu.addFlareStone(freePicksTotal - selectedVisibleCardInts.Count);
        }

        MenuControl.Instance.heroMenu.skippedLastLootDrops = !takeCards;

        MenuControl.Instance.heroMenu.flareStones = MenuControl.Instance.heroMenu.flareStones -
                                                    Mathf.Max(0, selectedVisibleCardInts.Count - freePicksTotal);

        //MenuControl.Instance.heroMenu.AddFlareStoneShards(remainingCards);

        MenuControl.Instance.dataControl.SaveData();

        if (MenuControl.Instance.heroMenu.seasonsMode)
        {
            if (MenuControl.Instance.heroMenu.seasonsLootCountDown == 0)
            {
                MenuControl.Instance.heroMenu.seasonsLootCountDown = 3;
                ShowSeasonsChoice();
            }
            else
            {
                MenuControl.Instance.heroMenu.seasonsLootCountDown -= 1;
                MenuControl.Instance.dataControl.SaveData();
                MenuControl.Instance.victoryMenu.FinishVictory();
            }
        }
        else
        {
            MenuControl.Instance.victoryMenu.FinishVictory();
        }
    }

    public void EncounterLoot(AdventureItemEncounter encounter)
    {
        bool testSimplifyDeleteAndUpgradeTest = false;
        if (!MenuControl.Instance.testMode)
        {
            testSimplifyDeleteAndUpgradeTest = false;
        }
        //special challenge - upgrade

        if (MenuControl.Instance.eventMenu.isSpecialChallenge &&
            MenuControl.Instance.eventMenu.specialChallengeReward ==
            AdventureItemEncounter.EncounterSpeicallChallengeRewardType.Upgrade
            && !(encounter == null))
        {
            var upgradeCount = GetChallengeRewardValue(MenuControl.Instance.eventMenu.specialChallengeRewardUpgrades,
                encounter);
            List<Card> cardList = new List<Card>();

            foreach (Card card in MenuControl.Instance.heroMenu.cardsOwned)
            {
                if (card.RandomUpgradeCard != null)
                {
                    cardList.Add(card);
                }
            }

            var test = new List<Card>();
            foreach (var card in cardList)
            {
                if (MenuControl.Instance.battleMenu.usedCards.Contains(card.UniqueID))
                {
                    test.Add(card);
                    MenuControl.Instance.battleMenu.usedCards.Remove(card.UniqueID);
                }
            }

            if (!testSimplifyDeleteAndUpgradeTest)
            {
                cardList = test;
            }

            upgradeCount = Math.Min(upgradeCount, cardList.Count);
            var desc = MenuControl.Instance.GetLocalizedString("SpecialChallenge_UpgradeBattleDescription");
            if (upgradeCount == 0)
            {
                desc = MenuControl.Instance.GetLocalizedString("SpecialChallenge_UpgradeBattleDescriptionNone");
            }

            cardsToShow.Clear();

            List<Card> cardsToUpgrade = new List<Card>();
            List<Card> cardsFinishedUpgrade = new List<Card>();
            List<Card> cardsToShowThisTime = new List<Card>();

            
            //显示几个升级选择UpgradeSelectCardView
            
            for (int i = 0; i < upgradeCount; i++)
            {
                var pickCard = cardList.PickItem();
                //  cardList.Remove(pickCard);
                cardsToUpgrade.Add(pickCard);
                cardsFinishedUpgrade.Add(pickCard.RandomUpgradeCard);
            }
            

            if (cardsToUpgrade.Count > 0)
            {
                foreach (var card in cardsToUpgrade)
                {
                    cardsToShowThisTime.Add(card);
                }

                {
                    cardsToShowThisTime.Add(null);
                }
                foreach (var card in cardsFinishedUpgrade)
                {
                    cardsToShowThisTime.Add(card);
                }
            }

            List<string> buttonLabels = new List<string>();
            buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Button_OK"));

            List<System.Action> actions = new List<System.Action>();
            // actions.Add(() => { });
            // actions.Add(() => { });
            actions.Add(() =>
            {
                foreach (var card in cardsToUpgrade)
                {
                    MenuControl.Instance.heroMenu.UpgradeToRandomCardInDeck(card);
                }

                foreach (Transform card in cardHolder)
                {
                    Destroy(card.gameObject);
                }

                MenuControl.Instance.dataControl.SaveData();

                //upgrade

                MenuControl.Instance.victoryMenu.FinishVictory();
            });
            ShowChoice(cardsToShowThisTime, buttonLabels, actions, desc
                , 0,
                -1, true, true, true, true,
                MenuControl.Instance.GetLocalizedString("SpecialChallenge_UpgradeBattleTitle"));
            for (int ii = 0; ii < promptHolder.childCount; ii += 1)
            {
                promptHolder.GetChild(ii).GetChild(0).GetChild(0).gameObject.SetActive(false);
            }

            return;
        }

        //special challenge - delete

        if (MenuControl.Instance.eventMenu.isSpecialChallenge &&
            MenuControl.Instance.eventMenu.specialChallengeReward ==
            AdventureItemEncounter.EncounterSpeicallChallengeRewardType.Delete
            && !(encounter == null))
        {
            var deleteCount = GetChallengeRewardValue(MenuControl.Instance.eventMenu.specialChallengeRewardUpgrades,
                encounter);
            List<Card> cardList = new List<Card>();

            if (!testSimplifyDeleteAndUpgradeTest)
            {
                foreach (Card cardOwned in MenuControl.Instance.heroMenu.cardsOwned)
                {
                    if (cardOwned is Minion minionOwned)
                    {
                        foreach (var destroyed in MenuControl.Instance.battleMenu.destroyedMinions)
                        {
                            if (destroyed.cardTemplate == cardOwned)
                            {
                                cardList.Add(destroyed);
                                
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (Card allcard in MenuControl.Instance.heroMenu.cardsOwned)
                {
                    if (allcard is Minion minion)
                    {
                        cardList.Add(minion);
                    }
                }
            }


            deleteCount = Math.Min(deleteCount, cardList.Count);

            var desc = MenuControl.Instance.GetLocalizedString("SpecialChallenge_DeleteBattleDescription");
            if (deleteCount == 0)
            {
                desc = MenuControl.Instance.GetLocalizedString("SpecialChallenge_DeleteBattleDescriptionNone");
            }


            cardsToShow.Clear();

            List<Card> newcardsToShowThisTime = new List<Card>();
            for (int i = 0; i < deleteCount; i++)
            {
                var pickCard = cardList.PickItem();
                //cardList.Remove(pickCard);
                newcardsToShowThisTime.Add(pickCard);
            }

            List<string> buttonLabels = new List<string>();
            buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Button_OK"));

            List<System.Action> actions = new List<System.Action>();
            // actions.Add(() => { });
            // actions.Add(() => { });
            actions.Add(() =>
            {
                foreach (Card card in cardsToShow)
                {
                    //delete
                    MenuControl.Instance.heroMenu.RemoveCardWithSameTemplate(card);
                    Destroy(card.gameObject);
                }

                MenuControl.Instance.dataControl.SaveData();
                MenuControl.Instance.victoryMenu.FinishVictory();
            });
            ShowChoice(newcardsToShowThisTime, buttonLabels, actions,
                desc, 0,
                -1, true, true, true, true,
                MenuControl.Instance.GetLocalizedString("SpecialChallenge_DeleteBattleTitle"));
            for (int ii = 0; ii < promptHolder.childCount; ii += 1)
            {
                promptHolder.GetChild(ii).GetChild(0).GetChild(0).gameObject.SetActive(false);
            }

            return;
        }

        //special challenge - extra stone
        if (MenuControl.Instance.eventMenu.isSpecialChallenge &&
            MenuControl.Instance.eventMenu.specialChallengeReward ==
            AdventureItemEncounter.EncounterSpeicallChallengeRewardType.ExtraStone
            && !(encounter == null))
        {
            var stoneCount = GetChallengeRewardValue(MenuControl.Instance.eventMenu.specialChallengeRewardStones,
                encounter);

            MenuControl.Instance.heroMenu.addFlareStone(stoneCount);
        }


        {
            freePicksTotal = (MenuControl.Instance.heroMenu.easyMode ? 2 : 1);

            List<Card> cardsToShowThisTime = new List<Card>();
            try
            {
                cardsToShowThisTime.AddRange(MenuControl.Instance.heroMenu.heroClass.GetComponent<ClassSpecialization>()
                    .CalculateVictoryDrops(encounter));
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }

            // if (MenuControl.Instance.heroMenu.ascensionMode >= 4)
            // {
            //     foreach (Card card in cardsToShowThisTime.ToArray())
            //     {
            //         if (card is NewWeapon && Random.Range(0, 10) != 0)
            //         {
            //             cardsToShowThisTime.Remove(card);
            //         }
            //     }
            // }

            if (MenuControl.Instance.heroMenu.ascensionMode >= 9)
            {
                if (encounter.signPosts.Contains("Elite") || encounter.isBoss)
                {
                    //one less minion and spell
                    foreach (Card card in cardsToShowThisTime.ToArray())
                    {
                        if (card is Minion)
                        {
                            cardsToShowThisTime.Remove(card);
                            break;
                        }
                    }

                    foreach (Card card in cardsToShowThisTime.ToArray())
                    {
                        if (card is Castable && card.cardTags.Contains(MenuControl.Instance.spellTag))
                        {
                            cardsToShowThisTime.Remove(card);
                            break;
                        }
                    }
                }
            }

            //Add rewarded drops
            if (encounter.rewardCards.Count > 0)
            {
                //todo: keep this?
                foreach (var card in encounter.rewardCards)
                {
                    if (MenuControl.Instance.heroMenu.isCardUnlockedAndAvailable(card))
                    {
                        Debug.Log($"遭遇战战利品 encounter自带的战利品 {card.GetChineseName()}");
                        cardsToShowThisTime.Add(card);
                    }
                }
            }


            // try
            // {
            //     //Bosses drop 1 artifact
            //     if (encounter.isBoss && MenuControl.Instance.areaMenu.areasVisited <= 2)
            //     {
            //         List<Card> elligibleCards1 = MenuControl.Instance.heroMenu.GetArtifacts(true);
            //         if (elligibleCards1.Count == 0)
            //         {
            //             elligibleCards1 = MenuControl.Instance.heroMenu.GetArtifacts(false);
            //         }
            //
            //         cardsToShowThisTime.Add(elligibleCards1[Random.Range(0, elligibleCards1.Count)]);
            //     }
            // }
            // catch (System.Exception e)
            // {
            //     Debug.LogError(e);
            // }

            if (encounter.isElite || encounter.isBoss)
            {
                try
                {
                    freePicksTotal = (MenuControl.Instance.heroMenu.easyMode ? 3 : 2);

                    MenuControl.Instance.heroMenu.extraLootCardLastOffered = 0;
                    List<Card> eligibleCards1 = new List<Card>();
                    //if (encounter.isBoss || encounter )
                    {
                        eligibleCards1 = MenuControl.Instance.heroMenu.CurrentHeroPotionCards();

                        // eligibleCards1.AddRange(MenuControl.Instance.heroMenu.heroClass
                        //     .GetComponent<ClassSpecialization>()
                        //     .RestrictForAreasVisited(MenuControl.Instance.heroMenu.GetAllLoot()));
                        if (eligibleCards1.Count > 0)
                        {
                            var card = MenuControl.Instance.heroMenu.heroClass.GetComponent<ClassSpecialization>()
                                .ChooseFromElligibleCards(eligibleCards1);
                            Debug.Log($"遭遇战战利品 boss的loot大礼包 {card.GetChineseName()}");
                            cardsToShowThisTime.Add(card);
                            MenuControl.Instance.heroMenu.extraLootCardLastOffered = 0;
                        }
                    }

                    List<Card> eligibleCards2 = MenuControl.Instance.heroMenu.heroClass
                        .GetComponent<ClassSpecialization>().RestrictForAreasVisited(
                            MenuControl.Instance.heroMenu.GetAllUnlockedTreasures());
                    if (eligibleCards2.Count > 0)
                    {
                        var card = MenuControl.Instance.heroMenu.heroClass.GetComponent<ClassSpecialization>()
                            .ChooseFromElligibleCards(eligibleCards2);
                        cardsToShowThisTime.Add(card);

                        Debug.Log($"遭遇战战利品 boss或elite的宝藏卡 {card.GetChineseName()}");
                        // cardsToShowThisTime.Add(MenuControl.Instance.heroMenu.heroClass.GetComponent<ClassSpecialization>()
                        //     .ChooseFromElligibleCards(eligibleCards2));

                        MenuControl.Instance.heroMenu.dropsSinceLastTreasureDropped = 0;
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                }
            }
            else //normal encounters
            {
                try
                {
                    //If havent dropped a treasure last 4 times
                    float treasureMod = 1f;
                    int treasureModTalents = MenuControl.Instance.CountOfCardsInList(
                        MenuControl.Instance.levelUpMenu.extraTreasureTalent,
                        MenuControl.Instance.levelUpMenu.variableTalentsAcquired);
                    if (treasureModTalents > 0)
                    {
                        treasureMod = treasureMod * Mathf.Pow(1.5f, treasureModTalents);
                    }

                    if (MenuControl.Instance.heroMenu.dropsSinceLastTreasureDropped >= 4 || Random.Range(1, 101) <=
                        (6 + (9 * MenuControl.Instance.heroMenu.dropsSinceLastTreasureDropped)) * treasureMod)
                    {
                        Debug.Log(
                            $"遭遇战dropsSinceLastTreasureDropped为{MenuControl.Instance.heroMenu.dropsSinceLastTreasureDropped},将会得到卡片");
                        List<Card> eligibleCards1 = MenuControl.Instance.heroMenu.GetAllUnlockedTreasures();
                        var card = MenuControl.Instance.heroMenu.heroClass.GetComponent<ClassSpecialization>()
                            .ChooseFromElligibleCards(eligibleCards1);
                        cardsToShowThisTime.Add(card);

                        Debug.Log($"遭遇战战利品 普通怪的宝藏卡 {card.GetChineseName()}");
                        MenuControl.Instance.heroMenu.dropsSinceLastTreasureDropped = 0;
                    }
                    else
                    {
                        Debug.Log(
                            $"遭遇战dropsSinceLastTreasureDropped为{MenuControl.Instance.heroMenu.dropsSinceLastTreasureDropped},没有得到卡片");
                        MenuControl.Instance.heroMenu.dropsSinceLastTreasureDropped += 1;
                    }

                    // if (Random.Range(1, 101) <= 6 + (9 * MenuControl.Instance.heroMenu.extraLootCardLastOffered))
                    // {
                    //     Debug.Log($"遭遇战extraLootCardLastOffered为{MenuControl.Instance.heroMenu.extraLootCardLastOffered},将会得到卡片");
                    //     List<Card> eligibleCards1 = new List<Card>();
                    //
                    //     eligibleCards1.AddRange(MenuControl.Instance.heroMenu.heroClass.GetComponent<ClassSpecialization>().RestrictForAreasVisited(MenuControl.Instance.heroMenu.GetAllLoot()));
                    //     if (eligibleCards1.Count > 0)
                    //     {
                    //         var card = MenuControl.Instance.heroMenu.heroClass
                    //             .GetComponent<ClassSpecialization>().ChooseFromElligibleCards(eligibleCards1);
                    //         cardsToShowThisTime.Add(card);
                    //         
                    //         Debug.Log($"遭遇战战利品 普通怪的loot大礼包 {card.GetChineseName()}");
                    //         MenuControl.Instance.heroMenu.extraLootCardLastOffered = 0;
                    //     }
                    // }
                    // else
                    // {
                    //     Debug.Log($"遭遇战extraLootCardLastOffered为{MenuControl.Instance.heroMenu.extraLootCardLastOffered},没有得到卡片");
                    //     MenuControl.Instance.heroMenu.extraLootCardLastOffered += 1;
                    // }
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                }
            }


            try
            {
                //Force one unlocked card loot drop per floor
                var unlockedCards = MenuControl.Instance.heroMenu.heroClass.GetComponent<ClassSpecialization>()
                    .RestrictForAreasVisited(MenuControl.Instance.heroMenu.GetUnlockedCards());
                if (unlockedCards.Count > 0)
                {
                    int integerY = 0;
                    for (int ii = 0; ii < MenuControl.Instance.currentSeed.Length; ii += 1)
                    {
                        char character = MenuControl.Instance.currentSeed[ii];
                        integerY += Mathf.RoundToInt(MenuControl.Instance.glyphs.IndexOf(character) *
                                                     Mathf.Pow(10, ii));
                    }

                    List<AdventureItemEncounter> encounters = new List<AdventureItemEncounter>();
                    foreach (AdventureItem item in MenuControl.Instance.adventureMenu.adventureItems)
                    {
                        if (item is AdventureItemEncounter)
                        {
                            encounters.Add((AdventureItemEncounter)item);
                        }
                    }

                    int lastIndex = encounters.IndexOf(encounter);
                    if (lastIndex > -1 && integerY % encounters.Count == lastIndex)
                    {
                        var card = unlockedCards[
                            Random.Range(0, unlockedCards.Count)];
                        cardsToShowThisTime.Add(card
                        );

                        Debug.Log($"遭遇战战利品 每层必然掉一张解锁的卡 {card.GetChineseName()}");
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }

            try
            {
                if (MenuControl.Instance.heroMenu.skippedLastLootDrops &&
                    MenuControl.Instance.levelUpMenu.variableTalentsAcquired.Contains(MenuControl.Instance.levelUpMenu
                        .extra3DropNoPickTalent))
                {
                    bool hasLevel3Card = false;
                    foreach (Card card in cardsToShowThisTime)
                    {
                        if (card.level == 3)
                        {
                            hasLevel3Card = true;
                            break;
                        }
                    }

                    if (!hasLevel3Card)
                    {
                        List<Card> eligibleCards1 = new List<Card>();

                        eligibleCards1.AddRange(
                            MenuControl.Instance.heroMenu.FilterCardsOfLevel(MenuControl.Instance.heroMenu.GetAllLoot(),
                                3));
                        if (eligibleCards1.Count > 0)
                        {
                            var card = eligibleCards1[Random.Range(0, eligibleCards1.Count)];
                            cardsToShowThisTime.Add(card);

                            Debug.Log($"遭遇战战利品 skippedLastLootDrops生效得到卡片 {card.GetChineseName()}");
                        }
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }

            //Build choice menu
            List<string> buttonLabels = new List<string>();
            buttonLabels.Add(MenuControl.Instance.GetLocalizedString("SkipLoot"));
            buttonLabels.Add(MenuControl.Instance.GetLocalizedString("ConfirmLoot"));

            List<System.Action> actions = new List<System.Action>();
            // actions.Add(() => { });
            // actions.Add(() => { });
            actions.Add(() =>
            {
                foreach (Card card in cardsToShow)
                {
                    Destroy(card.gameObject);
                }

                MenuControl.Instance.heroMenu.addFlareStone(1);
                MenuControl.Instance.victoryMenu.FinishVictory();
            });
            actions.Add(() =>
            {
                Card chosenCard =
                    MenuControl.Instance.cardChoiceMenu.cardsToShow[
                        MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts[0]];
                Card newCard = MenuControl.Instance.heroMenu.AddCardToDeck(chosenCard);

                MenuControl.Instance.dataControl.SaveData();
                foreach (Card card in cardsToShow)
                {
                    Destroy(card.gameObject);
                }

                MenuControl.Instance.cardChoiceMenu.ShowNotifcation(newCard,
                    () => { MenuControl.Instance.victoryMenu.FinishVictory(); },
                    MenuControl.Instance.GetLocalizedString("CardAddedToDeckPrompt"));
            });

            var newcardsToShowThisTime = new List<Card>();
            foreach (var card in cardsToShowThisTime)
            {
                if (MenuControl.Instance.heroMenu.isCardUnlockedAndAvailable(card))
                {
                    newcardsToShowThisTime.Add(card);
                }
                else
                {
                    Debug.LogError("invalid card in loot menu " + card.name);
                }
            }

            ShowChoice(newcardsToShowThisTime, buttonLabels, actions,
                getLootPrompt(), 1,
                freePicksTotal + MenuControl.Instance.heroMenu.flareStones, true, true, true, true,
                MenuControl.Instance.GetLocalizedString("VictoryLootPrompt"),
                string.Format(MenuControl.Instance.GetLocalizedString("SpecialChallenge_ExtraStoneBattleDescription"),
                    GetChallengeRewardValue(MenuControl.Instance.eventMenu.specialChallengeRewardStones, encounter)));
        }
    }

    bool isRealLoot()
    {
        return isLoot && !isChallengeLoot() && minSelectedCards > 0;
    }

    bool isChallengeLoot()
    {
        return (MenuControl.Instance.eventMenu.isSpecialChallenge &&
                (MenuControl.Instance.eventMenu.specialChallengeReward ==
                 AdventureItemEncounter.EncounterSpeicallChallengeRewardType.Upgrade ||
                 MenuControl.Instance.eventMenu.specialChallengeReward ==
                 AdventureItemEncounter.EncounterSpeicallChallengeRewardType.Delete));
    }

    string getLootPrompt()
    {
        if (!isRealLoot())
        {
            return promptText.text;
        }

        var res = "";
        if (selectedVisibleCardInts.Count < freePicksTotal)
        {
            res += string.Format(MenuControl.Instance.GetLocalizedString("VictoryLootPromptFreeLoot"),
                freePicksTotal - selectedVisibleCardInts.Count, freePicksTotal);
        }
        else
        {
            res += MenuControl.Instance.GetLocalizedString("VictoryLootPromptFlarestoneLoot");
        }

        return res;
    }

    public void ShowSeasonsChoice()
    {
        List<Card> eligibleCards = MenuControl.Instance.heroMenu.heroPath.pathCards;
        eligibleCards.AddRange(MenuControl.Instance.heroMenu.GetUnlockedMinionCards());

        List<Card> niceCards = new List<Card>();
        foreach (Card card in eligibleCards)
        {
            if (card.level == 1 && card is Minion)
            {
                niceCards.Add(card);
            }
        }

        List<Card> naughtyCards = new List<Card>();
        foreach (Card card in eligibleCards)
        {
            if (card.level == 3 && card is Minion)
            {
                naughtyCards.Add(card);
            }
        }

        if (niceCards.Count == 0 || naughtyCards.Count == 0)
        {
            MenuControl.Instance.victoryMenu.FinishVictory();
            return;
        }

        List<Card> cardsToShow = new List<Card>();
        Card newCard1 = Instantiate(niceCards[Random.Range(0, niceCards.Count)], transform);
        newCard1.cardTags.Add(MenuControl.Instance.niceTag);
        newCard1.temporaryOnly = true;

        Card newCard2 = Instantiate(naughtyCards[Random.Range(0, naughtyCards.Count)], transform);
        newCard2.cardTags.Add(MenuControl.Instance.naughtyTag);
        newCard2.temporaryOnly = true;

        cardsToShow.Add(newCard1);
        cardsToShow.Add(newCard2);

        HideMenu();

        List<string> buttonLabels = new List<string>();
        buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Skip"));
        buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Confirm"));

        List<System.Action> actionsToPerform = new List<System.Action>();
        actionsToPerform.Add(() =>
        {
            foreach (Card card in cardsToShow)
            {
                Destroy(card.gameObject);
            }

            MenuControl.Instance.victoryMenu.FinishVictory();
        });
        actionsToPerform.Add(() =>
        {
            Card chosenCard =
                MenuControl.Instance.cardChoiceMenu.cardsToShow[
                    MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts[0]];
            Card newCard = MenuControl.Instance.heroMenu.AddCardToDeck(chosenCard);

            MenuControl.Instance.dataControl.SaveData();
            foreach (Card card in cardsToShow)
            {
                Destroy(card.gameObject);
            }

            MenuControl.Instance.cardChoiceMenu.ShowNotifcation(newCard,
                () => { MenuControl.Instance.victoryMenu.FinishVictory(); },
                MenuControl.Instance.GetLocalizedString("CardAddedToDeckPrompt"));
        });

        MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actionsToPerform,
            MenuControl.Instance.GetLocalizedString("SeasonsChooseCardToAddPrompt"), 1, 1, true, 2, true);
        MenuControl.Instance.cardChoiceMenu.visibleCardsShown[0].niceCard.SetActive(true);
    }
}