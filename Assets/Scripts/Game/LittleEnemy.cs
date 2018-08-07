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
        Debug.Log("Derived Attacks");
        for (int i = 0; i < iAttackCount; i++)
        {
            QTEType = Random.Range(0, 4);
            if (QTEType == 0)
            {
                QTEManagerRef.AddQTEToQueue("LeftJoystickLeft", 1, "LLeft", 1, 1, true, LeftSlap.position);
            }
            if (QTEType == 1)
            {
                QTEManagerRef.AddQTEToQueue("LeftJoystickRight", 1, "LRight", 2, 0, true, RightSlap.position);
            }
            if (QTEType == 2)
            {
                QTEManagerRef.AddQTEToQueue("RightJoystickLeft", 1, "RLeft", 3, 0, true, LeftAttack.position);
            }
            if (QTEType == 3)
            {
                QTEManagerRef.AddQTEToQueue("RightJoystickRight", 1, "RRight", 2, 0, true, RightAttack.position);
            }
        }
        
    }
	void OnTriggerEnter2D(Collider2D col)
	{
		HealthBar.health -= 10.0f;
	}
}
