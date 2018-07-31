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
        PlayerRef = FindObjectOfType<GameController>().GetPlayer();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void CreateQTE(string Button, float damage)
    {
        Transform NewTransform = transform;
        NewTransform.position = PlayerRef.transform.position;
        GameObject NewQTE = Instantiate(QTEInstancePrefab, NewTransform);
        NewQTE.GetComponent<QTEInstance>().SetQTEInit(Button, damage);
        CurrentQTEs.Add(NewQTE);
    }
    

    public void RemoveQTE(GameObject QTERef)
    {
        CurrentQTEs.Remove(QTERef);
    }
}
