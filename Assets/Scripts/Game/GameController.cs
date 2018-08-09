using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.PostProcessing;
public class GameController : MonoBehaviour {

    public Player PlayerRef;
    public BaseEnemy InitialEnemey; // This should be base enemy //
    public Camera CamereRef;
    public QTEManager QTEManagerRef;
    public AudioManager AudioManagerRef;
    public Image EnemyHealthbar;
    public EndScreen EndScreenRef;
    public GameObject ModeText;
    public GameObject EndCameraPosition;
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
        ModeText.SetActive(false);
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
            //Debug.Log("Not attacking, so following");
            Vector3 CameraPosition = CamereRef.gameObject.transform.position;
            CameraPosition.x = PlayerRef.transform.position.x + XOffset;
            if (CameraPosition.x > EndCameraPosition.transform.position.x)
                CameraPosition.x = EndCameraPosition.transform.position.x;
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

        float TimePercentage = TimeTillBombu / MaxTime;
        CountdownBar.fillAmount = TimePercentage * 0.92f + 0.04f;
        TimePercentage = 1.0f - (TimePercentage);
        TimePercentage *= 0.16f;
        TimePercentage += 0.48f;
        VignetteModel.Settings VinSettings = CamereRef.GetComponent<PostProcessingBehaviour>().profile.vignette.settings;
        VinSettings.intensity = TimePercentage;
        CamereRef.GetComponent<PostProcessingBehaviour>().profile.vignette.settings = VinSettings;
    }
	void FixedUpdate()
	{
		
	}

    public bool GetIsPlaying()
    {
        return isPlaying;
    }

    public void GameComplete()
    {
        EndScreenRef.ShowEndScren("Safely arrived at bonkuro");
        QTEManagerRef.ClearQTEs();
        isPlaying = false;
        if (EndScreenRef.gameObject.activeSelf) Debug.Log("End Screen Active!");
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
