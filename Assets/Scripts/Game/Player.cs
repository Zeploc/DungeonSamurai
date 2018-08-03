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
        if (Input.GetKeyDown(KeyCode.R))
        {
            FindObjectOfType<QTEManager>().CreateQTE("Fire1",1, new Vector3(),"right");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            FindObjectOfType<QTEManager>().CreateQTE("Fire2", 1, new Vector3(), "left");
        }
    }
	public void MoveToDoor()
	{
		bMoveToDoor = true;
	}
}
