  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestMenu : BasicMenu
{

    public Text label;

    public override void ShowMenu()
    {
        base.ShowMenu();

        LeanTween.delayedCall(2f, () => 
        {
            CloseMenu();
          
        });

        label.text = MenuControl.Instance.GetLocalizedString("QuestPrompt" + (MenuControl.Instance.areaMenu.currentArea.levelId));
        //Show cutscenes for class
        for (int ii = 0; ii < MenuControl.Instance.heroMenu.heroClass.cutscenes.Count; ii += 1)
        {
            //MenuControl.Instance.loadingMenu.ShowLoading();
            // if (MenuControl.Instance.heroMenu.heroClass.cutsceneAreas[ii] == MenuControl.Instance.areaMenu.areasVisited)
            // {
            //     MenuControl.Instance.cutsceneMenu.ShowCutscene(MenuControl.Instance.heroMenu.heroClass.cutscenes[ii]);
            // }
        }

    }
}
