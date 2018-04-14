using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Fire : MonoBehaviour 
{
    public Text ScoreText;
    public Text BonusScoreText;
    public Slider TimeSlider;
    private float _score = 0.0F;
    // ================================================================================== //
    void Start () 
    {
        BonusScoreText.text = "";
    }
// ================================================================================== //
void Update () 
    {
        ScoreText.text = "Score: " + (_score * 10.0F).ToString("F1")+"%";

        // change color of text
        string levelType = PlayerPrefs.GetString("LevelType");
        if (PlayerPrefs.GetFloat(levelType + "BestScore") <= _score)
        {
            ScoreText.color = Color.yellow;
        }
        else
        {
            ScoreText.color = Color.white;
        }
	}
    // ================================================================================== //
    public void ExecuteFire()
    {
        // ammo handling
        if (DataManager.Instance.Ammo <= 0)
        {
            SoundManager.Instance.Play(SoundManager.Instance.EmptyGunSound);
            return;
        }
        DataManager.Instance.Ammo--;

        SoundManager.Instance.Play(SoundManager.Instance.FireSound);

        Vector3 hitPos = Vector3.zero;

        bool isHit = false;

        LayerMask layerMask = (1 << LayerMask.NameToLayer("DronesLayer"));

        RaycastHit2D[] hits;
        hits = Physics2D.RaycastAll(transform.position, -transform.right, 1000.0F, layerMask);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit2D hit = hits[i];
            Drone drone = hit.collider.GetComponent<Drone>();

            isHit = true;

            // handle score
            float scoreToAdd = 1.0F;
            if (TimeSlider.value < 5.0F && DataManager.Instance.CurrentLevelData.IsContainHalfTimePenalty)
                scoreToAdd -= (5.0F - TimeSlider.value) * 0.15F;
            addToScore(scoreToAdd);

            // got hit
            drone.GotHit(hit.point);

            // if not piercable, stop checking hits
            if (!drone.IsPiercable)
            {
                hitPos = hit.point;
                break;
            }
        }

        // hit position to draw the line to it
        if (hitPos == Vector3.zero)
            DrawLine(transform.position, -transform.right * 100.0F, Color.yellow, 0.02F, 0.05F);
        else
            DrawLine(transform.position, hitPos, Color.yellow, 0.02F, 0.05F);

        // score update if not hit anything
        if (!isHit)
            addToScore(-0.25F);
    }
    // ================================================================================== //
    void DrawLine(Vector3 start, Vector3 end, Color color, float width, float duration = 0.2f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.startColor = color;
        lr.endColor = color;
        lr.startWidth = width;
        lr.endWidth = width;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }
    // ================================================================================== //
    private void addToScore(float bonus)
    {
        _score += bonus;
        StartCoroutine("addToScoreCoroutine", bonus);
    }
    // ================================================================================== //
    private IEnumerator addToScoreCoroutine(float bonus)
    {
        string plus = "";
        if (bonus > 0.0F)
        {
            plus = "+";
            BonusScoreText.color = Color.green;
        }
        else
        {
            BonusScoreText.color = Color.red;
        }
        BonusScoreText.text = plus + (10 * bonus).ToString("F1") + "%";
        yield return new WaitForSeconds(1.0F);
        BonusScoreText.text = "";
    }
    // ================================================================================== //
}
