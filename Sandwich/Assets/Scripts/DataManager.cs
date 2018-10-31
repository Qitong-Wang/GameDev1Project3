using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {
    public float cleanProgress = 0f; //Total percentage of clean
    public float cleanGarbage = 0.5f; 
    public int sandwichTotal = 6; //number of total sandwich
    public int sandwichIdle = 0; //number of sandwiches who are idle
    public int sandwichHolding = 6; //number of sandwiches player is holding
    public int sandwichWorking = 0; //number of sandwiches who are working
   


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddProgress()
    {
        this.cleanProgress += this.cleanGarbage;
    }
}
