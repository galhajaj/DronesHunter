using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class HandleTimeSlider : MonoBehaviour 
{
    public Slider TimeSlider;
    public Image TimeSliderFill;
    public Text ScoreText;
    public Button FireButton;
    public GameObject BestScoreParticles;
    // ================================================================================== //
	void Start () 
    {
        TimeSlider.value = 10.0F;// + PlayerPrefs.GetFloat("TimeBonus");
	}
    // ================================================================================== //
	void Update () 
    {
        if (Time.timeSinceLevelLoad > 3.0F)
            TimeSlider.value -= Time.deltaTime;

        if (TimeSlider.value < 5.0F)
        {
            TimeSliderFill.color = Color.yellow;
        }

        if (TimeSlider.value <= 0.0F)
        {
            // cancel fire button
            FireButton.enabled = false;
            FireButton.GetComponent<Image>().color = Color.black;

            string levelType = PlayerPrefs.GetString("LevelType");
            float score = (float)(Convert.ToDouble(ScoreText.text.Split(':')[1].Trim('%').Trim()) / 10.0F);
            if (PlayerPrefs.GetFloat(levelType + "BestScore") < score)
            {
                BestScoreParticles.SetActive(true);
                PlayerPrefs.SetFloat(levelType + "BestScore", score);
            }

            //SceneManager.LoadScene("mainScene");

            // sky to red
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().backgroundColor = new Color(45/255, 7/255, 8/255, 1.0F);
        }
	}
    // ================================================================================== //
}
