using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour {
    public float distance;
    public GameObject objectTouch;
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
       
        
        Vector3 forward = transform.TransformDirection(Vector3.forward)*10;
        Debug.DrawRay(transform.position, forward, Color.green);
        if (Physics.Raycast(transform.position,(forward),out hit))
        {
            distance = hit.distance;
            objectTouch = hit.collider.gameObject; 
        }
        else
        {
            distance = -1f;
            objectTouch = null;
        }
       print(distance + " " + objectTouch);
	}
}
