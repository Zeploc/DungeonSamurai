using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    // Health
    [SerializeField] float maxHealth = 100.0f;
    float health = 100.0f;
    [SerializeField] Image Healthbar;

    // Movement
	public bool bMoveTowardsObject = false;
    GameObject MoveTowardsGameObject;
    [SerializeField] GameObject NextLevelPosition;
    [SerializeField] GameObject FallBackPos;
	[SerializeField] GameObject CurrentLevelPosition;
    [SerializeField] GameObject StabSlash;
    [SerializeField] GameObject UpSlash;
    [SerializeField] GameObject DownSlash;
    [SerializeField] GameObject BloodParticles;

    [SerializeField] GameObject DodgePosition;
    public float TotalReviveTime = 1.0f;
    float ReviveTime = 0.0f;

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
    float MoveBackSpeed = 2.0f;

    // Use this for initialization
    void Start ()
    {
        WomboComboRef = GetComponent<WomboCombo>();
        QTEManagerRef = FindObjectOfType<QTEManager>();
        health = maxHealth;
        SetHealthBar();
		MoveTowardsGameObject = CurrentLevelPosition;
        MoveToTargetObject();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (ReviveTime > 0)
        {
            ReviveTime -= Time.deltaTime;
            if (ReviveTime <= 0)
            {
                MoveTowardsGameObject = CurrentLevelPosition;
                health = maxHealth;
                MoveToTargetObject();
                SetHealthBar();
                ReviveTime = 0;
                GetComponent<Animator>().SetBool("Recover", false);
            }
        }
        if (bMoveTowardsObject)
        {
            Vector3 NewPosition = MoveTowardsGameObject.transform.position;
            NewPosition.y = transform.position.y;
            transform.position = Vector2.MoveTowards(transform.position, NewPosition, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, NewPosition) <= 0.2f)
            {
                Debug.Log("Completed");
                if (MoveTowardsGameObject.GetComponent<NextLevel>())
                {
                    if (MoveTowardsGameObject != CurrentLevelPosition)
                        NextLevelPosition.GetComponent<NextLevel>().AdvanceLevel();
                    else
                    {
                        FindObjectOfType<GameController>().EnemyHealthbar.transform.parent.gameObject.SetActive(true);
                        FindObjectOfType<GameController>().EnemyHealthbar.fillAmount = 1.0f;
                        bMoveTowardsObject = false;
                        MoveTowardsGameObject = NextLevelPosition;
                        transform.position = NewPosition;
                        GetComponent<Animator>().SetBool("Walk", false);
                    }
                }
                else if (MoveTowardsGameObject.GetComponent<EndOfGame>())
                {
                    Debug.Log("Arrived at bunker");
                    MoveTowardsGameObject.GetComponent<EndOfGame>().GameComplete();
                    bMoveTowardsObject = false;
                    GetComponent<Animator>().SetBool("Walk", false);

                }
                else if (MoveTowardsGameObject == FallBackPos)
                {
                    FindObjectOfType<GameController>().DeductReviveTime();
                    ReviveTime = TotalReviveTime;
                    GetComponent<Animator>().SetInteger("DamagePose", 0);
                    bMoveTowardsObject = false;
                    GetComponent<Animator>().SetBool("Walk", false);
                    GetComponent<Animator>().SetBool("Recover", true);
                }
                else
				{
					bMoveTowardsObject = false;
					MoveTowardsGameObject = NextLevelPosition;
                    transform.position = NewPosition;
                    GetComponent<Animator>().SetBool("Walk", false);
                }
			}
		}
        else if (bMoveBack)
        { 
            Vector3 NewPosition = CurrentLevelPosition.transform.position;
            NewPosition.y = transform.position.y;
            transform.position = Vector2.MoveTowards(transform.position, NewPosition, speed * Time.deltaTime);
                        
            if (Vector2.Distance(transform.position, NewPosition) <= 0.2f)
            {
                bMoveBack = false;
                transform.position = NewPosition;
            }
        }

        if(Input.GetKeyDown(KeyCode.G))
        {
            WomboComboRef.StartCombo();
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
            bMoveBack = false;
        }        
    }

	public void MoveToTargetObject()
    {
        bMoveTowardsObject = true;
        GetComponent<Animator>().SetBool("Walk", true);
        if (MoveTowardsGameObject.GetComponent<EndOfGame>())
        {
            QTEManagerRef.ClearQTEs();
            FindObjectOfType<BunkerScript>().OpenBunker();
        }
    }

    public bool IsMovingInAttack()
    {
        return bMoveBack;
    }

    public void SetEnemy(GameObject NewEnemy)
    {
        Enemy = NewEnemy.GetComponent<BaseEnemy>();
        WomboComboRef.CurrentEnemy = NewEnemy.GetComponent<BaseEnemy>();
    }

    public void SetAttackPose(int NewAttackPose, bool WithBlood = default(bool))
    {
        StabSlash.SetActive(false);
        UpSlash.SetActive(false);
        DownSlash.SetActive(false);
        if (WithBlood)
        {
            BloodParticles.GetComponent<ParticleSystem>().Stop();
            BloodParticles.GetComponent<ParticleSystem>().Play();
            if (NewAttackPose == 1) DownSlash.SetActive(true);
            else if (NewAttackPose == 2) UpSlash.SetActive(true);
            else if (NewAttackPose == 3) StabSlash.SetActive(true);
        }
        GetComponent<Animator>().SetInteger("AttackPose", NewAttackPose);
        
        if (NewAttackPose != 0)
        {

            ActionTimer = 0.0f;
            bActionPose = true;
            Vector3 Position = Enemy.transform.position;
            Position.x -= 3.0f;
            Position.y = transform.position.y;
            transform.position = Position;
            bMoveBack = true;
            return;
        }
        Vector3 NewPosition = CurrentLevelPosition.transform.position;
        NewPosition.y = transform.position.y;
        transform.position = NewPosition;
    }

    public void SetDeffensePose(int NewDefensePose)
    {
        GetComponent<Animator>().SetInteger("DefensePose", NewDefensePose);
        if (NewDefensePose != 0)
        {
            if (NewDefensePose == 3)
            {
                Vector3 NewPosition = DodgePosition.transform.position;
                NewPosition.y = transform.position.y;
                transform.position = NewPosition;
                bMoveBack = true;
            }
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
                QTEManagerRef.AddQTEToQueue("LeftTrigger", 1, 1, false, Enemy.LeftSlap, 1.0f);
            }
            if (QTEType == 1)
            {
                QTEManagerRef.AddQTEToQueue("RightTrigger", 2, 2, false, Enemy.RightSlap, 1.0f);
            }
            if (QTEType == 2)
            {
                QTEManagerRef.AddQTEToQueue("LeftBumper", 3, 3, false, Enemy.LeftAttack, 1.0f);
            }
            if (QTEType == 3)
            {
                QTEManagerRef.AddQTEToQueue("RightBumper", 2, 2, false, Enemy.RightAttack, 1.0f);
            }
        }
    }

    public void ReloadPlayer()
    {
        //health = maxHealth;
        //SetHealthBar();
		MoveTowardsGameObject = NextLevelPosition;
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
            MoveTowardsGameObject = FallBackPos;
            bMoveTowardsObject = true;
            GetComponent<Animator>().SetInteger("DamagePose", 0);
            GetComponent<Animator>().SetBool("Walk", true);
            QTEManagerRef.ClearQTEs();
        }
        SetHealthBar();
    }

    public void SetNextLevel(GameObject _FallBackPosition, GameObject _CurrentLevelPosition, GameObject _NextLevelPosition)
    {
        FallBackPos = _FallBackPosition;
        CurrentLevelPosition = _CurrentLevelPosition;
        NextLevelPosition = _NextLevelPosition;
        bMoveBack = false;
    }
}
