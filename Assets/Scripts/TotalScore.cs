using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalScore : MonoBehaviour
{
    public static TotalScore Instance;

    public Text TextTotalScore;
    private float _totalScore = 0.0F;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    public void AddToScore(float points)
    {
        _totalScore += points;
        TextTotalScore.text = (_totalScore).ToString("F2") + "% Completed ";

        // set score color
        Color color;
        if (_totalScore < 50.0F)
            color = Consts.Instance.RED_TEXT;
        else if (_totalScore <= 85F)
            color = Consts.Instance.BLACK_TEXT;
        else if (_totalScore < 100F)
            color = Consts.Instance.GOLD_TEXT;
        else
            color = Consts.Instance.MAGENTA_TEXT;
        TextTotalScore.color = color;
    }
}
