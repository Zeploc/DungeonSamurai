using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseEnemy : MonoBehaviour {

    // Attack / Health
    public float InitialHealth = 100.0f;
    protected float Health = 100.0f;
    public float QTETimer;
    public int QTEType;
    public int iAttackCount;
    public Image Healthbar;

    // Animations
    public float FullActionTimer = 0.5f;
    float ActionTimer = 0.0f;
    bool bActionPose = false;

    bool bMoveBack = false;
    Vector3 InitialPosition;
    float MoveBackSpeed = 2.0f;

    GameController GameControllerRef;
    protected QTEManager QTEManagerRef;
    protected Player PlayerRef;

    public GameObject LeftSlap;
    public GameObject RightSlap;
    public GameObject LeftAttack;
    public GameObject RightAttack;

    // Use this for initialization
    protected void Start ()
    {
		Health = InitialHealth;
		GameControllerRef = FindObjectOfType<GameController> ();
        QTEManagerRef = GameControllerRef.QTEManagerRef;
        PlayerRef = GameControllerRef.PlayerRef;
        InitialPosition = transform.position;
        Healthbar = GameControllerRef.EnemyHealthbar;
    }
	
	// Update is called once per frame
	protected void Update ()
    {
        if (bMoveBack)
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
            SetDamagedPose(0);
            bActionPose = false;
        }
    }
    public void SetAttackPose(int NewAttackPose)
    {
        GetComponent<Animator>().SetInteger("AttackPose", NewAttackPose);
        if (NewAttackPose != 0)
        {
            ActionTimer = 0.0f;
            bActionPose = true;
            Vector3 Position = PlayerRef.transform.position;
            Position.x += 3.0f;
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
        
    }

    public virtual void GenerateQTEAttacks()
    {
        Debug.Log("Base Attacks");
    }

    public void TakeDamage(float Damage)
    {
        Health -= Damage;
		if (Health <= 0)
        {
            Destroy(gameObject);
            GameControllerRef.PlayerRef.MoveToDoor ();
		}
        SetHealthBar();
        // Damage Effect
        // Damage frame/anim
    }

    public void CheckRef()
    {
        if (QTEManagerRef)
            Debug.Log("Is valid");
        else
            Debug.Log("not valid fuck");
    }

    public void SetHealthBar()
    {
        Healthbar.fillAmount = Health / InitialHealth;
    }
}
