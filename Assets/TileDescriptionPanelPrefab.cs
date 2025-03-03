using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileDescriptionPanelPrefab : MonoBehaviour
{
    public Transform sidePanelHolder;
    public GameObject descriptionSidePanelPrefab;
    // Start is called before the first frame update
    public void RenderPanel(Tile tile)
    {
        foreach (var effect in tile.currentEffects)
        {
            CreateEffect(effect);
            
        }

        if (tile.GetTrap())
        {
            CreateTrap(tile.GetTrap());
        }
        
        
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }

    void CreateTrap(WeatherTrap trap)
    {
        var sprite = MenuControl.Instance.csvLoader.buffSprite(trap.GetChineseName());
        var textToShown = trap.GetChineseName() + "\n" + trap.GetDescription();
        CreateSidePanel(sprite, textToShown);
    }

    void CreateEffect(Effect effect)
    {
        var sprite = MenuControl.Instance.csvLoader.buffSprite(effect.GetChineseName());
        var textToShown = effect.GetChineseName() + "\n" + effect.GetDescription();
        CreateSidePanel(sprite, textToShown);
    }
    
    void CreateSidePanel(Sprite sprite, string textToShow)
    {
        GameObject sidePanel = Instantiate(descriptionSidePanelPrefab, sidePanelHolder);

        var sidePanelScript =  sidePanel.GetComponent<DescriptionSidePanel>();
        
        sidePanel.GetComponentInChildren<Text>().text = textToShow;
        
        sidePanelScript.cardWithBorder.SetActive(true);
        sidePanelScript.contentImage.sprite = sprite;
           
        sidePanelScript.effectImage.gameObject.SetActive(false);
    }
}
