using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class GameController : MonoBehaviour {

    public Player PlayerRef;
	public BaseEnemy InitialEnemey; // This should be base enemy //
    public Camera CamereRef;
    public QTEManager QTEManagerRef;
    public Image EnemyHealthbar;

    public float TimeTillBombu;
    public float MaxTime = 500.0f;
    public float DecreaseSpeed = 5.0f;
	[SerializeField] Image CountdownBar;
	bool isPlaying;
    // Use this for initialization
    void Start ()
    {
		isPlaying = true;
	
        SetNewEnemey(InitialEnemey.gameObject);
        TimeTillBombu = MaxTime;
    }

	// Update is called once per frame
	void Update () {
		


		if (Input.GetKeyDown(KeyCode.G))
		{
			//if(InitialEnemey != null)
            InitialEnemey.GetComponent<BaseEnemy> ().TakeDamage (10);
		}
        TimeTillBombu -= Time.deltaTime * DecreaseSpeed;
        CountdownBar.fillAmount = (TimeTillBombu / MaxTime) * 0.92f + 0.04f;
    }
	void FixedUpdate()
	{
		
	}

    public Player GetPlayer()
    {
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
