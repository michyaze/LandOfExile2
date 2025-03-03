using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public string UniqueID;
    public Sprite sprite;
    public Sprite alternateSprite;

    public virtual string GetName()
    {
        return MenuControl.Instance.GetLocalizedString(UniqueID + "CardName");
    }

    public virtual string GetChineseName()
    {
        var keyName = UniqueID + "CardName";
        if (MenuControl.Instance.csvLoader.nameToChineseName.ContainsKey(keyName))
        {
            var res = MenuControl.Instance.csvLoader.nameToChineseName[keyName];
            if (res != null && res.Length != 0)
            {
                return res;
            }
        }
        return MenuControl.Instance.GetChineseLocalizedString(UniqueID + "CardName");
    }
    

    public virtual string GetDescription()
    {
        return MenuControl.Instance.GetLocalizedString(UniqueID + "CardDescription");
    }

    public virtual Sprite GetSprite()
    {
        if (MenuControl.Instance.useAlternateSprites && alternateSprite != null)
        {
            return alternateSprite;
        }

        return sprite;
    }

    public virtual string GetIDForOrderedList()
    {
        int count = 0;
        char[] numbers = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        foreach (char character in UniqueID)
        {
            foreach (char number in numbers){
                if (number == character)
                {
                    count += 1;
                }
            }
        }

        if (count >= 3)
        {
            return "0" + UniqueID;
        }

        return UniqueID;
    }
}
