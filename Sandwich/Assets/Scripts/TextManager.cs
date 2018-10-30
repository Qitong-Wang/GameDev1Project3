using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour {
    public GameObject DataManager;
    DataManager dataManager;
    public GameObject TimeText;
    Text timeText;
    public Slider ProgressSlider;
    public float second;
    private float totalTime = 0f;
    // Use this for initialization
    void Start () {
        timeText = TimeText.GetComponent<Text>();
        dataManager = DataManager.GetComponent<DataManager>();
        

    }
	
	// Update is called once per frame
	void Update () {
        
        //Time
        totalTime += Time.deltaTime;
        if (totalTime >= 1)
        {
            second--;
            timeText.text = string.Format("Time Remain:\n {0:00}:{1:00}\n Working Progress:\n {2:00}%", ((int)second / 60), second % 60,((int)(dataManager.cleanProgress*100)));
            totalTime = 0;
          
        }
        //Slider
        ProgressSlider.value = dataManager.cleanProgress;
    }
}
