using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntentSystem5rdHandOnceOnly : IntentSystem
{
    public bool thirdHasBeenDrawn; // Assumes only 3 hands 1 2 3 4 5 1 2 3 4 1 2 3 4 etc

    public override void TurnEnded(Player player)
    {
        if (GetCard().player == player)
        {
            handIndex += 1;

            if (!thirdHasBeenDrawn && handIndex == 5)
            {
                handIndex = 0;
                thirdHasBeenDrawn = true;
            }

            if (thirdHasBeenDrawn && handIndex == 4)
                handIndex = 0;
        }
    }



    public override IntentSystemHand GetNextHand()
    {
        int nextIndex = handIndex + 1; 

        if (thirdHasBeenDrawn && nextIndex == 4)
            nextIndex = 0;

        if (!thirdHasBeenDrawn && nextIndex == 5)
            nextIndex = 0;

        return hands[nextIndex];
    }
}
