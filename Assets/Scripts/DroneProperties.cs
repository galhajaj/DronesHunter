using UnityEngine;
using System.Collections;

public class DroneProperties : MonoBehaviour 
{
    public Sprite BigDroneSprite;
    public Sprite SmallDroneSprite;
    public Sprite TargetDroneSprite;
    public Sprite BombDroneSprite;

    public bool IsTarget = false;

    private string _levelTitle;

    // Use this for initialization
    void Start () 
    {
        _levelTitle = PlayerPrefs.GetString("LevelType");

        if (_levelTitle == "Easy")
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = BigDroneSprite;
            Destroy(this.gameObject.GetComponent<BoxCollider2D>());
            this.gameObject.AddComponent<BoxCollider2D>();
        }

        if (_levelTitle == "Tiny")
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = SmallDroneSprite;
            Destroy(this.gameObject.GetComponent<BoxCollider2D>());
            this.gameObject.AddComponent<BoxCollider2D>();
        }

        if (_levelTitle == "Mix")
        {
            int rand = Random.Range(0, 3);
            if (rand == 0)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = BigDroneSprite;
                Destroy(this.gameObject.GetComponent<BoxCollider2D>());
                this.gameObject.AddComponent<BoxCollider2D>();
            }
            else if (rand == 1)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = SmallDroneSprite;
                Destroy(this.gameObject.GetComponent<BoxCollider2D>());
                this.gameObject.AddComponent<BoxCollider2D>();
            }
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if (_levelTitle == "Target")
        {
            if (IsTarget)
                this.gameObject.GetComponent<SpriteRenderer>().sprite = TargetDroneSprite;
            else
                this.gameObject.GetComponent<SpriteRenderer>().sprite = BombDroneSprite;
        }
	}
}
