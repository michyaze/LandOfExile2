using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class ConfirmPopupView : BasicMenu
{
    public PopupMenu currentLayout;
    public PopupMenu defaultLayout;
    public PopupMenu getFlareLayout;

    public List<System.Action> actionsToPerform = new List<System.Action>();
    public void PressedButton(int buttonInt)
    {
        for (int ii = 0; ii <currentLayout. buttonObjects.Count; ii += 1)
        {
            currentLayout.buttonObjects[ii].GetComponent<Button>().interactable = false;
        }

        {
            this.actionsToPerform[buttonInt]();
        }
        CloseMenu();
    }

    public void ShowGetFlareInfoPopup()
    {
        currentLayout = getFlareLayout;
        List<string> buttonLabels = new List<string>();

        buttonLabels.Add(MenuControl.Instance.GetLocalizedString("Confirm"));
        List<System.Action> actions = new List<System.Action>();
        actions.Add(() => { });
        updatePopupMenu(buttonLabels, actions, MenuControl.Instance.GetLocalizedString("GetCrystalTitle"),
            MenuControl.Instance.GetLocalizedString("GetCrystalDescription"));
    }

    public void ShowConfirmPopup(List<string> buttonLabels, List<System.Action> actionsToPerform, string titleText,
        string bodyText)
    {
        currentLayout = defaultLayout;
        
        updatePopupMenu(buttonLabels, actionsToPerform, titleText, bodyText);
    }

    public void updatePopupMenu(List<string> buttonLabels, List<System.Action> actionsToPerform, string titleText,
        string bodyText)
    {
        defaultLayout.gameObject.SetActive(false);
        getFlareLayout.gameObject.SetActive(false);
        currentLayout.gameObject.SetActive(true);
        
        for (int i = 0; i < buttonLabels.Count; i++)
        {
            currentLayout.buttonObjects[i].GetComponent<Button>().GetComponentInChildren<Text>().text = buttonLabels[i];
            currentLayout.buttonObjects[i].SetActive(true);
            currentLayout.buttonObjects[i].GetComponent<Button>().interactable = true;
        }

        for (int i = buttonLabels.Count; i < currentLayout.buttonObjects.Count; i++)
        {
            currentLayout.buttonObjects[i].SetActive(false);
        }

        this.actionsToPerform = actionsToPerform;
        currentLayout.titleLabel.text = titleText;
        currentLayout.bodyLabel.text = bodyText;
        ShowMenu();
    }
    
    public override void SelectVisibleCard(VisibleCard vc, bool withClick = true)
    {
        base.SelectVisibleCard(vc, withClick);
        if (GetComponent<Doozy.Engine.UI.UIView>().IsHiding) return;
        MenuControl.Instance.infoMenu.ShowInfo(vc);
    }

    public override void DeSelectVisibleCard(VisibleCard vc, bool withClick = true)
    {
        base.DeSelectVisibleCard(vc, withClick);
        MenuControl.Instance.infoMenu.HideMenu();
    }
}