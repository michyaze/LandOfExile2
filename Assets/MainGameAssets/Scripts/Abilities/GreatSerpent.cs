using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatSerpent : Trigger
{
    public override void GameStarted()
    {
        Hero hero = GetCard().player.GetHero();
        hero.TargetTile(MenuControl.Instance.battleMenu.boardMenu.tiles[1],false);
        hero.TargetTile(MenuControl.Instance.battleMenu.boardMenu.tiles[5], false);
    }
}
