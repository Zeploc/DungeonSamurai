using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    // Health
    [SerializeField] float maxHealth = 100.0f;
    float health = 100.0f;
    [SerializeField] Image Healthbar;

    // Door
    [SerializeField] GameObject Door;
	public bool bMoveTowardsObject = false;
	[SerializeField] GameObject OutSidePos;
	[SerializeField] GameObject StartPos;
	GameObject MoveTowardsGameObject;

    private int QTEType;
    [SerializeField] float speed = 4.0f;
    
    // Animation
    public float FullActionTimer = 0.5f;
    float ActionTimer = 0.0f;
    bool bActionPose = false;

    // Attack
    public int iAttackCount;
    public GameObject LeftDodge;
    public GameObject RightDodge;
    public GameObject LeftParray;
    public GameObject RightParray;

    QTEManager QTEManagerRef;
    BaseEnemy Enemy;

    WomboCombo WomboComboRef;

    bool bMoveBack = false;
    Vector3 InitialPosition;
    float MoveBackSpeed = 2.0f;

    // Use this for initialization
    void Start ()
    {
        WomboComboRef = GetComponent<WomboCombo>();
        QTEManagerRef = FindObjectOfType<QTEManager>();
        InitialPosition = transform.position;
        health = maxHealth;
        SetHealthBar();
		MoveTowardsGameObject = Door;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (bMoveTowardsObject == true)
        {
			transform.position = Vector2.MoveTowards (transform.position, MoveTowardsGameObject.transform.position, speed * Time.deltaTime);
			if (Vector2.Distance (transform.position, MoveTowardsGameObject.transform.position) <= 0.2f)
            {
				if (MoveTowardsGameObject.GetComponent<NextLevel> ()) {
					Door.GetComponent<NextLevel> ().AdvanceLevel ();
				}
				else
				{
					bMoveTowardsObject = false;
					MoveTowardsGameObject = Door;
				}
				transform.position = Vector2.MoveTowards(transform.position,  StartPos.transform.position, speed * Time.deltaTime);
			}
		}
        else if (bMoveBack)
        {
            transform.position = Vector2.MoveTowards(transform.position, InitialPosition, MoveBackSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, InitialPosition) <= 0.2f)
            {
				Debug.Log ("Next Level");
                bMoveBack = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.G))
        {
            WomboComboRef.DoingACombo = true;
        }
        if (ActionTimer <= FullActionTimer)
        {
            ActionTimer += Time.deltaTime;
        }
        else if (bActionPose)
        {
            SetAttackPose(0);
            SetDeffensePose(0);
            SetDamagedPose(0);
            bActionPose = false;
        }
    }

	public void MoveToDoor()
	{
		bMoveTowardsObject = true;
	}

    public void SetEnemy(GameObject NewEnemy)
    {
        Enemy = NewEnemy.GetComponent<BaseEnemy>();
        InitialPosition = transform.position;
        WomboComboRef.CurrentEnemy = NewEnemy.GetComponent<BaseEnemy>();
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
    public void SetDamagedPose(int NewDamagePose)
    {
        GetComponent<Animator>().SetInteger("DamagePose", NewDamagePose);
        if (NewDamagePose != 0)
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
                QTEManagerRef.AddQTEToQueue("LeftTrigger", 1, "LT", 1, 1, false, Enemy.LeftSlap);
            }
            if (QTEType == 1)
            {
                QTEManagerRef.AddQTEToQueue("RightTrigger", 1, "RT", 2, 2, false, Enemy.RightSlap);
            }
            if (QTEType == 2)
            {
                QTEManagerRef.AddQTEToQueue("LeftBumper", 1, "LB", 3, 1, false, Enemy.LeftAttack);
            }
            if (QTEType == 3)
            {
                QTEManagerRef.AddQTEToQueue("RightBumper", 1, "RB", 2, 2, false, Enemy.RightAttack);
            }
        }
    }

    public void ReloadPlayer()
    {
        health = maxHealth;
        SetHealthBar();
		transform.position = OutSidePos.transform.position;
		MoveTowardsGameObject = StartPos;
		bMoveBack = false;
    }

    public void SetHealthBar()
    {
        Healthbar.fillAmount = health / maxHealth;
    }

    public void DamagePlayer(int Damage)
    {
        health -= Damage;
        if (health <= 0)
        {
            Debug.Log("Help I've fallen and I can't get up");
        }
        SetHealthBar();
    }
}
