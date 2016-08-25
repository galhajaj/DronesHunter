using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Fire : MonoBehaviour 
{
    public GameObject Smoke;

    public AudioClip FireSound;
    public AudioClip HitDroneSound;

    public Text ScoreText;
    public Slider TimeSlider;
    private float _score = 0.0F;
    //public float Score{ get { return _score; } }
    // ================================================================================== //
	void Start () 
    {
	
	}
    // ================================================================================== //
	void Update () 
    {
        ScoreText.text = "Score: " + (_score * 10.0F).ToString("F1")+"%";

        // change color of text
        string levelType = PlayerPrefs.GetString("LevelType");
        if (PlayerPrefs.GetFloat(levelType + "BestScore") < _score)
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
        AudioSource.PlayClipAtPoint(FireSound, Camera.main.transform.position);

        Vector3 hitPos = Vector3.zero;

        bool isHit = false;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.right);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.GetComponent<DroneAI>() != null)
            {
                GameObject smoke = Instantiate(Smoke) as GameObject;
                smoke.transform.position = hit.collider.transform.position;
                smoke.transform.parent = hit.collider.transform;

                hit.collider.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1.0F;

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
            _score += 1.0F;
            if (TimeSlider.value < 5.0F)
                _score -= (5.0F - TimeSlider.value) * 0.15F;
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
