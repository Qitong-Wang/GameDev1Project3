using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour
{
    public GameObject DataManager; //DataManager
    DataManager dataManager;
    //Use for the Time text
    public GameObject TimeText;
    Text timeText;
    public Slider ProgressSlider;
    public float second;
    private float totalTime = 0f;
    //Use for Status Text
    public GameObject StatusText;
    Text statusText;


    // Use this for initialization
    void Start()
    {
        timeText = TimeText.GetComponent<Text>();
        dataManager = DataManager.GetComponent<DataManager>();
        statusText = StatusText.GetComponent<Text>();
        this.UpdateSandwich();

        timeText.text = string.Format("Time Remaining: {0:00}:{1:00}\nWork Progress:  {2:00}%", ((int)second / 60), second % 60, ((int)(dataManager.cleanProgress * 100)));


    }

    // Update is called once per frame
    void Update()
    {

        //Time
        totalTime += Time.deltaTime;
        if (totalTime >= 1)
        {
            second--;
            timeText.text = string.Format("Time Remaining: {0:00}:{1:00}\nWork Progress:  {2:00}%", ((int)second / 60), second % 60, ((int)(dataManager.cleanProgress * 100)));
            totalTime = 0;

        }
        //Slider
        ProgressSlider.value = dataManager.cleanProgress;
        if (second <= 0)
        {
            SceneManager.LoadScene(1);
        }
    }
    public void UpdateSandwich()
    {
        statusText.text = string.Format("Working Sandwiches: {0}/{1}\nIdle Sandwiches:      {2}/{1}\nHeld Sandwiches:     {3}/{1}"
           , dataManager.sandwichWorking, dataManager.sandwichTotal, dataManager.sandwichIdle, dataManager.sandwichHolding);

    }

}
    