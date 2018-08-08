using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour {
    
    Player PlayerRef;
    [SerializeField] GameObject FallBackPosition;
    [SerializeField] GameObject NewNextLevelObject;
    [SerializeField] GameObject LevelEnemy;
    
	// Use this for initialization
	void Start ()
    {
        PlayerRef = FindObjectOfType<GameController>().PlayerRef;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
	public void AdvanceLevel()
	{
        GameController ControllerRef = FindObjectOfType<GameController>();
        ControllerRef.NextLevel();
        PlayerRef.SetNextLevel(FallBackPosition, gameObject, NewNextLevelObject);
        ControllerRef.SetNewEnemey(LevelEnemy);
    }

}
