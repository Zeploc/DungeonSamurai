using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleEnemy : BaseEnemy
{

    // Use this for initialization
    void Start ()
    {
        base.Start();
    }
	
	// Update is called once per frame
	void Update ()
    {
        base.Update();

        CheckRef();
    }

    public override void GenerateQTEAttacks()
    {
        CheckRef();
        Debug.Log("Derived Attacks");
        if (!QTEManagerRef) Debug.Log("Bad qtemang");
        for (int i = 0; i < iAttackCount; i++)
        {
            QTEType = Random.Range(0, 4);
            if (QTEType == 0)
            {
                QTEManagerRef.AddQTEToQueue("LeftJoystickLeft", 1, "LLeft", 1, 1, true, PlayerRef.LeftDodge);
            }
            if (QTEType == 1)
            {
                QTEManagerRef.AddQTEToQueue("LeftJoystickRight", 1, "LRight", 2, 0, true, PlayerRef.RightDodge);
            }
            if (QTEType == 2)
            {
                QTEManagerRef.AddQTEToQueue("RightJoystickLeft", 1, "RLeft", 3, 0, true, PlayerRef.LeftParray);
            }
            if (QTEType == 3)
            {
                QTEManagerRef.AddQTEToQueue("RightJoystickRight", 1, "RRight", 2, 0, true, PlayerRef.RightParray);
            }
        }
        
    }
}
