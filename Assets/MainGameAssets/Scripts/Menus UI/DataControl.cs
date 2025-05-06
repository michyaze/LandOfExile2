using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

public class DataControl : MonoBehaviour
{

    string currentDataVersion = "1";
    public MenuControl mc;
    public string surfix = "Publish";
    public void SaveData()
    {
        SaveGlobalData();
        SaveSettingData();
        SaveProgress saveProgress = new SaveProgress();
        Hero hero = MenuControl.Instance.heroMenu.hero;

        saveProgress.selectedPath = MenuControl.Instance.pathMenu.selectedPath;
         saveProgress.hasInitHero = MenuControl.Instance.heroMenu.hasInitHero;
        saveProgress.currentSeed = MenuControl.Instance.currentSeed;

        saveProgress.heroClassInt = MenuControl.Instance.heroMenu.heroClasses.IndexOf(MenuControl.Instance.heroMenu.heroClass);
        saveProgress.heroPathInt = MenuControl.Instance.heroMenu.heroPaths.IndexOf(MenuControl.Instance.heroMenu.heroPath);

        saveProgress.flareStones = MenuControl.Instance.heroMenu.flareStones;
        saveProgress.currentXP = MenuControl.Instance.heroMenu.currentXP;
        saveProgress.currentLevel = MenuControl.Instance.heroMenu.currentLevel;
        saveProgress.isAlive = MenuControl.Instance.heroMenu.isAlive;
        saveProgress.damageDealt = MenuControl.Instance.heroMenu.damageDealtThisRun;
        saveProgress.turnsUsed = MenuControl.Instance.heroMenu.turnsUsedThisRun;
        saveProgress.dataVersion = currentDataVersion;
        //saveProgress.goldConvertedThisRun = MenuControl.Instance.heroMenu.goldConvertedThisRun;

        saveProgress.reaperProgress = MenuControl.Instance.heroMenu.reaperProgress;
        saveProgress.reaperMode = MenuControl.Instance.heroMenu.reaperMode;
        saveProgress.easyMode = MenuControl.Instance.heroMenu.easyMode;


        saveProgress.heroNameString = MenuControl.Instance.heroMenu.heroNameText.text;
        saveProgress.synergisticDropsSkipped = MenuControl.Instance.heroMenu.synergisticDropsSkipped;
        saveProgress.dropsSinceLastTreasureDropped = MenuControl.Instance.heroMenu.dropsSinceLastTreasureDropped;
        saveProgress.skippedLastLootDrops = MenuControl.Instance.heroMenu.skippedLastLootDrops;

        saveProgress.maxHP = hero.initialHP;
        saveProgress.currentHP = hero.currentHP;
        saveProgress.initialPower = hero.initialPower;
        saveProgress.initialMana = MenuControl.Instance.heroMenu.initialMana;
        saveProgress.drawsPerTurn = MenuControl.Instance.heroMenu.drawsPerTurn;
        //saveProgress.currentWeaponString = MenuControl.Instance.heroMenu.hero.weapon.UniqueID;
        saveProgress.initialActions = hero.initialActions;
        saveProgress.movementType = MenuControl.Instance.heroMenu.allMovementTypes.IndexOf(hero.movementType);
        saveProgress.currentWeather = (int)MenuControl.Instance.adventureMenu.weatherController.currentWeather;


        foreach (Card card in MenuControl.Instance.heroMenu.cardsOwned)
        {
            saveProgress.allOwnedCardIDs.Add(card.UniqueID);
            saveProgress.allOwnedCardUniqueIDs.Add(card.CardUniqueId);
            
            if (card.cardTags.Contains(MenuControl.Instance.naughtyTag))
            {
                saveProgress.naughtyIndexes.Add(MenuControl.Instance.heroMenu.cardsOwned.IndexOf(card));
            }
            if (card.cardTags.Contains(MenuControl.Instance.niceTag))
            {
                saveProgress.niceIndexes.Add(MenuControl.Instance.heroMenu.cardsOwned.IndexOf(card));
            }
        }
        foreach (Card card in MenuControl.Instance.heroMenu.weaponsOwned)
        {
            saveProgress.weaponsOwnedCardIDs.Add(card.UniqueID);
        }
        foreach (Card card in MenuControl.Instance.heroMenu.artifactsOwned)
        {
            saveProgress.artifactsOwnedCardIDs.Add(card.UniqueID);
        }
        foreach (Card card in MenuControl.Instance.heroMenu.artifactsEquipped)
        {
            saveProgress.artifactsEquippedCardIDs.Add(card.UniqueID);
            saveProgress.artifactsEquippedUniqueCardIDs.Add(card.CardUniqueId);
        }
        saveProgress.seasonsMode = MenuControl.Instance.heroMenu.seasonsMode;
        saveProgress.foundSanta = MenuControl.Instance.heroMenu.foundSanta;
        saveProgress.foundKrampus = MenuControl.Instance.heroMenu.foundKrampus;

        saveProgress.startOfBattleHandCardsIDs.AddRange(MenuControl.Instance.heroMenu.startOfBattleHandCardIDs);

        foreach (Card card in MenuControl.Instance.heroMenu.originalStartingCards)
        {
            saveProgress.originalStartingCardsIDs.Add(card.UniqueID);
        }

        for (int ii = 0; ii < hero.startingEffects.Count; ii += 1)
        {
            Effect effect = hero.startingEffects[ii];
            saveProgress.startingEffectIDs.Add(effect.UniqueID);
            saveProgress.startingEffectCharges.Add(hero.startingEffectCharges[ii]);
        }
        
        for (int ii = 0; ii < hero.tempStartingEffects.Count; ii += 1)
        {
            Effect effect = hero.tempStartingEffects[ii];
            saveProgress.tempStartingEffectIDs.Add(effect.UniqueID);
            saveProgress.tempStartingEffectCharges.Add(hero.tempStartingEffectCharges[ii]);
        }

        foreach (Card card in MenuControl.Instance.adventureMenu.itemCards)
        {
            saveProgress.itemCards.Add(card.UniqueID);
        }
        foreach (int integer in MenuControl.Instance.adventureMenu.itemCardsForItemIndex)
        {
            saveProgress.itemCardsForItemIndex.Add(integer);
        }
        foreach (int integer in MenuControl.Instance.adventureMenu.itemCardsToExtraData)
        {
            saveProgress.itemCardsToExtraData.Add(integer);
        }

        saveProgress.upgrades = MenuControl.Instance.shopMenu.upgrades;
        saveProgress.purchaseRefreshCount = MenuControl.Instance.shopMenu.purchaseRefreshCount;
        saveProgress.purchases = MenuControl.Instance.shopMenu.purchases;
        saveProgress.removals = MenuControl.Instance.shopMenu.removals;
        saveProgress.currentRemoveCardCost = MenuControl.Instance.shopMenu.currentRemoveCardCost;

        saveProgress.areasVisited = MenuControl.Instance.areaMenu.areasVisited;
        saveProgress.skipsTaken = MenuControl.Instance.areaMenu.skipsTaken;
        saveProgress.currentAreaIndex = MenuControl.Instance.areaMenu.allAreas.IndexOf(MenuControl.Instance.areaMenu.currentArea);
        saveProgress.currentAreaComplete = MenuControl.Instance.areaMenu.currentAreaComplete;

        saveProgress.cardsDiscoveredThisRun = MenuControl.Instance.heroMenu.cardsDiscoveredThisRun;
        saveProgress.turnsUsedThisRun = MenuControl.Instance.heroMenu.turnsUsedThisRun;
        saveProgress.encountersNormalWonThisRun = MenuControl.Instance.heroMenu.encountersNormalWonThisRun;
        saveProgress.encountersEliteWonThisRun = MenuControl.Instance.heroMenu.encountersEliteWonThisRun;
        saveProgress.encountersBossWonThisRun = MenuControl.Instance.heroMenu.encountersBossWonThisRun;
        saveProgress.damageDealtThisRun = MenuControl.Instance.heroMenu.damageDealtThisRun;

        int jj = 0;
        foreach (AdventureItem item in MenuControl.Instance.adventureMenu.adventureItems)
        {
            // var index = MenuControl.Instance.adventureMenu.allAdventureItems.IndexOf(item);
            // if (index == -1)
            // {
            //     index = MenuControl.Instance.adventureMenu.allAdventureItems.Count - 1;
            // }
            saveProgress.adventureItemIndexes.Add(item.UniqueID);
            if (item is AdventureItemEncounter encounter)
            {
                saveProgress.encounterSpecialChallenge.Add(MenuControl.Instance.adventureMenu.adventureItemInfos[jj].specialChallengeIndex);
                saveProgress.encounterSpecialChallengeReward.Add((int)MenuControl.Instance.adventureMenu.adventureItemInfos[jj].specialChallengeRewardIndex);
                
            }
            else
            {
                saveProgress.encounterSpecialChallenge.Add(-1);
                saveProgress.encounterSpecialChallengeReward.Add(-1);
            }

            jj++;
        }
        saveProgress.adventureItemCompletions.AddRange(MenuControl.Instance.adventureMenu.adventureItemCompletions);
        saveProgress.adventureItemChecked.AddRange(MenuControl.Instance.adventureMenu.adventureItemChecked);
        saveProgress.currentMapTileIndex = MenuControl.Instance.adventureMenu.currentMapTileIndex;
        foreach (var pair in MenuControl.Instance.adventureMenu.eventIdSelectOptionId)
        {
            saveProgress.eventIdSelectOptionIdKey .Add(pair.Key);
            saveProgress.eventIdSelectOptionIdValue .Add(pair.Value);
        }
        foreach (EventTile tile in MenuControl.Instance.adventureMenu.mapTiles)
        {
            //saveProgress.mapTileDirections.Add(tile.directions);
            saveProgress.mapInfoId.Add(tile.info.index);
            saveProgress.mapTileAdventureItemIndexes.Add(tile.adventureItemIndex);
            saveProgress.mapTilePosX.Add(tile.posX);
            saveProgress.mapTilePosY.Add(tile.posY);
            saveProgress.mapTilesRevealed.Add(tile.isRevealed);
            saveProgress.mapTilesFirstRevealed.Add(tile.isFirstReveal);
            //saveProgress.mapTilesSkipped.Add(tile.skipped);
        }

        saveProgress.playerPosX = MenuControl.Instance.adventureMenu.playerChess.transform.localPosition.x;
        saveProgress.playerPosY = MenuControl.Instance.adventureMenu.playerChess.transform.localPosition.y;
        //which random adventure is used. might not need later
        foreach (AdventureItem item in MenuControl.Instance.adventureMenu.randomAdventureItems)
        {
            saveProgress.randomAdventureItemIndexes.Add(MenuControl.Instance.adventureMenu.allAdventureItems.IndexOf(item));
        }


        //Progress
        saveProgress.highestDamageDealt = MenuControl.Instance.progressMenu.highestDamageDealt;
        //saveProgress.highestGoldRemaining = MenuControl.Instance.progressMenu.highestGoldRemaining;
        saveProgress.classCompletions.AddRange(MenuControl.Instance.progressMenu.classCompletions);

        saveProgress.pathCompletions.AddRange(MenuControl.Instance.progressMenu.pathCompletions);

        saveProgress.encounterCompletions = MenuControl.Instance.progressMenu.encounterCompletions;
        saveProgress.encountersWon = MenuControl.Instance.progressMenu.encountersWon;
        saveProgress.potionsConsumed = MenuControl.Instance.progressMenu.potionsConsumed;
        saveProgress.spellsCast = MenuControl.Instance.progressMenu.spellsCast;
        saveProgress.actionsPlayed = MenuControl.Instance.progressMenu.actionsPlayed;
        saveProgress.skillsUsed = MenuControl.Instance.progressMenu.skillsUsed;
        saveProgress.areasExplored = MenuControl.Instance.progressMenu.areasExplored;
        saveProgress.totalDamageDealt = MenuControl.Instance.progressMenu.totalDamageDealt;
        //saveProgress.goldCollected = MenuControl.Instance.progressMenu.goldCollected;
        saveProgress.xPEarned = MenuControl.Instance.progressMenu.xPEarned;
        saveProgress.leastTurnsUsed = MenuControl.Instance.progressMenu.leastTurnsUsed;



        foreach (Card card in MenuControl.Instance.levelUpMenu.variableTalentsAcquired)
        {
            saveProgress.variableTalentStrings.Add(card.UniqueID);
        }

        saveProgress.extraLootCardLastOffered = MenuControl.Instance.heroMenu.extraLootCardLastOffered;
        saveProgress.flareStoneShards = MenuControl.Instance.heroMenu.flareStoneShards;


        saveProgress.startDateString = MenuControl.Instance.heroMenu.startDate.ToBinary().ToString();
       // saveProgress.autoEquipNewWeapons = MenuControl.Instance.weaponsMenu.autoEquipNewWeaponsToggle.isOn;
       // saveProgress.stackWeapons = MenuControl.Instance.weaponsMenu.stackDuplicatesToggle.isOn;
        saveProgress.stackDeckCards = MenuControl.Instance.deckMenu.stackDuplicatesToggle.isOn;

        saveProgress.previousVersionText = MenuControl.Instance.GetBuildLabelText();

        saveProgress.seasonsLootCountDown = MenuControl.Instance.heroMenu.seasonsLootCountDown;
        saveProgress.orderedStorysEventIndexThisRound = MenuControl.Instance.eventMenu.orderedStorysEventIndexThisRound;

        File.WriteAllText(Application.persistentDataPath + $"/SaveData{surfix}.txt", saveProgress.SaveToString());

    }

