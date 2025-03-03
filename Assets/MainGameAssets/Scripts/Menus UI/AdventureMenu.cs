using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AdventureItemInfo
{
    public int specialChallengeIndex;
    public AdventureItemEncounter.EncounterSpeicallChallengeRewardType specialChallengeRewardIndex;

    public AdventureItemInfo(int index, AdventureItemEncounter.EncounterSpeicallChallengeRewardType rewardIndex)
    {
        specialChallengeIndex = index;
        specialChallengeRewardIndex = rewardIndex;
    }

    public AdventureItemInfo()
    {
        specialChallengeIndex = -1;
        specialChallengeRewardIndex = AdventureItemEncounter.EncounterSpeicallChallengeRewardType.None;
    }
}
public class AdventureMenu : BasicMenu
{
    public Doozy.Engine.UI.UIView adventureUIView;
    public List<Card> tempEnemyDeck = new List<Card>(); //TODO FIXME turn into scriptableobject defining encounter
    public List<AdventureItem> allAdventureItems = new List<AdventureItem>();
    public Dictionary<string, AdventureItem> allAdventureItemDict = new Dictionary<string, AdventureItem>();
    public List<AdventureItem> originalAllAdventureItems = new List<AdventureItem>();
    
    public List<AdventureItem> adventureItems = new List<AdventureItem>();
    public List<AdventureItemInfo> adventureItemInfos = new List<AdventureItemInfo>();
    
    public AdventureItemEncounter finalBossEncounter;
    public int selectedIndex = -1;
    [HideInInspector] public int currentMapTileIndex;
    public List<AdventureItem> randomAdventureItems = new List<AdventureItem>();

    public List<bool> adventureItemCompletions = new List<bool>();
    public List<bool> adventureItemAsCompletions = new List<bool>();
    public List<bool> adventureItemChecked = new List<bool>();
    public List<Card> itemCards = new List<Card>(); //放了所有的事件们需要用的卡牌
    public List<int> itemCardsForItemIndex = new List<int>();//标记每个上面的卡牌对应的事件index，通过GetItemCards取出正确的卡牌们
    public List<int> itemCardsToExtraData = new List<int>();//标记每个上面的卡牌对应的事件index，通过GetItemCards取出正确的卡牌们

    public void addToItemCardsForItemIndex(int i,int j=0)
    {
        itemCardsForItemIndex.Add(i);
        itemCardsToExtraData.Add(j);
    }

    public Image bigMap;
    public Text LevelNameText;
    public Transform spawnPos;
    public Transform spawnAbovePlayerPos;
    public Transform spawnAboveEverythingPos;

    public bool shouldShowAllEvents = true; //test only
    //public List<Transform> followPlayerMovePanels;
    public EventTile mapEventPrefab;

    [HideInInspector] public List<EventTile> mapTiles = new List<EventTile>();
    //[HideInInspector]public List<EventTile> checkedTiles = new List<EventTile>();
    //[HideInInspector]public List<EventTile> uncheckedTiles = new List<EventTile>();

    private List<EventTile> checkedTiles => mapTiles.Where(x => adventureItemChecked[x.adventureItemIndex]).ToList();
    private List<EventTile> uncheckedTiles => mapTiles.Where(x => !adventureItemChecked[x.adventureItemIndex]).ToList();

    //[HideInInspector]public List<int> finishedTileInfoIds = new List<int>();
    [HideInInspector] public Dictionary<int, int> eventIdSelectOptionId = new Dictionary<int, int>();

    public DrawParabola parabolaVisual;

    public GameObject storyEventPrefab;

    [HideInInspector] public bool ignoreMapClicks;

    [HideInInspector] public bool isMoving = false;

    public AdventureItemEncounter testEncounter;
    public bool testEncounterOnly;

    public AdventureItemEncounter tutorialEncounter;
    public bool tutorialEncounterOnly;

    //public VisibleCard playerIcon;
    public GameObject playerChess;

    public Sprite[] playerChessImages;
    
    private ParticleSystem playerMoveParticle;
    public EventDefinition exitEarlyEvent;
    public bool exitEarlyAsked;

    public Vector2 pointerOriginalPos;
    public Vector3 mapPanelOriginalPos;

    public Text healingPotionsText;
    public Text treasureText;

    public Transform deckIcon;

    public Doozy.Engine.Soundy.SoundyData footstepsSound;
    public Doozy.Engine.Soundy.SoundyData healingPotionSound;

    public GameObject instructionPanel;

    public Text cardListText;
    public Transform deckCardHolder;
    public RectTransform deckListPanel;
    public VisibleDeckCard deckCardPrefab;
    public Image deckToggleButtonImage;
    public Sprite deckListShowSprite;
    public Sprite deckListHideSprite;
    public bool showDeckList;
    public Sprite hiddenEncounterSprite;
    
    
    public float eventItemScale = 0.75f;
    
    
    //specialChallenge
    public List<int> specialChallengeProbability = new List<int>();
    
[HideInInspector]
    public WeatherController weatherController;

    public WeatherIcon weatherIcon;

    public AdventureItem GetAdventureItemByKey(string key)
    {
        if (allAdventureItemDict.ContainsKey(key))
        {
            return allAdventureItemDict[key];
        }
Debug.LogError(($"GetAdventureItemByKey: key {key} not found"));
        return null;
    }
    private void Awake()
    {
        playerMoveParticle = playerChess.GetComponentInChildren<ParticleSystem>();
        weatherController = GetComponent<WeatherController>();

        if (!MenuControl.Instance.weatherFeatureOn)
        {
            weatherIcon.gameObject.SetActive(false);
        }

        if (specialChallengeProbability.Count != 3)
        {
            Debug.LogError(("specialChallengeProbability.Count != 3"));
        }

        if (MenuControl.Instance.publishVersionFeatureOn)
        {
            var gameObjects = Resources.LoadAll<GameObject>("Adventure Items/Encounters/Magic").ToList();
            //gameObjects.AddRange(Resources.LoadAll<GameObject>("Adventure Items/Encounters/Test"));
            //convert from array of gameobjects to array of card
            foreach (var go in gameObjects)
            {
                if (go.GetComponent<AdventureItemEncounter>())
                {
                    allAdventureItems.Insert(allAdventureItems.Count-2,go.GetComponent<AdventureItemEncounter>());
                }
            }
        }

        foreach (var item in allAdventureItems)
        {
            allAdventureItemDict[item.UniqueID] = item;
        }
        
    }

    private void OnEnable()
    {
        playerMoveParticle.Stop();
    }


