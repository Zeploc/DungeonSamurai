using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour {

	private Scene CurrentScene;

	//Array of gameobjects (Enemy prefabs)
	// Use this for initialization
	void Start () {
		CurrentScene = SceneManager.GetActiveScene ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void AdvanceLevel()
	{
		SceneManager.LoadScene(CurrentScene.name);
		Debug.Log ("Loaded");
		//set new enemies
	}

}
