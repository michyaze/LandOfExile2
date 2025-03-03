using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardWithDetails : MonoBehaviour
{
    public VisibleCard vc;
    public Text description;
    [HideInInspector]
    public UpgradeSelectCardView upgradeSelectCardView;
    public GameObject selectBK;
    public Text basicLabel;
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            upgradeSelectCardView.SelectCard(  this);
        });
    }

    public void Select()
    {
        transform.localScale = 1.05f * Vector3.one;
        selectBK.SetActive(true);
    }

    public void Deselect()
    {
        transform.localScale = Vector3.one;
        selectBK.SetActive(false);
    }
    public void Render(Card card,UpgradeSelectCardView upgradeSelectCardView,string str )
    {
        this.upgradeSelectCardView = upgradeSelectCardView;
        vc.RenderCard((card));
        var descriptionText = CardDescriptionPanel.DescriptionText(card, null, null, false);
        descriptionText = descriptionText.Replace("<color=upgrade>", upgradeSelectCardView.colorString);
        description.text = descriptionText;

        basicLabel.text = str;
        
        Deselect();
    }
}
