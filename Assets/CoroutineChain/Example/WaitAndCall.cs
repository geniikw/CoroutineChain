using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using geniikw.CChain;

public class WaitAndCall : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.StartChain()
            .Call(() => Debug.Log("1"))
            .Wait(1)
            .Log("2")
            .Wait(1)            
            .Log("end");
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
