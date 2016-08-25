using UnityEngine;
using System.Collections;

public class DroneProperties : MonoBehaviour 
{
    public Sprite BigDroneSprite;
	// Use this for initialization
	void Start () 
    {
        if (PlayerPrefs.GetString("LevelType") == "Easy")
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = BigDroneSprite;
            Destroy(this.gameObject.GetComponent<BoxCollider2D>());
            this.gameObject.AddComponent<BoxCollider2D>();
            //this.transform.localScale = new Vector3(2.0F, 2.0F);
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
