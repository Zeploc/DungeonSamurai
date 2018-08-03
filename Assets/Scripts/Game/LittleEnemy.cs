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
    QTEManager QTEManagerRef;

    // Use this for initialization
    void Start ()
    {
        QTEManagerRef = FindObjectOfType<QTEManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        QTETimer -= Time.deltaTime;
        int QTEType = Random.Range(0, 5);
		if(QTETimer <= 0)
        {
            if(QTEType == 0)
            {
                QTEManagerRef.CreateQTE("Fire2", 1, LeftDodge.position, "Left");
                QTETimer = 2.1f;
            }
           if(QTEType == 1)
            {
                QTEManagerRef.CreateQTE("Fire1", 1, RightDodge.position, "Right");
                QTETimer = 2.1f;
            }
            if (QTEType == 1)
            {
                QTEManagerRef.CreateQTE("Fire1", 1, RightParry.position, "Right");
                QTETimer = 2.1f;
            }
            if (QTEType == 1)
            {
                QTEManagerRef.CreateQTE("Fire2", 1, LeftParry.position, "Left");
                QTETimer = 2.1f;
            }
        }
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		HealthBar.health -= 10.0f;
	}
}
