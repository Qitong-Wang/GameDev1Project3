using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Kitchen : Stuff
{
    public float workingTime;
    public bool clean = false;

    void Update()
    {
        if (clean == false)//Before clean 
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
                currentTime = 0;
                this.Finsish();
                timeBar.color = Color.yellow;

            }
        }
        else //After clean. Just produce more sandwich
        {
            if (workingSandwiches >= 1)
            {
                timeCalculate += Time.deltaTime;
                if (timeCalculate >= 1) //+1S
                {
                    currentTime += workingSandwiches;
                    timeCalculate = 0;
                }
                timeBar.fillAmount = currentTime / workingTime;
            }
            if (currentTime >= workingTime)
            {
                this.Produce();
                currentTime = 0;
            }
        }

    }
    public override void Finsish()
    {

        //Increase progress
        dataManager.AddProgress();
        //Release the same amount of sandwich
        for (int i = 1; i <= workingSandwiches; i++)
        {
            InstantiateSandwich();
        }
        workingSandwiches = 0;
        //Is able to produce new sandwich
        clean = true;
        textManager.UpdateSandwich();
        timeBar.fillAmount = currentTime / maximumTime;
    }
    //Produce new sandwiches
    public void Produce()
    {
        dataManager.sandwichIdle += 1;
        dataManager.sandwichTotal += 1;
        InstantiateSandwich();
        textManager.UpdateSandwich();
    }

}
