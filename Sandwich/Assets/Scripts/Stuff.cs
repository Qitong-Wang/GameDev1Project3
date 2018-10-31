using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stuff : MonoBehaviour {
    public int maximumSandwiches; //The maximum of sandwiches who can work here
    public int workingSandwiches; //The number of sandwiches who are working here
    public float maximumTime;
    private float currentTime = 0f; //The initial time to complete the whole work
    private float timeCalculate = 0f; //Jump from 0 to 1
    private Image timeBar;
    public GameObject DataManager;
    DataManager dataManager;
    public GameObject TextManager;
    TextManager textManager;
	// Use this for initialization
	void Start () {
        timeBar = transform.Find("StuffCanvas").Find("TimeBG").Find("Time").GetComponent<Image>();
        dataManager = DataManager.GetComponent<DataManager>();
        textManager = TextManager.GetComponent<TextManager>();
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
        if (currentTime >= maximumTime)
        {
            this.Finsish();
        }
    }

    public void IncreaseSandwich()
    {
        if (workingSandwiches < maximumSandwiches && dataManager.sandwichHolding>=1)
        {
            workingSandwiches += 1;
            dataManager.sandwichHolding -= 1;
            dataManager.sandwichWorking += 1;
            textManager.UpdateSandwich();

        }
    }
    public void Finsish()
    {
        dataManager.sandwichIdle += workingSandwiches;
        workingSandwiches = 0;
        dataManager.AddProgress();
        Destroy(gameObject);
    }
}
