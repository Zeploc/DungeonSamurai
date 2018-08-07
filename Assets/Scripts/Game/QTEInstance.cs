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
            // damage player [PlayerRef]
            QTEManagerRef.RemoveQTE(gameObject);
            Destroy(gameObject);
        }
        bool QTEPressed = false;
        if (JoystickInput)
        {
            // QTE stick moved
            if ((QTEkey == "LeftJoystickLeft" && Input.GetAxis("LeftJoystickHorizontal") < -0.5f) ||
                (QTEkey == "LeftJoystickRight" && Input.GetAxis("LeftJoystickHorizontal") > 0.5f) ||
                (QTEkey == "RightJoystickLeft" && Input.GetAxis("RightJoystickHorizontal") < -0.5f) ||
                (QTEkey == "RightJoystickRight" && Input.GetAxis("RightJoystickHorizontal") > 0.5f))
            {
                QTEPressed = true;
            }
        }
        else
        {
            // QTE button pressed
            if (Input.GetButtonDown(QTEkey))
            {
                QTEPressed = true;
            }
        }

        if (QTEPressed)
        {
            QTEComplete();
            QTEManagerRef.timer = 0.0f;
        }
    }

    void QTEComplete()
    {
        // QTE effect
        // QTE deal damage to [DamageEnemy]
        DamageEnemy.TakeDamage(5);
        // Damage frame/anim player [PlayerRef] 
        if (bEnemyAttack) PlayerRef.SetDeffensePose(iPlayerAnimVal);
        else PlayerRef.SetAttackPose(iPlayerAnimVal);

        // Remove QTE from manager:
        QTEManagerRef.RemoveQTE(gameObject);
        Destroy(gameObject);
    }
    public void ActivateQTE()
    {
        gameObject.SetActive(true);
    }
}
