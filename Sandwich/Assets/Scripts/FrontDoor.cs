using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontDoor : Stuff
{

    public override void Finish()
    {
        base.Finish();
        dataManager.frontDoorOpen = true;
    }
}
