using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneMirror : Ability
{
//选择一个随从，并在相邻的空格中生成具有急速属性的复制品

    public Effect copyEffectTemplate;

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        List<Tile> tiles = new List<Tile>();

        foreach (Tile tile1 in targetTile.GetAdjacentTilesLinear())
        {
            if (!tiles.Contains(tile1) && tile1.isMoveable())
            {
                tiles.Add(tile1);
            }
        }

        if (tiles.Count > 0)
        {
            Tile nextTile = tiles[Random.Range(0, tiles.Count)];

            Unit unitToCopy = targetTile.GetUnit();

            Minion newMinion = (Minion)GetCard().player.CreateCardInGameFromTemplate(unitToCopy.cardTemplate);

            newMinion.TargetTile(nextTile, false);

            newMinion.RemoveEffect(null, null, newMinion.GetEffectsWithTemplate(MenuControl.Instance.battleMenu.summoningSickEffectTemplate)[0]);

            newMinion.ApplyEffect(sourceCard, this, copyEffectTemplate, 0);


        }
    }
}
