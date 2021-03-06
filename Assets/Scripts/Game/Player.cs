﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    
    //game object for door
    GameObject Door;
	private bool bMoveToDoor = false;
    private int QTEType;
    [SerializeField] float speed = 4.0f;
    
    public float FullActionTimer = 0.5f;
    public int iAttackCount;
    float ActionTimer = 0.0f;
    bool bActionPose = false;

    QTEManager QTEManagerRef;

    public Transform LeftDodge;
    public Transform RightDodge;
    public Transform LeftParry;
    public Transform RightParry;

    // Use this for initialization
    void Start ()
    {
        QTEManagerRef = FindObjectOfType<QTEManager>();
    }
	
	// Update is called once per frame
	void Update () {
		if (bMoveToDoor == true) {
			transform.position = Vector2.MoveTowards (transform.position, Door.transform.position, speed * Time.deltaTime);
			if (Vector2.Distance (transform.position, Door.transform.position) <= 2.0f) {
				Door.GetComponent<NextLevel> ().AdvanceLevel ();
				bMoveToDoor = false;
			}
		}
        if (Input.GetKeyDown(KeyCode.R))
        {
           //FindObjectOfType<QTEManager>().CreateQTE("Fire1",1, new Vector3(),"right",0,0);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            //FindObjectOfType<QTEManager>().CreateQTE("Fire2", 1, new Vector3(), "left",0,0);
        }
        if (ActionTimer <= FullActionTimer)
        {
            ActionTimer += Time.deltaTime;
        }
        else if (bActionPose)
        {
            SetAttackPose(0);
            bActionPose = false;
        }
    }
	public void MoveToDoor()
	{
		bMoveToDoor = true;
	}

    public void SetAttackPose(int NewAttackPos)
    {
        GetComponent<Animator>().SetInteger("AttackPose", NewAttackPos);
        if (NewAttackPos != 0)
        {
            ActionTimer = 0.0f;
            bActionPose = true;
        }
    }
    public void GeneratePlayerQTEAttacks()
    {
        for (int i = 0; i < iAttackCount; i++)
        {
            QTEType = Random.Range(0, 4);
            if (QTEType == 0)
            {
                QTEManagerRef.AddQTEToQueue("Fire2", 1, "SLAP", 1, 1, true, LeftDodge.position);
            }
            if (QTEType == 1)
            {
                QTEManagerRef.AddQTEToQueue("Fire1", 1, "SLAP", 2, 0, true, RightDodge.position);
            }
            if (QTEType == 2)
            {
                QTEManagerRef.AddQTEToQueue("Fire2", 1, "ATT", 3, 0, true, LeftParry.position);
            }
            if (QTEType == 3)
            {
                QTEManagerRef.AddQTEToQueue("Fire1", 1, "ATT", 2, 0, true, RightParry.position);
            }
        }
    }
}
