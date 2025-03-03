using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDefinition : CollectibleItem
{
    public List<EventChoice> eventChoices = new List<EventChoice>();
    public string questId;

    public bool isEncounter => name == "Event Definition - Encounter" ||
                               name == "Event Definition - Boss Encounter" ||
                               name == "Event Definition - Unskippable Encounter";

    public bool hasLargeImage = true;
    public List<EventChoice> GetEventChoices()
    {
        List<EventChoice> res = new List<EventChoice>();
        foreach (var choice in eventChoices)
        {
            if (choice.IsVisible())
            {
                res.Add(choice);
            }
        }
        return res;
    }

    public override string GetName()
    {
        if (questId.Length != 0)
        {
            return MenuControl.Instance.GetLocalizedString(questId+"CardName");
        }
        return base.GetName();
    }

    public override string GetChineseName()
    {
        if (questId.Length != 0)
        {
            var keyName = questId + "CardName";
            if (MenuControl.Instance.csvLoader.nameToChineseName.ContainsKey(keyName))
            {
                return MenuControl.Instance.csvLoader.nameToChineseName[keyName];
            }
        return MenuControl.Instance.GetChineseLocalizedString(questId + "CardName");
        }
        return base.GetChineseName();
    }

    public override string GetDescription()
    {
        if (questId.Length != 0)
        {
            return MenuControl.Instance.GetLocalizedString(questId+"CardDescription");
        }
        return base.GetDescription();
    }

    public override Sprite GetSprite()
    {
        return MenuControl.Instance.csvLoader.eventSprite(
            GetChineseName());
    }
}
