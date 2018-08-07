using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	Image Healthbar;
    Player PlayerRef;

	// Use this for initialization
	void Start () {
		Healthbar = GetComponent<Image> ();
        PlayerRef = FindObjectOfType<GameController>().PlayerRef;
	}
	
	// Update is called once per frame
	void Update () {
        Healthbar.fillAmount = PlayerRef.GetHealthPercentage();
	}

}
