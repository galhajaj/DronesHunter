using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButtonScript : MonoBehaviour
{
    public string Title = "";
    public string Prerequisite = "";
    public Sprite LockLevelSprite;

    private Text _textScore;
    private Text _textTitle;
    private float _score;

    void Awake()
    {
        
    }

    void Start ()
    {
        _textScore = transform.Find("TextScore").GetComponent<Text>();
        _textTitle = transform.Find("TextTitle").GetComponent<Text>();

        Title = _textTitle.text;
        _score = PlayerPrefs.GetFloat(Title + "BestScore");
        
        // if prerequisite not met
        if (Prerequisite != "")
        {
            float scoreOfPrerequisite = PlayerPrefs.GetFloat(Prerequisite + "BestScore");
            if (scoreOfPrerequisite < 5.0F)
            {
                GetComponent<Button>().enabled = false;
                GetComponent<Image>().sprite = LockLevelSprite;
                _textTitle.text = "???";
                _textScore.text = "";
                return;
            }
        }

        TotalScore.Instance.AddToScore(_score);
        _textScore.text = (_score * 10.0F).ToString("F1") + "%";

        // set score color
        Color color;
        if (_score < 5.0F)
            color = Consts.Instance.RED_TEXT;
        else if (_score <= 8.5F)
            color = Consts.Instance.BLACK_TEXT;
        else if (_score < 10.0F)
            color = Consts.Instance.GOLD_TEXT;
        else
            color = Consts.Instance.MAGENTA_TEXT;
        _textScore.color = color;
    }

    public void StartLevel()
    {
        PlayerPrefs.SetString("LevelType", Title);
        SceneManager.LoadScene("huntScene");
    }
}
