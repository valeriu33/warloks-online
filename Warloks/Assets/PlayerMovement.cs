using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 1F;
    private Vector3 velocity = Vector3.zero;
    private Vector3 targetPosition;

    // Use this for initialization
    void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButton(0))
	    {
	        targetPosition = mousePosition;
	        targetPosition.z = 0;
	        //            gameObject.transform.position = pz;

	    }

        Vector3 diff = mousePosition - transform.position;
	    diff.Normalize();

	    float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
	    transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

	    transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 1/speed);

        

	}
}