    public void SaveGlobalData()
    {
        
        SaveGlobal saveProgress = new SaveGlobal();
        saveProgress.defeatedEncounter = MenuControl.Instance.defeatedEncounter;
        saveProgress.shownFlareInfoInShop = MenuControl.Instance.shopMenu.shownFlareInfoInShop;
        
        saveProgress.startingDeckInt = MenuControl.Instance.heroMenu.startingDeckInt;
        saveProgress.draftMode = MenuControl.Instance.heroMenu.draftMode;
        saveProgress.ascensionInt = MenuControl.Instance.heroMenu.ascensionMode;
        saveProgress.ascensionUnlocks = MenuControl.Instance.heroMenu.ascensionUnlocks;
        saveProgress.shownSteamPage = MenuControl.Instance.shownSteamPage;
        //saveProgress.artifactSlots = MenuControl.Instance.heroMenu.artifactSlots;

        foreach (string card in MenuControl.Instance.progressMenu.cardsDiscovered)
        {
            saveProgress.cardsDiscovered.Add(card);
        }
        //saveProgress.accumulatedGold = MenuControl.Instance.heroMenu.accumulatedGold;
        foreach (Card card in MenuControl.Instance.heroMenu.startingCardsUnlocked)
        {
            saveProgress.additionalStartingCards.Add(card.UniqueID);
        }
        foreach (Card card in MenuControl.Instance.heroMenu.artifactsUnlocked)
        {
            saveProgress.artifactStringsUnlocked.Add(card.UniqueID);
        }
        saveProgress.unlockedClasses = MenuControl.Instance.heroMenu.unlockedClasses;
        
        saveProgress.finishedClasses = MenuControl.Instance.heroMenu.finishedClasses;
        saveProgress.finishedUnlockVisualizationClasses = MenuControl.Instance.heroMenu.finishedUnlockVisualizationClasses;
        
        
        saveProgress.battleTutorialFinished = MenuControl.Instance.tutorialFinished;
        saveProgress.cutSceneFinished = MenuControl.Instance.cutSceneFinished;
        saveProgress.firstRun = MenuControl.Instance.firstRun;
        
        for (int ii = 0; ii < MenuControl.Instance.heroMenu.heroClasses.Count; ii += 1)
        {
            if (ii >= saveProgress.levelForClass.Count)
            {
                saveProgress.levelForClass.Add(1);
                saveProgress.xpForClass.Add(0);
            }
            saveProgress.levelForClass[ii] = MenuControl.Instance.heroMenu.heroClasses[ii].level;
            saveProgress.xpForClass[ii] = MenuControl.Instance.heroMenu.heroClasses[ii].experience;
            
        }
        
        // Debug.Log("achievementProgressCount when save " +
        //           MenuControl.Instance.achievementsMenu.achievementProgressCount);
        
        saveProgress.achievementStringsCompleted.AddRange(MenuControl.Instance.achievementsMenu.achievementStringsCompleted);
        saveProgress.achievementProgressCount.AddRange(MenuControl.Instance.achievementsMenu.achievementProgressCount);
        saveProgress.achievementStringsCompletedThisRun = MenuControl.Instance.heroMenu.achievementStringsCompletedThisRun;
        saveProgress.orderedStorysEventIndex = MenuControl.Instance.eventMenu.orderedStorysEventIndex;
        
        
        File.WriteAllText(Application.persistentDataPath + $"/SaveGlobal{surfix}.txt", saveProgress.SaveToString());

        
    }

