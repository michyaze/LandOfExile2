using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureItemStory : AdventureItemKnownEvent
{

    public EventChoiceSetSelection eventChoiceSetSelection;
    public EventChoiceGoBack eventChoiceGoBack;
    
    public void init(StoryEventInfo info, MapEventInfo mapInfo)
    {
        var definition = GetComponent<EventDefinition>();
        //definition.sprite = MenuControl.Instance.csvLoader.eventSprite(info.spriteName);
        definition.questId = info.questName;

        if (info.optionsShowLogic == "all")
        {
            for (int i = 0; i < info.options.Count; i++)
            {
                var option = info.options[i];
                if (option == "goback")
                {
                    definition.eventChoices.Add(eventChoiceGoBack);
                
                    eventChoiceGoBack.UniqueID = info.questName+"_"+info.options[i];
                }
                else
                {
                    var setSelectionChoice = Instantiate(eventChoiceSetSelection);
                    definition.eventChoices.Add(setSelectionChoice);
                    setSelectionChoice.UniqueID = info.questName+"_"+info.options[i];
                    setSelectionChoice.eventId = mapInfo.eventId;
                    setSelectionChoice.selectId = i+1;
                }
            }
        }
        else
        {
            // 如果orderedStorysEventIndexThisRound已经有了这个事件的，直接用
            //如果没有，先生成orderedStorysEventIndexThisRound的对饮值
            int thisItemId = 0;
            bool hasSetValue = false;
            foreach (var str in MenuControl.Instance.eventMenu.orderedStorysEventIndexThisRound)
            {
                if (str.Contains(definition.questId+"_"))
                {
                    var numStr = str.Substring(definition.questId.Length + 1);
                    thisItemId = int.Parse(numStr);
                    hasSetValue = true;
                }
            }

            if (!hasSetValue)
            {
                int num = 0;
                if (MenuControl.Instance.eventMenu.orderedStorysEventIndex != null)
                {
                    for (int i = 0; i < MenuControl.Instance.eventMenu.orderedStorysEventIndex.Count; i++)
                    {
                        var str = MenuControl.Instance.eventMenu.orderedStorysEventIndex[i];
                        if (str.Contains(definition.questId+"_"))
                        {
                            var numStr = str.Substring(definition.questId.Length + 1);
                            num = int.Parse(numStr);
                            MenuControl.Instance.eventMenu.orderedStorysEventIndex.RemoveAt(i);
                            break;
                        }
                    }
                }

                MenuControl.Instance.eventMenu.orderedStorysEventIndexThisRound.Add((definition.questId) + "_" + num);
                thisItemId = num;
                num++;
                if (num > info.options.Count - 1)
                {
                    num = 0;
                }
                
                MenuControl.Instance.eventMenu.orderedStorysEventIndex.Add((definition.questId) + "_" + num);
            }



            if (thisItemId >= info.options.Count)
            {
                thisItemId = 0;
            }

            // var option = info.options[thisRoundNum];
            var setSelectionChoice = Instantiate(eventChoiceSetSelection);
            definition.eventChoices.Add(setSelectionChoice);
            setSelectionChoice.UniqueID = info.questName + "_" + info.options[thisItemId];
            setSelectionChoice.eventId = mapInfo.eventId;
            setSelectionChoice.selectId = thisItemId + 1;

        }

    }
}
