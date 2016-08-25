using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class HandleTimeSlider : MonoBehaviour 
{
    public Slider TimeSlider;
    public Text ScoreText;
    public Button FireButton;
    // ================================================================================== //
	void Start () 
    {
        TimeSlider.value = 10.0F;// + PlayerPrefs.GetFloat("TimeBonus");
	}
    // ================================================================================== //
	void Update () 
    {
        TimeSlider.value -= Time.deltaTime;

        if (TimeSlider.value <= 0.0F)
        {
            // cancel fire button
            FireButton.enabled = false;
            FireButton.GetComponent<Image>().color = Color.black;

            string levelType = PlayerPrefs.GetString("LevelType");
            float score = (float)(Convert.ToDouble(ScoreText.text.Split(':')[1].Trim('%').Trim()) / 10.0F);
            if (PlayerPrefs.GetFloat(levelType + "BestScore") < score)
            {
                PlayerPrefs.SetFloat(levelType + "BestScore", score);
            }

            //SceneManager.LoadScene("mainScene");
        }
	}
    // ================================================================================== //
}
