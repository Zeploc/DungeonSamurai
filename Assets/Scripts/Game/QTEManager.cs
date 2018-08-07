using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEManager : MonoBehaviour {

    Queue<GameObject> CurrentQTEs;
    public float timer = 1.5f;
   [SerializeField]
    GameObject QTEInstancePrefab;
    bool EnemyTurn = false;
    BaseEnemy CurrentEnemyRef; // I'm not sure how to make this dynamic

    Player PlayerRef;

	// Use this for initialization
	void Start ()
    {
        CurrentQTEs = new Queue<GameObject>();
        PlayerRef = FindObjectOfType<GameController>().PlayerRef;
    }
	
	// Update is called once per frame
	void Update ()
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

    public void AddQTEToQueue(string Button, float damage, string Text, int PlayerPose, int EnemyPose, bool EnemyAttack, Vector3 Position)
    {
        GameObject NewQTE = Instantiate(QTEInstancePrefab, transform);
        NewQTE.transform.position = Position; 
        NewQTE.GetComponent<QTEInstance>().SetQTEInit(Button, damage, Text, PlayerPose, EnemyPose, EnemyAttack, Position);
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
