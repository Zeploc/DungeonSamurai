using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunkerScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OpenBunker()
    {
        GetComponent<Animator>().SetBool("Open", true);
    }
}
