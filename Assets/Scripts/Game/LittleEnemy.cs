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

    // Use this for initialization
    void Start ()
    {
        QTEManagerRef = FindObjectOfType<QTEManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        QTETimer -= Time.deltaTime;
        QTEType = Random.Range(0, 4);
		if(QTETimer <= 0)
        {
            if(QTEType == 0)
            {
                QTEManagerRef.CreateQTE("Fire2", 1, new Vector3(50,50,0), "Left",1,0);
                QTETimer = 2.1f;
            }
           if(QTEType == 1)
            {
                QTEManagerRef.CreateQTE("Fire1", 1, new Vector3(50, 50, 0), "Right",2,0);
                QTETimer = 2.1f;
            }
            if (QTEType == 2)
            {
                QTEManagerRef.CreateQTE("Fire1", 1, new Vector3(50, 50, 0), "Right",3,0);
                QTETimer = 2.1f;
            }
            if (QTEType == 3)
            {
                QTEManagerRef.CreateQTE("Fire2", 1, LeftParry.position, "Left",2,0);
                QTETimer = 2.1f;
            }
        }
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		HealthBar.health -= 10.0f;
	}
}
