using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactSlotMenu : BasicMenu
{

    public List<VisibleArtifactSlot> slots = new List<VisibleArtifactSlot>();

    public void SetupSlots()
    {
       
        //Create the card objects (assign a player)
        if (!MenuControl.Instance.battleMenu.tutorialMode)
        {
            for (int ii = 0; ii < MenuControl.Instance.heroMenu.artifactsEquipped.Count; ii += 1)
            {
                Card cardTemplate = MenuControl.Instance.heroMenu.artifactsEquipped[ii];

                Card card = MenuControl.Instance.battleMenu.player1.CreateCardInGameFromTemplate(cardTemplate);
                slots[ii].RenderArtifact(card);
            }
        }
    }

    public void RenderSlots()
    {
        for (int ii = 0; ii < slots.Count; ii += 1)
        {
            // slots[ii].gameObject.SetActive(ii < MenuControl.Instance.heroMenu.artifactsEquipped.Count && !MenuControl.Instance.battleMenu.tutorialMode);
            //
            // if (ii < MenuControl.Instance.heroMenu.artifactsEquipped.Count)
            // {
            //     slots[ii].RenderArtifact(MenuControl.Instance.heroMenu.artifactsEquipped[ii]);
            // }
            slots[ii].gameObject.SetActive(ii < MenuControl.Instance.battleMenu.player1.GetArtifacts().Count && !MenuControl.Instance.battleMenu.tutorialMode);
            if (ii < MenuControl.Instance.battleMenu.player1.GetArtifacts().Count)
            {
                var card = MenuControl.Instance.battleMenu.player1.GetArtifacts()[ii];
                slots[ii].RenderArtifact(card);
                slots[ii].RenderOutliner(card);
            }
            
        }
    }


    public override void SelectVisibleCard(VisibleCard vc, bool withClick = true)
    {
        MenuControl.Instance.battleMenu.SelectVisibleCard(vc, withClick);
    }
    public override void DeSelectVisibleCard(VisibleCard vc, bool withClick = true)
    {
        MenuControl.Instance.battleMenu.DeSelectVisibleCard(vc, withClick);
    }
    public override void ClickVisibleCard(VisibleCard vc)
    {
        MenuControl.Instance.battleMenu.ClickVisibleCard(vc);
    }
}
