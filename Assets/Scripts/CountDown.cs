using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountDown : MonoBehaviour 
{
    public Text CountDownText;
    public Button FireButton;

    //private float _time = 3.0F;
    private bool _isCountDownOver = false;
    private Color _initColorOfFireButton;
	// Use this for initialization
	void Start () 
    {
        //Time.timeScale = 0.0F;
        // cancel fire button
        FireButton.enabled = false;
        _initColorOfFireButton = FireButton.GetComponent<Image>().color;
        FireButton.GetComponent<Image>().color = Color.black;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (_isCountDownOver)
            return;

        //_time -= Time.timeSinceLevelLoad.deltaTime;

        if (Time.timeSinceLevelLoad < 1.0F)
        {
            CountDownText.text = "3";
        }
        else if (Time.timeSinceLevelLoad < 2.0F)
        {
            CountDownText.text = "2";
        }
        else if (Time.timeSinceLevelLoad < 3.0F)
        {
            CountDownText.text = "1";
        }
        else
        {
            CountDownText.gameObject.SetActive(false);
            _isCountDownOver = true;
            //Time.timeScale = 1.0F;

            // cancel fire button
            FireButton.enabled = true;
            FireButton.GetComponent<Image>().color = _initColorOfFireButton;
        }
	}
}
