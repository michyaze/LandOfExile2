using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine.UI;
using UnityEngine.UI;

public class LoadingMenu : BasicMenu
{
    public UIView view;

    private void Update()
    {
        // if (view.IsVisible && !view.IsHiding)
        // {
        //     if (UIPopupManager.QueueIsEmpty && UIPopupManager.CurrentVisibleQueuePopup == null)
        //     {
        //         HideMenu();
        //     }
        // }
    }

    public void ShowLoading()
    {
        ShowMenu();
        
        //StartCoroutine(test());
    }

    IEnumerator test()
    {
        yield return new WaitForSeconds(1);
        view.Hide();
    }

    public void ShowCutscene(Cutscene cutscene)
    {
        ShowMenu();

        UIPopup popup = UIPopup.GetPopup("Cutscene");
        if (popup == null) return;

        popup.Data.Images[0].sprite = cutscene.GetSprite();
        popup.Data.SetLabelsTexts(cutscene.GetDescription());

        if (I2.Loc.LocalizationManager.CurrentLanguage != MenuControl.Languages.English.ToString())
        {
            popup.Data.Labels[0].GetComponent<Text>().font = MenuControl.Instance.GetSafeFont();
        }

#if !UNITY_STANDALONE
        popup.Data.Labels[0].GetComponent<Text>().fontSize = 53;
#endif

        UIPopupManager.AddToQueue(popup);
    }




}
