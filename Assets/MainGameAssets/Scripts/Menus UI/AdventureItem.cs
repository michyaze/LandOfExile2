using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureItem : CollectibleItem {

    public bool alwaysOpen;

    public virtual void PerformItem(int index)
    {

    }

    public virtual void CleanUpItem(int index)
    {

    }

    public virtual void SkipItem(int index)
    {

    }

    public virtual string GetChoiceLabel()
    {
        return "<b>" + GetName() + "</b>";
    }

}
