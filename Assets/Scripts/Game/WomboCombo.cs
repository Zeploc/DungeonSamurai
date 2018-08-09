using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WomboCombo : MonoBehaviour {

    List<GameObject> ComboForWombo;
    public int ComboCount = 5;
    int QTEType;
    bool DoingACombo = false;
    QTEManager QTEManagerRef;
    public BaseEnemy CurrentEnemy;
    // Use this for initialization
    void Start ()
    {
        QTEManagerRef = FindObjectOfType<GameController>().QTEManagerRef;
        ComboForWombo = new List<GameObject>();
        
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public bool IsDoingCombo()
    {
        return DoingACombo;
    }

    public void StartCombo()
    {
        DoingACombo = true;
        QTEManagerRef.timer = 0;
    }

    public void StopCombo()
    {
        DoingACombo = false;
    }

    public void GenerateWombo()
    {
        Vector2 LocationVector;
        for (int i = 0; i < ComboCount; i++)
        {
            LocationVector = new Vector2(i, 0);
            QTEType = Random.Range(0, 8);
            if (QTEType == 0)
            {
                ComboForWombo.Add(QTEManagerRef.CreateQTE("AButton", 1, 1, false, CurrentEnemy.ComboDisplayLocation, LocationVector));
            }
            if (QTEType == 1)
            {
                ComboForWombo.Add(QTEManagerRef.CreateQTE("BButton", 1, 1, false, CurrentEnemy.ComboDisplayLocation, LocationVector));
            }
            if (QTEType == 2)
            {
                ComboForWombo.Add(QTEManagerRef.CreateQTE("XButton", 1, 1, false, CurrentEnemy.ComboDisplayLocation, LocationVector));
            }
            if (QTEType == 3)
            {
                ComboForWombo.Add(QTEManagerRef.CreateQTE("YButton", 1, 1, false, CurrentEnemy.ComboDisplayLocation, LocationVector));
            }
            if (QTEType == 4)
            {
                ComboForWombo.Add(QTEManagerRef.CreateQTE("RightBumper", 1, 1, false, CurrentEnemy.ComboDisplayLocation, LocationVector));
            }
            if (QTEType == 5)
            {
                ComboForWombo.Add(QTEManagerRef.CreateQTE("LeftBumper", 1, 1, false, CurrentEnemy.ComboDisplayLocation, LocationVector));
            }
            if (QTEType == 6)
            {
                ComboForWombo.Add(QTEManagerRef.CreateQTE("LeftTrigger", 1, 1, false, CurrentEnemy.ComboDisplayLocation, LocationVector));
            }
            if (QTEType == 7)
            {
                ComboForWombo.Add(QTEManagerRef.CreateQTE("RightTrigger", 1, 1, false, CurrentEnemy.ComboDisplayLocation, LocationVector));
            }
        }

        foreach (GameObject QTE in ComboForWombo)
        {
            QTE.SetActive(true);
        }
        foreach (GameObject QTE in ComboForWombo)
        {
            Debug.Log(QTE.activeSelf);
        }
    }

    public void SendComboToManager()
    {
        //DoingACombo = true;
        for (int i = 0; i < ComboCount; i++)
        {
            QTEManagerRef.CurrentQTEs.Enqueue(ComboForWombo[i]);
        }
        ComboForWombo.Clear();
    }
}
