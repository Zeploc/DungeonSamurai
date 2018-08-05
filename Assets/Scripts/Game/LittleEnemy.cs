using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleEnemy : BaseEnemy
{
    public float QTETimer;
    public Transform LeftDodge;
    public Transform RightDodge;
    public Transform LeftParry;
    public Transform RightParry;
    public int QTEType;
    QTEManager QTEManagerRef;
    public int iAttackCount;

    // Use this for initialization
    void Start ()
    {
        QTEManagerRef = FindObjectOfType<QTEManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
       
		
	}

    public void GenerateQTEAttacks()
    {
        for (int i = 0; i < iAttackCount; i++)
        {
            QTEType = Random.Range(0, 4);
            if (QTEType == 0)
            {
                QTEManagerRef.AddQTEToQueue("Fire2", 1, "Left", 1, 1, true, LeftDodge.position);
            }
            if (QTEType == 1)
            {
                QTEManagerRef.AddQTEToQueue("Fire1", 1, "Right", 2, 0, true, RightDodge.position);
            }
            if (QTEType == 2)
            {
                QTEManagerRef.AddQTEToQueue("Fire2", 1, "Left", 3, 0, true, LeftParry.position);
            }
            if (QTEType == 3)
            {
                QTEManagerRef.AddQTEToQueue("Fire1", 1, "Right", 2, 0, true, RightParry.position);
            }
        }
        
    }
	void OnTriggerEnter2D(Collider2D col)
	{
		HealthBar.health -= 10.0f;
	}
}
