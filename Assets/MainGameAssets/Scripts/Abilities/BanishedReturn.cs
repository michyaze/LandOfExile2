using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanishedReturn : Trigger
{
    //在回合开始时，回归被虚无的随从并清除此效果。如果回归时，另一个随从站在被虚无随从的方格中，则摧毁两个随从；如果英雄站在被虚无随从的方格上，则对英雄造成5点伤害
    public Minion banishedMinion;
    public Tile originalTile;

    public int heroDamage = 5;

    public override void TurnStarted(Player player)
    {
        if (GetCard().player == player)
        {
            MenuControl.Instance.battleMenu.AddTriggeredAbility(GetCard(), GetEffect(), () =>
            {
                
                if (originalTile.isMoveable())
                {
                    banishedMinion.TargetTile(originalTile, false);
                    banishedMinion.RemoveEffect(null, null, banishedMinion.GetEffectsWithTemplate(MenuControl.Instance.battleMenu.summoningSickEffectTemplate)[0]);
                }
                else if (originalTile.GetUnit() is Hero)
                {
                    banishedMinion.DiscardThisCard();
                    ((Hero)originalTile.GetUnit()).SufferDamage(banishedMinion, this, heroDamage);
                }
                else
                {
                    banishedMinion.DiscardThisCard();
                    ((Minion)originalTile.GetUnit()).SufferDamage(banishedMinion, this, 0, true);
                }
                ((Unit)GetCard()).RemoveEffect(GetCard(), this, GetEffect());
            });
        }
    }
}
