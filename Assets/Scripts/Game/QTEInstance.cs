using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTEInstance : MonoBehaviour {

    float CurrentTime = 0.0f;
    public float Timer = 1.0f;

    string QTEkey = "Fire1"; //Overridden
    bool JoystickInput = false;

    QTEManager QTEManagerRef;
    BaseEnemy DamageEnemy;
    Player PlayerRef;
    GameController GameControllerRef;

    int iPlayerAnimVal = 0;
    int iEnemyAnimVal = 0;
    bool bEnemyAttack = false;
    
    GameObject QTEObjectPostition;


    // Use this for initialization
    void Start ()
    {
        QTEManagerRef = FindObjectOfType<QTEManager>();
        PlayerRef = FindObjectOfType<GameController>().GetPlayer();
        DamageEnemy = QTEManagerRef.CurrentEnemyRef;
        gameObject.SetActive(false);
        GameControllerRef = FindObjectOfType<GameController>();


    }

    public void SetQTEInit(string button, float damage, string Text, int PlayerAnimVal, int EnemyAnimVal, bool EnemyAttack, GameObject ObjectPosition)
    {
        QTEkey = button;
        gameObject.GetComponentInChildren<Text>().text = Text;
        iPlayerAnimVal = PlayerAnimVal;
        iEnemyAnimVal = EnemyAnimVal;
        bEnemyAttack = EnemyAttack;
        QTEObjectPostition = ObjectPosition;
        if (QTEkey == "LeftJoystickLeft" ||
            QTEkey == "LeftJoystickRight" ||
            QTEkey == "RightJoystickLeft" ||
            QTEkey == "RightJoystickRight")
        {
            JoystickInput = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = QTEObjectPostition.transform.position;
        CurrentTime += Time.deltaTime;
        if (CurrentTime > Timer)
        {
            // Failed QTE
            QTEMissed();
        }
        bool QTEPressed = false;
        float LeftJoystickAxis = Input.GetAxis("LeftJoystickHorizontal");
        float RightJoystickAxis = Input.GetAxis("RightJoystickHorizontal");
        if (JoystickInput)
        {
            if (LeftJoystickAxis > 0.5 || LeftJoystickAxis < -0.5 || RightJoystickAxis > 0.5 || RightJoystickAxis < -0.5)
            {
                // QTE stick moved
                if ((QTEkey == "LeftJoystickLeft" && LeftJoystickAxis < -0.5f) ||
                    (QTEkey == "LeftJoystickRight" && LeftJoystickAxis > 0.5f) ||
                    (QTEkey == "RightJoystickLeft" && RightJoystickAxis < -0.5f) ||
                    (QTEkey == "RightJoystickRight" && RightJoystickAxis > 0.5f))
                {
                    QTEPressed = true;
                }
                else
                {
                    QTEFailed();
                }
            }
            
        }
        else
        {
            if (Input.anyKeyDown)
            {
                // QTE button pressed
                if (Input.GetButtonDown(QTEkey))
                {
                    QTEPressed = true;
                }
                else
                {
                    QTEFailed();
                }
            }
            
        }

        if (QTEPressed)
        {
            QTEComplete();
            QTEManagerRef.timer = 0.0f;
        }
    }

    void QTEFailed()
    {
        // Loose time

        PlayerRef.SetAttackPose(iPlayerAnimVal);
        DamageEnemy.SetDeffensePose(iPlayerAnimVal);

        QTEManagerRef.RemoveQTE(gameObject);
        Destroy(gameObject);
    }

    void QTEMissed()
    {
        // Missed particle effect
        if (bEnemyAttack == true)
        {
            Debug.Log("Hurt");
            PlayerRef.DamagePlayer(5);
        }
        else
        {
            GameControllerRef.TimeTillBombu -= 5.0f;
        }
        QTEManagerRef.RemoveQTE(gameObject);
        Destroy(gameObject);
    }

    void QTEComplete()
    {
        // QTE effect
        // QTE deal damage to [DamageEnemy]
        DamageEnemy.TakeDamage(5);
        // Damage frame/anim player [PlayerRef] 
        if (bEnemyAttack)
        {
            PlayerRef.SetDeffensePose(iPlayerAnimVal);
            DamageEnemy.SetAttackPose(iPlayerAnimVal);
        }
        else
        {
            PlayerRef.SetAttackPose(iPlayerAnimVal);
            DamageEnemy.SetDamagedPose(iPlayerAnimVal);
        }

        // Remove QTE from manager:
        QTEManagerRef.RemoveQTE(gameObject);
        Destroy(gameObject);
    }
    public void ActivateQTE()
    {
        gameObject.SetActive(true);
    }
}