    public void SaveSettingData()
    {
        
        SaveSetting saveProgress = new SaveSetting();
        saveProgress.languageSetting = MenuControl.Instance.settingsMenu.languageSetting.value;
        File.WriteAllText(Application.persistentDataPath + $"/SaveSetting{surfix}.txt", saveProgress.SaveToString());
    }

    public bool HasSettingData()
    {
        return File.Exists(Application.persistentDataPath + $"/SaveSetting{surfix}.txt");
    }
    public void LoadSettingData()
    {
        SaveSetting saveProgress = new SaveSetting();

        if (File.Exists(Application.persistentDataPath + $"/SaveSetting{surfix}.txt"))
        {
            string textRead = File.ReadAllText(Application.persistentDataPath + $"/SaveSetting{surfix}.txt");
            if (textRead != "")
            {
                try
                {
                    //throw new Exception(); //TESTING ONLY FIXME
                    saveProgress = SaveSetting.CreateFromJSONString<SaveSetting>(textRead);
                    
                    
                    MenuControl.Instance.settingsMenu.languageSetting.value = saveProgress.languageSetting;
                }
                catch (Exception e)
                {
                    //Do nothing and start a new file
                }
            }

        }
    }

    public void LoadGlobalData()
    {
        SaveGlobal saveProgress = new SaveGlobal();

        for (int ii = 0; ii < MenuControl.Instance.heroMenu.heroClasses.Count; ii += 1)
        {
            MenuControl.Instance.heroMenu.unlockedClasses.Add(ii == 0);
            MenuControl.Instance.heroMenu.ascensionUnlocks.Add(0);
            
            MenuControl.Instance.heroMenu.finishedClasses.Add(true);
            MenuControl.Instance.heroMenu.finishedUnlockVisualizationClasses.Add(ii == 0);
        }
        if (File.Exists(Application.persistentDataPath + $"/SaveGlobal{surfix}.txt"))
        {
            string textRead = File.ReadAllText(Application.persistentDataPath + $"/SaveGlobal{surfix}.txt");
            if (textRead != "")
            {
                try
                {
                    //throw new Exception(); //TESTING ONLY FIXME
                    saveProgress = SaveSetting.CreateFromJSONString<SaveGlobal>(textRead);
                    
                    
                    MenuControl.Instance.defeatedEncounter = saveProgress.defeatedEncounter;
                    MenuControl.Instance.shownSteamPage = saveProgress.shownSteamPage;
                    //start load
                    foreach (string cardString in saveProgress.additionalStartingCards)
                    {
                        Card card = MenuControl.Instance.heroMenu.GetCardByID(cardString);
                        if (card != null)
                        {
                            
                            MenuControl.Instance.heroMenu.startingCardsUnlocked.Add(card);
                            MenuControl.Instance.heroMenu.startingCardsUnlockedNames.Add(card.UniqueID);
                        }
                    }
                    foreach (string cardString in saveProgress.artifactStringsUnlocked)
                    {
                        Card card = MenuControl.Instance.heroMenu.GetCardByID(cardString);
                        if (card != null)
                            MenuControl.Instance.heroMenu.artifactsUnlocked.Add(card);
                    }
                    
                    if (saveProgress.unlockedClasses.Count == 0)
                    {
                    }
                    else
                    {

                        MenuControl.Instance.heroMenu.unlockedClasses.Clear();
                        MenuControl.Instance.heroMenu.unlockedClasses.AddRange(saveProgress.unlockedClasses);
                        for (int ii = 0; ii < MenuControl.Instance.heroMenu.heroClasses.Count; ii += 1)
                        {
                            if (MenuControl.Instance.heroMenu.unlockedClasses.Count <= ii)
                            {
                                MenuControl.Instance.heroMenu.unlockedClasses.Add(ii == 0);
                            }
                        }

                        MenuControl.Instance.heroMenu.finishedClasses = saveProgress.finishedClasses;
                        MenuControl.Instance.heroMenu.finishedUnlockVisualizationClasses = saveProgress.finishedUnlockVisualizationClasses;
                    }

                     MenuControl.Instance.shopMenu.shownFlareInfoInShop = saveProgress.shownFlareInfoInShop;

                    MenuControl.Instance.tutorialFinished = saveProgress.battleTutorialFinished;
                    MenuControl.Instance.cutSceneFinished = saveProgress.cutSceneFinished;
                    MenuControl.Instance.firstRun = saveProgress.firstRun;
                    
                    MenuControl.Instance.heroMenu.startingDeckInt = saveProgress.startingDeckInt;
                    MenuControl.Instance.heroMenu.draftMode = saveProgress.draftMode;
                    MenuControl.Instance.heroMenu.ascensionMode = saveProgress.ascensionInt;
                    //MenuControl.Instance.heroMenu.artifactSlots = saveProgress.artifactSlots;
                    
                    
                    MenuControl.Instance.progressMenu.cardsDiscovered.Clear();
                    foreach (string cardString in saveProgress.cardsDiscovered)
                    {
                            MenuControl.Instance.progressMenu.cardsDiscovered.Add(cardString);
                    }

                    MenuControl.Instance.heroMenu.ascensionUnlocks.Clear();
                    for (int ii = 0; ii < MenuControl.Instance.heroMenu.heroClasses.Count; ii += 1)
                    {
                        if (MenuControl.Instance.heroMenu.ascensionUnlocks.Count > ii)
                        {
                            MenuControl.Instance.heroMenu.ascensionUnlocks[ii] = (saveProgress.ascensionUnlocks[ii]);
                            
                        }
                        else
                        {
                            MenuControl.Instance.heroMenu.ascensionUnlocks.Add(0);
                        }

                        if (saveProgress.levelForClass.Count>ii)
                        {
                            MenuControl.Instance.heroMenu.heroClasses[ii].level =(saveProgress.levelForClass[ii]);
                            MenuControl.Instance.heroMenu.heroClasses[ii].experience =(saveProgress.xpForClass[ii]);
                        }
                    }
                    
                    MenuControl.Instance.achievementsMenu.achievementStringsCompleted.Clear();
                    MenuControl.Instance.achievementsMenu.achievementStringsCompleted.AddRange(saveProgress.achievementStringsCompleted);

                    MenuControl.Instance.achievementsMenu.achievementProgressCount.Clear();
                    if (saveProgress.achievementProgressCount != null)
                    {
                        MenuControl.Instance.achievementsMenu.achievementProgressCount.AddRange(saveProgress.achievementProgressCount);
                    }
                    int diff = MenuControl.Instance.achievementsMenu.achievements.Count - MenuControl.Instance.achievementsMenu.achievementProgressCount.Count;
                    for (int ii = 0; ii < diff; ii += 1)
                    {
                        MenuControl.Instance.achievementsMenu.achievementProgressCount.Add(0);
                    }

                    // Debug.Log("achievementProgressCount " +
                    //           MenuControl.Instance.achievementsMenu.achievementProgressCount);

                    MenuControl.Instance.heroMenu.achievementStringsCompletedThisRun = saveProgress.achievementStringsCompletedThisRun;
                    
                    MenuControl.Instance.eventMenu.orderedStorysEventIndex = saveProgress.orderedStorysEventIndex;
                    if (MenuControl.Instance.eventMenu.orderedStorysEventIndex == null)
                    {
                        MenuControl.Instance.eventMenu.orderedStorysEventIndex = new List<string>();
                    }
                }
                catch (Exception e)
                {
                    //Do nothing and start a new file
                    Debug.LogError("something wrong in loading global");
                }
            }

        }
    }
    
