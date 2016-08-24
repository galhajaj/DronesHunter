using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class HandleTimeSlider : MonoBehaviour 
{
    public Slider TimeSlider;
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
            SceneManager.LoadScene("mainScene");
	}
    // ================================================================================== //
}
