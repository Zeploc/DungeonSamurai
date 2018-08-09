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
        
    }

    public override void GenerateQTEAttacks()
    {
        for (int i = 0; i < iAttackCount; i++)
        {
            QTEType = Random.Range(0, 4);
            if (QTEType == 0)
            {
                QTEManagerRef.AddQTEToQueue("LeftJoystickLeft", 3, 1, true, PlayerRef.LeftDodge, QTETimer);
            }
            if (QTEType == 1)
            {
                QTEManagerRef.AddQTEToQueue("LeftJoystickRight", 4, 2, true, PlayerRef.RightDodge, QTETimer);
            }
            if (QTEType == 2)
            {
                QTEManagerRef.AddQTEToQueue("AButton", 1, 3, true, PlayerRef.LeftParray, QTETimer);
            }
            if (QTEType == 3)
            {
                QTEManagerRef.AddQTEToQueue("BButton", 2, 1, true, PlayerRef.RightParray, QTETimer);
            }
        }
        
    }
}
