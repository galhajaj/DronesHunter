using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class HandleTimeSlider : MonoBehaviour 
{
    public Slider TimeSlider;
    public Text ScoreText;
    // ================================================================================== //
	void Start () 
    {
        TimeSlider.value = 10.0F + PlayerPrefs.GetFloat("TimeBonus");
	}
    // ================================================================================== //
	void Update () 
    {
        TimeSlider.value -= Time.deltaTime;

        if (TimeSlider.value <= 0.0F)
        {
            float score = (float)Convert.ToDouble(ScoreText.text.Split(':')[1].Trim());
            if (PlayerPrefs.GetFloat("BestScore") < score)
                PlayerPrefs.SetFloat("BestScore", score);

            SceneManager.LoadScene("mainScene");
        }
	}
    // ================================================================================== //
}
