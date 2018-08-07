using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WomboCombo : MonoBehaviour {

    Queue<string> ComboForWombo;
    int ComboCount = 5;
    int QTEType;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void GenerateWombo()
    {
        for (int i = 0; i < ComboCount; i++)
        {
            //QTEType = Random.Range(0, 4);
            //if (QTEType == 0)
            //{
            //    QTEManagerRef.AddQTEToQueue("LeftJoystickLeft", 1, "LLeft", 1, 1, true, PlayerRef.LeftDodge);
            //}
            //if (QTEType == 1)
            //{
            //    QTEManagerRef.AddQTEToQueue("LeftJoystickRight", 1, "LRight", 2, 0, true, PlayerRef.RightDodge);
            //}
            //if (QTEType == 2)
            //{
            //    QTEManagerRef.AddQTEToQueue("RightJoystickLeft", 1, "RLeft", 3, 0, true, PlayerRef.LeftParray);
            //}
            //if (QTEType == 3)
            //{
            //    QTEManagerRef.AddQTEToQueue("RightJoystickRight", 1, "RRight", 2, 0, true, PlayerRef.RightParray);
            //}
        }
    }
}
