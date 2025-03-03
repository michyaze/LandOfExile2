using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDialogue : Ability
{
    public string dialogueId;
    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        MenuControl.Instance.battleMenu.GetComponent<InBattleDialogueController>().ShowBattleDialogue(dialogueId);
    }
}

