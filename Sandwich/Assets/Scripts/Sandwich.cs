using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Sandwich : MonoBehaviour
{
    public Vector3 goal; //The vector of the goal
    public NavMeshAgent agent;
    public bool navigating;
    public GameObject goalObject; //The object of the goal
    GameObject DataManager;
    DataManager dataManager;
    GameObject TextManager;
    TextManager textManager;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal;
        agent.isStopped = true;
        DataManager = GameObject.FindGameObjectWithTag("DataManager");
        dataManager = DataManager.GetComponent<DataManager>();
        TextManager = GameObject.FindGameObjectWithTag("TextManager");
        textManager = TextManager.GetComponent<TextManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetDestination(Vector3 dest)
    {
        agent.destination = dest;
        agent.isStopped = false;
    }
    public void SelectSandwich()
    { //Player right click the mouse and select the sandwich
        if (goalObject != null)//Needs to cancel the the traveling of sandwich
        {
            stopMoving();
            Stuff s = goalObject.GetComponent<Stuff>();
            s.CancelSandwich(gameObject);
            goalObject = null;
            goal = new Vector3();
            dataManager.sandwichIdle += 1;
        }
        if (!dataManager.sandwichWaitingList.Contains(gameObject))
        {
            dataManager.sandwichHolding += 1;
            dataManager.sandwichWaitingList.Add(gameObject);
        }
        textManager.UpdateSandwich();
    }

    public void stopMoving()
    {
        agent.isStopped = true;
    }
}
