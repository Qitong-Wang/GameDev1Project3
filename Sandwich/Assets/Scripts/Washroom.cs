using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Washroom : Stuff
{
    public GameObject cleanObjectPrefab;
    public float cleanConstant = 1.5f;

    public override void Finish()
    {
        dataManager.cleanConstant = this.cleanConstant;
        Instantiate(cleanObjectPrefab, position, Quaternion.Euler(Vector3.zero));
        base.Finish();
        newsText.text += "\nNow sandwiches clean faster!";
    }
}
