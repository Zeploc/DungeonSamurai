using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour {

    [SerializeField] BaseEnemy[] InitialEnemyPrefabs; // Prefabs

    Queue<BaseEnemy> Enemies;
	// Use this for initialization
	void Start ()
    {
        Enemies = new Queue<BaseEnemy>();
        foreach (BaseEnemy E in InitialEnemyPrefabs)
        {
            Enemies.Enqueue(E);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void AdvanceLevel()
	{
		Debug.Log ("Loaded");
        FindObjectOfType<GameController>().ReloadLevel();
        FindObjectOfType<GameController>().SetNewEnemey(Enemies.Dequeue().gameObject);
	}

}
