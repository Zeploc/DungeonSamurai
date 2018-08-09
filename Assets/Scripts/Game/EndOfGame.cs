using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfGame : MonoBehaviour {

    public GameController GameControllerRef;
    
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GameComplete()
    {
        GameControllerRef.GameComplete();
    }
}
