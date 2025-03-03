using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct RestrictLevelForAreasVisited
{
    public List<int> percentageForEachLevel;
}
public class AreaMenu : BasicMenu
{

    public List<Area> allAreas = new List<Area>();
    public List<int> areaChoices = new List<int>();
    public int areasVisited;
    public Area currentArea;
    public int skipsTaken;

    public bool currentAreaComplete;

    public Button choiceButtonA;
    public Button choiceButtonB;

    public Image areaImage;
    public Text areaDescriptionText;

    public List<RestrictLevelForAreasVisited> restrictLevelForAreasVisitedAreas;

    public void ShowAreaSelection()
    {

        // areaChoices.Clear();
        // areaChoices.Add(allAreas.IndexOf(currentArea) + 1);
        SelectArea(allAreas.IndexOf(currentArea) + 1);
    }

    public void HoverOverArea(int index)
    {
        //areaImage.sprite = allAreas[areaChoices[index]].battleMapBGSprites[0];
        areaDescriptionText.text = allAreas[areaChoices[index]].GetDescription();
    }

    public void SelectArea(int index)
    {
        currentArea = allAreas[index];
        MenuControl.Instance.infoMenu.HideMenu();
        HideMenu();

        skipsTaken = 0;
        areasVisited += 1;
        MenuControl.Instance.progressMenu.areasExplored += 1;
        currentAreaComplete = false;

        MenuControl.Instance.adventureMenu.GenerateItemsForNewArea(); //Savedata
        MenuControl.Instance.adventureMenu.ContinueAdventure();

        var newIntroText = "IntroPopupTitle" + (MenuControl.Instance.areaMenu.currentArea.levelId);
        if (MenuControl.Instance.GetLocalizedString(newIntroText) == newIntroText)
        {
            
            MenuControl.Instance.questMenu.ShowMenu();
        }
        else
        {
            MenuControl.Instance.imageAndTextPopup.ShowIntro();
        }
        
        MenuControl.Instance.LogEvent("GeneratedArea" + areasVisited);
    }

    public void SetupFirstArea()
    {
        areaChoices.Clear();
        areaChoices.Add(0);
        var heroIndex = MenuControl.Instance.heroMenu.getCurrentClassIndex();
        SelectArea(heroIndex*3);
    }

    public float GetSkipChance()
    {
        if (MenuControl.Instance.heroMenu.ascensionMode >= 11) return 0f;

        float chance = 0.25f;
        if (MenuControl.Instance.levelUpMenu.variableTalentsAcquired.Contains(MenuControl.Instance.levelUpMenu.extraSneakingTalent))
        {
            chance = 0.75f;
            if (skipsTaken == 0) chance = 1f;
            if (skipsTaken == 1) chance = .95f;
            if (skipsTaken == 2) chance = .90f;
            if (skipsTaken == 3) chance = .85f;
            if (skipsTaken == 4) chance = .80f;
        }
        else
        {
            if (skipsTaken == 0) chance = 1f;
            if (skipsTaken == 1) chance = .75f;
            if (skipsTaken == 2) chance = .50f;
        }
        return chance;

    }
}
