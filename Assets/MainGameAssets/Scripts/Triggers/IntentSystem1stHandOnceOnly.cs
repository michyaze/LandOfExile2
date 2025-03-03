using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntentSystem1stHandOnceOnly : IntentSystem
{
  
    public override void TurnEnded(Player player)
    {
        if (GetCard().player == player)
        {
            handIndex += 1;
            if (handIndex == hands.Count)
                handIndex = 1;
        }
    }



    public override IntentSystemHand GetNextHand()
    {
        int nextIndex = handIndex + 1;
        if (nextIndex == hands.Count)
            nextIndex = 1;

        return hands[nextIndex];
    }
}
