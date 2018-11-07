using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingSceneManager : MonoBehaviour {
    public float cleanProgress; //Total percentage of clean
    public int sandwichTotal; //number of total sandwich
    public bool goodEnding;
    public bool frontDoorOpen;
    DataManager dataManager;

    [Header("Images/Text")]
    public GameObject[] good;
    public GameObject[] bad;
    public GameObject[] neutral;

    
    void Start () {
        dataManager = FindObjectOfType<DataManager>();
        Cursor.lockState = CursorLockMode.None;
        cleanProgress = dataManager.cleanProgress;
        sandwichTotal = dataManager.sandwichTotal;
        goodEnding = dataManager.goodEnding;
        frontDoorOpen = dataManager.frontDoorOpen;
        dataManager.DestoryThisObject();

    }
	
	// Update is called once per frame
	void Update () {
        if (frontDoorOpen)
        {
            if (goodEnding)
            {
                UnlockAll(good);
            }
            else
            {
                UnlockAll(neutral);
            }
        }
  
        else
        {
            UnlockAll(bad);
        }
    }

    void UnlockAll(GameObject[] g)
    {
        foreach(GameObject item in g)
        {
            item.SetActive(true);
        }
    }
}
