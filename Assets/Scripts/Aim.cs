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
        /*AimSlider.value = Mathf.Abs(Mathf.Sin(Mathf.Deg2Rad * Time.time * 50.0F));*/
    }
    // ================================================================================== //
    // Invoked when the value of the slider changes.
    public void ValueChangeCheck()
    {
        Gun.transform.rotation = Quaternion.Euler(0.0F, 0.0F, -90.0F * AimSlider.value);
    }
    // ================================================================================== //
}
