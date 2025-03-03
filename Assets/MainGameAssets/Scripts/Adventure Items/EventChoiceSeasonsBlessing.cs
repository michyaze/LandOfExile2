using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceSeasonsBlessing : EventChoice
{

    public EventDefinition nothingEventDefinition;
    public EventDefinition santaEventDefinition;
    public EventDefinition krampusEventDefinition;
    public override void PerformChoice()
    {

        int niceCardCount = 0;
        int naughtyCardCount = 0;
        foreach (Card card in MenuControl.Instance.heroMenu.cardsOwned)
        {
            if (card.cardTags.Contains(MenuControl.Instance.niceTag))
            {
                niceCardCount += 1;
            }
            if (card.cardTags.Contains(MenuControl.Instance.naughtyTag))
            {
                naughtyCardCount += 1;
            }
        }

        int total = niceCardCount - naughtyCardCount;
        if (!MenuControl.Instance.heroMenu.foundSanta && total > 0 && Random.Range(1, 101) <= total * 15)
        {
            MenuControl.Instance.heroMenu.foundSanta = true;
            MenuControl.Instance.eventMenu.ShowEvent(santaEventDefinition);
            return;
        }
        else if (!MenuControl.Instance.heroMenu.foundKrampus && total < 0 && Random.Range(1, 101) <= -total * 15)
        {
            MenuControl.Instance.heroMenu.foundKrampus = true;
            int hpToLose = Mathf.FloorToInt( MenuControl.Instance.heroMenu.hero.GetHP()/3f);
            MenuControl.Instance.heroMenu.hero.SufferDamage(null,null, hpToLose);
            MenuControl.Instance.adventureMenu.RenderUI();
            MenuControl.Instance.dataControl.SaveData();
            MenuControl.Instance.eventMenu.ShowEvent(krampusEventDefinition);

            return;
        }
        else
        {
            MenuControl.Instance.eventMenu.ShowEvent(nothingEventDefinition);
            return;
        }

    }

}
