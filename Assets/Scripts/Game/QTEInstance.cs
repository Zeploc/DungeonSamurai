using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTEInstance : MonoBehaviour {

    float CurrentTime = 0.0f;
    public float Timer = 1.0f;

    string QTEkey = "Fire1"; //Overridden

    QTEManager QTEManagerRef;
    GameObject DamageEnemy;
    Player PlayerRef;
    

    int iPlayerAnimVal = 0;
    int iEnemyAnimVal = 0;
    bool bEnemyAttack = false;
    
    Vector3 QTEPostition;


    // Use this for initialization
    void Start ()
    {
        QTEManagerRef = FindObjectOfType<QTEManager>();
        PlayerRef = FindObjectOfType<GameController>().GetPlayer();
        gameObject.SetActive(false);
        
    }

    public void SetQTEInit(string button, float damage, string Text, int PlayerAnimVal, int EnemyAnimVal, bool EnemyAttack, Vector3 Position)
    {
        QTEkey = button;
        gameObject.GetComponentInChildren<Text>().text = Text;
        iPlayerAnimVal = PlayerAnimVal;
        iEnemyAnimVal = EnemyAnimVal;
        bEnemyAttack = EnemyAttack;
        QTEPostition = Position;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentTime += Time.deltaTime;
        if (CurrentTime > Timer)
        {
            // Failed QTE
            // damage player [PlayerRef]
            QTEManagerRef.RemoveQTE(gameObject);
            Destroy(gameObject);
        }
        // QTE button pressed
        if (Input.GetButtonDown(QTEkey))
        {
            QTEComplete();
            QTEManagerRef.timer = 0.0f;
        }
    }

    void QTEComplete()
    {
        // QTE effect
        // QTE deal damage to [DamageEnemy]
        // Damage frame/anim player [PlayerRef] 
        PlayerRef.SetAttackPose(iPlayerAnimVal);

        // Remove QTE from manager:
        QTEManagerRef.RemoveQTE(gameObject);
        Destroy(gameObject);
    }
    public void ActivateQTE()
    {
        gameObject.SetActive(true);
    }
}
