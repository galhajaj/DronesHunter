using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public event Action Event_AmmoUpdated;

    public LevelData CurrentLevelData { get; set; }

    public int TopLevelUnlocked
    {
        get { return PlayerPrefs.GetInt("TopLevelUnlocked"); }
        set { PlayerPrefs.SetInt("TopLevelUnlocked", value); }
    }

    public int Ammo
    {
        get { return PlayerPrefs.GetInt("Ammo"); }
        set { PlayerPrefs.SetInt("Ammo", value); Event_AmmoUpdated(); }
    }
}
