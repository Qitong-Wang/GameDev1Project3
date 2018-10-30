using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {
    public float cleanProgress = 0f;
    public float cleanGarbage = 0.5f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddProgress()
    {
        this.cleanProgress += this.cleanGarbage;
    }
}
