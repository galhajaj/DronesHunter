using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour 
{
    public GameObject Smoke;

    public AudioClip FireSound;
    public AudioClip HitDroneSound;
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
        AudioSource.PlayClipAtPoint(FireSound, Camera.main.transform.position);

        Vector3 hitPos = Vector3.zero;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.right);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.GetComponent<DroneAI>() != null)
            {
                GameObject smoke = Instantiate(Smoke) as GameObject;
                smoke.transform.position = hit.collider.transform.position;
                smoke.transform.parent = hit.collider.transform;

                hit.collider.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1.0F;
                hit.collider.gameObject.GetComponent<DroneAI>().enabled = false;

                hitPos = hit.point;

                AudioSource.PlayClipAtPoint(HitDroneSound, Camera.main.transform.position);
            }
        }

        if (hitPos == Vector3.zero)
            DrawLine(transform.position, -transform.right * 100.0F, Color.yellow, 0.02F, 0.05F);
        else
            DrawLine(transform.position, hitPos, Color.yellow, 0.02F, 0.05F);
    }
    // ================================================================================== //
    void DrawLine(Vector3 start, Vector3 end, Color color, float width, float duration = 0.2f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.SetColors(color, color);
        lr.SetWidth(width, width);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }
    // ================================================================================== //
}
