using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingSceneManager : MonoBehaviour {
    public float cleanProgress; //Total percentage of clean
    public int sandwichTotal; //number of total sandwich
    public bool goodEnding;
    public bool frontDoorOpen;
    DataManager dataManager;
    
    void Start () {
        dataManager = GameObject.FindGameObjectWithTag("DataManager").GetComponent<DataManager>();
        cleanProgress = dataManager.cleanProgress;
        sandwichTotal = dataManager.sandwichTotal;
        goodEnding = dataManager.goodEnding;
        frontDoorOpen = dataManager.frontDoorOpen;
        dataManager.DestoryThisObject();

    }
	
	// Update is called once per frame
	void Update () {
        print(frontDoorOpen);
	}
}
