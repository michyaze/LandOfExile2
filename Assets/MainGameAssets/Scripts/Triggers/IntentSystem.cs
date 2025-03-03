using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntentSystem : Trigger
{
    public List<IntentSystemHand> hands = new List<IntentSystemHand>();

    public int handIndex;

    public override void TurnEnded(Player player)
    {
        if (GetCard().player == player)
        {
            handIndex += 1;
            if (handIndex == hands.Count)
                handIndex = 0;
        }
    }

    public virtual IntentSystemHand GetCurrentHand()
    {
        if (handIndex >= hands.Count)
        {
            Debug.LogError("GetCurrentHand out of range "+handIndex+" "+hands.Count);
            return hands.LastItem();
        }
        return hands[handIndex];
    }

    public virtual IntentSystemHand GetNextHand()
    {
        int nextIndex = handIndex + 1;
        if (nextIndex == hands.Count)
            nextIndex = 0;

        return hands[nextIndex];
    }
}
