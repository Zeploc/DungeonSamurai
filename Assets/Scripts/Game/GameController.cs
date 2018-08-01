using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Player PlayerRef;
	public GameObject CurrentEnemy;

	// Use this for initialization
	void Start ()
    {
        //PlayerRef = FindObjectOfType<Player>();
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.G))
		{
			CurrentEnemy.GetComponent<BaseEnemy> ().TakeDamage (10);
		}
	}

    public Player GetPlayer()
    {
		if (PlayerRef)
			Debug.Log ("Returning player");
		else
			Debug.Log ("Not valid");
        return PlayerRef;
    }
}
