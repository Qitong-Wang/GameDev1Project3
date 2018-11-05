using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bedroom : Stuff
{
    public GameObject cleanObjectPrefab;

    public override void Finish()
    {
        dataManager.goodEnding = true;
        Instantiate(cleanObjectPrefab, position, Quaternion.Euler(Vector3.zero));
        base.Finish();
        
    }
}
