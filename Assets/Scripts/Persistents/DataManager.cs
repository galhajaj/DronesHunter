using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public event Action Event_AmmoUpdated;
    public event Action Event_GoldUpdated;

    public LevelData CurrentLevelData { get; set; }

    public int TopLevelUnlocked
    {
        get { return PlayerPrefs.GetInt("TopLevelUnlocked"); }
        set { PlayerPrefs.SetInt("TopLevelUnlocked", value); }
    }

    // ammo
    public int Ammo
    {
        get { return PlayerPrefs.GetInt("Ammo"); }
        set { PlayerPrefs.SetInt("Ammo", value); Event_AmmoUpdated(); }
    }

    // gold
    public int Gold
    {
        get { return PlayerPrefs.GetInt("Gold"); }
        set { PlayerPrefs.SetInt("Gold", value); Event_GoldUpdated(); }
    }

    // best scores
    public float GetLevelBestScore(int levelNumber)
    {
        return PlayerPrefs.GetFloat(levelNumber.ToString() + "_BestScore");
    }
    public void SetLevelBestScore(int levelNumber, float score)
    {
        PlayerPrefs.SetFloat(levelNumber.ToString() + "_BestScore", score);
    }
    public float CurrentLevelBestScore
    {
        get { return PlayerPrefs.GetFloat(CurrentLevelData.LevelNumber.ToString() + "_BestScore"); }
        set { PlayerPrefs.SetFloat(CurrentLevelData.LevelNumber.ToString() + "_BestScore", value); }
    }
}
