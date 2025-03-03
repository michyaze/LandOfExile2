
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Area : CollectibleItem {

    //public List<AdventureItemEncounter> enemies = new List<AdventureItemEncounter>();
   // public List<AdventureItemEncounter> bosses = new List<AdventureItemEncounter>();

    public List<GameObject> battleMapBGPrefabs = new List<GameObject>();
    public List<GameObject> alternateBattleMapBGPrefabs = new List<GameObject>();
    public Sprite choiceButtonSprite;
    public List<Sprite> mapRoomSprites = new List<Sprite>();
    public List<Sprite> alternateMapRoomSprites = new List<Sprite>();

    public List<Sprite> cardFrontSprites = new List<Sprite>();
    public Sprite boardCardHeroSprite;
    public Sprite boardCardMinionSprite;
    public Color cardHistoryBorderColor;

    public Sprite bigMapSprite;

    public int levelId = 0;

    public float xScale = 0.7f;
    public float yScale = 0.71f;
    
    public Obstacle obstacle;

    public void GenerateItems(bool testModeOnly)
    {
        if (testModeOnly) GetComponent<AreaGeneration>().GenerateTestItems();
        else
            GetComponent<AreaGeneration>().GenerateItems(levelId);
    }

    // public AdventureItemEncounter GetRandomBoss()
    // {
    //     return bosses[Random.Range(0, bosses.Count)];
    // }
}
