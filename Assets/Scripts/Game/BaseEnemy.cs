using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour {

    public float InitialHealth = 100.0f;
    protected float Health = 100.0f;
    // attacks?

	// Use this for initialization
	void Start ()
    {
        Health = InitialHealth;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage(float Damage)
    {
        Health -= Damage;
        // Damage Effect
        // Damage frame/anim
    }
}
