using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Kitchen : Stuff {
    public float workingTime;
    public bool clean = false;
    DataManager dataManager;
    TextManager textManager;
    void Update() 
    {
        timeCalculate += Time.deltaTime;
        if (clean == false)//Before clean 
        {
            if (timeCalculate >= 1)//+1S
            {
                currentTime += workingSandwiches;
                timeCalculate = 0;
            }
            timeBar.fillAmount = currentTime / maximumTime;
            if (currentTime >= maximumTime)
            {
                currentTime = 0;
                this.Finsish();
            }
        }
        else //After clean. Just produce more sandwich
        {
            if (timeCalculate >= 1) //+1S
            {
                currentTime += workingSandwiches;
                timeCalculate = 0;
            }
            timeBar.fillAmount = currentTime / workingTime;
            if (currentTime >= workingTime)
            {
                currentTime = 0;
                this.Produce();
            }
        }
       
    }
    public override void Finsish()
    {
        dataManager.sandwichIdle += workingSandwiches;
        workingSandwiches = 0;
        dataManager.AddProgress();
        //Release Idle
        clean = true;
    }

    public void Produce()
    {
        dataManager.sandwichIdle += workingSandwiches;
        workingSandwiches = 0;
        dataManager.AddProgress();
    }
	
}
