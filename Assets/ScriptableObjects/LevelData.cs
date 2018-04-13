using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Data", menuName = "LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public enum DronesFormationType
    {
        RANDOM,
        TWO_AT_A_TIME
    }

    public DronesFormationType FormationType = DronesFormationType.RANDOM;
    public bool IsOnlyOneTargetAtATime = false;
    //public int DurationInSec = 10;
    public int NumberOfDrones = 10;
    public List<GameObject> Drones;
}
