﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


    [SerializeField] float maxHealth = 100.0f;
    static float health;

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

    public GameObject LeftDodge;
    public GameObject RightDodge;
    public GameObject LeftParray;
    public GameObject RightParray;

    BaseEnemy Enemy;

    bool bMoveBack = false;
    Vector3 InitialPosition;
    float MoveBackSpeed = 2.0f;

    // Use this for initialization
    void Start ()
    {
        QTEManagerRef = FindObjectOfType<QTEManager>();
        InitialPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (bMoveToDoor == true)
        {
			transform.position = Vector2.MoveTowards (transform.position, Door.transform.position, speed * Time.deltaTime);
			if (Vector2.Distance (transform.position, Door.transform.position) <= 0.2f)
            {
				Door.GetComponent<NextLevel> ().AdvanceLevel ();
				bMoveToDoor = false;
			}
		}
        else if (bMoveBack)
        {
            transform.position = Vector2.MoveTowards(transform.position, InitialPosition, MoveBackSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, InitialPosition) <= 0.2f)
            {
                bMoveBack = false;
            }
        }

        if (ActionTimer <= FullActionTimer)
        {
            ActionTimer += Time.deltaTime;
        }
        else if (bActionPose)
        {
            SetAttackPose(0);
            SetDeffensePose(0);
            bActionPose = false;
        }
    }

	public void MoveToDoor()
	{
		bMoveToDoor = true;
	}

    public void SetEnemy(GameObject NewEnemy)
    {
        Enemy = NewEnemy.GetComponent<BaseEnemy>();
        InitialPosition = transform.position;
    }

    public void SetAttackPose(int NewAttackPose)
    {
        GetComponent<Animator>().SetInteger("AttackPose", NewAttackPose);
        if (NewAttackPose != 0)
        {
            ActionTimer = 0.0f;
            bActionPose = true;
            Vector3 Position = Enemy.transform.position;
            Position.x -= 3.0f;
            transform.position = Position;
            bMoveBack = true;
            return;
        }
        transform.position = InitialPosition;
    }
    public void SetDeffensePose(int NewDefensePose)
    {
        GetComponent<Animator>().SetInteger("DefensePose", NewDefensePose);
        if (NewDefensePose != 0)
        {
            ActionTimer = 0.0f;
            bActionPose = true;
            return;
        }

        //Vector3 Position = transform.position;
    }

    public void GeneratePlayerQTEAttacks()
    {
        for (int i = 0; i < iAttackCount; i++)
        {
            QTEType = Random.Range(0, 4);
            if (QTEType == 0)
            {
                QTEManagerRef.AddQTEToQueue("LeftBumper", 1, "LB", 1, 1, false, Enemy.LeftSlap);
            }
            if (QTEType == 1)
            {
                QTEManagerRef.AddQTEToQueue("RightBumper", 1, "RB", 2, 0, false, Enemy.RightSlap);
            }
            if (QTEType == 2)
            {
                QTEManagerRef.AddQTEToQueue("LeftBumper", 1, "LB", 3, 0, false, Enemy.LeftAttack);
            }
            if (QTEType == 3)
            {
                QTEManagerRef.AddQTEToQueue("RightBumper", 1, "RB", 2, 0, false, Enemy.RightAttack);
            }
        }
    }

    public void ReloadPlayer()
    {
        health = maxHealth;
    }

    public float GetHealthPercentage()
    {
        return health / maxHealth;
    }
}
