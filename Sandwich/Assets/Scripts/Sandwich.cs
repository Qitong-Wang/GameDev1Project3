using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;


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
    Animator anim;

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
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetDestination(Vector3 dest)
    {
        agent.destination = dest;
        agent.isStopped = false;
        anim.SetBool("Walking", true);
    }
    public void SelectSandwich()
    { //Player right click the mouse and select the sandwich
        if (goalObject != null)//Needs to cancel the the traveling of sandwich
        {
            
            Stuff s = goalObject.GetComponent<Stuff>();
            s.CancelSandwich(gameObject);
            stopMoving();
            goal = new Vector3();
            
        }
        if (!dataManager.sandwichWaitingList.Contains(gameObject))
        {
            dataManager.sandwichHolding += 1;
            dataManager.sandwichWaitingList.Add(gameObject);
        }
        textManager.UpdateSandwich();
    }
    //Deselect Sandwich when the sandwich is idling
    public bool DeselectSandwich()
    {
        if (goalObject == null)
        {
            for (int i = 0; i<dataManager.sandwichWaitingList.Count; i++)
            {
                if (GameObject.ReferenceEquals(dataManager.sandwichWaitingList[i], gameObject))
                {
                    dataManager.sandwichWaitingList.RemoveAt(i);
                    dataManager.sandwichHolding -= 1;
                    textManager.UpdateSandwich();
                    return true;                  
                }
            }
        }
    
        return false;
    }

    public void stopMoving()
    {
        goalObject = null;
        dataManager.sandwichIdle += 1;
        agent.isStopped = true;
        anim.SetBool("Walking", false);
        
    }
}
