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

    [HideInInspector] public float TimeTillBombu;
    [HideInInspector] public float MaxTime;
    public float DecreaseSpeed = 2.0f;
	[SerializeField] Image CountdownBar;
	bool isPlaying = false;
    float fSirenCount = 0.0f;
    // Use this for initialization
    void Start ()
    {
		isPlaying = true;
        TimeTillBombu = 0;
        MaxTime = 500.0f;
        SetNewEnemey(InitialEnemey.gameObject);
        TimeTillBombu = MaxTime;
        Debug.Log(MaxTime);
        AudioManagerRef = FindObjectOfType<AudioManager>();
        AudioManagerRef.PlaySound("Gunfire");
   
    }

	// Update is called once per frame
	void Update ()
    {
        if (fSirenCount == 30.0f)
        {
            AudioManagerRef.StopSound("Sirens");
            fSirenCount = 0.0f;
        }
        else if (fSirenCount == 0.0f)
        {
            AudioManagerRef.PlaySound("Sirens");
            fSirenCount ++;
        }
        else
        {
            fSirenCount ++;
        }
        
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
