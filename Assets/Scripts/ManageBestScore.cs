using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManageBestScore : MonoBehaviour 
{
    public Text EasyScoreText;
    public Text NormalScoreText;
    public Text InvisibleScoreText;
    public Text ImpossibleScoreText;

	// Use this for initialization
	void Start () 
    {
        {
            float score = PlayerPrefs.GetFloat("EasyBestScore");
            EasyScoreText.text = (score * 10.0F).ToString("F1") + "%";
            Color color;
            if (score <= 5.0F)
                color = Color.red;
            else if (score <= 8.5F)
                color = Color.black;
            else if (score < 10.0F)
                color = Color.yellow;
            else
                color = Color.magenta;
            EasyScoreText.color = color;
        }
        
        {
            float score = PlayerPrefs.GetFloat("NormalBestScore");
            NormalScoreText.text = (score * 10.0F).ToString("F1") + "%";
            Color color;
            if (score <= 5.0F)
                color = Color.red;
            else if (score <= 8.5F)
                color = Color.black;
            else if (score < 10.0F)
                color = Color.yellow;
            else
                color = Color.magenta;
            NormalScoreText.color = color;
        }

        {
            float score = PlayerPrefs.GetFloat("InvisibleBestScore");
            InvisibleScoreText.text = (score * 10.0F).ToString("F1") + "%";
            Color color;
            if (score <= 5.0F)
                color = Color.red;
            else if (score <= 8.5F)
                color = Color.black;
            else if (score < 10.0F)
                color = Color.yellow;
            else
                color = Color.magenta;
            InvisibleScoreText.color = color;
        }

        {
            float score = PlayerPrefs.GetFloat("ImpossibleBestScore");
            ImpossibleScoreText.text = (score * 10.0F).ToString("F1") + "%";
            Color color;
            if (score <= 5.0F)
                color = Color.red;
            else if (score <= 8.5F)
                color = Color.black;
            else if (score < 10.0F)
                color = Color.yellow;
            else
                color = Color.magenta;
            ImpossibleScoreText.color = color;
        }

        /*if (PlayerPrefs.GetFloat("BestScore") == 10.0F)
            TeaseText.text = "OMG! A Perfect 10.00!!! Cheater...";*/
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
