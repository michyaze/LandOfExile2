using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EncounterMapSetting 
{
    public int boardColumns = 4;
    public bool player1Starts;
    public bool randomStartingPositions = true;
    public int player1StartingPos = 15;
    public int playerAIStartingPos = 0;
    
    public List<Card> cardsInPlay = new List<Card>();
    public List<int> positionsInPlay = new List<int>();
    
    
    public int minObstacleCount = 0;
    public int maxObstacleCount = 0;
}
