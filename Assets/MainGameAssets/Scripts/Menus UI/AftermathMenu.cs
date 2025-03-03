using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using DG.Tweening;
using Doozy.Engine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AftermathMenu : BasicMenu
{

    //public Text goldText;
    // public Text cardsDiscoveredText;
    // public Text battlesText;
    // public Text areasText;
    // public Text damageDealtText;
    // public Text battleTurnsText;
    // public Text timeTakenText;

    public GameObject normalKilledGO;
    public GameObject eliteKilledGO;
    public GameObject bossKilledGO;

    public Text cardCountText;
    public GameObject addHpGo;

    public Text xPText;
    public Slider xPSlider;
    public Text levelText;

    public Text classText;
    public Image clasIcon;
    public Text pathText;
    public Text ascensionText;

    public Image infoButtonImage;
    public Transform cardListHolder;
    public GameObject cardListItemPrefab;

    public Text seedText;
    public GameObject successGO;
    public GameObject failedGO;

    public GameObject unlockCardButton;
    public GameObject nextButton;

    private int originLevel;
    private int newLevel;

    public void ShowLoseMenu()
    {
        successGO.SetActive(false);
        failedGO.SetActive(true);
        ShowMenu();
    }

    public void ShowWinMenu()
    {
        
        successGO.SetActive(true);
        failedGO.SetActive(false);

        //if (!MenuControl.Instance.shownSteamPage)
        {
            //MenuControl.Instance.demoView.ShowMenu();
        }
        
        ShowMenu();
        // if (!MenuControl.Instance.DemoMode)
        // {
        //     MenuControl.Instance.heroMenu.finishClass(MenuControl.Instance.heroMenu.GetHeroClassIndex());
        // }
    }

    public override void ShowMenu()
    {
        MenuControl.Instance.heroMenu.isAlive = false;
        base.ShowMenu();
        // var test = MenuControl.Instance.adventureMenu.GetCurrentAdventureItem();
        // var currentEncounter =  test as AdventureItemEncounter;
        // if (currentEncounter)
        // {
        //     MenuControl.Instance.achievementsMenu.CheckAchievements(currentEncounter);
        // }
        seedText.text = "Seed: " + MenuControl.Instance.currentSeed;
        //string textString = MenuControl.Instance.GetLocalizedString("Damage Dealt (this run)") + ": " + MenuControl.Instance.heroMenu.damageDealt;
        //textString += "\n" + MenuControl.Instance.GetLocalizedString("Gold Kept (this run)") + ": " + MenuControl.Instance.heroMenu.GetCurrentGold();
        //textString += "\n" + MenuControl.Instance.GetLocalizedString("Turns Used (this run)") + ": " + MenuControl.Instance.heroMenu.turnsUsed;
        //textString += "\n\n" + MenuControl.Instance.GetLocalizedString("Best Damage Dealt") + ": " + MenuControl.Instance.progressMenu.highestDamageDealt;
        //textString += "\n" + MenuControl.Instance.GetLocalizedString("Best Gold Kept") + ": " + MenuControl.Instance.progressMenu.highestGoldRemaining;
        //textString += "\n" + MenuControl.Instance.GetLocalizedString("Least Turns Used") + ": " + MenuControl.Instance.progressMenu.leastTurnsUsed;

        //statsText.text = textString;
        //goldText.text = (MenuControl.Instance.heroMenu.goldConvertedThisRun + MenuControl.Instance.heroMenu.GetCurrentGold()).ToString();

        // areasText.text = Mathf.Min(3, MenuControl.Instance.areaMenu.areasVisited).ToString();
        // cardsDiscoveredText.text = MenuControl.Instance.heroMenu.cardsDiscoveredThisRun.ToString();
        // battlesText.text = MenuControl.Instance.heroMenu.encountersWonThisRun.ToString();
        // battleTurnsText.text = MenuControl.Instance.heroMenu.turnsUsedThisRun.ToString();
        // damageDealtText.text = MenuControl.Instance.heroMenu.damageDealtThisRun.ToString();

        // System.DateTime currentDate = System.DateTime.Now;
        // System.TimeSpan difference = currentDate.Subtract(MenuControl.Instance.heroMenu.startDate);
        // timeTakenText.text = difference.Hours.ToString() + " : " + difference.Minutes.ToString();

        int addExperience = 0;
        var normalXP = MenuControl.Instance.heroMenu.encountersNormalWonThisRun * 10;
        var eliteXP = MenuControl.Instance.heroMenu.encountersEliteWonThisRun * 20;
        var bossXP = MenuControl.Instance.heroMenu.encountersBossWonThisRun * 30;
        addExperience = normalXP + eliteXP + bossXP;

        infoButtonImage.sprite = MenuControl.Instance.csvLoader.playerCardSprite(MenuControl.Instance.heroMenu.hero);
        
        normalKilledGO.GetComponentsInChildren<Text>()[0].text = MenuControl.Instance.GetLocalizedString("AftermathKilledMonsters")+": "+ MenuControl.Instance.heroMenu.encountersNormalWonThisRun.ToString();
        normalKilledGO.GetComponentsInChildren<Text>()[1].text = MenuControl.Instance.GetLocalizedString("AftermathExperience")+" + "+ MenuControl.Instance.heroMenu.encountersNormalWonThisRun * 10;
        eliteKilledGO.GetComponentsInChildren<Text>()[0].text = MenuControl.Instance.GetLocalizedString("AftermathKilledElites")+": "+ MenuControl.Instance.heroMenu.encountersEliteWonThisRun.ToString();
        eliteKilledGO.GetComponentsInChildren<Text>()[1].text = MenuControl.Instance.GetLocalizedString("AftermathExperience")+" + "+ MenuControl.Instance.heroMenu.encountersEliteWonThisRun * 20;
        bossKilledGO.GetComponentsInChildren<Text>()[0].text = MenuControl.Instance.GetLocalizedString("AftermathKilledBoss")+": "+ MenuControl.Instance.heroMenu.encountersBossWonThisRun.ToString();
        bossKilledGO.GetComponentsInChildren<Text>()[1].text = MenuControl.Instance.GetLocalizedString("AftermathExperience")+" + "+ MenuControl.Instance.heroMenu.encountersBossWonThisRun * 30;
        
        classText.text = MenuControl.Instance.heroMenu.heroClass.GetName();
        clasIcon.sprite = MenuControl.Instance.heroMenu.heroClass.iconSprite;
        //pathText.text = MenuControl.Instance.heroMenu.heroPath.GetName();
        //ascensionText.text = MenuControl.Instance.heroMenu.ascensionMode.ToString();

        HeroClass hero = MenuControl.Instance.heroMenu.heroClass;
        if (hero.isAtMaxLevel())
        {
            xPSlider.value = 1;
            xPText.text = "MAX";
        }
        else
        {
            
            xPSlider.value = (float)hero.experience / (float)hero.currentLevelMaxExperience();
            xPText.text = hero.experience + " / " + hero.currentLevelMaxExperience();
        }

        levelText.text = string.Format(MenuControl.Instance.GetLocalizedString("AftermathLevel"), hero.level);

        //#if UNITY_STANDALONE
        //        leaderBoardsButton.SetActive(false);
        //#endif

        foreach (Transform child in cardListHolder)
        {
            Destroy(child.gameObject);
        }

        List<Card> uniqueCards = new List<Card>();
        foreach (Card card in MenuControl.Instance.heroMenu.cardsOwned)
        {
            bool unique = true;
            foreach (Card cc in uniqueCards)
            {
                if (card.UniqueID == cc.UniqueID)
                {
                    unique = false;
                }
            }
            if (unique) uniqueCards.Add(card);
        }

        foreach (Card card in uniqueCards)
        {
            GameObject obj = Instantiate(cardListItemPrefab, cardListHolder) as GameObject;
            obj.GetComponentsInChildren<Text>()[0].text = card.GetName();
            obj.GetComponentsInChildren<Text>()[1].text = "x" + GetCountOfCard(card).ToString();
        }

        List<Card> uniqueTalents = new List<Card>();
        foreach (Card card in MenuControl.Instance.levelUpMenu.variableTalentsAcquired)
        {
            if (!uniqueTalents.Contains(card)) uniqueTalents.Add(card);
        }
        if (uniqueTalents.Count > 0)
        {
            GameObject obj1 = Instantiate(cardListItemPrefab, cardListHolder) as GameObject;
            obj1.GetComponentsInChildren<Text>()[0].text = "";
            obj1.GetComponentsInChildren<Text>()[1].text = "";

            GameObject obj = Instantiate(cardListItemPrefab, cardListHolder) as GameObject;
            obj.GetComponentsInChildren<Text>()[0].text = "--- " + MenuControl.Instance.GetLocalizedString("Hero Talents") + " ---";
            obj.GetComponentsInChildren<Text>()[1].text = "";
        }
        foreach (Card card in uniqueTalents)
        {
            GameObject obj = Instantiate(cardListItemPrefab, cardListHolder) as GameObject;
            obj.GetComponentsInChildren<Text>()[0].text = card.GetName();
            obj.GetComponentsInChildren<Text>()[1].text = "x" + MenuControl.Instance.CountOfCardsInList(card, MenuControl.Instance.levelUpMenu.variableTalentsAcquired).ToString();
        }

        string encounterString = "";
        if (MenuControl.Instance.adventureMenu.GetCurrentAdventureItemEncounter() != null)
        {
            encounterString = MenuControl.Instance.adventureMenu.GetCurrentAdventureItemEncounter().UniqueID;
        }

        MenuControl.Instance.LogEvent("Escape_" + encounterString + MenuControl.Instance.heroMenu.GetLevelClassPathString());
        MenuControl.Instance.LogEvent("FinishedGameViaArea" + MenuControl.Instance.areaMenu.areasVisited);

        

       var experienceSequence =  DOTween.Sequence();

       originLevel = hero.level;
       var originalStartHp = hero.InitialHpAfterLevel();
       var originExpRatio = hero.experienceRatio;
       hero.addExperience(addExperience);
       newLevel = hero.level;
       var newStartHp = hero.InitialHpAfterLevel();
       var index = MenuControl.Instance.heroMenu.getCurrentClassIndex();
       //var count = MenuControl.Instance.heroMenu.levelForClass.Count;
       // for(int i = count;i<=index;i++)
       // {
       //     MenuControl.Instance.heroMenu.levelForClass.Add(0);
       //     MenuControl.Instance.heroMenu.xpForClass.Add(0);
       // }
       if (MenuControl.Instance.heroMenu.heroClasses.Count <= index)
       {
           Debug.LogError("hero classes count is smaller than "+index);
       }
       MenuControl.Instance.heroMenu.heroClasses[index].level = newLevel;
       //MenuControl.Instance.heroMenu.heroClasses[index].experience = hero.experience;
       

       cardCountText.transform.parent.gameObject.SetActive(false);
       addHpGo.SetActive(false);
       var xpFillOneTime = 0.5f;
       unlockCardButton.gameObject.SetActive(false);
       nextButton.gameObject.SetActive(false);

       if (MenuControl.Instance.settingsMenu.alwaysUpgradeAfterBattle)
       {
           newLevel = originLevel + 1;
       }

       bool noNewCards = newLevel == originLevel || getRewardCardsCount() == 0;

       if (originalStartHp != newStartHp)
       {
           addHpGo.SetActive(true);
           var hpDiff = newStartHp - originalStartHp;
           addHpGo.GetComponentInChildren<Text>().text = "+" + hpDiff.ToString();
       }
       
       if (noNewCards)
       {
           
           nextButton.gameObject.SetActive(true);
           experienceSequence.Append(DOTween.To(() => xPSlider.value, x => xPSlider.value = x, hero.experienceRatio, xpFillOneTime));
       }
       else
       {
           unlockCardButton.gameObject.SetActive(true);
           cardCountText.transform.parent.gameObject.SetActive(true);
           cardCountText.text =  getRewardCardsCount().ToString();
           levelText.text = originLevel.ToString();
           var tempLevel = originLevel;
           experienceSequence.Append(DOTween.To(() => xPSlider.value, x => xPSlider.value = x, 1, xpFillOneTime*(1-originExpRatio)));
           experienceSequence.AppendCallback(() =>
           {
               levelText.text = (tempLevel + 1).ToString();
               tempLevel++;
           });
           for (int i = originLevel+1; i < newLevel; i++)
           {
               experienceSequence.AppendCallback(() =>
               {
                   xPSlider.value = 0;
               });
               experienceSequence.Append(DOTween.To(() => xPSlider.value, x => xPSlider.value = x, 1, xpFillOneTime));
               experienceSequence.AppendCallback(() =>
               {
                   levelText.text = (tempLevel + 1).ToString();
                   tempLevel++;
               });
           }
    
           experienceSequence.AppendCallback(() =>
           {
               xPSlider.value = 0;
           });
           experienceSequence.Append(DOTween.To(() => xPSlider.value, x => xPSlider.value = x, hero.experienceRatio, xpFillOneTime));
       }

       experienceSequence.OnUpdate(() =>
       {
           xPText.text = ((int)(xPSlider.value * hero.currentLevelMaxExperience())).ToString()+ " / " + hero.currentLevelMaxExperience();
           //levelText.text
       });
       unlockCardButton.GetComponent<CanvasGroup>().DOFade(0, 0);
       nextButton.GetComponent<CanvasGroup>().DOFade(0, 0);
       experienceSequence.OnComplete(() =>
       {
           xPText.text = ((int)( hero.experience)).ToString()+ " / " + hero.currentLevelMaxExperience();
           levelText.text = newLevel.ToString();
           if (noNewCards)
           {
               
               nextButton.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
           }
           else
           {
               
               unlockCardButton.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
           }
       });
       experienceSequence.Play();
       MenuControl.Instance.dataControl.SaveGlobalData();
       MenuControl.Instance.dataControl.ResetData();
       // .SetId(GetTweenId(target, animation.AnimationType, AnimationAction.Fade))
       // .SetUpdate(Settings.IgnoreUnityTimescale)
       // .SetSpeedBased(Settings.SpeedBasedAnimations)
       // .OnStart(() =>
       // {
       //     if (onStartCallback != null) onStartCallback.Invoke();
       // })
       // .OnComplete(() =>
       // {
       //     if (onCompleteCallback != null) onCompleteCallback.Invoke();
       // })
       // .Append(FadeTween(target, animation, startValue, endValue))
       // .Play();
    }

    int GetCountOfCard(Card cardToCount)
    {
        int count = 0;
        foreach (Card card in MenuControl.Instance.heroMenu.cardsOwned)
        {
            if (card.UniqueID == cardToCount.UniqueID)
                count += 1;
        }

        return count;
    }

    int getRewardCardsCount()
    {
        if (newLevel != originLevel)
        {
            var upgradeCSV = MenuControl.Instance.csvLoader.levelToUnlockCardsInfoMap;
            var unlockCardCount = 0;
            for (int i = originLevel+1; i <= Math.Min(newLevel,upgradeCSV.Count-1); i++)
            {
                unlockCardCount += upgradeCSV[i].cardCountToUnlock;
            }

            var lockedCards = MenuControl.Instance.heroMenu.GetLockedLevel1Cards();

            if (lockedCards.Count <= unlockCardCount)
            {
                Debug.LogError("unlockable cards not enough");
            }

            var finalSelectCardCount = Math.Min(lockedCards.Count, unlockCardCount);
            return finalSelectCardCount;
        }

        return 0;
    }
    public void RewardButtonPressed()
    {
        if (newLevel != originLevel)
        {
            var finalSelectCardCount = getRewardCardsCount();
            List<Card> unlockingCards = new List<Card>();
            var lockedCards = MenuControl.Instance.heroMenu.GetLockedLevel1Cards();
            //remove lockedCards duplication
            lockedCards = lockedCards.Distinct().ToList();
            finalSelectCardCount = Math.Min(finalSelectCardCount, lockedCards.Count);
            for (int i = 0; i <  finalSelectCardCount; i++)
            {
                if (lockedCards.Count <=0)
                {
                    break;
                }
                unlockingCards.Add(lockedCards.PickItem());
            }

            foreach (var card in unlockingCards)
            {
                MenuControl.Instance.heroMenu. startingCardsUnlocked.Add(card);
                MenuControl.Instance.heroMenu. startingCardsUnlockedNames.Add(card.UniqueID);

                if (card.upgradeCards.Count > 0)
                {
                    foreach (var upgrade in card.upgradeCards)
                    {
                        
                        MenuControl.Instance.heroMenu. startingCardsUnlocked.Add(upgrade);
                        MenuControl.Instance.heroMenu. startingCardsUnlockedNames.Add(upgrade.UniqueID);
                        
                        if (upgrade.upgradeCards.Count > 0)
                        {
                            foreach (var upgrade2 in upgrade.upgradeCards)
                            {
                        
                                MenuControl.Instance.heroMenu. startingCardsUnlocked.Add(upgrade2);
                                MenuControl.Instance.heroMenu. startingCardsUnlockedNames.Add(upgrade2.UniqueID);
                            }
                        }
                        
                    }
                }
            }
            MenuControl.Instance.dataControl.SaveGlobalData();
            List<string> buttonLabels = new List<string>();
            buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Confirm"));
            List<System.Action> actions = new List<System.Action>();
            actions.Add(() => {

                
                MenuControl.Instance.ReloadGame();

            });
            
            MenuControl.Instance.lootMenu.ShowChoice(unlockingCards,buttonLabels,actions,MenuControl.Instance.GetLocalizedString("AftermathUnlockCardName"),0,0,true,true,false,false,MenuControl.Instance.GetLocalizedString("AftermathUnlockCardDesc"),null,true);
            // MenuControl.Instance.cardChoiceMenu.ShowChoice(unlockingCards, buttonLabels, actions,
            //     MenuControl.Instance.GetLocalizedString("AftermathUnlockCardName"), 0,
            //     0, true, true, false, MenuControl.Instance.GetLocalizedString("AftermathUnlockCardDesc"),false);
            MenuControl.Instance.cardChoiceMenu.noChoice = true;
        }
        else
        {
            Debug.LogError("should not go to reward since level is the same");
        }
    }
    public void ContinueButtonPressed()
    {

        
        
        
        MenuControl.Instance.ReloadGame();
        

    }


}
