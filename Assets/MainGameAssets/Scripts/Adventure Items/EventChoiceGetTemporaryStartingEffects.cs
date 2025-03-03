using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceGetTemporaryStartingEffects : EventChoice
{

    public List<Effect> effectTemplate;
    public List<int> charges;

    public override void PerformChoice()
    {
        if (effectTemplate.Count != charges.Count)
        {
            Debug.LogError($"EventChoiceGetTemporaryStartingEffects effect 和层数不匹配");
        }
        for (int i = 0; i < Math.Min( effectTemplate.Count, charges.Count); i++)
        {
            MenuControl.Instance.heroMenu.hero.AddTempEffect(effectTemplate[i],charges[i]);
            
        }
                MenuControl.Instance.dataControl.SaveData();
                CloseEvent();

    }
}
