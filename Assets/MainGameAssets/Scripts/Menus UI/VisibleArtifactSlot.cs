using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VisibleArtifactSlot : VisibleCard
{

    public Image bgImage;
    public Image cardImage;
    public Text coolDownText;
    public Sprite passiveBorder;
    public Sprite activeBorder;

    public void RenderArtifact(Card card)
    {
        if (card is Artifact artifact)
        {
            coolDownText.transform.parent.gameObject.SetActive(artifact.currentCoolDown > 0 || card.GetZone() == MenuControl.Instance.battleMenu.removedFromGame);
            coolDownText.text = artifact.currentCoolDown.ToString();
        }
        else
        {
            coolDownText.transform.parent.gameObject.SetActive(false);
        }
        this.card = card;

        cardImage.sprite = MenuControl.Instance.csvLoader.playerCardSprite(card); //card.GetSprite();
        //costText.transform.parent.gameObject.SetActive(card.GetCost() > 0 && card.currentCoolDown == 0);
        //cardImage.sprite = MenuControl.Instance.csvLoader.playerCardSprite(card.GetChineseName()); //card.GetSprite();
        costText.transform.parent.gameObject.SetActive(card.GetCost() > 0 && ((!(card is Artifact artifact2)) || artifact2.currentCoolDown == 0));
        costText.text = card.GetCost().ToString();
    }

    public void RenderOutliner(Card card)
    {
        if (card is Castable castable && castable.activatedAbility != null)
        {
            if (!(card is Artifact artifact) || artifact.currentCoolDown == 0)
            {
                bgImage.sprite = activeBorder;
                return;
            }
        }

        bgImage.sprite = passiveBorder;
    }

}
