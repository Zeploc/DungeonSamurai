using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleEnemy : BaseEnemy
{
    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		HealthBar.health -= 10.0f;
	}
}
