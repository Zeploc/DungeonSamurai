using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WomboCombo : MonoBehaviour {

    Queue<GameObject> ComboForWombo;
    public int ComboCount = 5;
    int QTEType;
    public bool DoingACombo = false;
    QTEManager QTEManagerRef;
    BaseEnemy CurrentEnemy;
    // Use this for initialization
    void Start ()
    {
        QTEManagerRef = FindObjectOfType<QTEManager>();
        CurrentEnemy = QTEManagerRef.CurrentEnemyRef;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
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
                ComboForWombo.Enqueue(QTEManagerRef.CreateQTE("AButton", 1, "A", 1, 1, false, CurrentEnemy.ComboDisplayLocation, LocationVector));
            }
            if (QTEType == 1)
            {
                ComboForWombo.Enqueue(QTEManagerRef.CreateQTE("BButton", 1, "B", 1, 1, false, CurrentEnemy.ComboDisplayLocation, LocationVector));
            }
            if (QTEType == 2)
            {
                ComboForWombo.Enqueue(QTEManagerRef.CreateQTE("XButton", 1, "X", 1, 1, false, CurrentEnemy.ComboDisplayLocation, LocationVector));
            }
            if (QTEType == 3)
            {
                ComboForWombo.Enqueue(QTEManagerRef.CreateQTE("YButton", 1, "Y", 1, 1, false, CurrentEnemy.ComboDisplayLocation, LocationVector));
            }
            if (QTEType == 4)
            {
                ComboForWombo.Enqueue(QTEManagerRef.CreateQTE("RightBumper", 1, "RB", 1, 1, false, CurrentEnemy.ComboDisplayLocation, LocationVector));
            }
            if (QTEType == 5)
            {
                ComboForWombo.Enqueue(QTEManagerRef.CreateQTE("LeftBumper", 1, "LB", 1, 1, false, CurrentEnemy.ComboDisplayLocation, LocationVector));
            }
            if (QTEType == 6)
            {
                ComboForWombo.Enqueue(QTEManagerRef.CreateQTE("LeftTrigger", 1, "LT", 1, 1, false, CurrentEnemy.ComboDisplayLocation, LocationVector));
            }
            if (QTEType == 7)
            {
                ComboForWombo.Enqueue(QTEManagerRef.CreateQTE("RightTrigger", 1, "RT", 1, 1, false, CurrentEnemy.ComboDisplayLocation, LocationVector));
            }
        }
    }

    public void SendComboToManager()
    {
        //DoingACombo = true;
        QTEManagerRef.CurrentQTEs.Clear();
        QTEManagerRef.CurrentQTEs.Enqueue(ComboForWombo.Dequeue());
    }
}