    public void LoadData()
    {

        SaveProgress saveProgress = new SaveProgress();

        if (File.Exists(Application.persistentDataPath + $"/SaveData{surfix}.txt"))
        {
            string textRead = File.ReadAllText(Application.persistentDataPath + $"/SaveData{surfix}.txt");
            if (textRead != "")
            {
                try
                {
                    //throw new Exception(); //TESTING ONLY FIXME
                    saveProgress = SaveProgress.CreateFromJSONString<SaveProgress>(textRead);
                }
                catch (Exception e)
                {
                    //Do nothing and start a new file
                }
            }

        }

        if (saveProgress.dataVersion != currentDataVersion)
        {
            saveProgress = new SaveProgress();
        }


        MenuControl.Instance.pathMenu.selectedPath = saveProgress.selectedPath;
        MenuControl.Instance.heroMenu.hasInitHero = saveProgress.hasInitHero;
        MenuControl.Instance.currentSeed = saveProgress.currentSeed;

        MenuControl.Instance.heroMenu.heroClass = MenuControl.Instance.heroMenu.heroClasses[saveProgress.heroClassInt];

        MenuControl.Instance.heroMenu.heroPath = MenuControl.Instance.heroMenu.heroPaths[saveProgress.heroPathInt];

        
        MenuControl.Instance.heroMenu.flareStones = saveProgress.flareStones;
        MenuControl.Instance.heroMenu.currentXP = saveProgress.currentXP;
        MenuControl.Instance.heroMenu.currentLevel = saveProgress.currentLevel;
        MenuControl.Instance.heroMenu.isAlive = saveProgress.isAlive;
        MenuControl.Instance.heroMenu.damageDealtThisRun = saveProgress.damageDealtThisRun;
        MenuControl.Instance.heroMenu.turnsUsedThisRun = saveProgress.turnsUsedThisRun;
        MenuControl.Instance.heroMenu.cardsDiscoveredThisRun = saveProgress.cardsDiscoveredThisRun;
        MenuControl.Instance.heroMenu.encountersNormalWonThisRun = saveProgress.encountersNormalWonThisRun;
        MenuControl.Instance.heroMenu.encountersEliteWonThisRun = saveProgress.encountersEliteWonThisRun;
        MenuControl.Instance.heroMenu.encountersBossWonThisRun = saveProgress.encountersBossWonThisRun;


        MenuControl.Instance.heroMenu.reaperProgress = saveProgress.reaperProgress;
        MenuControl.Instance.heroMenu.reaperMode = saveProgress.reaperMode;
        MenuControl.Instance.heroMenu.easyMode = saveProgress.easyMode;

        MenuControl.Instance.heroMenu.heroNameText.text = saveProgress.heroNameString;
        MenuControl.Instance.heroMenu.synergisticDropsSkipped = saveProgress.synergisticDropsSkipped;
        MenuControl.Instance.heroMenu.dropsSinceLastTreasureDropped = saveProgress.dropsSinceLastTreasureDropped;
        MenuControl.Instance.heroMenu.skippedLastLootDrops = saveProgress.skippedLastLootDrops;

        MenuControl.Instance.heroMenu.InitalizeHero();
        Hero hero = MenuControl.Instance.heroMenu.hero;

        hero.initialHP = saveProgress.maxHP;
        hero.currentHP = saveProgress.currentHP;
        hero.initialPower = saveProgress.initialPower;
        hero.initialActions = saveProgress.initialActions;
        hero.movementType = MenuControl.Instance.heroMenu.allMovementTypes[saveProgress.movementType];


        MenuControl.Instance.heroMenu.initialMana = saveProgress.initialMana;
        MenuControl.Instance.heroMenu.drawsPerTurn = saveProgress.drawsPerTurn;


        

        hero.startingEffects.Clear();
        hero.startingEffectCharges.Clear();
        hero.tempStartingEffects.Clear();
        hero.tempStartingEffectCharges.Clear();
        for (int ii = 0; ii < saveProgress.startingEffectIDs.Count; ii += 1)
        {
            string effectString = saveProgress.startingEffectIDs[ii];
            Effect effect = MenuControl.Instance.heroMenu.GetEffectByID(effectString);
            if (effect != null)
            {
                hero.startingEffects.Add(effect);
                hero.startingEffectCharges.Add(saveProgress.startingEffectCharges[ii]);
            }
            else
            {
                Debug.LogError("start effect does not found "+effectString);
            }
        }
        
        for (int ii = 0; ii < saveProgress.tempStartingEffectIDs.Count; ii += 1)
        {
            string effectString = saveProgress.tempStartingEffectIDs[ii];
            Effect effect = MenuControl.Instance.heroMenu.GetEffectByID(effectString);
            if (effect != null)
            {
                hero.tempStartingEffects.Add(effect);
                hero.tempStartingEffectCharges.Add(saveProgress.tempStartingEffectCharges[ii]);
            }
            else
            {
                Debug.LogError("start effect does not found "+effectString);
            }
        }
        for(int i = 0;i<saveProgress.allOwnedCardIDs.Count;i++)
        {
            var cardString = saveProgress.allOwnedCardIDs[i];
            Card card = MenuControl.Instance.heroMenu.GetCardByID(cardString);
            if (card != null)
            {
                if (card is Hero)
                {
                    MenuControl.Instance.heroMenu.cardsOwned.Add(hero);
                }
                else
                {
                    Card createdCard = MenuControl.Instance.heroMenu.CreateCardToOwn(card);
                    if (saveProgress.allOwnedCardUniqueIDs.Count > i)
                    {
                        createdCard.CardUniqueId = saveProgress.allOwnedCardUniqueIDs[i];
                    }
                }
            }
            else
            {
                Debug.LogError(cardString+" is not in allcards");
            }
        }
        foreach (string cardString in saveProgress.weaponsOwnedCardIDs)
        {
            Card card = MenuControl.Instance.heroMenu.GetCardByID(cardString);
            if (card != null)
            {
                MenuControl.Instance.heroMenu.weaponsOwned.Add(card);
            }
        }

        if (saveProgress.startOfBattleHandCardsIDs != null)
        {
            MenuControl.Instance.heroMenu.startOfBattleHandCardIDs.AddRange(saveProgress.startOfBattleHandCardsIDs);
        }

        if (saveProgress.weaponsOwnedCardIDs.Count == 0)
        {
            if (MenuControl.Instance.heroMenu.hero != null && MenuControl.Instance.heroMenu.hero.weapon != null)
            {
                MenuControl.Instance.heroMenu.weaponsOwned.Add(hero.weapon);
            }
        }

        foreach (string cardString in saveProgress.artifactsOwnedCardIDs)
        {
            Card card = MenuControl.Instance.heroMenu.GetCardByID(cardString);
            if (card != null)
            {
                MenuControl.Instance.heroMenu.artifactsOwned.Add(card);
            }
        }

        int artifactIndex = 0;
        List<string> artifactsToBeRemoved = new List<string>();
        foreach (string cardString in saveProgress.artifactsEquippedCardIDs)
        {
            Card card = MenuControl.Instance.heroMenu.GetCardByID(cardString);
            if (card != null)
            {
                if (card.isPotion)
                {
                    if (MenuControl.Instance.heroMenu.GetCardByCardUniqueId(
                            saveProgress.artifactsEquippedUniqueCardIDs[artifactIndex]))
                    {
                        MenuControl.Instance.heroMenu.artifactsEquipped.Add(MenuControl.Instance.heroMenu.GetCardByCardUniqueId(
                            saveProgress.artifactsEquippedUniqueCardIDs[artifactIndex]));
                    }
                    else
                    {
                        artifactsToBeRemoved.Add((cardString));
                    }
                    
                }
                else
                {
                    MenuControl.Instance.heroMenu.artifactsEquipped.Add(card);
                }
            }

            artifactIndex++;
        }
        
        

        if (saveProgress.weaponsOwnedCardIDs.Count == 0)
        {
            if (MenuControl.Instance.heroMenu.hero != null && MenuControl.Instance.heroMenu.hero.weapon != null)
            {
                MenuControl.Instance.heroMenu.weaponsOwned.Add(hero.weapon);
            }
        }

        MenuControl.Instance.heroMenu.seasonsMode = saveProgress.seasonsMode;
        MenuControl.Instance.heroMenu.foundSanta = saveProgress.foundSanta;
        MenuControl.Instance.heroMenu.foundKrampus = saveProgress.foundKrampus;

        if (saveProgress.naughtyIndexes != null)
        {
            foreach (int index in saveProgress.naughtyIndexes)
            {
                MenuControl.Instance.heroMenu.cardsOwned[index].cardTags.Add(MenuControl.Instance.naughtyTag);
            }
        }
        if (saveProgress.niceIndexes != null)
        {
            foreach (int index in saveProgress.niceIndexes)
            {
                MenuControl.Instance.heroMenu.cardsOwned[index].cardTags.Add(MenuControl.Instance.niceTag);
            }
        }

        foreach (string cardString in saveProgress.originalStartingCardsIDs)
        {
            Card card = MenuControl.Instance.heroMenu.GetCardByID(cardString);
            if (card != null)
            {
                if (card is Hero)
                {
                    MenuControl.Instance.heroMenu.originalStartingCards.Add(hero);
                }
                else
                {
                    MenuControl.Instance.heroMenu.originalStartingCards.Add(card);
                }
            }
        }

        for (int ii = 0; ii < saveProgress.itemCards.Count; ii++)
        {
            string cardString = saveProgress.itemCards[ii];
            Card card = MenuControl.Instance.heroMenu.GetCardByID(cardString);
            if (card != null)
            {
                MenuControl.Instance.adventureMenu.itemCards.Add(card);
                MenuControl.Instance.adventureMenu.itemCardsForItemIndex.Add(saveProgress.itemCardsForItemIndex[ii]);
                if (saveProgress.itemCardsToExtraData == null || ii >= saveProgress.itemCardsToExtraData.Count)
                {
                    MenuControl.Instance.adventureMenu.itemCardsToExtraData.Add(0);
                }
                else
                {
                    MenuControl.Instance.adventureMenu.itemCardsToExtraData.Add(saveProgress.itemCardsToExtraData[ii]);
                }
            }
        }

        MenuControl.Instance.shopMenu.upgrades = saveProgress.upgrades;
        MenuControl.Instance.shopMenu.purchases = saveProgress.purchases;
        MenuControl.Instance.shopMenu.removals = saveProgress.removals;
        MenuControl.Instance.shopMenu.purchaseRefreshCount = saveProgress.purchaseRefreshCount;
        
        MenuControl.Instance.shopMenu.currentRemoveCardCost = Math.Max(MenuControl.Instance.shopMenu.removeCardStartCost, saveProgress.currentRemoveCardCost);

        MenuControl.Instance.areaMenu.areasVisited = saveProgress.areasVisited;
        MenuControl.Instance.areaMenu.skipsTaken = saveProgress.skipsTaken;
        if (saveProgress.currentAreaIndex != -1)
            MenuControl.Instance.areaMenu.currentArea = MenuControl.Instance.areaMenu.allAreas[saveProgress.currentAreaIndex];
        MenuControl.Instance.areaMenu.currentAreaComplete = saveProgress.currentAreaComplete;

        MenuControl.Instance.heroMenu.cardsDiscoveredThisRun = saveProgress.cardsDiscoveredThisRun;
        for (int ii = 0; ii < saveProgress.adventureItemIndexes.Count; ii += 1)
        {
            var index = saveProgress.adventureItemIndexes[ii];
            if (index !=null && index.Length>0 && MenuControl.Instance.adventureMenu.GetAdventureItemByKey(index))
            {
                var item =MenuControl.Instance.adventureMenu.GetAdventureItemByKey(index);
                MenuControl.Instance.adventureMenu.adventureItems.Add(item);
                if (item is AdventureItemEncounter encounter)
                {
                    encounter.PickRandomSettingValue();
                }
                if (saveProgress.encounterSpecialChallenge.Count > ii)
                {
                    MenuControl.Instance.adventureMenu.adventureItemInfos.Add(new AdventureItemInfo(saveProgress.encounterSpecialChallenge[ii],(AdventureItemEncounter.EncounterSpeicallChallengeRewardType)saveProgress.encounterSpecialChallengeReward[ii]));
                }

            }
            else
            {
                  Debug.LogError("Adventure item not found:"+index);
            }
            
        }
        MenuControl.Instance.adventureMenu.adventureItemCompletions.AddRange(saveProgress.adventureItemCompletions);
        MenuControl.Instance.adventureMenu.adventureItemChecked.AddRange(saveProgress.adventureItemChecked);
        for (int i = 0; i < saveProgress.eventIdSelectOptionIdKey.Count;i++)
        {
            MenuControl.Instance.adventureMenu.eventIdSelectOptionId[saveProgress.eventIdSelectOptionIdKey[i]] = saveProgress.eventIdSelectOptionIdValue[i];
        }
        
        
        MenuControl.Instance.eventMenu.orderedStorysEventIndexThisRound = saveProgress.orderedStorysEventIndexThisRound;
        if (MenuControl.Instance.adventureMenu.weatherController == null)
        {
            MenuControl.Instance.eventMenu.orderedStorysEventIndexThisRound = new List<string>();
        }

        for (int ii = 0; ii < saveProgress.mapTileAdventureItemIndexes.Count; ii += 1)
        {
            // if (ii >= saveProgress.mapInfoId.Count ||
            //     saveProgress.mapInfoId[ii] >= MenuControl.Instance.csvLoader.mapEventInfos.Count ||
            //     saveProgress.mapInfoId[ii] < 0)
            // {
            //     Debug.LogError("error in load");
            //     continue;
            // }
            EventTile tile = Instantiate(MenuControl.Instance.adventureMenu.mapEventPrefab, MenuControl.Instance.adventureMenu.spawnPos);
            MenuControl.Instance.adventureMenu.mapTiles.Add(tile);
            //tile.directions = saveProgress.mapTileDirections[ii];
            tile.adventureItemIndex = saveProgress.mapTileAdventureItemIndexes[ii];
            tile.transform.localPosition = new Vector2(saveProgress.mapTilePosX[ii], saveProgress.mapTilePosY[ii]);
            
            var scale = MenuControl.Instance.adventureMenu.eventItemScale;
            tile.transform.localScale = new Vector3(scale, scale, scale);
            tile.posX = saveProgress.mapTilePosX[ii];
            tile.posY = saveProgress.mapTilePosY[ii];
            tile.info = MenuControl.Instance.csvLoader.mapEventInfos[saveProgress.mapInfoId[ii]];
            tile.isRevealed = saveProgress.mapTilesRevealed[ii];
            if (saveProgress.mapTilesRevealed!=null && saveProgress.mapTilesFirstRevealed.Count > ii)
            {
                
                tile.isFirstReveal = saveProgress.mapTilesFirstRevealed[ii];
            }
            tile.init(tile.info,ii,ii);
            //tile.skipped = saveProgress.mapTilesSkipped[ii];
        }
        MenuControl.Instance.adventureMenu.playerChess.transform.localPosition = new Vector2(saveProgress.playerPosX ,saveProgress.playerPosY) ;
        MenuControl.Instance.adventureMenu.UpdateChess();
        MenuControl.Instance.adventureMenu.currentMapTileIndex = saveProgress.currentMapTileIndex;
        for (int ii = 0; ii < saveProgress.randomAdventureItemIndexes.Count; ii += 1)
        {
            int index = saveProgress.randomAdventureItemIndexes[ii];
            if (index != -1 && MenuControl.Instance.adventureMenu.allAdventureItems.Count > index)
                MenuControl.Instance.adventureMenu.randomAdventureItems.Add(MenuControl.Instance.adventureMenu.allAdventureItems[index]);
        }

        MenuControl.Instance.progressMenu.highestDamageDealt = saveProgress.highestDamageDealt;
        //MenuControl.Instance.progressMenu.highestGoldRemaining = saveProgress.highestGoldRemaining;
        MenuControl.Instance.progressMenu.classCompletions.Clear();
        MenuControl.Instance.progressMenu.classCompletions.AddRange(saveProgress.classCompletions);
        int count = MenuControl.Instance.heroMenu.heroClasses.Count - MenuControl.Instance.progressMenu.classCompletions.Count;
        for (int ii = 0; ii < count; ii += 1)
        {
            MenuControl.Instance.progressMenu.classCompletions.Add(0);
        }

        MenuControl.Instance.progressMenu.pathCompletions.Clear();
        MenuControl.Instance.progressMenu.pathCompletions.AddRange(saveProgress.pathCompletions);
        count = MenuControl.Instance.heroMenu.heroPaths.Count - MenuControl.Instance.progressMenu.pathCompletions.Count;
        for (int ii = 0; ii < count; ii += 1)
        {
            MenuControl.Instance.progressMenu.pathCompletions.Add(0);
        }


        MenuControl.Instance.progressMenu.encounterCompletions = saveProgress.encounterCompletions;

        MenuControl.Instance.progressMenu.encountersWon = saveProgress.encountersWon;
        MenuControl.Instance.progressMenu.potionsConsumed = saveProgress.potionsConsumed;
        MenuControl.Instance.progressMenu.spellsCast = saveProgress.spellsCast;
        MenuControl.Instance.progressMenu.actionsPlayed = saveProgress.actionsPlayed;
        MenuControl.Instance.progressMenu.skillsUsed = saveProgress.skillsUsed;
        MenuControl.Instance.progressMenu.areasExplored = saveProgress.areasExplored;
        MenuControl.Instance.progressMenu.totalDamageDealt = saveProgress.totalDamageDealt;
       // MenuControl.Instance.progressMenu.goldCollected = saveProgress.goldCollected;
        MenuControl.Instance.progressMenu.xPEarned = saveProgress.xPEarned;
        MenuControl.Instance.progressMenu.leastTurnsUsed = saveProgress.leastTurnsUsed;
        MenuControl.Instance.adventureMenu.weatherController.currentWeather = (WeatherType)saveProgress.currentWeather;



        foreach (string cardString in saveProgress.variableTalentStrings)
        {
            Card card = MenuControl.Instance.heroMenu.GetCardByID(cardString);
            if (card != null)
                MenuControl.Instance.levelUpMenu.variableTalentsAcquired.Add(card);
        }

        MenuControl.Instance.heroMenu.extraLootCardLastOffered = saveProgress.extraLootCardLastOffered;
        MenuControl.Instance.heroMenu.flareStoneShards = saveProgress.flareStoneShards;

      //  MenuControl.Instance.weaponsMenu.autoEquipNewWeaponsToggle.isOn = saveProgress.autoEquipNewWeapons;
        //MenuControl.Instance.weaponsMenu.stackDuplicatesToggle.isOn = saveProgress.stackWeapons;
        MenuControl.Instance.deckMenu.stackDuplicatesToggle.isOn = saveProgress.stackDeckCards;

        long temp = Convert.ToInt64(saveProgress.startDateString);
        MenuControl.Instance.heroMenu.startDate = DateTime.FromBinary(temp);

        MenuControl.Instance.previousVersionText = saveProgress.previousVersionText;

        MenuControl.Instance.heroMenu.seasonsLootCountDown = saveProgress.seasonsLootCountDown;
        
        
            MenuControl.Instance.adventureMenu.UpdateWeather();
        
    }

