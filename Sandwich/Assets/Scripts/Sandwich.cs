﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Sandwich : MonoBehaviour {
    public Vector3 goal; //The vector of the goal
    public NavMeshAgent agent;
    public bool navigating;
    public GameObject goalObject; //The object of the goal
    public GameObject DataManager;
    DataManager dataManager;
    public GameObject TextManager;
    TextManager textManager;
    
    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal;
        agent.isStopped = true;
        dataManager = DataManager.GetComponent<DataManager>();
        textManager = TextManager.GetComponent<TextManager>();
    }
	
	// Update is called once per frame
	void Update () {

	}
    public void SetDestination(Vector3 dest) {
        agent.destination = dest;
        agent.isStopped = false;
    }
    public void SelectSandwich() //Player right click the mouse and select the sandwich
    {
        if (!dataManager.sandwichWaitingList.Contains(gameObject))
        {
            dataManager.sandwichHolding += 1;
            dataManager.sandwichWaitingList.Add(gameObject);
            textManager.UpdateSandwich();
     
        }

    }
    public void stopMoving() {
        agent.isStopped = true;
    }
}
