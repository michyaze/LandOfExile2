using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum WeatherType{Sun,Rain,Thunder}
public class WeatherController : MonoBehaviour
{

    public List<Sprite> weatherIconInAdventure;
    public List<GameObject> trapObject;
    [HideInInspector]
    public WeatherType currentWeather;

    public List<int> trapGenerationCountColumn = new List<int>(){4,6,8};
    public List<int> trapGenerationCountMin= new List<int>(){0,1,2};
    public List<int> trapGenerationCountMax= new List<int>(){2,3,4};

    public Sprite CurrentWeatherIconInAdventure()
    {
        return weatherIconInAdventure[(int)currentWeather];
    }
    public GameObject CurrentWeatherTrap()
    {
        return trapObject[(int)currentWeather];
    }
    public void RandomPickWeather()
    {
        WeatherType[] enumValues = (WeatherType[])Enum.GetValues(typeof(WeatherType));
        currentWeather = enumValues[Random.Range(0,enumValues.Length)];

       // currentWeather = WeatherType.Thunder;
    }

    public void SetWeather(WeatherType weather)
    {
        currentWeather = weather;
        MenuControl.Instance.adventureMenu.UpdateWeather();
    }

    public int GetBattleTrapCount(int column)
    {
        int res = 0;
        int i = 0;
        for(i = 0;i<trapGenerationCountColumn.Count;i++)
        {
            if (column <= trapGenerationCountColumn[i])
            {
                break;
            }
        }

        i = Math.Min(i, trapGenerationCountColumn.Count - 1);
        res = Random.Range(trapGenerationCountMin[i],trapGenerationCountMax[i]+1);

        return res;
    }
}
