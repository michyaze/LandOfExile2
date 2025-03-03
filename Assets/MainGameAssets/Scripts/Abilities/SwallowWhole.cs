using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwallowWhole : Ability
{
    public Card hydraHeadTemplate;
    public Effect swallowedEffectTemplate;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        if (sourceCard.player.GetOpponent().GetMinionsOnBoard().Count > 0)
        {

            Minion randomMinion = sourceCard.player.GetOpponent().GetMinionsOnBoard()[Random.Range(0, sourceCard.player.GetOpponent().GetMinionsOnBoard().Count)];

            List<Minion> hydraHeads = new List<Minion>();
            foreach (Card card in MenuControl.Instance.battleMenu.GetAllUnitsOnBoard())
            {
                if (card.cardTemplate.UniqueID == hydraHeadTemplate.UniqueID)
                {
                    if (((Minion)card).GetEffectsWithTemplate(swallowedEffectTemplate).Count == 0)
                    {
                        hydraHeads.Add((Minion)card);
                    }
                }
            }
            if (hydraHeads.Count > 0)
            {

                Effect swallowedEffect = hydraHeads[Random.Range(0, hydraHeads.Count)].ApplyEffect(sourceCard, this, swallowedEffectTemplate, 0);
                if (swallowedEffect == null)
                {
                    return;
                }
                swallowedEffect.GetComponent<TriggerSwallowedWhole>().cardToDraw = randomMinion;

                randomMinion.SufferDamage(sourceCard, this, 0, true);
                randomMinion.RemoveFromGame();

            }

        }

    }
}
