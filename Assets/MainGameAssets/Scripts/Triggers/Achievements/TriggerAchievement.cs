using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAchievement : Trigger
{
    public void MarkAchievementCompleted()
    {
        if (!GetComponent<Achievement>().IsCompleted())
            GetComponent<Achievement>().Complete();
    }

}
