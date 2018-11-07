using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontDoor : Stuff
{
    public GameObject cleanObjectPrefab;
    public override void Finish()
    {
        dataManager.frontDoorOpen = true;
        Instantiate(cleanObjectPrefab, position, Quaternion.Euler(new Vector3(0,-90,0)));
        base.Finish();
        newsText.text += "\nMom can enter the room!";

    }
}
