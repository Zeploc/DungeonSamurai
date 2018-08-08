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
    public AudioManager AudioManagerRef;
    public Image EnemyHealthbar;
    public EndScreen EndScreenRef;
    [SerializeField] float XOffset;

    public float TimeTillBombu;
    public float MaxTime = 500.0f;
    public float DecreaseSpeed = 5.0f;
	[SerializeField] Image CountdownBar;
	bool isPlaying = false;

    // Use this for initialization
    void Start ()
    {
		isPlaying = true;
	
        SetNewEnemey(InitialEnemey.gameObject);
        TimeTillBombu = MaxTime;
        AudioManagerRef = FindObjectOfType<AudioManager>();
    }

	// Update is called once per frame
	void Update ()
    {
        if (!PlayerRef.IsMovingInAttack())
        {
            Debug.Log("Not attacking, so following");
            Vector3 CameraPosition = CamereRef.gameObject.transform.position;
            CameraPosition.x = PlayerRef.transform.position.x + XOffset;
            CamereRef.gameObject.transform.position = CameraPosition;
        }

		//if (Input.GetKeyDown(KeyCode.G))
		//{
		//	//if(InitialEnemey != null)
        //  InitialEnemey.GetComponent<BaseEnemy> ().TakeDamage (10);
		//}
        TimeTillBombu -= Time.deltaTime * DecreaseSpeed;
        if (TimeTillBombu <= 0)
        {
            // Bomb gone off
            EndScreenRef.ShowEndScren("Big boom you die");
            QTEManagerRef.ClearQTEs();
            isPlaying = false;
        }
        CountdownBar.fillAmount = (TimeTillBombu / MaxTime) * 0.92f + 0.04f;
    }
	void FixedUpdate()
	{
		
	}

    public bool GetIsPlaying()
    {
        return isPlaying;
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

    public void NextLevel()
    {
        PlayerRef.ReloadPlayer();
        QTEManagerRef.ClearQTEs();
    }

    public void DeductReviveTime()
    {
        TimeTillBombu -= 20.0f;
    }
}
