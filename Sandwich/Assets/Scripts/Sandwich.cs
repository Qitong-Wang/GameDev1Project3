using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Sandwich : MonoBehaviour {
    public Vector3 goal;
    public NavMeshAgent agent;
    public bool navigating;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal;
	}
	
	// Update is called once per frame
	void Update () {
        if (!navigating) {
            agent.destination = transform.position;
        }
	}
    public void setDestination(Vector3 dest) {
        agent.destination = dest;
        navigating = true;
    }
}