    public void ResetData()
    {
        if (File.Exists(Application.persistentDataPath + $"/SaveData{surfix}.txt"))
        {
            File.WriteAllText(Application.persistentDataPath + $"/SaveData{surfix}.txt", null);
        }
    }

    public void ResetAllData()
    {
        PlayerPrefs.DeleteAll();
        ResetData();
    }
}

public class SaveGlobal:SaveDataBase
{

    public bool shownFlareInfoInShop;
    public List<string> defeatedEncounter = new List<string>();
    
    public List<string> cardsDiscovered = new List<string>();
    public List<string> additionalStartingCards = new List<string>();
    public List<string> artifactStringsUnlocked = new List<string>();
    public List<bool> unlockedClasses = new List<bool>();
    
    
    public bool battleTutorialFinished;
    public bool cutSceneFinished;
    public bool firstRun = true;

    public bool shownSteamPage = false;
    
    public int startingDeckInt;
    public bool draftMode;
    public int ascensionInt;
    public List<int> ascensionUnlocks = new List<int>();
    public int artifactSlots;

    public List<int> levelForClass = new List<int>();
    public List<int> xpForClass = new List<int>();
    
    public List<string> achievementStringsCompletedThisRun = new List<string>();
    public List<int> achievementProgressCount = new List<int>();
    public List<string> achievementStringsCompleted = new List<string>();

