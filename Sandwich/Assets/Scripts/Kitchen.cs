using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Kitchen : Stuff
{
    public float workingTime;
    public bool clean = false;
    public Material cleanMaterial;


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
                this.Finish();
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
    public override void Finish()
    {
        if (workingSandwiches > 1)
        {
            newsText.text = string.Format("{0} was cleaned by {1} sandwiches"
            , gameObject.name, workingSandwiches);
        }
        else
        {
            newsText.text = string.Format("{0} was cleaned by {1} sandwiches"
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
        //Is able to produce new sandwich
        clean = true;
        textManager.UpdateSandwich();
        smoke.gameObject.SetActive(false);
        timeBar.fillAmount = currentTime / maximumTime;
        //Change the material
        transform.Find("pCube4").GetComponent<MeshRenderer>().material = cleanMaterial;
      
    }
    //Produce new sandwiches
    public void Produce()
    {
        newsText.text = string.Format("{0} produced 1 new sandwich", gameObject.name);
        dataManager.sandwichIdle += 1;
        dataManager.sandwichTotal += 1;
        InstantiateSandwich();
        textManager.UpdateSandwich();
    }

}
