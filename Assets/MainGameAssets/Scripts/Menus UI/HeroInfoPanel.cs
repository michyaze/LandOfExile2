using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.UI;
using UnityEngine;
using UnityEngine.UI;

public class HeroInfoPanel : MonoBehaviour
{
    // left bar, hero info

    public Image heroImage;
    public Text heroLevelText;
    public Text heroHPText;
    public Image heroHPBar;
    public Image heroXPProgressBar;
    public Text heroFlareStoneOwnedText;

    public Text artifactCountText;
    public Text treasureCountText;
    public Text cardCountText;
    public int parentTemporaryDiff = 0;

    public GameObject levelUpIndicator;
    public UIButton flareButton;

    public Text extraValueText;

    private void Start()
    {
        artifactCountText.GetComponentInParent<UIButton>().OnClick.OnTrigger.Action = delegate(GameObject o)
        {
            // MenuControl.Instance.adventureMenu
            //     .ClickOnHealingPotions();
            
            MenuControl.Instance.itemsMenu.ShowMyArtifacts();
            /*MenuControl.Instance.weaponsMenu.ShowMyArtifacts();*/
        };
        

        treasureCountText.GetComponentInParent<UIButton>().OnClick.OnTrigger.Action = delegate(GameObject o) { MenuControl.Instance.deckMenu.ShowMyTreasures(); };
        cardCountText.GetComponentInParent<UIButton>().OnClick.OnTrigger.Action = delegate(GameObject o) { MenuControl.Instance.deckMenu.ShowMyDeck(); };
        if (heroImage.GetComponentInParent<UIButton>() == null)
        {
            Debug.Log("");
        }

        heroImage.GetComponentInParent<UIButton>().OnClick.OnTrigger.Action = delegate(GameObject o)
        {
            MenuControl.Instance.levelUpMenu.ShowCharacterSheet();
        };
        levelUpIndicator.SetActive(false);
        updateHeroInfo();
    }

    public void showAddStone(int value)
    {
        if (value == 0)
        {
            return;
        }

        levelUpIndicator.GetComponent<Text>().text = value > 0 ? "+" + value : value.ToString();
        if (value > 0)
        {
            levelUpIndicator.GetComponent<Text>().color = Color.green;
        }
        else
        {
            levelUpIndicator.GetComponent<Text>().color = Color.red;
        }

        //"+" + MenuControl.Instance.heroMenu.heroClass.GetHPGainPerLevel().ToString() + " HP!";
        LeanTween.delayedCall(0.3f, () =>
        {
            var newOb = Instantiate(levelUpIndicator, levelUpIndicator.transform.parent);
            newOb.SetActive(true);
            Destroy(newOb,2f);
        });
    }

    public void updateHeroInfo()
    {
        var heroMenu = MenuControl.Instance.heroMenu;
        var hero = heroMenu.hero;

        heroImage.sprite = heroMenu.heroClass.sprite;
        heroLevelText.text = heroMenu.currentLevel.ToString();
        heroHPText.text = hero.currentHP + " / " + hero.initialHP;
        heroHPBar.fillAmount = (float)hero.currentHP / hero.initialHP;
        heroXPProgressBar.fillAmount = (float)heroMenu.currentXPForLevel() / heroMenu.xPForNextLevel();
        heroFlareStoneOwnedText.text = (heroMenu.flareStones + parentTemporaryDiff).ToString();
        //artifactCountText.text = heroMenu.artifactsOwned.Count.ToString();
        //healingPotionsText.text = "x" + MenuControl.Instance.heroMenu.GetHealingPotionsInDeck().Count.ToString();
        artifactCountText.text = "X"+MenuControl.Instance.heroMenu.GetItemsInDeck().Count.ToString();
        treasureCountText.text = "X"+heroMenu.GetTreasureCardsOwned().Count.ToString();
        cardCountText.text = "X"+heroMenu.cardsInDeck().Count.ToString();
        
        
        
        var canEquippedArtifactCount = Math.Min(MenuControl.Instance.heroMenu.GetItemsInDeck().Count, MenuControl.Instance.heroMenu.artifactSlots )-
                                       MenuControl.Instance.heroMenu.artifactsEquipped.Count;
        
        //var extraValue = artifactCountText.transform.parent.transform.Find("ExtraValue");
        if (canEquippedArtifactCount > 0)
        {
            extraValueText.transform.parent.gameObject .SetActive(true);
            extraValueText
                .text = canEquippedArtifactCount.ToString();
        }
        else
        {
            extraValueText.transform.parent.gameObject .SetActive(false);
        }
    }

    private void Awake()
    {
        flareButton.OnClick.OnTrigger.Action = delegate(GameObject o) { MenuControl.Instance.confirmPopupView.ShowGetFlareInfoPopup(); };
    }
}