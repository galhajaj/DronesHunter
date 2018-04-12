using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Fire : MonoBehaviour 
{
    public GameObject Smoke;

    public AudioClip FireSound;
    public AudioClip HitDroneSound;

    public Text ScoreText;
    public Text BonusScoreText;
    public Slider TimeSlider;
    private float _score = 0.0F;

    private string _levelTitle;
    //public float Score{ get { return _score; } }
    // ================================================================================== //
    void Start () 
    {
        _levelTitle = PlayerPrefs.GetString("LevelType");
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
        bool isTargetAndHitBomb = false;

        AudioSource.PlayClipAtPoint(FireSound, Camera.main.transform.position);

        if (_levelTitle == "Piercing")
        {
            piercingRaycast();
            return;
        }

        Vector3 hitPos = Vector3.zero;

        bool isHit = false;

        LayerMask layerMask = (1 << LayerMask.NameToLayer("DronesLayer"));
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.right, 1000.0F, layerMask);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.GetComponent<DroneAI>() != null)
            {
                if (_levelTitle == "Target")
                {
                    if (hit.collider.gameObject.GetComponent<DroneProperties>().IsTarget)
                    {
                        List<GameObject> bombsDrones = new List<GameObject>();
                        foreach (GameObject drn in GameObject.FindGameObjectsWithTag("DroneTag"))
                        {
                            if (!drn.GetComponent<DroneProperties>().IsTarget && drn.GetComponent<Rigidbody2D>().gravityScale != 1.0F)
                            {
                                bombsDrones.Add(drn);
                            }
                        }
                        int rand = UnityEngine.Random.Range(0, bombsDrones.Count);
                        bombsDrones[rand].GetComponent<DroneProperties>().IsTarget = true;
                    }
                    else
                    {
                        isTargetAndHitBomb = true;
                    }
                }

                // force
                hit.rigidbody.AddForceAtPosition(200.0F * -transform.right, hit.point);

                GameObject smoke = Instantiate(Smoke) as GameObject;
                smoke.transform.position = hit.collider.transform.position;
                smoke.transform.parent = hit.collider.transform;

                hit.collider.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1.0F;

                hit.collider.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                hit.collider.gameObject.layer = LayerMask.NameToLayer("DestroyedDronesLayer");

                if (hit.collider.gameObject.GetComponent<DroneAI>().isActiveAndEnabled)
                    isHit = true;

                hit.collider.gameObject.GetComponent<DroneAI>().enabled = false;

                hitPos = hit.point;

                AudioSource.PlayClipAtPoint(HitDroneSound, Camera.main.transform.position);
            }
        }

        if (hitPos == Vector3.zero)
            DrawLine(transform.position, -transform.right * 100.0F, Color.yellow, 0.02F, 0.05F);
        else
            DrawLine(transform.position, hitPos, Color.yellow, 0.02F, 0.05F);

        if (isHit)
        {
            if (!isTargetAndHitBomb)
            {
                float scoreToAdd = 1.0F;
                if (TimeSlider.value < 5.0F && _levelTitle != "Skeet")
                    scoreToAdd -= (5.0F - TimeSlider.value) * 0.15F;
                addToScore(scoreToAdd);
            }
        }
        else
        {
            addToScore(-0.25F);
        }
    }
    // ================================================================================== //
    private void piercingRaycast()
    {
        int hitCount = 0;

        RaycastHit2D[] hits;
        hits = Physics2D.RaycastAll(transform.position, -transform.right, 100.0F);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit2D hit = hits[i];
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.GetComponent<DroneAI>() != null)
                {
                    GameObject smoke = Instantiate(Smoke) as GameObject;
                    smoke.transform.position = hit.collider.transform.position;
                    smoke.transform.parent = hit.collider.transform;

                    hit.collider.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1.0F;

                    hit.collider.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    hit.collider.gameObject.layer = LayerMask.NameToLayer("DestroyedDronesLayer");

                    if (hit.collider.gameObject.GetComponent<DroneAI>().isActiveAndEnabled)
                        hitCount++;

                    hit.collider.gameObject.GetComponent<DroneAI>().enabled = false;

                    AudioSource.PlayClipAtPoint(HitDroneSound, Camera.main.transform.position);
                }
            }
        }

        DrawLine(transform.position, -transform.right * 100.0F, Color.yellow, 0.02F, 0.05F);

        if (hitCount > 0)
        {
            float scoreToAdd = hitCount * 1.0F;
            if (TimeSlider.value < 5.0F)
                scoreToAdd -= hitCount * (5.0F - TimeSlider.value) * 0.15F;
            addToScore(scoreToAdd);
        }
        else
        {
            addToScore(-0.25F);
        }
    }
    // ================================================================================== //
    void DrawLine(Vector3 start, Vector3 end, Color color, float width, float duration = 0.2f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.SetColors(color, color);
        lr.SetWidth(width, width);
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
