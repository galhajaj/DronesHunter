using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Aim : MonoBehaviour 
{
    public Slider AimSlider;
    public GameObject Gun;
    // ================================================================================== //
	void Start () 
    {
        //Adds a listener to the main slider and invokes a method when the value changes.
        AimSlider.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
	}
	// ================================================================================== //
	void Update () 
    {
        // oscilation
        /*AimSlider.value = Mathf.Abs(Mathf.Sin(Mathf.Deg2Rad * Time.time * 50.0F));*/

        // joystick
        /*LayerMask layerMask = (1 << LayerMask.NameToLayer("JoystickLayer"));
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0.0F, layerMask);

        if (!hit)
            return;
        if (hit.collider == null)
            return;

        var dir = transform.position - (Vector3)hit.point;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Gun.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);*/
    }
    // ================================================================================== //
    // Invoked when the value of the slider changes.
    public void ValueChangeCheck()
    {
        Gun.transform.rotation = Quaternion.Euler(0.0F, 0.0F, -90.0F * AimSlider.value);
    }
    // ================================================================================== //
}
