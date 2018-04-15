using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public LevelData LevelData;

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

        _score = DataManager.Instance.GetLevelBestScore(LevelData.LevelNumber);
        
        // if prerequisite not met
        if (LevelData.LevelNumber > DataManager.Instance.TopLevelUnlocked + 1)
        {
            GetComponent<Button>().enabled = false;
            GetComponent<Image>().sprite = LockLevelSprite;
            this.transform.Find("ImageIcon").GetComponent<Image>().enabled = false;
            _textTitle.text = "???";
            _textScore.text = "";
            return;
        }

        TotalScore.Instance.AddToScore(_score);
        _textScore.text = (_score * 10.0F).ToString("F1") + "%";

        // set score color
        Color color;
        if (_score < Consts.Instance.SCORE_TO_PASS_LEVEL)
            color = Consts.Instance.RED_TEXT;
        else if (_score <= Consts.Instance.GREAT_SCORE)
            color = Consts.Instance.BLACK_TEXT;
        else if (_score < Consts.Instance.PERFECT_SCORE)
            color = Consts.Instance.GOLD_TEXT;
        else
            color = Consts.Instance.MAGENTA_TEXT;
        _textScore.color = color;
    }

    public void StartLevel()
    {
        DataManager.Instance.CurrentLevelData = LevelData;
        SceneManager.LoadScene("Hunt");
    }
}
