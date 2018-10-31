using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stuff : MonoBehaviour {
    public int maximumSandwiches; //The maximum of sandwiches who can work here
    public int workingSandwiches; //The number of sandwiches who are working here
    public float maximumTime;
    public float currentTime = 0f; //The initial time to complete the whole work
    public float timeCalculate = 0f; //Jump from 0 to 1
    public Image timeBar;
    public GameObject DataManager;
    DataManager dataManager;
    public GameObject TextManager;
    TextManager textManager;
    public List<GameObject> sandwichWaitingList;//Sandwich selection list
    Vector3 position;

    

	// Use this for initialization
	void Start () {
        timeBar = transform.Find("StuffCanvas").Find("TimeBG").Find("Time").GetComponent<Image>();
        dataManager = DataManager.GetComponent<DataManager>();
        textManager = TextManager.GetComponent<TextManager>();
        sandwichWaitingList = new List<GameObject>();
        position = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);

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
        //print(workingSandwiches);

    }
    //After the player left click the sandwich
    public void OrderSandwich()
    {
        //Prevent mistake touch
        if (dataManager.sandwichWaitingList!= null)
        {
            //Copy the dataManager.sandwichWaitingList to the stuff's waiting list
            this.sandwichWaitingList = new List<GameObject>(dataManager.sandwichWaitingList);
            dataManager.sandwichWaitingList.Clear();
            foreach (GameObject g in this.sandwichWaitingList)
            {
                Sandwich s = g.GetComponent<Sandwich>();
                s.SetDestination(this.position);
                s.goalObject = gameObject;
                dataManager.sandwichIdle -= 1;
                dataManager.sandwichHolding -= 1;
                textManager.UpdateSandwich();
            }
        }

    }
    //Finish Clean the stuff
    public virtual void Finsish()
    {
        dataManager.sandwichIdle += workingSandwiches;
        workingSandwiches = 0;
        dataManager.AddProgress();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject g in this.sandwichWaitingList){
            if (GameObject.ReferenceEquals(g, other.gameObject))
            {
                if (workingSandwiches < maximumSandwiches) { 
                    workingSandwiches += 1;
                    dataManager.sandwichWorking += 1;
                    textManager.UpdateSandwich();
                    Destroy(other.gameObject);
                }
                if (workingSandwiches == maximumSandwiches)
                {
                    this.sandwichWaitingList.Clear();
                    //Need implement data change and idle stop
                    break;
                }
            }
        }
    }
}
