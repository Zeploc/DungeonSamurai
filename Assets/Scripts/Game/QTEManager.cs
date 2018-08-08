using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEManager : MonoBehaviour {

    Queue<GameObject> CurrentQTEs;
    public float timer = 1.5f;
   [SerializeField]
    GameObject QTEInstancePrefab;
    bool EnemyTurn = false;
    public BaseEnemy CurrentEnemyRef; // I'm not sure how to make this dynamic

    Player PlayerRef;

    // Input
    bool JoystickReset = true;

    // Use this for initialization
    void Start ()
    {
        CurrentQTEs = new Queue<GameObject>();
        PlayerRef = FindObjectOfType<GameController>().PlayerRef;
    }
	
	// Update is called once per frame
	void Update ()
    {
        CheckForQTEInput();
		if (PlayerRef.bMoveTowardsObject == false) 
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
				}
				else if (EnemyTurn == false)
				{
					Debug.Log("Spawned Player attacks");
					PlayerRef.GeneratePlayerQTEAttacks();
					EnemyTurn = true;
				}
			}       

			if (timer <= 0)
			{
				if (CurrentQTEs.Count > 0) ActivateQTE();
				timer = 1.5f;
			}

		}
       
    }

    //public void CreateQTE(string Button, float damage, Vector3 Position, string Text, int PlayerPose, int EnemyPose, bool EnemyAttack)
    //{
    //    if (!PlayerRef) Debug.Log("Can't find player");
    //    Position = Camera.main.WorldToScreenPoint(Position + (PlayerRef.transform.position * 2));
    //    //Position = Vector3.zero;
    //    Debug.Log("Position " + Position.ToString());
    //    GameObject NewQTE = Instantiate(QTEInstancePrefab, Position, Quaternion.identity);
    //    NewQTE.transform.SetParent(gameObject.transform, false);
    //    NewQTE.GetComponent<QTEInstance>().SetQTEInit(Button, damage, Text, PlayerPose, EnemyPose, EnemyAttack,Position);
    //    //CurrentQTEs.Enqueue(NewQTE);
    //}

    public void AddQTEToQueue(string Button, float damage, string Text, int PlayerPose, int EnemyPose, bool EnemyAttack, GameObject ObjectPosition)
    {
        GameObject NewQTE = Instantiate(QTEInstancePrefab, transform);
        NewQTE.transform.position = ObjectPosition.transform.position; 
        NewQTE.GetComponent<QTEInstance>().SetQTEInit(Button, damage, Text, PlayerPose, EnemyPose, EnemyAttack, ObjectPosition);
        CurrentQTEs.Enqueue(NewQTE);
        //Debug.Log("Added");
    }

    public void RemoveQTE(GameObject QTERef)
    {
        CurrentQTEs.Dequeue();
    }

    void ActivateQTE()
    {
        CurrentQTEs.Peek().SetActive(true);
    }

    public void ApplyNewEnemy(GameObject NewEnemey)
    {
        CurrentEnemyRef = NewEnemey.GetComponent<BaseEnemy>();
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
