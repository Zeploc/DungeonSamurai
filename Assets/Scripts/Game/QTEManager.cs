using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEManager : MonoBehaviour {

    public Queue<GameObject> CurrentQTEs;
    public float timer = 1.5f;
    public WomboCombo WomboComboRef;
   [SerializeField]
    public GameObject QTEInstancePrefab;
    bool EnemyTurn = false;
    public BaseEnemy CurrentEnemyRef; // I'm not sure how to make this dynamic

    Player PlayerRef;

	// Use this for initialization
	void Start ()
    {
		FindObjectOfType<AudioManager>().StopSound("bgMusic");
		FindObjectOfType<AudioManager>().PlaySound("BattleMusic");
        CurrentQTEs = new Queue<GameObject>();
        PlayerRef = FindObjectOfType<GameController>().PlayerRef;
        WomboComboRef = PlayerRef.GetComponent<WomboCombo>();
    }
	
	// Update is called once per frame
	void Update ()
    {      
		if (PlayerRef.bMoveTowardsObject == false && WomboComboRef.DoingACombo == false) 
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
        else
        {

            Debug.Log("You've done it");
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

    public GameObject CreateQTE(string Button, float damage, string Text, int PlayerPose, int EnemyPose, bool EnemyAttack, GameObject ObjectPosition, Vector2 Offset = default(Vector2))
    {
        GameObject NewQTE = Instantiate(QTEInstancePrefab, transform);
        NewQTE.transform.position = ObjectPosition.transform.position;
        NewQTE.GetComponent<QTEInstance>().SetQTEInit(Button, damage, Text, PlayerPose, EnemyPose, EnemyAttack, ObjectPosition, Offset);
        return NewQTE;
    }

    public void AddQTEToQueue(string Button, float damage, string Text, int PlayerPose, int EnemyPose, bool EnemyAttack, GameObject ObjectPosition, Vector2 Offset = default(Vector2))
    {
        GameObject NewQTE = Instantiate(QTEInstancePrefab, transform);
        NewQTE.transform.position = ObjectPosition.transform.position; 
        NewQTE.GetComponent<QTEInstance>().SetQTEInit(Button, damage, Text, PlayerPose, EnemyPose, EnemyAttack, ObjectPosition,Offset);
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
}
