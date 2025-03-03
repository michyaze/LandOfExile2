using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeather : Ability
{
    public WeatherType weather;
    

    public override void PerformAbility(Card sourceCard, Tile targetTile, int amount = 0)
    {

        MenuControl.Instance.adventureMenu.weatherController.SetWeather((weather));
    }

}
