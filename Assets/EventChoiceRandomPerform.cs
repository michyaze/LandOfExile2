using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiceRandomPerform : EventChoice
{
    public EventChoice[] choices;
    public List<int> prob;

    public override void PerformChoice()
    {
        var pickIndex = RandomUtil.RandomBasedOnProbability(prob);
        
        choices[pickIndex].BeforePerformChoice();
        choices[pickIndex].PerformChoice();
        
    }
}
