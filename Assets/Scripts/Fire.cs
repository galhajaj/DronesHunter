using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour 
{

    // ================================================================================== //
	void Start () 
    {
	
	}
    // ================================================================================== //
	void Update () 
    {
	
	}
    // ================================================================================== //
    public void ExecuteFire()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.right);
        if (hit.collider != null)
        {
            hit.collider.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1.0F;
            hit.collider.gameObject.GetComponent<DroneAI>().enabled = false;
            //Debug.Log(hit.collider.gameObject.name);
        }
    }
    // ================================================================================== //
}
