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
    public Slider TimeSlider;
    private float _score = 0.0F;

    private string _levelTitle;
    //public float Score{ get { return _score; } }
    // ================================================================================== //
    void Start () 
    {
        _levelTitle = PlayerPrefs.GetString("LevelType");
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

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.right);
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
                            if (!drn.GetComponent<DroneProperties>().IsTarget)
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

                GameObject smoke = Instantiate(Smoke) as GameObject;
                smoke.transform.position = hit.collider.transform.position;
                smoke.transform.parent = hit.collider.transform;

                hit.collider.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1.0F;

                hit.collider.gameObject.GetComponent<SpriteRenderer>().enabled = true;

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
            if (isTargetAndHitBomb)
            {
                _score -= 1.0F;
            }
            else
            {
                _score += 1.0F;
                if (TimeSlider.value < 5.0F && _levelTitle != "Skeet")
                    _score -= (5.0F - TimeSlider.value) * 0.15F;
            }
        }
        else
        {
            _score -= 0.25F;
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
            _score += hitCount * 1.0F;
            if (TimeSlider.value < 5.0F)
                _score -= hitCount * (5.0F - TimeSlider.value) * 0.15F;
        }
        else
        {
            _score -= 0.25F;
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
}
