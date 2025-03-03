using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeatherTrap : Card
{
    public Ability TriggerAbility;
    public bool Persistent;

    private void Awake()
    {
        GetComponentInChildren<Image>().gameObject.SetActive(false);
    }

    public Tile GetTile()
    {
        //if (GetZone() == MenuControl.Instance.battleMenu.board)
        {
            return MenuControl.Instance.battleMenu.boardMenu.GetTileOfUnit(this);
        }

       // return null;
    }
    
}
