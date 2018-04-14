using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronesManager : MonoBehaviour
{
    private GameObject[] _drones;
    private List<Vector3> _targetPositions = new List<Vector3>();

    private LevelData _levelData;

    void Start ()
    {
        _levelData = DataManager.Instance.CurrentLevelData;

        // create drones
        _drones = new GameObject[_levelData.NumberOfDrones];
        for (int i = 0; i < _levelData.NumberOfDrones; ++i)
        {
            Vector3 randomPosition = Utils.GetRandomPositionInsideGameArea();
            _drones[i] = Instantiate(_levelData.Drones[0], randomPosition, Quaternion.identity);
        }

        // drones formation - "two at a time"
        if (_levelData.FormationType == DronesFormationType.TWO_AT_A_TIME)
        {
            for (int i = 0; i < _levelData.NumberOfDrones; ++i)
            {
                _drones[i].transform.position = new Vector2(-10000.0F, -10000.0F);
                _drones[i].GetComponent<Drone>().RandomPosition = new Vector2(9.0F, Random.Range(-1.0F, 4.0F));
            }
            StartCoroutine(TwoAtATimeDronesCoroutine());
        }

        // only one target
        if (_levelData.IsOnlyOneTargetAtATime)
        {
            int rand = Random.Range(0, 10);
            _drones[rand].GetComponent<Drone>().IsTarget = true;
        }
    }
	
	void Update ()
    {
        
    }

    IEnumerator TwoAtATimeDronesCoroutine()
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
