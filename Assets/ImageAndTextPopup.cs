using System.Collections;
using System.Collections.Generic;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class ImageAndTextPopup : BasicMenu
{
    
    public Localize titleLabel;
    public Localize bodyLabel;
    public Image mainImage;
    
    public CanvasGroup blockerPanel;
    public void ShowPopup()
    {
        
        ShowMenu();
    }
    public override void ShowMenu()
    {
        base.ShowMenu();
        blockerPanel.alpha = 0f;
        LeanTween.alphaCanvas(blockerPanel, 1f, 0.3f).setDelay(0.4f);

    }
    public override void HideMenu(bool instantly = false)
    {
        base.HideMenu(instantly);
        LeanTween.alphaCanvas(blockerPanel, 0f, 0.2f);
    }

    public void ShowIntro()
    {
        var levelId = MenuControl.Instance.areaMenu.currentArea.levelId;
        titleLabel.Term = "IntroPopupTitle" + levelId;
        bodyLabel.Term = "IntroPopupText" +levelId;
        mainImage.sprite = Resources.Load<Sprite>("Art/intro/IntroPopupImage" + levelId);
ShowMenu();
    }
    
    
    
}
