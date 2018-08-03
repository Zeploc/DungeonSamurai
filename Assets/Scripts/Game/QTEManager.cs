using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEManager : MonoBehaviour {

    List<GameObject> CurrentQTEs;

    [SerializeField]
    GameObject QTEInstancePrefab;

    Player PlayerRef;

	// Use this for initialization
	void Start ()
    {
        CurrentQTEs = new List<GameObject>();
        PlayerRef = FindObjectOfType<GameController>().PlayerRef;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void CreateQTE(string Button, float damage, Vector3 Position,string Text, int PlayerPose, int EnemyPose)
    {
        if (!PlayerRef) Debug.Log("Can't find player");
        Position = Camera.main.WorldToScreenPoint(Position + (PlayerRef.transform.position * 2));
        //Position = Vector3.zero;
        Debug.Log("Position " + Position.ToString());
        GameObject NewQTE = Instantiate(QTEInstancePrefab, Position, Quaternion.identity);
        NewQTE.transform.SetParent(gameObject.transform, false);
        NewQTE.GetComponent<QTEInstance>().SetQTEInit(Button, damage,Text,PlayerPose, EnemyPose);
        CurrentQTEs.Add(NewQTE);
    }
    

    public void RemoveQTE(GameObject QTERef)
    {
        CurrentQTEs.Remove(QTERef);
    }
}
