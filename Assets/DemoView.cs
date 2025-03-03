using System.Collections;
using System.Collections.Generic;
using I2.Loc;
using Pool;
using UnityEngine;
using UnityEngine.UI;

public class DemoView : BasicMenu
{  public GameObject[] chineseOnlyObjects;

    public float enableButtonDelay = 2f;
     // void UpdateLanguageRelatedStuff()
     // {
     //
     //     if (LocalizationManager.CurrentLanguage == MenuControl.Languages.Chinese.ToString())
     //     {
     //         foreach (GameObject obj in chineseOnlyObjects)
     //         {
     //             obj.SetActive(true);
     //         }
     //
     //     }
     //     else
     //     {
     //         foreach (GameObject obj in chineseOnlyObjects)
     //         {
     //             obj.SetActive(false);
     //         }
     //     }
     // }
    public override void ShowMenu()
    {
        base.ShowMenu();
        MenuControl.Instance.shownSteamPage = true;
        //UpdateLanguageRelatedStuff();
        //EventPool.OptIn("ChangeLanguage", UpdateLanguageRelatedStuff);
        foreach (var button in GetComponentsInChildren<Button>())
        {
            button.interactable = (false);
        }

        StartCoroutine((enableButton()));
    }

    IEnumerator enableButton()
    {
        yield return new WaitForSeconds(enableButtonDelay);
        foreach (var button in GetComponentsInChildren<Button>())
        {
            button.interactable = (true);
        }
    }
}
