using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTEManager : MonoBehaviour {

    public Queue<GameObject> CurrentQTEs;
    public float timer = 1.5f;
    public WomboCombo WomboComboRef;
   [SerializeField]
    public GameObject QTEInstancePrefab;
    bool EnemyTurn = false;
    public BaseEnemy CurrentEnemyRef; // I'm not sure how to make this dynamic

    Player PlayerRef;
    GameController GameControllerRef;

    public float ModeTextTime = 2.0f;
    float CurrentModeTextTime = 0.0f;

    // Input
    bool JoystickReset = true;

    // Use this for initialization
    void Start ()
    {
        CurrentQTEs = new Queue<GameObject>();
        GameControllerRef = FindObjectOfType<GameController>();
        PlayerRef = GameControllerRef.PlayerRef;
        WomboComboRef = PlayerRef.GetComponent<WomboCombo>();

        GameControllerRef.AudioManagerRef.StopSound("bgMusic");
        GameControllerRef.AudioManagerRef.PlaySound("BattleMusic");
    }
	
	// Update is called once per frame
	void Update ()
    {
        CheckForQTEInput();

        CurrentModeTextTime -= Time.deltaTime;
        if (CurrentModeTextTime <= 0)
        {
            GameControllerRef.ModeText.SetActive(false);
        }

        if (PlayerRef.bMoveTowardsObject == false && GameControllerRef.GetIsPlaying()) 
		{
			//Debug.Log(CurrentQTEs.Count);
			timer -= Time.deltaTime;
			if (CurrentQTEs.Count == 0)
			{         
				if (EnemyTurn)
				{
					Debug.Log("Spawned enemy attacks");

					CurrentEnemyRef.GenerateQTEAttacks();
					EnemyTurn = false;
                    GameControllerRef.ModeText.SetActive(true);
                    GameControllerRef.ModeText.GetComponent<Text>().text = "DEFEND";
                }
				else if (EnemyTurn == false)
				{
					
					Debug.Log("Spawned Player attacks");
					PlayerRef.GeneratePlayerQTEAttacks();
					EnemyTurn = true;
                    GameControllerRef.ModeText.SetActive(true);
                    GameControllerRef.ModeText.GetComponent<Text>().text = "ATTACK";
                }
                CurrentModeTextTime = ModeTextTime;
            }        
            if (timer <= 0)
			{
				if (CurrentQTEs.Count > 0) ActivateQTE();
				timer = 1.5f;
                

            }
		}
        else if (WomboComboRef.DoingACombo)
        {
            //Debug.Log("You've done it");
            WomboComboRef.GenerateWombo();
            WomboComboRef.SendComboToManager();
            for (int i = 0; i < WomboComboRef.ComboCount; i++)
            {
                if (CurrentQTEs.Count >= 1)
                {
                    CurrentQTEs.Dequeue().SetActive(true);                 
                }

            }
            WomboComboRef.DoingACombo = false;
            timer = 5.0f; // sets the time until the combo ends and the normal QTE starts again
        }

    }

    public GameObject CreateQTE(string Button, int PlayerPose, int EnemyPose, bool EnemyAttack, GameObject ObjectPosition, Vector2 Offset = default(Vector2))
    {
        GameObject NewQTE = Instantiate(QTEInstancePrefab, transform);
        NewQTE.transform.position = ObjectPosition.transform.position + (Vector3)Offset;
        NewQTE.GetComponent<QTEInstance>().SetQTEInit(Button, PlayerPose, EnemyPose, EnemyAttack, ObjectPosition, Offset);
        return NewQTE;
    }

    public void AddQTEToQueue(string Button, int PlayerPose, int EnemyPose, bool EnemyAttack, GameObject ObjectPosition, Vector2 Offset = default(Vector2))
    {
        GameObject NewQTE = Instantiate(QTEInstancePrefab, transform);
        NewQTE.transform.position = ObjectPosition.transform.position + (Vector3)Offset; 
        NewQTE.GetComponent<QTEInstance>().SetQTEInit(Button, PlayerPose, EnemyPose, EnemyAttack, ObjectPosition,Offset);
        CurrentQTEs.Enqueue(NewQTE);
    }

    public void RemoveQTE(GameObject QTERef)
    {
        if (CurrentQTEs.Count > 0) CurrentQTEs.Dequeue();
    }

    void ActivateQTE()
    {
        CurrentQTEs.Peek().SetActive(true);
    }

    public void ClearQTEs()
    {
        foreach(GameObject QTE in CurrentQTEs)
        {
            Destroy(QTE);
        }
        CurrentQTEs.Clear();
        timer = 1.5f;
        EnemyTurn = false;
    }

    public void ApplyNewEnemy(GameObject NewEnemey)
    {
        CurrentEnemyRef = NewEnemey.GetComponent<BaseEnemy>();
    }

    public bool GetJoystickReset()
    {
        return JoystickReset;
    }

    void CheckForQTEInput()
    {
        float LeftJoystickAxis = Input.GetAxis("LeftJoystickHorizontal");
        float RightJoystickAxis = Input.GetAxis("RightJoystickHorizontal");
        float LeftTriggerAxis = Mathf.Abs(Input.GetAxis("LeftTrigger"));
        float RightTriggerAxis = Mathf.Abs(Input.GetAxis("RightTrigger"));
        bool LeftJoystickLeftSet = LeftJoystickAxis < -0.9f && JoystickReset;
        bool LeftJoystickRightSet = LeftJoystickAxis > 0.9f && JoystickReset;
        bool LeftTriggerSet = LeftTriggerAxis > 0.9f && JoystickReset;
        bool RightTriggerSet = RightTriggerAxis > 0.9f && JoystickReset;

        // Trigger Button Pressed
        if (LeftJoystickLeftSet || LeftJoystickRightSet || LeftTriggerSet || RightTriggerSet)
        {
            // Axis Button pressed so waits to be released before allowing as new input
            JoystickReset = false;

            if (LeftJoystickLeftSet)
            {
                CurrentQTEs.Peek().GetComponent<QTEInstance>().InputPressed("LeftJoystickLeft");
            }
            if (LeftJoystickRightSet)
            {
                CurrentQTEs.Peek().GetComponent<QTEInstance>().InputPressed("LeftJoystickRight");
            }
            if (LeftTriggerSet)
            {
                CurrentQTEs.Peek().GetComponent<QTEInstance>().InputPressed("LeftTrigger");
            }
            if (RightTriggerSet)
            {
                CurrentQTEs.Peek().GetComponent<QTEInstance>().InputPressed("RightTrigger");
            }            
        }
        else
        {
            if (LeftJoystickAxis > -0.9f && LeftJoystickAxis < 0.9f && LeftTriggerAxis < 0.9f && RightTriggerAxis < 0.9f) JoystickReset = true;
            if (Input.GetButtonDown("AButton"))
            {
                CurrentQTEs.Peek().GetComponent<QTEInstance>().InputPressed("AButton");
            }
            else if (Input.GetButtonDown("BButton"))
            {
                CurrentQTEs.Peek().GetComponent<QTEInstance>().InputPressed("BButton");
            }
            else if (Input.GetButtonDown("LeftBumper"))
            {
                CurrentQTEs.Peek().GetComponent<QTEInstance>().InputPressed("LeftBumper");
            }
            else if (Input.GetButtonDown("RightBumper"))
            {
                CurrentQTEs.Peek().GetComponent<QTEInstance>().InputPressed("RightBumper");
            }
        }
    }
}