    private void Update()
    {
        if (isMoving)
        {
            centerPlayer();
        }

        if (gameObject.activeInHierarchy && pointerOriginalPos != Vector2.zero )
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                MenuControl.Instance.GetComponent<Canvas>().transform as RectTransform,
                Input.mousePosition, MenuControl.Instance.GetComponent<Canvas>().worldCamera, out pos);
            // mapPanel.position = mapPanelOriginalPos + 
            //     MenuControl.Instance.GetComponent<Canvas>().transform.TransformPoint(pos) 
            //     - MenuControl.Instance.GetComponent<Canvas>().transform.TransformPoint(pointerOriginalPos);
            //
            // mapPanel.GetComponent<RectTransform>().anchoredPosition = 
            //     new Vector2(Mathf.Clamp(mapPanel.GetComponent<RectTransform>().anchoredPosition.x, -3000f, 3000f),
            //         Mathf.Clamp(mapPanel.GetComponent<RectTransform>().anchoredPosition.y, -2200f, 2200f));
        }
    }

    public void UpdateScreenIfOnTop()
    {
        
        updateHeroInfo();
        var menuControl = MenuControl.Instance;
        if (menuControl.eventMenu.isVisible() || menuControl.battleMenu.isVisible() ||
            menuControl.shopMenu.isVisible() ||
            menuControl.shopMenu.isVisible() ||
            menuControl.victoryMenu.isVisible() ||
            menuControl.cardChoiceMenu.isVisible()||menuControl.levelUpMenu.isVisible() || menuControl.lootMenu.isVisible()
            ||menuControl.imageAndTextPopup.isVisible())
        {
            return;
        }

        RenderMapTiles();
    }

    public void ContinueAdventure()
    {
        pointerOriginalPos = Vector2.zero;
        selectedIndex = -1;

        if (MenuControl.Instance.areaMenu.currentArea)
        {
            bigMap.sprite = MenuControl.Instance.areaMenu.currentArea.bigMapSprite;
        }

        if (!MenuControl.Instance.heroMenu.hasInitHero)
        {
            MenuControl.Instance.heroMenu.hasInitHero = true;
        }

        if (MenuControl.Instance.heroMenu.hero.GetHP() <= 0)
        {
            MenuControl.Instance.aftermathMenu.ShowLoseMenu();
            //MenuControl.Instance.defeatMenu.ShowMenu();
            return;
        }

        // if (!MenuControl.Instance.pathMenu.selectedPath)
        // {
        //     MenuControl.Instance.heroMenu.ShowMenu();
        //     return;
        // }

        //TODO check for level up and show choices
        if (MenuControl.Instance.heroMenu.CanLevelUp() && !(MenuControl.Instance.areaMenu.currentAreaComplete &&
                                                            MenuControl.Instance.areaMenu.areasVisited == 4))
        {
            MenuControl.Instance.levelUpMenu.ShowLevelUp();
            return;
        }


        if (MenuControl.Instance.areaMenu.currentAreaComplete)
        {
            //if (!MenuControl.Instance.DemoMode)
            if(MenuControl.Instance.areaMenu.areasVisited == 1)
            {
                MenuControl.Instance.heroMenu.finishClass(MenuControl.Instance.heroMenu.GetHeroClassIndex());
            }
            MenuControl.Instance.LogEvent("AreaCompleted" + MenuControl.Instance.areaMenu.areasVisited);
            if (MenuControl.Instance.areaMenu.areasVisited == 4 && MenuControl.Instance.heroMenu.reaperMode)
            {
                EndRun(false);
            }
            else if (MenuControl.Instance.areaMenu.areasVisited == 4) //Completed game final boss
            {
                EndRun(false);
            }
            else if (!exitEarlyAsked && MenuControl.Instance.areaMenu.areasVisited > 0)
            {
                RenderScreen();
                exitEarlyAsked = true;

                MenuControl.Instance.adventureMenu.ContinueAdventure();
                //MenuControl.Instance.eventMenu.ShowEvent(exitEarlyEvent);
            }
            else if (MenuControl.Instance.areaMenu.areasVisited == 0)
            {
                MenuControl.Instance.areaMenu.SetupFirstArea();
            }
            else
            {
                MenuControl.Instance.areaMenu.ShowAreaSelection();
            }
        }
        else
        {
            if (currentMapTileIndex >= 0)
            {
                int index = mapTiles[currentMapTileIndex].adventureItemIndex;

                if (index > -1 && adventureItems[index] is AdventureItemEncounter)
                {
                    AdventureItemEncounter encounter = (AdventureItemEncounter)adventureItems[index];
                    if (encounter.isBoss && adventureItemCompletions[index])
                    {
                        if (MenuControl.Instance.heroMenu.ascensionMode >= 1 && encounter.level == 15)
                        {
                            EndRun(false);
                            return;
                        }

                        if (MenuControl.Instance.heroMenu.ascensionMode == 0 && encounter.level == 8)
                        {
                            EndRun(false);
                            return;
                        }

                        /*
                        if (MenuControl.Instance.heroMenu.reaperMode && encounter.level == 8)
                        {
                            EndRun(false);
                            return;
                        }
                        */
                    }
                }
            }

            RenderScreen();
        }

        //playerChess.GetComponentInChildren<Text>().text = MenuControl.Instance.heroMenu.HeroName;

        MenuControl.Instance.adventureMenu.SortChildrenByY();
        UpdateScreenIfOnTop();
        ShowMenu();
        //ignoreMapClicks = false;
    }


    public void EndRun(bool playerQuitEarly)
    {
        //Save stats
        MenuControl.Instance.heroMenu.isAlive = false;
        if (!playerQuitEarly)
        {
            // if (MenuControl.Instance.heroMenu.GetCurrentGold() > MenuControl.Instance.progressMenu.highestGoldRemaining)
            // {
            //     MenuControl.Instance.progressMenu.highestGoldRemaining = MenuControl.Instance.heroMenu.GetCurrentGold();
            // }
            if (MenuControl.Instance.heroMenu.damageDealtThisRun > MenuControl.Instance.progressMenu.highestDamageDealt)
            {
                MenuControl.Instance.progressMenu.highestDamageDealt = MenuControl.Instance.heroMenu.damageDealtThisRun;
            }

            if (MenuControl.Instance.progressMenu.leastTurnsUsed == 0 ||
                MenuControl.Instance.heroMenu.turnsUsedThisRun < MenuControl.Instance.progressMenu.leastTurnsUsed)
            {
                MenuControl.Instance.progressMenu.leastTurnsUsed = MenuControl.Instance.heroMenu.turnsUsedThisRun;
            }

            MenuControl.Instance.progressMenu.classCompletions[
                MenuControl.Instance.heroMenu.heroClasses.IndexOf(MenuControl.Instance.heroMenu.heroClass)] += 1;
            MenuControl.Instance.progressMenu.pathCompletions[
                MenuControl.Instance.heroMenu.heroPaths.IndexOf(MenuControl.Instance.heroMenu.heroPath)] += 1;

            if (MenuControl.Instance.heroMenu.reaperMode)
            {
                int difficultyIndex = MenuControl.Instance.heroMenu.reaperProgressHeroPathInts.IndexOf(
                    MenuControl.Instance.heroMenu.heroPaths.IndexOf(MenuControl.Instance.heroMenu.heroPath));

                if (MenuControl.Instance.heroMenu.reaperProgress == difficultyIndex &&
                    MenuControl.Instance.heroMenu.reaperProgress < 2)
                {
                    MenuControl.Instance.heroMenu.reaperProgress = difficultyIndex + 1;
                }
            }
        }

        // int goldToGive = MenuControl.Instance.heroMenu.GetCurrentGold() + (MenuControl.Instance.heroMenu.flareStones * 5);
        // if (MenuControl.Instance.heroMenu.easyMode)
        // {
        //     goldToGive = Mathf.CeilToInt(goldToGive / 2f);
        // }
        //MenuControl.Instance.heroMenu.accumulatedGold += goldToGive;

        //Raise ascension unlock if higher than before
        if (MenuControl.Instance.heroMenu.ascensionUnlocks[
                MenuControl.Instance.heroMenu.heroClasses.IndexOf(MenuControl.Instance.heroMenu.heroClass)] <
            MenuControl.Instance.heroMenu.ascensionMode + 1)
        {
            MenuControl.Instance.heroMenu.ascensionUnlocks[
                    MenuControl.Instance.heroMenu.heroClasses.IndexOf(MenuControl.Instance.heroMenu.heroClass)] =
                Mathf.Min(MenuControl.Instance.heroMenu.maxAscensionLevels + 1,
                    MenuControl.Instance.heroMenu.ascensionMode + 1);
        }

        MenuControl.Instance.achievementsMenu.CheckAchievements();

        MenuControl.Instance.dataControl.SaveData();
        HideMenu();
        MenuControl.Instance.aftermathMenu.ShowWinMenu();

        MenuControl.Instance.ReportLeaderboardScores();
    }

    public override void ShowMenu()
    {
        base.ShowMenu();
        if (!adventureUIView.IsVisible && !adventureUIView.IsShowing)
            adventureUIView.Show();
    }


    public override void HideMenu(bool instantly = false)
    {
        base.HideMenu(instantly);
        adventureUIView.Hide(instantly);
    }

    public AdventureItem GetItemByID(string itemID)
    {
        foreach (AdventureItem item in allAdventureItems)
        {
            if (item.UniqueID == itemID)
            {
                return item;
            }
        }

        return null;
    }

    public void GenerateItemsForNewArea(bool testModeOnly = false)
    {
        
        //change weather
        if (MenuControl.Instance.weatherFeatureOn)
        {
            weatherController.RandomPickWeather();
            UpdateWeather();
        }
        
        MenuControl.Instance.shopMenu.purchases = 0;
        MenuControl.Instance.shopMenu.removals = 0;
        MenuControl.Instance.shopMenu.upgrades = 0;
        MenuControl.Instance.shopMenu.purchaseRefreshCount = 0;
        //MenuControl.Instance.shopMenu.currentRemoveCardCost = MenuControl.Instance.shopMenu.removeCardStartCost;

        exitEarlyAsked = false;
        itemCards.Clear();
        itemCardsForItemIndex.Clear();
        itemCardsToExtraData.Clear();
        adventureItems.Clear();
        
        adventureItemInfos.Clear();
        currentMapTileIndex = -1;
        adventureItemCompletions.Clear();
        adventureItemChecked.Clear();

        foreach (EventTile mapTile in mapTiles)
        {
            Destroy(mapTile.gameObject);
        }

        mapTiles.Clear();
        //checkedTiles.Clear();
        //uncheckedTiles.Clear();
        //finishedTileInfoIds.Clear();

        MenuControl.Instance.areaMenu.currentArea.GenerateItems(testModeOnly);

        for (int ii = 0; ii < adventureItems.Count; ii += 1)
        {
            adventureItemCompletions.Add(false);
            adventureItemChecked.Add(false);
        }

        UpdateChess();
        // if (playerChess)
        // {
        //     Destroy(playerChess.gameObject);
        //     playerChess = null;
        // }

        MenuControl.Instance.dataControl.SaveData();
    }

    public void UpdateChess()
    {
        
        playerChess.GetComponentInChildren<Image>().sprite = playerChessImages[MenuControl.Instance.heroMenu.GetHeroClassIndex()];
    }

    public void UpdateWeather()
    {
        if (MenuControl.Instance.weatherFeatureOn)
        {
            weatherIcon.icon.sprite = weatherController.CurrentWeatherIconInAdventure();
        }
    }

    void centerPlayer()
    {
        var playerCenterPosition = -playerChess.GetComponent<RectTransform>().anchoredPosition;
        playerCenterPosition = new Vector2(Mathf.Clamp(playerCenterPosition.x, -170, 170),
            Mathf.Clamp(playerCenterPosition.y, -100, 100));
        //mapPanel.GetComponent<RectTransform>().anchoredPosition = playerCenterPosition;
        // foreach (var panel in followPlayerMovePanels)
        // {
        //     panel.GetComponent<RectTransform>().anchoredPosition = playerCenterPosition;
        // }
    }

    public void RenderScreen(bool dontCentreMap = false)
    {
        UpdateWeather();
        if (MenuControl.Instance.firstRun)
        {
            //instructionPanel.SetActive(true);
            MenuControl.Instance.firstRun = false;
        }
        else
        {
            instructionPanel.SetActive(false);
        }

        // if (!dontCentreMap)
        //     mapPanel.GetComponent<RectTransform>().anchoredPosition = -mapTiles[currentMapTileIndex].GetComponent<RectTransform>().anchoredPosition;
        centerPlayer();

        RenderPlayerIcon();

        RenderUI();

        RevealTilesByCondition();
    }

    public void RenderMapTiles()
    {
        //Render Map Tiles
        foreach (var mapTile in mapTiles)
        {
            mapTile.Render();
        }
    }

    public void addMapTile(EventTile tile)
    {
        mapTiles.Add(tile);
        //uncheckedTiles.Add(tile);
    }

    public void SortChildrenByY()
    {
        sortChildrenByY(MenuControl.Instance.adventureMenu.spawnPos);
        sortChildrenByY(MenuControl.Instance.adventureMenu.spawnAbovePlayerPos);
    }

    void sortChildrenByY(Transform parent)
    {
        
            
            
        // 获取所有子元素
        var children = parent.Cast<Transform>().ToList();

        // 根据Y值排序
        children.Sort((Transform t1, Transform t2) =>
        {
            var t1Adjust = t1.GetComponent<EventTile>() ? 100 : 0;
            var t2Adjust = t2.GetComponent<EventTile>() ? 100 : 0;
            return (t2.GetComponent<RectTransform>().anchoredPosition.y+t1Adjust)
                .CompareTo(t1.GetComponent<RectTransform>().anchoredPosition.y+t2Adjust);
        });

        // 重新设置子元素的层级关系，以保持正确的顺序
        for (int i = 0; i < children.Count; i++)
        {
            children[i].SetSiblingIndex(i);
        }
    }

    public void updateHeroInfo()
    {
        adventureUIView.GetComponentInChildren<HeroInfoPanel>().updateHeroInfo();
    }

    private float levelLabelHideToAlpha = 0.5f;
    private float levelLabelHideTime = 0.3f;
    public void HideLevelName()
    {
        
        LevelNameText.transform.parent.GetComponent<CanvasGroup>().DOFade(levelLabelHideToAlpha, levelLabelHideTime);
    }

    public void ShowLevelName()
    {
        
        LevelNameText.transform.parent.GetComponent<CanvasGroup>().DOFade(1, levelLabelHideTime);
    }
    
    public void RenderUI()
    {
        //RenderDeckList();
        LevelNameText.text = MenuControl.Instance.areaMenu.currentArea.GetName();
        updateHeroInfo();
        //healingPotionsText.text = "x" + MenuControl.Instance.heroMenu.GetHealingPotionsInDeck().Count.ToString();

        //treasureText.text = "x" + MenuControl.Instance.heroMenu.GetTreasureCardsOwned().Count;
    }

    public void RenderPlayerIcon()
    {
        // if (playerChess == null)
        // {
        //     playerChess = Instantiate(playerChessPrefab, mapPanel);
        //     playerChess.transform.localPosition = new Vector3(mapTiles[currentMapTileIndex].posX * 400, mapTiles[currentMapTileIndex].posY * 400, 0f) + Vector3.left * 75f + Vector3.down * 75f;
        //     playerIcon.transform.localScale = Vector3.one * 0.7f;
        //     playerIcon.disableInteraction = true;
        //     playerIcon.transform.GetChild(0).gameObject.SetActive(true);
        // }
        // var tile = mapTiles[currentMapTileIndex];
        //playerChess.GetComponent<RectTransform>().anchoredPosition = tile.GetComponent<RectTransform>().anchoredPosition;
        // playerIcon.RenderCardOnBoard(MenuControl.Instance.heroMenu.hero);
        // //playerIcon.boardBGImagePlayer1.gameObject.SetActive(true);
        // playerIcon.SetInactive(false);
        // LeanTween.moveLocal(playerIcon.gameObject, mapTiles[currentMapTileIndex].transform.localPosition + Vector3.left * 75f + Vector3.down * 75f, MenuControl.Instance.battleMenu.GetPlaySpeed()).setEaseInOutSine();
    }


    public void SkipCurrentItem()
    {
        GetCurrentAdventureItem().SkipItem(selectedIndex);
        RemoveItem();
    }

    public void FinishItem()
    {
        //var infoId = mapTiles[selectedIndex].info.eventId;
        //finishedTileInfoIds.Add(infoId);
        adventureItemCompletions[selectedIndex] = true;

        
        RenderScreen();
        MenuControl.Instance.dataControl.SaveData();
    }

    public void RemoveItem()
    {
        if (selectedIndex < 0 || selectedIndex >= adventureItems.Count)
        {
            //Debug.LogError($"try to remove item {adventureItems.Count} with wrong selectedIndex {selectedIndex}");
            return;
        }

        adventureItems[selectedIndex].CleanUpItem(selectedIndex);

        adventureItemCompletions[selectedIndex] = true;
        //var infoId = mapTiles[selectedIndex].info.eventId;
        //finishedTileInfoIds.Add(infoId);
        selectedIndex = -1;

        RenderScreen();
        MenuControl.Instance.dataControl.SaveData();
    }

    //public void ConfirmItem(int index)
    //{
    //    selectedIndex = index;
    //    adventureItems[selectedIndex].PerformItem(selectedIndex);
    //}

    public AdventureItemEncounter GetCurrentAdventureItemEncounter()
    {
        if (testEncounterOnly)
        {
            return testEncounter;
        }

        if (tutorialEncounterOnly)
        {
            return tutorialEncounter;
        }

        if (adventureItems.Count > selectedIndex && selectedIndex > -1 &&
            adventureItems[selectedIndex] is AdventureItemEncounter)
        {
            return (AdventureItemEncounter)adventureItems[selectedIndex];
        }

        return null;
    }

    public AdventureItem GetCurrentAdventureItem()
    {
        if (adventureItems.Count > selectedIndex && selectedIndex > -1)
            return adventureItems[selectedIndex];

        return null;
    }

    // public List<AdventureItemEncounter> GetEncountersByLevel(int level)
    // {
    //     List<AdventureItemEncounter> items = new List<AdventureItemEncounter>();
    //     foreach (AdventureItemEncounter item in MenuControl.Instance.areaMenu.currentArea.enemies)
    //     {
    //         if (item.level == level)
    //         {
    //             items.Add(item);
    //         }
    //     }
    //
    //     return items;
    // }

    public AdventureItemRandomEvent GetNextRandomEvent(List<AdventureItemRandomEvent> randomEvents)
    {
        if (randomEvents.Count == 0)
        {
            Debug.LogError("random event count is 0");
            
            randomEvents = MenuControl.Instance.adventureMenu.GetRandomEvents();
            
        }
        

        AdventureItemRandomEvent randomEvent =
            randomEvents[
                Random.Range(0, randomEvents.Count())];
        randomAdventureItems.Add(randomEvent);
        randomEvents.Remove(randomEvent);
        return randomEvent;
    }

    public List<AdventureItemRandomEvent> GetRandomEvents()
    {
        List<AdventureItemRandomEvent> items = new List<AdventureItemRandomEvent>();
        foreach (AdventureItem item in allAdventureItems)
        {
            if (item is AdventureItemRandomEvent)
            {
                AdventureItemRandomEvent randomEvent = (AdventureItemRandomEvent)item;
                if (!MenuControl.Instance.heroMenu.seasonsMode &&
                    randomEvent.eventDefinition.UniqueID == "EventSantaKrumpusOrNothing")
                {
                    break;
                }

                if (!MenuControl.Instance.csvLoader.chineseNameToEventMap.ContainsKey(randomEvent.GetChineseName()))
                {
                    continue;
                }

                if (randomEvent.GetChineseName() == "低级宝箱")
                {
                    Debug.LogError("???");
                }

                //if (!randomAdventureItems.Contains(randomEvent))
                {
                    if (randomEvent.validAreaInts.Count == 0 ||
                        randomEvent.validAreaInts.Contains(MenuControl.Instance.areaMenu.areasVisited))
                    {
                        if (randomEvent.validWithTalents.Count != 0)
                        {
                            bool valid = false;
                            foreach (var talentAcquired in  MenuControl.Instance.levelUpMenu.variableTalentsAcquired)
                            {
                                foreach (var talent in randomEvent.validWithTalents)
                                {
                                    if (talentAcquired.UniqueID == talent.UniqueID)
                                    {
                                        valid = true;
                                        break;
                                    }
                                }
                            }

                            if (!valid)
                            {
                                continue;
                            }
                        }
                           
                        items.Add((AdventureItemRandomEvent)item);
                    }
                }
            }
        }

        return items;
    }


    public AdventureItemPurchaseCards GetShop()
    {
        foreach (AdventureItem item in allAdventureItems)
        {
            if (item is AdventureItemPurchaseCards)
            {
                return (AdventureItemPurchaseCards)item;
            }
        }

        return null;
    }

    public AdventureItemUpgradeCards GetBlacksmith()
    {
        foreach (AdventureItem item in allAdventureItems)
        {
            if (item is AdventureItemUpgradeCards)
            {
                return (AdventureItemUpgradeCards)item;
            }
        }

        return null;
    }

    public AdventureItemHealingShrine GetShrine()
    {
        foreach (AdventureItem item in allAdventureItems)
        {
            if (item is AdventureItemHealingShrine)
            {
                return (AdventureItemHealingShrine)item;
            }
        }

        return null;
    }

    public AdventureItemDoorwayBoss GetBossDoorway()
    {
        foreach (AdventureItem item in allAdventureItems)
        {
            if (item is AdventureItemDoorwayBoss)
            {
                return (AdventureItemDoorwayBoss)item;
            }
        }

        return null;
    }

    public AdventureItem GetStoryEvent(MapEventInfo info)
    {
        if (info.storyType.Length == 0)
        {
            Debug.LogError(info.eventId + " event id does not have story type");
        }

        if (!MenuControl.Instance.csvLoader.storyEventInfoByEventId.ContainsKey(info.storyType))
        {
            Debug.LogError(info.eventId + " 's story event ID " + info.storyType + " is not in storyEvent");
        }

        var storyItem = Instantiate<GameObject>(storyEventPrefab);
        return storyItem.GetComponent<AdventureItemStory>();
    }

    public AdventureItemTreasure GetTreasure()
    {
        foreach (AdventureItem item in allAdventureItems)
        {
            if (item is AdventureItemTreasure)
            {
                return (AdventureItemTreasure)item;
            }
        }

        return null;
    }
    
    

    public AdventureItemRemoveCards GetMonastery()
    {
        foreach (AdventureItem item in allAdventureItems)
        {
            if (item is AdventureItemRemoveCards)
            {
                return (AdventureItemRemoveCards)item;
            }
        }

        return null;
    }

    public AdventureItem GetItemOfType<T>()
    {
        foreach (AdventureItem item in allAdventureItems)
        {
            if (item is T)
            {
                return item;
            }
        }

        return null;
    }


    public void HideInfo()
    {
        MenuControl.Instance.infoMenu.HideMenu();
    }


    public void StartEncounter()
    {
        MenuControl.Instance.HideSubMenus();

        MenuControl.Instance.battleMenu.ResetBattle();

        MenuControl.Instance.battleMenu.SetupBattle();
    }

    public bool isRemovedFromScene(EventTile tile)
    {
        var eventId = tile.info.eventId;
        if (!isEventCountAsFinished(eventId))
        {
            return false;
        }

        var info = tile.info;
        if (info.eventType == "旅行商人" || info.eventType == "吟游诗人" || info.eventType == "驱灵人")
        {
            return false;
        }

        return true;
    }

    public void SelectMapTile(EventTile tile)
    {
    }

    public void DeSelectMapTile(EventTile tile)
    {
    }

    public void ClickMapTile(EventTile tile)
    {
        //if (ignoreMapClicks) return;

        // List<MapTile> destinationPath = TilesToMapTile(mapTiles[currentMapTileIndex], tile, new List<MapTile>());
        // MapTile closedDoortile = null;
        // MapTile skippedEncounterTile = null;
        // foreach (MapTile mapTile in destinationPath)
        // {
        //     //if (!MenuControl.Instance.testMode && mapTile.adventureItemIndex >= 0 && adventureItems[mapTile.adventureItemIndex] == GetItemOfType<AdventureItemDoorwayClosed>() && adventureItemCompletions[mapTile.adventureItemIndex] == false)
        //     //{
        //     //    closedDoortile = mapTile;
        //     //    break;
        //     //} //Not needed anymore
        //     if (!MenuControl.Instance.testMode && mapTile.adventureItemIndex >= 0 && adventureItems[mapTile.adventureItemIndex] is AdventureItemEncounter && !adventureItemCompletions[mapTile.adventureItemIndex] && destinationPath.IndexOf(mapTile) > 0 && destinationPath.IndexOf(mapTile) < destinationPath.Count - 1)
        //     {
        //         skippedEncounterTile = mapTile;
        //         break;
        //     }
        // }
        // if (closedDoortile != null)
        // {
        //     List<MapTile> doorPathToStart = TilesToMapTile(mapTiles[0], closedDoortile, new List<MapTile>());
        //     doorPathToStart.Remove(closedDoortile);
        //     foreach (MapTile mapTile in doorPathToStart)
        //     {
        //         if (destinationPath.Contains(mapTile))
        //         {
        //             LeanTween.rotateZ(playerIcon.gameObject, 15f, 0.07f).setEaseInOutSine().setLoopPingPong(3);
        //             MenuControl.Instance.ShowBlockingNotification(null, MenuControl.Instance.GetLocalizedString("LockedDoorTitle"), MenuControl.Instance.GetLocalizedString("LockedDoorPrompt"), null);
        //             return;
        //         }
        //     }
        // }
        // if (skippedEncounterTile != null)
        // {
        //     LeanTween.rotateZ(playerIcon.gameObject, 15f, 0.07f).setEaseInOutSine().setLoopPingPong(3);
        //     MenuControl.Instance.ShowBlockingNotification(null, MenuControl.Instance.GetLocalizedString("SkippedEncounterTitle"), MenuControl.Instance.GetLocalizedString("SkippedEncounterPrompt"), null);
        //     return;
        //
        // }

        // if (!tile.revealed && tile.CanBeRevealed())
        // {
        //
        //
        // }
        // else
        {
            if (mapTiles.IndexOf(tile) == currentMapTileIndex)
            {
                performItem(tile);
            }
            else
            {
                //destinationPath.RemoveAt(0);
                //for (int ii = 0; ii < destinationPath.Count; ii += 1)
                // {
                //    // int xx = ii;
                //     LeanTween.delayedCall(MenuControl.Instance.battleMenu.GetPlaySpeed() * ii, () =>
                //     {
                //         //MapTile tile2 = destinationPath[xx];
                currentMapTileIndex = mapTiles.IndexOf(tile);
                //         RenderPlayerIcon();
                //         Doozy.Engine.Soundy.SoundyManager.Play(footstepsSound);
                //         RevealTilesAroundMe();
                //     });
                //
                // }
                var targetPosition = tile.GetComponent<RectTransform>().anchoredPosition;

               StartCoroutine( playerMove(targetPosition, () => { performItem(tile); }));
            }
        }
    }

    public IEnumerator playerMove(Vector2 targetPosition, Action callback)
    {
        
        if (isMoving)
        {
            playerChess.GetComponent<RectTransform>().DOKill(false);
            
            parabolaVisual.clearParabola();
        }

        yield return new WaitForSeconds(0.01f);
        var playerPosition = playerChess.GetComponent<RectTransform>().anchoredPosition;
        // LTBezier bezier = new LTBezier(playerChess.GetComponent<RectTransform>().anchoredPosition,tile.GetComponent<RectTransform>().anchoredPosition)

        List<Vector3> parabolaPoints = new List<Vector3>();
        float dis = (targetPosition - playerPosition).magnitude;

        var resolution = (int)Math.Max(parabolaResolution, dis / minParabolaDotDistance); //min count
        // var startPosition = playerChess.GetComponent<RectTransform>().position;

        for (int i = 0; i <= resolution; i++)
        {
            float t = i / (float)resolution;
            Vector3 position = CalculateParabola(playerPosition, targetPosition, parabolaHeight, t);
            parabolaPoints.Add(position);
        }

        var parabolaOBs = parabolaVisual.CreateParabola(playerPosition, targetPosition, resolution);
        // LeanTween.move(gameObject, parabolaPoints.ToArray(), MenuControl.Instance.battleMenu.GetPlaySpeed()) // Set desired duration
        //     .setEase(LeanTweenType.linear) // Set the desired easing type
        //     .setOnComplete(() => 
        //     {
        //         Debug.Log("Movement complete!");
        //         parabolaVisual.clearParabola();
        //     });
        var distance = (targetPosition - playerPosition).magnitude;
        var moveSpeed = MenuControl.Instance.battleMenu.GetPlaySpeed() * distance / 150f * playerMoveSpeed;
        playerChess.GetComponent<RectTransform>().DOLocalPath(parabolaPoints.ToArray(), moveSpeed, PathType.CatmullRom)
            .SetEase(Ease.Linear)
            .OnWaypointChange((int waypointIndex) =>
            {
                //Debug.Log("Reached waypoint: " + waypointIndex);
                if (waypointIndex < parabolaOBs.Count)
                {
                    Destroy(parabolaOBs[waypointIndex]);
                }
            })
            .OnComplete(() =>
            {
                //Debug.Log("Movement complete!");
                parabolaVisual.clearParabola();
                playerMoveParticle.Stop();
                isMoving = false;
                callback();
            });
        playerMoveParticle.Play();
        // LeanTween.move(playerChess.GetComponent<RectTransform>(), targetPosition, MenuControl.Instance.battleMenu.GetPlaySpeed());
        //LeanTween.move(mapPanel.GetComponent<RectTransform>(), -tile.GetComponent<RectTransform>().anchoredPosition, MenuControl.Instance.battleMenu.GetPlaySpeed() * (destinationPath.Count + 1)).setEaseInOutSine();

        //ignoreMapClicks = true;
        isMoving = true;
        // LeanTween.delayedCall(moveSpeed, () =>
        // {
        //     //ignoreMapClicks = false;
        // });
    }

    void performItem(EventTile tile)
    {
        if (tile.story)
        {
            selectedIndex = tile.adventureItemIndex;
            MenuControl.Instance.dataControl.SaveData();
            tile.story.PerformItem(selectedIndex);
        }
        else if (tile.adventureItemIndex != -1 && !isRemovedFromScene(tile))
        {
            selectedIndex = tile.adventureItemIndex;
            MenuControl.Instance.dataControl.SaveData();
            adventureItems[selectedIndex].PerformItem(selectedIndex);
        }
    }

    public float parabolaHeight = 2f; // 抛物线高度
    public int parabolaResolution = 10; // 抛物线分辨率
    public float minParabolaDotDistance = 1f;
    public float playerMoveSpeed = 1;

    public Vector3 CalculateParabola(Vector2 start, Vector2 end, float height, float t)
    {
        Vector2 direction = end - start;
        Vector2 orthoDirection = new Vector2(-direction.y, direction.x).normalized * height; // 计算法线方向

        float parabolicT = t * 2 - 1;
        Vector2 travelDirection = end - start;

        Vector2 result = start + t * travelDirection;

        var dis = (end - start).magnitude * 0.25f;
        result += ((-parabolicT * parabolicT + 1) * dis) * orthoDirection; // 使用法线方向来添加高度
        return result;
    }

    void revealTile(EventTile tile)
    {
        tile.isRevealed = true;
        adventureItemChecked[tile.adventureItemIndex] = true;
        //uncheckedTiles.Remove(tile);
        //checkedTiles.Add(tile);
    }

    bool isEventCountAsFinished(int eventId)
    {
        var adventureId = -1;
        foreach (var tile in mapTiles)
        {
            if (tile.info.eventId == eventId)
            {
                adventureId = tile.adventureItemIndex;
            }
        }

        if (adventureId == -1)
        {
            Debug.LogError("adventureId " + adventureId + " not in map tile");
            return true;
        }

        if (adventureItemCompletions[adventureId])
        {
            return true;
        }


        return false;
    }

    public void RevealTilesByCondition()
    {
        List<EventTile> toRevealList = new List<EventTile>();
        foreach (var tile in uncheckedTiles.ToList())
        {
            if (tile.info.unlockConditionParam == null || tile.info.unlockConditionParam.Count == 0)
            {
                revealTile(tile);
            }
            else
            {
                var unlockConditionType = tile.info.unlockConditionType;
                switch (unlockConditionType)
                {
                    case "完成全部指定id事件":
                        var allConditionSatisfied = true;
                        foreach (var condition in tile.info.unlockConditionParam)
                        {
                            if (!isEventCountAsFinished(condition))
                            {
                                allConditionSatisfied = false;
                                break;
                            }
                        }

                        if (allConditionSatisfied)
                        {
                            toRevealList.Add(tile);
                        }

                        break;
                    case "完成任意指定id事件":
                        var anyConditionSatisfied = false;
                        foreach (var condition in tile.info.unlockConditionParam)
                        {
                            if (isEventCountAsFinished(condition))
                            {
                                anyConditionSatisfied = true;
                                break;
                            }
                        }

                        if (anyConditionSatisfied)
                        {
                            toRevealList.Add(tile);
                        }

                        break;
                    case "完成指定事件id的指定选项":
                        if (tile.info.unlockConditionParam.Count != 2)
                        {
                            Debug.LogError(tile.info.eventId + " is option type but condition count is not 2");
                        }
                        else
                        {
                            if (eventIdSelectOptionId.ContainsKey(tile.info.unlockConditionParam[0]))
                            {
                                if (eventIdSelectOptionId[tile.info.unlockConditionParam[0]] ==
                                    tile.info.unlockConditionParam[1])
                                {
                                    toRevealList.Add(tile);
                                }
                            }
                        }

                        break;
                }
            }
        }

        if (toRevealList.Count > 0)
        {
            // List<int> chance = new List<int>() { 35, 50, 15 };
            // int totalChance = 0;
            // // todo: need to shuffle to reveal list
            // var chanceCount = Math.Min(chance.Count, toRevealList.Count);
            // for (int i = 0; i < chanceCount; i++)
            // {
            //     totalChance += chance[i];
            // }
            //
            // var randomValue = Random.Range(0, totalChance);
            // if (shouldShowAllEvents)
            // {
            //     randomValue = totalChance;
            // }
            // var currentChance = 0;

            var chanceCount = toRevealList.Count;
            foreach (var toReveal in toRevealList)
            {
                adventureItemChecked[toReveal.adventureItemIndex] = true;
            }

            for (int i = 0; i < chanceCount; i++)
            {
                //if (randomValue >= currentChance)
                {
                    // for (int j = 0; j < i; j++)
                    // {
                    revealTile(toRevealList[i]);
                    //}

                    //break;
                }
                //currentChance += chance[i];
            }
        }
    }

    // void RevealTile(MapTile tile, int andNextTilesCount)
    // {
    //     if (!tile.revealed)
    //     {
    //         tile.revealed = true;
    //         tile.GetComponent<CanvasGroup>().alpha = 0f;
    //         LeanTween.alphaCanvas(tile.GetComponent<CanvasGroup>(), 1f, 0.7f).setEaseOutSine();
    //         MenuControl.Instance.dataControl.SaveData();
    //         RenderScreen(true);
    //     }
    //
    //     if (tile.adventureItemIndex >= 0)
    //     {
    //
    //         if (adventureItemCompletions[tile.adventureItemIndex] || tile.skipped)
    //         {
    //
    //         }
    //         else if (adventureItems[tile.adventureItemIndex].alwaysOpen)
    //         {
    //
    //         }
    //         else
    //         {
    //             return;
    //         }
    //     }
    //
    //     if (andNextTilesCount > 0)
    //     {
    //         //If tile is not a skipped encounter && not a lock door reveal around it
    //         foreach (MapTileDirection direction in tile.GetDirections())
    //         {
    //             MapTile nextTile = tile.GetTileInDirection(direction);
    //             if (nextTile != null)
    //             {
    //                 RevealTile(nextTile, andNextTilesCount - 1);
    //             }
    //         }
    //     }
    // }


    // public List<MapTile> TilesToMapTile(MapTile startingTile, MapTile targetTile, List<MapTile> tilesBefore)
    // {
    //     List<MapTile> tilesSoFar = new List<MapTile>();
    //     tilesSoFar.AddRange(tilesBefore);
    //
    //     if (!tilesSoFar.Contains(startingTile))
    //     {
    //         tilesSoFar.Add(startingTile);
    //     }
    //     if (targetTile == startingTile)
    //     {
    //         return tilesSoFar;
    //     }
    //
    //     foreach (MapTileDirection direction in startingTile.GetDirections())
    //     {
    //         MapTile tile = startingTile.GetTileInDirection(direction);
    //         if (tile != null && !tilesSoFar.Contains(tile))
    //         {
    //             List<MapTile> newPath = TilesToMapTile(tile, targetTile, tilesSoFar);
    //             if (newPath.Count > 0) return newPath;
    //         }
    //     }
    //
    //     return new List<MapTile>();
    // }

    public List<Card> GetItemCards(int index = -1)
    {
        if (index == -1)
        {
            index = selectedIndex;
        }
        List<Card> cards = new List<Card>();
        for (int ii = 0; ii < itemCards.Count; ii += 1)
        {
            if (itemCardsForItemIndex[ii] == index)
            {
                cards.Add(itemCards[ii]);
            }
        }
        return cards;
    }
    public List<int> GetItemExtraData(int index = -1)
    {if (index == -1)
        {
            index = selectedIndex;
        }
        List<int> cards = new List<int>();
        for (int ii = 0; ii < itemCards.Count; ii += 1)
        {
            if (itemCardsForItemIndex[ii] == index)
            {
                cards.Add(itemCardsToExtraData[ii]);
            }
        }
        return cards;
    }

    public void ReplaceItemCards(List<Card> newCards,int index = -1)
    {if (index == -1)
        {
            index = selectedIndex;
        }
        int i = 0;
        if (newCards.Count == 0)
        {
            return;
        }
        for (int ii = 0; ii < itemCards.Count; ii += 1)
        {
            if (itemCardsForItemIndex[ii] == index)
            {
                if (i >= newCards.Count)
                {
                    itemCards.RemoveAt(ii);
                    itemCardsForItemIndex.RemoveAt(ii);
                    itemCardsToExtraData.RemoveAt(ii);
                    ii--;
                    //return;
                }
                else
                {
                    itemCards[ii] = newCards[i] ;
                    i++;
                }
            }
        }

        for (; i < newCards.Count; i++)
        {
            itemCards.Add((newCards[i]));
            addToItemCardsForItemIndex(index);
        }
        
    }public void ReplaceItemExtraData(List<int> newCards,int index = -1)
    {if (index == -1)
        {
            index = selectedIndex;
        }
        int i = 0;
        if (newCards.Count == 0)
        {
            return;
        }
        
        for (int ii = 0; ii < itemCards.Count; ii += 1)
        {
            if (itemCardsForItemIndex[ii] == index)
            {
                itemCardsToExtraData[ii] = newCards[i] ;
                i++;
                if (i >= newCards.Count)
                {
                    return;
                }
            }
        }

        if (i != newCards.Count)
        {
            Debug.LogError(("Error replacing item extra data"));
        }

        
    }
    

    public void RemoveItemCard(Card card,int index = -1)
    {if (index == -1)
        {
            index = selectedIndex;
        }
        for (int ii = 0; ii < itemCards.Count; ii += 1)
        {
            if (itemCardsForItemIndex[ii] == index)
            {
                if (itemCards[ii] == card)
                {
                    itemCards.RemoveAt(ii);
                    itemCardsForItemIndex.RemoveAt(ii);
                    itemCardsToExtraData.RemoveAt(ii);
                    return;
                }
            }
        }
    }

    public void ShowHeroInfo()
    {
        //todo: don't delete!!
        //MenuControl.Instance.infoMenu.ShowInfo(MenuControl.Instance.heroMenu.hero, heroImage.transform.position);
    }

    public void ClickDownOnMap()
    {
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(MenuControl.Instance.GetComponent<Canvas>().transform as RectTransform, Input.mousePosition, MenuControl.Instance.GetComponent<Canvas>().worldCamera, out pointerOriginalPos);

        //mapPanelOriginalPos = mapPanel.transform.position;
    }

    public void ClickUpOnMap()
    {
        pointerOriginalPos = Vector2.zero;
    }

    // List<MapTile> GetAdjacentUnrevealedTiles(MapTile tile)
    // {
    //     List<MapTile> otherTiles = new List<MapTile>();
    //     foreach (MapTileDirection direction in tile.GetDirections())
    //     {
    //         MapTile otherTile = tile.GetTileInDir'ection(direction);
    //         if (!otherTile.revealed)
    //             otherTiles.Add(otherTile);
    //     }
    //
    //     return otherTiles;
    // }

    public void AnimateVCsToDeck(List<VisibleCard> vcsToAnimateToDeck)
    {
        for (int ii = 0; ii < vcsToAnimateToDeck.Count; ii += 1)
        {
            VisibleCard vc = Instantiate(vcsToAnimateToDeck[ii], MenuControl.Instance.transform);
            vc.transform.position = vcsToAnimateToDeck[ii].transform.position;
            vc.disableInteraction = true;
            vc.isMenuCard = true;
            vc.StopHighlight();
            vc.GetComponent<Canvas>().overrideSorting = true;
            vc.GetComponent<Canvas>().sortingOrder = 12;
            Vector3 pos = deckIcon.position;


            LeanTween.move(vc.gameObject, pos, MenuControl.Instance.battleMenu.GetPlaySpeed()).setEaseOutSine()
                .setDestroyOnComplete(true).setDelay(0.07f * ii);
            LeanTween.scale(vc.gameObject, Vector3.one * 0.5f, MenuControl.Instance.battleMenu.GetPlaySpeed())
                .setDelay(0.07f * ii);
            LeanTween.rotateAround(vc.gameObject, Vector3.forward, 720f, MenuControl.Instance.battleMenu.GetPlaySpeed())
                .setDelay(0.07f * ii);
            LeanTween.alphaCanvas(vc.GetComponent<CanvasGroup>(), 0f,
                    MenuControl.Instance.battleMenu.GetPlaySpeed() * 0.3f)
                .setDelay((0.07f * ii) + (MenuControl.Instance.battleMenu.GetPlaySpeed() * 0.7f));

            Destroy(vc.gameObject, MenuControl.Instance.battleMenu.GetPlaySpeed() + (0.07f * ii));

            LeanTween.delayedCall(0.07f * ii, () => { Doozy.Engine.Soundy.SoundyManager.Play("Menu", "CardLift"); });
        }
    }

    public void ClickOnHealingPotions(Card card)
    {
            List<Card> cardsToShow = new List<Card>();
            cardsToShow.Add(card);

            List<string> buttonLabels = new List<string>();
            buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Cancel"));
            buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Confirm"));

            List<System.Action> actions = new List<System.Action>();
            actions.Add(() => { });
            actions.Add(() =>
            {
                MenuControl.Instance.cardChoiceMenu.GetComponent<Canvas>().sortingOrder = 7;
                foreach (int integer in MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts)
                {
                    Card card = MenuControl.Instance.cardChoiceMenu.visibleCardsShown[integer].card;
                    MenuControl.Instance.heroMenu.RemoveCardFromDeck(card);
                    MenuControl.Instance.heroMenu.hero.Heal(null, null, card.GetComponent<Heal>().amountToHeal);
                }

                RenderScreen(false);
                Doozy.Engine.Soundy.SoundyManager.Play(healingPotionSound);
                MenuControl.Instance.dataControl.SaveData();

                if (MenuControl.Instance.shopMenu.gameObject.activeInHierarchy)
                {
                    MenuControl.Instance.shopMenu.ShowMenu();
                }
                MenuControl.Instance.itemsMenu.ShowMyArtifacts();
            });

            MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions,
                MenuControl.Instance.GetLocalizedString("ConsumeHealingPotiontPrompt"), 1, 1, true, -1, false);
            MenuControl.Instance.cardChoiceMenu.GetComponent<Canvas>().sortingOrder = 10;
        
    }
    public void ClickOnHealingPotions()
    {
        if (MenuControl.Instance.heroMenu.GetHealingPotionsInDeck().Count > 0)
        {
            List<Card> cardsToShow = new List<Card>();
            cardsToShow.Add(MenuControl.Instance.heroMenu.GetHealingPotionsInDeck()[0]);

            List<string> buttonLabels = new List<string>();
            buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Cancel"));
            buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Confirm"));

            List<System.Action> actions = new List<System.Action>();
            actions.Add(() => { });
            actions.Add(() =>
            {
                
                MenuControl.Instance.cardChoiceMenu.GetComponent<Canvas>().sortingOrder = 7;
                foreach (int integer in MenuControl.Instance.cardChoiceMenu.selectedVisibleCardInts)
                {
                    Card card = MenuControl.Instance.cardChoiceMenu.visibleCardsShown[integer].card;
                    MenuControl.Instance.heroMenu.RemoveCardFromDeck(card);
                    MenuControl.Instance.heroMenu.hero.Heal(null, null, card.GetComponent<Heal>().amountToHeal);
                }

                RenderScreen(false);
                Doozy.Engine.Soundy.SoundyManager.Play(healingPotionSound);
                MenuControl.Instance.dataControl.SaveData();

                if (MenuControl.Instance.shopMenu.gameObject.activeInHierarchy)
                {
                    MenuControl.Instance.shopMenu.ShowMenu();
                }
            });

            MenuControl.Instance.cardChoiceMenu.ShowChoice(cardsToShow, buttonLabels, actions,
                MenuControl.Instance.GetLocalizedString("ConsumeHealingPotiontPrompt"), 1, 1, true, -1, false);
            MenuControl.Instance.cardChoiceMenu.GetComponent<Canvas>().sortingOrder = 10;
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

        cardListText.text = MenuControl.Instance.heroMenu.cardsOwned.Count + " " +
                            MenuControl.Instance.GetLocalizedString("Cards");
    }
}