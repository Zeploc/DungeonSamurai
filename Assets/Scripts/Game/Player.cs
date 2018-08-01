using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	//game object for door
	GameObject Door;
	private bool bMoveToDoor = false;
	[SerializeField] float speed = 4.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (bMoveToDoor == true) {
			transform.position = Vector2.MoveTowards (transform.position, Door.transform.position, speed * Time.deltaTime);
			if (Vector2.Distance (transform.position, Door.transform.position) <= 2.0f) {
				Door.GetComponent<NextLevel> ().AdvanceLevel ();
				bMoveToDoor = false;
			}
		}
	}
	public void MoveToDoor()
	{
		bMoveToDoor = true;
	}
}
