using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tinker : Ability
{

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {
        List<Minion> minionsUpgradable = new List<Minion>();
        foreach (Minion minion in sourceCard.player.GetMinionsOnBoard())
        {
            if (minion.RandomUpgradeCard != null)
            {
                minionsUpgradable.Add(minion);
            }
        }

        if (minionsUpgradable.Count > 0)
        {
            Minion randomMinion = minionsUpgradable[Random.Range(0, minionsUpgradable.Count)];
            Tile tile = randomMinion.GetTile();


            Card newCard = sourceCard.player.CreateCardInGameFromTemplate(randomMinion.RandomUpgradeCard);
            randomMinion.PutIntoZone(MenuControl.Instance.battleMenu.removedFromGame);

            newCard.TargetTile(tile,true);
            ((Unit)newCard).RemoveEffect(sourceCard, this, ((Unit)newCard).GetEffectsWithTemplate(MenuControl.Instance.battleMenu.summoningSickEffectTemplate)[0]);
        }
    }
}

