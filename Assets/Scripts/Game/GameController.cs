using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    Player PlayerRef;

	// Use this for initialization
	void Start ()
    {
        PlayerRef = FindObjectOfType<Player>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public Player GetPlayer()
    {
        return PlayerRef;
    }
}
