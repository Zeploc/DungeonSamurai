using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Player PlayerRef;
	public BaseEnemy InitialEnemey; // This should be base enemy //
    public Camera CamereRef;
    public QTEManager QTEManagerRef;
    public float TimeTillBombu = 1000.0f;
    // Use this for initialization
    void Start ()
    {
        SetNewEnemey(InitialEnemey.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.G))
		{
            InitialEnemey.GetComponent<BaseEnemy> ().TakeDamage (10);
		}
        TimeTillBombu -= Time.deltaTime;
    }

    public Player GetPlayer()
    {
		if (PlayerRef)
			Debug.Log ("Returning player");
		else
			Debug.Log ("Not valid");
        return PlayerRef;
    }

    public void SetNewEnemey(GameObject NewEnemy)
    {
        InitialEnemey = NewEnemy.GetComponent<BaseEnemy>();
        PlayerRef.SetEnemy(NewEnemy);
        QTEManagerRef.ApplyNewEnemy(NewEnemy);
    }

    public void ReloadLevel()
    {
        PlayerRef.ReloadPlayer();
    }
}
