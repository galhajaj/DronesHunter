using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DebugButtons : MonoBehaviour
{
    public bool IsDebug = false;
    public Button ResetButton;
    public Button All50Button;
    public Button All85Button;
    public Button All100Button;


    void Start ()
    {
		if (!IsDebug)
        {
            Destroy(ResetButton.gameObject);
            Destroy(All50Button.gameObject);
            Destroy(All85Button.gameObject);
            Destroy(All100Button.gameObject);
        }
	}
	
	void Update ()
    {
		
	}

    public void ResetLevels()
    {
        foreach (GameObject levelButton in GameObject.FindGameObjectsWithTag("LevelButtonTag"))
        {
            int levelNumber = levelButton.GetComponent<LevelButton>().LevelData.LevelNumber;
            DataManager.Instance.SetLevelBestScore(levelNumber, 0.0F);
            DataManager.Instance.TopLevelUnlocked = 0;
            SceneManager.LoadScene("mainScene");
        }
    }

    public void All50()
    {
        foreach (GameObject levelButton in GameObject.FindGameObjectsWithTag("LevelButtonTag"))
        {
            int levelNumber = levelButton.GetComponent<LevelButton>().LevelData.LevelNumber;
            DataManager.Instance.SetLevelBestScore(levelNumber, 5.0F);
            SceneManager.LoadScene("mainScene");
        }
    }

    public void All85()
    {
        foreach (GameObject levelButton in GameObject.FindGameObjectsWithTag("LevelButtonTag"))
        {
            int levelNumber = levelButton.GetComponent<LevelButton>().LevelData.LevelNumber;
            DataManager.Instance.SetLevelBestScore(levelNumber, 8.51F);
            SceneManager.LoadScene("mainScene");
        }
    }

    public void All100()
    {
        foreach (GameObject levelButton in GameObject.FindGameObjectsWithTag("LevelButtonTag"))
        {
            int levelNumber = levelButton.GetComponent<LevelButton>().LevelData.LevelNumber;
            DataManager.Instance.SetLevelBestScore(levelNumber, 10.0F);
            SceneManager.LoadScene("mainScene");
        }
    }

    public void AddAmmo()
    {
        DataManager.Instance.Ammo += 15;
    }

    public void AddGold()
    {
        DataManager.Instance.Gold += 100;
    }
}
