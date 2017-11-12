using UnityEngine;
using System.Collections;

public class DroneAI : MonoBehaviour 
{
    public float Speed;

    private string _levelTitle;

    private Vector2 _initPosition;
    public Vector2 RandomPosition;

    private float _timeToSwitchPosition = 0.0F;

    private float _visibilityStateTime = 0.0F;
    private bool _isVisible;
    // ================================================================================== //
	void Start () 
    {
        _levelTitle = PlayerPrefs.GetString("LevelType");

        _initPosition = transform.position;

        _isVisible = (Random.Range(0, 2) == 0);

        if (_levelTitle == "Static")
            Speed = 0;

        if (_levelTitle == "Fast")
            Speed *= 2;
    }
    // ================================================================================== //
	void Update () 
    {
        _timeToSwitchPosition -= Time.deltaTime;
        moveToPosition();
        //hover();

        if (_levelTitle == "Skeet")
            return;

        if (_timeToSwitchPosition <= 0.0F)
        {
            _timeToSwitchPosition = Random.Range(1.0F, 5.0F);

            if (_levelTitle == "Fast")
                _timeToSwitchPosition = Random.Range(0.25F, 1.5F);

            RandomPosition = new Vector2(Random.Range(-7.0F, 6.5F), Random.Range(-1.0F, 4.0F));

            //hover();
        }

        if (_levelTitle == "Invisible")
        {
            if (_visibilityStateTime > 0.0F)
            {
                _visibilityStateTime -= Time.deltaTime;
            }
            else
            {
                _visibilityStateTime = Random.Range(0.0F, 2.0F);
                _isVisible = !_isVisible;
                if (_isVisible)
                    this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                else
                    this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
	}
    // ================================================================================== //
    private void moveToPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, RandomPosition, Speed * Time.deltaTime);
    }
    // ================================================================================== //
    private void hover()
    {
        //transform.position = new Vector3(transform.position.x + Random.Range(-0.1F, 0.1F), transform.position.y + Random.Range(-0.1F, 0.1F));
    }
    // ================================================================================== //
}
