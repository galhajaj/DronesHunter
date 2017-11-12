using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronesManager : MonoBehaviour
{
    private GameObject[] _drones;
    private List<Vector3> _targetPositions = new List<Vector3>();
    private string _levelTitle;

    void Start ()
    {
        _levelTitle = PlayerPrefs.GetString("LevelType");
        _drones = GameObject.FindGameObjectsWithTag("DroneTag");

        if (_levelTitle == "Skeet")
        {
            for (int i = 0; i < 10; ++i)
            {
                _drones[i].transform.position = new Vector2(-10000.0F, -10000.0F);
                _drones[i].GetComponent<DroneAI>().RandomPosition = new Vector2(9.0F, Random.Range(-1.0F, 4.0F));
            }
            StartCoroutine(SkeetDronesCoroutine());
        }

        if (_levelTitle == "Target")
        {
            int rand = Random.Range(0, 10);
            _drones[rand].GetComponent<DroneProperties>().IsTarget = true;
        }
    }
	
	void Update ()
    {
        
    }

    IEnumerator SkeetDronesCoroutine()
    {
        yield return new WaitForSeconds(3.0F);
        for (int i = 0; i < 10; i+=2)
        {
            _drones[i].transform.position = new Vector2(-8.0F, Random.Range(-1.0F, 4.0F));
            _drones[i+1].transform.position = new Vector2(-8.0F, Random.Range(-1.0F, 4.0F));
            yield return new WaitForSeconds(2.0F);
        }
    }
}
