using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Stuff such as sofa and TV that needs to make a clean one.
public class ReusableStuff : Stuff
{   
    public GameObject cleanObjectPrefab;

    public override void Finish()
    {
        Instantiate(cleanObjectPrefab, position, Quaternion.Euler(Vector3.zero));
        base.Finish();
        
    }
}
