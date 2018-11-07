using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public float cleanProgress = 0f; //Total percentage of clean
    public int sandwichTotal = 6; //number of total sandwich
    public int sandwichIdle = 0; //number of sandwiches who are idle
    public int sandwichHolding = 6; //number of sandwiches player is holding
    public int sandwichWorking = 0; //number of sandwiches who are working
    public float cleanConstant = 1f; //Constant that a sandwich's working efficient
    public bool goodEnding = false;
    public bool frontDoorOpen = false;
    public List<GameObject> sandwichWaitingList = new List<GameObject>();//Sandwich selection list

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DestoryThisObject()
    {
        Destroy(gameObject);
    }


}
