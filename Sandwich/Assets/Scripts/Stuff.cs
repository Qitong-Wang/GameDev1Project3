using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stuff : MonoBehaviour
{
    public int maximumSandwiches; //The maximum of sandwiches who can work here
    public int workingSandwiches; //The number of sandwiches who are working here
    public float maximumTime; //Maximum time of clean a stuff
    public float currentTime = 0f; //The initial time to complete the whole work
    public GameObject sandwichPrefab; //Sandwich for instantiate
    public Vector3 releasePos;  //Position of releasing sandwiches
    public float releaseOffset; //Position of releasing sandwich offset
    public float increaseProcess = 0.15f; //The increase of total process

    protected float timeCalculate = 0f; //Jump from 0 to 1
    protected Image timeBar; //The time bar above the stuff
    protected GameObject DataManager;
    protected DataManager dataManager;
    protected GameObject TextManager;
    protected TextManager textManager;
    protected GameObject NewsText;
    protected Text newsText;
    protected List<GameObject> sandwichWaitingList;//Sandwich selection list
    protected Vector3 position; //Position of this stuff
    protected Transform smoke; //Smoke on the stuff



    // Use this for initialization
    void Start()
    {
        timeBar = transform.Find("StuffCanvas").Find("TimeBG").Find("Time").GetComponent<Image>();
        smoke = transform.Find("smokeCloudPrefab");
        DataManager = GameObject.FindGameObjectWithTag("DataManager");
        dataManager = DataManager.GetComponent<DataManager>();
        TextManager = GameObject.FindGameObjectWithTag("TextManager");
        textManager = TextManager.GetComponent<TextManager>();
        sandwichWaitingList = new List<GameObject>();
        NewsText = GameObject.FindGameObjectWithTag("NewsText");
        newsText = NewsText.GetComponent<Text>();
        position = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);
        timeBar.fillAmount = currentTime / maximumTime;

    }

    // Update is called once per frame
    void Update()
    {
        if (workingSandwiches >= 1)
        {
            
            timeCalculate += Time.deltaTime;
            if (timeCalculate >= 1) //+1S
            {
                currentTime += workingSandwiches * dataManager.cleanConstant;
                timeCalculate = 0;
            }
            timeBar.fillAmount = currentTime / maximumTime;
        }
       
        if (currentTime >= maximumTime)
        {
            this.Finish();
        }


    }
    //After the player left click the sandwich
    public void OrderSandwich()
    {
        //Copy the dataManager.sandwichWaitingList to the stuff's waiting list
        this.sandwichWaitingList.AddRange(dataManager.sandwichWaitingList);
        foreach (GameObject g in dataManager.sandwichWaitingList)
        {
            Sandwich s = g.GetComponent<Sandwich>();
            s.SetDestination(this.position);
            s.goalObject = gameObject;
            dataManager.sandwichIdle -= 1;
            dataManager.sandwichHolding -= 1;

        }
        dataManager.sandwichWaitingList.Clear();
        textManager.UpdateSandwich();
    }
    //Finish Cleaning the stuff
    public virtual void Finish()
    {
        if (workingSandwiches > 1)
        {
            newsText.text = string.Format("{0} was cleaned by\n{1} sandwiches"
            , gameObject.name, workingSandwiches);
        }
        else
        {
            newsText.text = string.Format("{0} was cleaned by\n{1} sandwich"
            , gameObject.name, workingSandwiches);
        }
        //Increase progress
        if (dataManager.cleanProgress + this.increaseProcess <= 1f)
        {
            dataManager.cleanProgress += this.increaseProcess;
        }
        else
        {
            dataManager.cleanProgress = 1f;
        }

        //Release the same amount of sandwich
        for (int i = 1; i <= workingSandwiches; i++)
        {
            InstantiateSandwich();
            dataManager.sandwichWorking -= 1;
            dataManager.sandwichIdle += 1;
        }
        workingSandwiches = 0;

        //Relase the sandwich in the waiting list
        foreach (GameObject sandwich in sandwichWaitingList)
        {
            Sandwich s = sandwich.GetComponent<Sandwich>();
            s.stopMoving();
            dataManager.sandwichIdle += 1;
        }
       
        textManager.UpdateSandwich();
        Destroy(gameObject);
    }
    //A sandwich is trigger a stuff
    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < sandwichWaitingList.Count; i++)
        {
            //Sandwich is in the list
            if (GameObject.ReferenceEquals(sandwichWaitingList[i], other.gameObject))
            {
                if (workingSandwiches < maximumSandwiches)
                {
                    smoke.gameObject.SetActive(true);
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
        textManager.UpdateSandwich();
    }
    //Player right click a stuff and cancel the work
    public void CancelWork()
    {
        if (workingSandwiches >= 1)
        {
            workingSandwiches -= 1;
            InstantiateSandwich();
            dataManager.sandwichIdle += 1;
            dataManager.sandwichWorking -= 1;
        }
        if (workingSandwiches < 1)
        {
            smoke.gameObject.SetActive(false);
        }
        textManager.UpdateSandwich();
    }
    //Instantiate a sandwich 
    public void InstantiateSandwich()
    {
        var temp = new Vector3((releasePos + Random.insideUnitSphere * releaseOffset).x, 0, (releasePos + Random.insideUnitSphere * releaseOffset).z);
        Instantiate(sandwichPrefab, temp, Quaternion.Euler(Vector3.zero));

    }
}
