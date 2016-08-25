using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class EscapeToMainScene : MonoBehaviour 
{
    public Text ScoreText;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            /*float score = (float)Convert.ToDouble(ScoreText.text.Split(':')[1].Trim());
            if (PlayerPrefs.GetFloat("BestScore") < score)
                PlayerPrefs.SetFloat("BestScore", score);*/
            
            SceneManager.LoadScene("mainScene");
        }
	
	}
}
