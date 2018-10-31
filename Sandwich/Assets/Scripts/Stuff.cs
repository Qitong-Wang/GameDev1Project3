using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stuff : MonoBehaviour {
    public float maximumSandwiches; //The maximum of sandwiches who can work here
    public float workingSandwiches; //The number of sandwiches who are working here
    public float maximumTime;
    private float currentTime = 0f; //The initial time to complete the whole work
    private float timeCalculate = 0f; //Jump from 0 to 1
    private Image timeBar;
    public GameObject DataManager;
    DataManager dataManager;
	// Use this for initialization
	void Start () {
        timeBar = transform.Find("StuffCanvas").Find("TimeBG").Find("Time").GetComponent<Image>();

	}
	
	// Update is called once per frame
	void Update () {
        timeCalculate += Time.deltaTime;
        if (timeCalculate >= 1)
        {
            currentTime += workingSandwiches;
            timeCalculate = 0;
        }
        timeBar.fillAmount = currentTime / maximumTime;
        print(workingSandwiches);
    }

    public void IncreaseSandwich()
    {
        if (workingSandwiches < maximumSandwiches)
        {
            workingSandwiches += 1;

        }
    }
}
