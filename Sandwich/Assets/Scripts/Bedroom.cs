using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bedroom : Stuff
{

    public override void Finish()
    {
        base.Finish();
        dataManager.goodEnding = true;
    }
}