    public List<string> orderedStorysEventIndex = new List<string>();

    public List<bool> finishedClasses = new List<bool>();
    public List<bool> finishedUnlockVisualizationClasses = new List<bool>();
}
public class SaveSetting:SaveDataBase
{
    public int languageSetting;
}
public class SaveProgress:SaveDataBase
{
    public string currentSeed;
    public int heroClassInt;

    public bool selectedPath;

    public int heroPathInt;

    public bool hasInitHero;


    public int flareStones;
    public int currentXP;
    public int currentLevel;
    public bool isAlive;
    public int damageDealt;
    public int turnsUsed;
    //public int goldConvertedThisRun;
    public int turnsUsedThisRun;
    public int encountersNormalWonThisRun;
    public int encountersEliteWonThisRun;
    public int encountersBossWonThisRun;
    public int damageDealtThisRun;
    public int cardsDiscoveredThisRun;

    public bool easyMode;
    public bool reaperMode;
    public int reaperProgress;

    public string heroNameString;
    public int synergisticDropsSkipped;
    public int dropsSinceLastTreasureDropped;

    public int maxHP;
    public int currentHP;
    public int initialMana;
    public int drawsPerTurn = 5;
    public int initialPower;
    public int initialActions;
    public int movementType;

    public List<string> startingEffectIDs = new List<string>();
    public List<int> startingEffectCharges = new List<int>();
    public List<string> tempStartingEffectIDs = new List<string>();
    public List<int> tempStartingEffectCharges = new List<int>();
    public List<string> allOwnedCardIDs = new List<string>();
    public List<int> allOwnedCardUniqueIDs = new List<int>();
    public List<string> weaponsOwnedCardIDs = new List<string>();
    public List<string> artifactsOwnedCardIDs = new List<string>();
    public List<string> artifactsEquippedCardIDs = new List<string>();
    public List<int> artifactsEquippedUniqueCardIDs = new List<int>();

