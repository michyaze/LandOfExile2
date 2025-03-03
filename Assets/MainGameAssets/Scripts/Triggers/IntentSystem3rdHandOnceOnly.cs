using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntentSystem3rdHandOnceOnly : IntentSystem
{
    public bool thirdHasBeenDrawn; // Assumes only 3 hands 1 2 3 1 2 1 2 etc

    public override void TurnEnded(Player player)
    {
        if (GetCard().player == player)
        {
            handIndex += 1;

            if (!thirdHasBeenDrawn && handIndex == 3)
            {
                handIndex = 0;
                thirdHasBeenDrawn = true;
            }

            if (thirdHasBeenDrawn && handIndex == 2)
                handIndex = 0;
        }
    }



    public override IntentSystemHand GetNextHand()
    {
        int nextIndex = handIndex + 1;

        if (thirdHasBeenDrawn && nextIndex == 2)
            nextIndex = 0;

        if (!thirdHasBeenDrawn && nextIndex == 3)
            nextIndex = 0;

        return hands[nextIndex];
    }
}
