using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManageBestScore : MonoBehaviour 
{
    public Text BestScoreText;
    public Text TeaseText;
	// Use this for initialization
	void Start () 
    {
        BestScoreText.text = "Best Score: " + PlayerPrefs.GetFloat("BestScore").ToString("F2");

        if (PlayerPrefs.GetFloat("BestScore") == 10.0F)
            TeaseText.text = "OMG! A Perfect 10.00!!! Cheater...";
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
