using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Spine.Unity;
using UnityEngine;

public class HeroClass : CollectibleItem {

    public Sprite iconSprite;
    public Sprite largeHeroSelectSprite;
    public Hero heroPrefab;
    public int initialActions;
    public int initialHP;

    public int InitialHpAfterLevel()
    {
        var hpModified = 0;
        var increaseLevel = level;
        hpModified = increaseLevel / MenuControl.Instance.heroMenu.levelToIncreaseHp;
        return initialHP + hpModified;
    }
    public int initialMana;
    public int initialCardsDrawnPerTurn;
    //public int unlockGold;
    public int hpGainPerLevel;

    public SkeletonDataAsset spineAsset;

    public List<Card> classCards =>MenuControl.Instance.heroMenu.ClassCards();
    List<Card> testAllCards = new List<Card>();

    public List<Card> startCards = new List<Card>();
    
    public List<Card> basicTalents = new List<Card>();
    public List<Card> advancedTalents = new List<Card>();
    public List<Card> midpointTalents = new List<Card>();

    public List<string> defaultNamesForPaths = new List<string>();

    public List<Cutscene> cutscenes = new List<Cutscene>();
    public List<int> cutsceneAreas = new List<int>();
    
    public List<Card> startTalents = new List<Card>();

    public int index = 0;

    public string classFolderName;
    public int level
    {
        get { return MenuControl.Instance.heroMenu.getHeroLevel(index); }
        set { MenuControl.Instance.heroMenu.setHeroLevel(index,value); }
    }
    public int experience
    {
        get { return MenuControl.Instance.heroMenu.getHeroExperience(index); }
        set { MenuControl.Instance.heroMenu.setHeroExperience(index,value); }
    }
    public float experienceRatio => (float)experience / currentLevelMaxExperience();

    void purifyTalents(List<Card> toPurify)
    {
        var newAllCards = new List<Card>();
        var csvLoader = MenuControl.Instance.csvLoader;
        foreach (var card in toPurify)
        {
            var chineseName = card.UniqueID;
            if(csvLoader.chineseNameToPlayerCardMap.ContainsKey(chineseName) || 
               csvLoader.chineseNameToTalentMap.ContainsKey(chineseName))
            {newAllCards.Add(card);}
        }
        toPurify.Clear();
        foreach (var card in newAllCards)
        {
            toPurify.Add(card);
        }
    }
    public void Init()
    {
        if (MenuControl.Instance.heroMenu.useResource)
        {
            addCardResource();
            var test = new List<Card>();
            var heroMenu = MenuControl.Instance.heroMenu;
            // foreach (var card in classCards)
            // {
            //     if ( heroMenu.isCardAvailable(card))
            //     {
            //         test.Add(card);
            //     }
            // }
            //test.AddRange(classCards);
            // test.AddRange(testAllCards);
            // test.Distinct();
            // test.RemoveAll(card => card == null);
            //
            // HashSet<string> cardset = new HashSet<string>();
            // for (int i = test.Count - 1; i >= 0; i--)
            // {
            //     if (cardset.Contains(test[i].UniqueID))
            //     {
            //         test.RemoveAt(i);
            //         continue;
            //     }
            //     
            //     if ( !heroMenu.isCardAvailable(test[i]))
            //     {
            //         test.RemoveAt(i);
            //         continue;
            //     }
            //     cardset.Add((test[i].UniqueID));
            // }
           // classCards = test;
        }
        
        purifyTalents(basicTalents);
        purifyTalents(advancedTalents);
        purifyTalents(midpointTalents);
    }
    void addCardResource()
    {
        testAllCards.Clear();
        var gameObjects = Resources.LoadAll<GameObject>("Cards/"+classFolderName);
        //convert from array of gameobjects to array of card
        testAllCards.AddRange(gameObjects.Select(go => go.GetComponent<Card>()));
        
        var heroMenu = MenuControl.Instance.heroMenu;
        for (int i = testAllCards.Count - 1; i >= 0; i--)
        {
            if ( !heroMenu.isCardAvailable(testAllCards[i]))
            {
                testAllCards.RemoveAt(i);
            }
        }
    }

    public void addExperience(int e)
    {
        experience += e;
        if (isAtMaxLevel())
        {
            return;
        }
        while (experience >= currentLevelMaxExperience())
        {
            experience -= currentLevelMaxExperience();
            level += 1;
            if (isAtMaxLevel())
            {
                break;
            }
        }
    }

    // public int getMaxExperienceOfLevel(int level)
    // {
    //     
    //     if (isAtMaxLevel())
    //     {
    //         return -1;
    //     }
    //     return MenuControl.Instance.csvLoader.levelToUnlockCardsInfoMap[level].experienceToNextLevel;
    // }
    public int currentLevelMaxExperience()
    {
        if (isAtMaxLevel())
        {
            return MenuControl.Instance.csvLoader.levelToUnlockCardsInfoMap[MenuControl.Instance.csvLoader.levelToUnlockCardsInfoMap.Count-1].experienceToNextLevel;
        }
        return MenuControl.Instance.csvLoader.levelToUnlockCardsInfoMap[level].experienceToNextLevel;
    }
    public bool isAtMaxLevel(int level)
    {
        return !MenuControl.Instance.csvLoader.levelToUnlockCardsInfoMap.ContainsKey(level);
    }
    public bool isAtMaxLevel()
    {
        return !MenuControl.Instance.csvLoader.levelToUnlockCardsInfoMap.ContainsKey(level);
    }


    public int GetHPGainPerLevel()
    {
       return hpGainPerLevel + MenuControl.Instance.CountOfCardsInList(MenuControl.Instance.levelUpMenu.extraHPPerLevelTalent, MenuControl.Instance.levelUpMenu.variableTalentsAcquired);
    }
}
