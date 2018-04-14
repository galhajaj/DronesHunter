using UnityEngine;
using System.Collections;

public class Drone : MonoBehaviour 
{
    [SerializeField]
    private float _speed = 3.0F;

    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;

    private Vector2 _initPosition;
    public Vector2 RandomPosition;

    [SerializeField]
    private GameObject _smokePrefab = null;

    [Header("Switch Position")]
    [SerializeField]
    private float _switchPositionMinTime = 1.0F;
    [SerializeField]
    private float _switchPositionMaxTime = 5.0F;
    private float _timeToSwitchPosition = 0.0F;

    [Header("Invisibility")]
    [SerializeField]
    private bool _isGotInvisibility = false;
    [SerializeField]
    private float _invisibilityMinTime = 0.0F;
    [SerializeField]
    private float _invisibilityMaxTime = 2.0F;
    private float _timeToChangeVisibilityState = 0.0F;
    private bool _isVisible;

    [Header("Only One Target")]
    [SerializeField]
    private bool _isTarget = false;
    public bool IsTarget
    {
        get { return _isTarget; }
        set { _isTarget = value; /*TODO: add set texture to target/bomb*/}
    }

    [Header("Piercable")]
    [SerializeField]
    private bool _isPiercable = false;
    public bool IsPiercable
    {
        get { return _isPiercable; }
        set { _isPiercable = value; }
    }

    [Header("Floating")]
    [SerializeField]
    private bool _isFloating = false;
    [SerializeField]
    private float _floatingAmplitude = 0.005F;
    [SerializeField]
    private float _floatingFrequency = 0.5F;
    // ================================================================================== //
    void Awake()
    {
        _rigidBody = this.GetComponent<Rigidbody2D>();
        _spriteRenderer = this.GetComponent<SpriteRenderer>();
    }
    // ================================================================================== //
    void Start () 
    {
        _initPosition = transform.position;

        _isVisible = (Random.Range(0, 2) == 0);
    }
    // ================================================================================== //
	void Update () 
    {
        // floating
        if (_isFloating)
        {
            float dy = Mathf.Sin(Time.fixedTime * Mathf.PI * _floatingFrequency) * _floatingAmplitude;
            transform.position = new Vector3(transform.position.x, transform.position.y + dy, transform.position.z);
        }

        _timeToSwitchPosition -= Time.deltaTime;
        moveToPosition();

        // for skeet
        if (DataManager.Instance.CurrentLevelData.FormationType == DronesFormationType.TWO_AT_A_TIME)
            return;

        // switch position
        if (_timeToSwitchPosition <= 0.0F)
        {
            _timeToSwitchPosition = Random.Range(_switchPositionMinTime, _switchPositionMaxTime);

            RandomPosition = Utils.GetRandomPositionInsideGameArea();
        }

        // invisibility
        if (_isGotInvisibility)
        {
            if (_timeToChangeVisibilityState > 0.0F)
            {
                _timeToChangeVisibilityState -= Time.deltaTime;
            }
            else
            {
                _timeToChangeVisibilityState = Random.Range(_invisibilityMinTime, _invisibilityMaxTime);
                _isVisible = !_isVisible;
                _spriteRenderer.enabled = _isVisible;
            }
        }
	}
    // ================================================================================== //
    private void moveToPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, RandomPosition, _speed * Time.deltaTime);
    }
    // ================================================================================== //
    public void GotHit(Vector2 hitPoint)
    {
        // hit sound
        SoundManager.Instance.Play(SoundManager.Instance.HitDroneSound);

        // add jumping force (if not piercable)
        if (!_isPiercable)
            _rigidBody.AddForceAtPosition(200.0F * -transform.right, hitPoint);

        // smoke
        GameObject smoke = Instantiate(_smokePrefab) as GameObject;
        smoke.transform.position = this.transform.position;
        smoke.transform.parent = this.transform;

        // gravity - falling
        _rigidBody.gravityScale = 1.0F;

        // make visible when dies
        _spriteRenderer.enabled = true;

        // update layer to DestroyedDronesLayer
        this.gameObject.layer = LayerMask.NameToLayer("DestroyedDronesLayer");

        /*if (hit.collider.gameObject.GetComponent<Drone>().isActiveAndEnabled)
            isHit = true;*/

        // disable the Drone script
        this.enabled = false;

        //hitPos = hit.point;
    }
    // ================================================================================== //
}
