using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum DronesFormationType
{
    RANDOM,
    TWO_AT_A_TIME
}

[CreateAssetMenu(fileName = "Data", menuName = "LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public int LevelNumber = 0;

    public DronesFormationType FormationType = DronesFormationType.RANDOM;
    public bool IsOnlyOneTargetAtATime = false;
    //public int DurationInSec = 10;
    public int NumberOfDrones = 10;
    public List<GameObject> Drones;

    public bool IsContainHalfTimePenalty = true;
}
