using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefeatMenu : BasicMenu {

    public Spine.Unity.SkeletonGraphic skeletonGraphic;
    public Doozy.Engine.Soundy.SoundyData impactSFX;
    public Text seedText;
    public Text summmaryText;

    public override void ShowMenu()
    {
        base.ShowMenu();

        // int goldToGive = Mathf.FloorToInt(MenuControl.Instance.heroMenu.GetCurrentGold() * 0.25f);
        // if (MenuControl.Instance.heroMenu.easyMode)
        // {
        //     goldToGive = Mathf.CeilToInt(goldToGive / 2f);
        // }
        // MenuControl.Instance.heroMenu.accumulatedGold += goldToGive;

        MenuControl.Instance.heroMenu.isAlive = false;
        MenuControl.Instance.dataControl.SaveData();

        seedText.text = "Seed: " + MenuControl.Instance.currentSeed;

        //Animate 
        //skeletonGraphic.AnimationState.SetAnimation(0, "defeat", false);
        //skeletonGraphic.AnimationState.AddAnimation(0, "defeat_idle", true, 1.2f);

        LeanTween.delayedCall(0.2f, () =>
        {
            Doozy.Engine.Soundy.SoundyManager.Play(impactSFX);

        });


        summmaryText.text = MenuControl.Instance.GetLocalizedString("DefeatSummaryPrompt");
        //summmaryText.text = summmaryText.text.Replace("AA", "<color=yellow>" + goldToGive.ToString() + " / " + MenuControl.Instance.heroMenu.GetCurrentGold() + "</color>");
        summmaryText.text = summmaryText.text.Replace("ZZ", "<color=yellow>" + MenuControl.Instance.areaMenu.areasVisited.ToString() + " / 3" + "</color>");
        summmaryText.text = summmaryText.text.Replace("YY", "<color=yellow>" + MenuControl.Instance.areaMenu.currentArea.GetComponent<AreaGeneration>().GetItemsRemainingText() + "</color>");
        summmaryText.text = summmaryText.text.Replace("XX", "<color=yellow>" + MenuControl.Instance.heroMenu.currentLevel.ToString() + "</color>");
        summmaryText.text = summmaryText.text.Replace("WW", "<color=yellow>" + MenuControl.Instance.heroMenu.cardsDiscoveredThisRun.ToString() + "</color>");
        summmaryText.text = summmaryText.text.Replace("VV", "<color=yellow>" + MenuControl.Instance.heroMenu.ascensionMode.ToString() + "</color>");

        //TODO hint strings go here    

        string encounterString = "";
        if (MenuControl.Instance.adventureMenu.GetCurrentAdventureItemEncounter() != null)
        {
            encounterString = MenuControl.Instance.adventureMenu.GetCurrentAdventureItemEncounter().UniqueID;
        }

        MenuControl.Instance.LogEvent("Defeat_" + encounterString);// + MenuControl.Instance.heroMenu.GetLevelClassPathString());
    }

    public void GoMainMenu()
    {
     
        MenuControl.Instance.ReloadGame();

    }
}