    public bool seasonsMode;
    public bool foundSanta;
    public bool foundKrampus;

    public List<int> naughtyIndexes = new List<int>();
    public List<int> niceIndexes = new List<int>();

    public List<string> startOfBattleHandCardsIDs = new List<string>();
    public List<string> originalStartingCardsIDs = new List<string>();
    

    public List<string> itemCards = new List<string>();
    public List<int> itemCardsForItemIndex = new List<int>();
    public List<int> itemCardsToExtraData = new List<int>();

    public int upgrades;
    public int purchases;
    public int removals;
    public int currentRemoveCardCost;
    public int purchaseRefreshCount;

    public int areasVisited;
    public int skipsTaken;
    public int currentAreaIndex;
    public bool currentAreaComplete;

    public List<string> adventureItemIndexes = new List<string>();
    public List<bool> adventureItemCompletions = new List<bool>();
    public List<bool> adventureItemChecked = new List<bool>();
    public List<int> mapTileAdventureItemIndexes = new List<int>();
    public List<int> mapInfoId = new List<int>();
    public List<float> mapTilePosX = new List<float>();
    public List<float> mapTilePosY = new List<float>();
    public List<bool> mapTilesRevealed = new List<bool>();
    public List<bool> mapTilesFirstRevealed = new List<bool>();
    public int currentMapTileIndex;
    public List<int> randomAdventureItemIndexes = new List<int>();
    public List<int> eventIdSelectOptionIdKey = new List<int>();
    public List<int> eventIdSelectOptionIdValue = new List<int>();
    public List<int> encounterSpecialChallenge = new List<int>();
    public List<int> encounterSpecialChallengeReward = new List<int>();

