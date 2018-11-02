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
    public Vector3 position; //Position of this stuff
    public GameObject sandwichPrefab; //Sandwich for instantiate
    public Vector3 releasePos;  //Position of releasing sandwiches
    public float releaseOffset; //Position of releasing sandwich offset

    

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
    //Finish Cleaning the stuff
    public virtual void Finsish()
    {
        dataManager.sandwichIdle += workingSandwiches;
        workingSandwiches = 0;
        dataManager.AddProgress();
        var temp = new Vector3((releasePos + Random.insideUnitSphere).x, 0, (releasePos + Random.insideUnitSphere).z);
        Instantiate(sandwichPrefab, temp * releaseOffset, Quaternion.Euler(Vector3.zero));
        Destroy(gameObject);
    }
    //A sandwich is trigger a stuff
    private void OnTriggerEnter(Collider other)
    {
        for (int i=0;  i < sandwichWaitingList.Count; i ++)
        {
            if (GameObject.ReferenceEquals(sandwichWaitingList[i], other.gameObject))
            {
                if (workingSandwiches < maximumSandwiches)
                {
                    workingSandwiches += 1;
                    dataManager.sandwichWorking += 1;
                    Destroy(other.gameObject);
                    sandwichWaitingList.RemoveAt(i);
                    break;
                }
            }
        }
       
        //If there are enough sandwiches are working , stop the other traveling sandwiches
        if (workingSandwiches == maximumSandwiches)
        {
            foreach (GameObject g in this.sandwichWaitingList)
            {
                Sandwich sandwich = g.GetComponent<Sandwich>();
                sandwich.stopMoving();
                dataManager.sandwichIdle += 1;
            }
            this.sandwichWaitingList.Clear();
           
           
        }
        //Update textManager
        textManager.UpdateSandwich();
    }
    
    //Get the order of cancel a sandwich in the waitlist
    public void CancelSandwich(GameObject sandwich)
    {
        for (int i = 0; i < sandwichWaitingList.Count; i++)
        {
            if (GameObject.ReferenceEquals(sandwichWaitingList[i], sandwich))
            {
                sandwichWaitingList.RemoveAt(i);
                break;
            }
        }
    }
    //Player right click a stuff and cancel the work
    public void CancelWork()
    {
        if (workingSandwiches >= 1)
        {
            workingSandwiches -= 1;
            dataManager.sandwichIdle += 1;

            dataManager.sandwichWorking -= 1;
          
            textManager.UpdateSandwich();
        }
    }
}
