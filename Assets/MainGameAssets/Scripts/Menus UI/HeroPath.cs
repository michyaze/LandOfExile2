using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroPath : CollectibleItem {

    public List<Card> pathCards =>MenuControl.Instance.heroMenu.PathCards();
    List<Card> testAllCards = new List<Card>();
    public List<Card> startingCards = new List<Card>();

    public List<Sprite> cardFrontSprites = new List<Sprite>();
    public Sprite boardCardHeroSprite;
    public Sprite boardCardMinionSprite;
    public Sprite icon;
    public Color cardHistoryBorderColor;

    public string heroFolderName;

    void purifyTalents(List<Card> toPurify)
    {
        var newAllCards = new List<Card>();
        var csvLoader = MenuControl.Instance.csvLoader;
        foreach (var card in toPurify)
        {
            var chineseName = card.GetChineseName();
            //chineseName = MenuControl.Instance.csvLoader.downgradeChineseName(chineseName);
            if(csvLoader.isValidInCurrentVersion(card.UniqueID)||  
               csvLoader.chineseNameToTalentMap.ContainsKey(chineseName))
            {newAllCards.Add(card);}
        }
        toPurify.Clear();
        foreach (var card in newAllCards)
        {
            toPurify.Add(card);
        }
    }
    public void Init()
    {
        //purifyTalents(pathCards);
        if (MenuControl.Instance.heroMenu.useResource)
        {
            // addCardResource();
            // var test = new List<Card>();
            // test.AddRange(pathCards);
            // test.AddRange(testAllCards);
            // var heroMenu = MenuControl.Instance.heroMenu;
            // test.RemoveAll(card => card == null);
            // HashSet<string> cardset = new HashSet<string>();
            // for (int i = test.Count - 1; i >= 0; i--)
            // {
            //     if (cardset.Contains(test[i].UniqueID))
            //     {
            //         test.RemoveAt(i);
            //         continue;
            //     }
            //     if ( !heroMenu.isCardAvailable(test[i]))
            //     {
            //         test.RemoveAt(i);
            //         continue;
            //     }
            //     cardset.Add((test[i].UniqueID));
            // }
            // pathCards = test;;
        }

    }
    void addCardResource()
    {
        var heroMenu = MenuControl.Instance.heroMenu;
        var gameObjects = Resources.LoadAll<GameObject>("Cards/"+heroFolderName);
        //convert from array of gameobjects to array of card
        testAllCards.AddRange(gameObjects.Select(go => go.GetComponent<Card>()));
        for (int i = testAllCards.Count - 1; i >= 0; i--)
        {
            if ( !heroMenu.isCardAvailable(testAllCards[i]))
            {
                testAllCards.RemoveAt(i);
            }
        }
    }
}