    public float playerPosX;
    public float playerPosY;
    

    public int highestDamageDealt;
    //public int highestGoldRemaining;
    public int totalCompletions;
    public List<int> classCompletions = new List<int>();
    public List<int> pathCompletions = new List<int>();

    public List<string> encounterCompletions = new List<string>();
    public int encountersWon;
    public int potionsConsumed;
    public int spellsCast;
    public int actionsPlayed;
    public int skillsUsed;
    public int areasExplored;
    public int totalDamageDealt;
    //public int goldCollected;
    public int xPEarned;
    public int leastTurnsUsed;

    public bool hasUnlockedGame;



    public List<string> variableTalentStrings = new List<string>();

    public int extraLootCardLastOffered;
    public int flareStoneShards;

    public string startDateString;
    public bool stackDeckCards = true;
    public string previousVersionText;

    public string dataVersion;
    public bool skippedLastLootDrops;

    public int seasonsLootCountDown;

    public int currentWeather;
    public List<string> orderedStorysEventIndexThisRound = new List<string>();

}

public class SaveDataBase
{
    
    public static T CreateFromJSONString<T>(string jsonString)
    {
        if (jsonString == "") return JsonUtility.FromJson<T>(jsonString);

        return JsonUtility.FromJson<T>(StringUtil.Decrypt(jsonString));
    }
    public string SaveToString()
    {
        return StringUtil.Crypt(JsonUtility.ToJson(this));
    }
}

public static class StringUtil
{
    private static byte[] key = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
    private static byte[] iv = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };

    public static string Crypt(this string text)
    {
        SymmetricAlgorithm algorithm = DES.Create();
        ICryptoTransform transform = algorithm.CreateEncryptor(key, iv);
        byte[] inputbuffer = Encoding.Unicode.GetBytes(text);
        byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
        return Convert.ToBase64String(outputBuffer);
    }

    public static string Decrypt(this string text)
    {
        SymmetricAlgorithm algorithm = DES.Create();
        ICryptoTransform transform = algorithm.CreateDecryptor(key, iv);
        byte[] inputbuffer = Convert.FromBase64String(text);
        byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
        return Encoding.Unicode.GetString(outputBuffer);
    }
}

