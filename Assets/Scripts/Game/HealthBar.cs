using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
	Image Healthbar;
	float maxHealth = 100.0f;
	public static float health;


	// Use this for initialization
	void Start () {
		Healthbar = GetComponent<Image> ();
		health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		Healthbar.fillAmount = health / maxHealth;
	}

}
