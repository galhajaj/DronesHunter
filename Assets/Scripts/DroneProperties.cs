using UnityEngine;
using System.Collections;

public class DroneProperties : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
        if (PlayerPrefs.GetString("LevelType") == "Easy")
            this.transform.localScale = new Vector3(2.0F, 2.0F);
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
