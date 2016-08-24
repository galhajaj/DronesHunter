using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManageBestScore : MonoBehaviour 
{
    public Text BestScoreText;
	// Use this for initialization
	void Start () 
    {
        BestScoreText.text = "Best Score: " + PlayerPrefs.GetFloat("BestScore").ToString("F2");
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
