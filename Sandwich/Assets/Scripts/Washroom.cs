using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Washroom : Stuff {

    public float cleanConstant = 1.5f;
    public override void Finish()
    {
        base.Finish();
        dataManager.cleanConstant = this.cleanConstant;
    }
}
