using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTEInstance : MonoBehaviour {

    // QTE Time
    float CurrentTime = 0.0f;
    public float Timer = 1.0f;

    // QTE Input
    string QTEkey = "Fire1"; //Overridden
    bool JoystickInput = false;
    bool JoystickReset = true;

    // References
    QTEManager QTEManagerRef;
    BaseEnemy DamageEnemy;
    Player PlayerRef;
    
    // Animation values
    int iPlayerAnimVal = 0;
    int iEnemyAnimVal = 0;
    bool bEnemyAttack = false;
    
    GameObject QTEObjectPostition;

    // QTE Images
    [SerializeField] Sprite BumperImage;
    [SerializeField] Sprite TriggerImage;
    [SerializeField] Sprite ButtonImage;
    [SerializeField] Sprite StickSlider;
    [SerializeField] Image StickCircle;
    float XStickCircleDistance;
    [SerializeField] Transform StickCircleEndPosition;
    [SerializeField] GameObject TextObject;

    // Use this for initialization
    void Start ()
    {
        QTEManagerRef = FindObjectOfType<QTEManager>();
        PlayerRef = FindObjectOfType<GameController>().GetPlayer();
        DamageEnemy = QTEManagerRef.CurrentEnemyRef;
        gameObject.SetActive(false);
        XStickCircleDistance = StickCircleEndPosition.position.x - StickCircle.transform.position.x;
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
        SetImage();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = QTEObjectPostition.transform.position;
        CurrentTime += Time.deltaTime;
        if (CurrentTime > Timer)
        {
            // Failed QTE
            if (bEnemyAttack) QTEFailed();
            else QTEMissed();
        }
        bool QTEPressed = false;
        float LeftJoystickAxis = Input.GetAxis("LeftJoystickHorizontal");
        float RightJoystickAxis = Input.GetAxis("RightJoystickHorizontal");
        if (JoystickInput)
        {
            if ((LeftJoystickAxis > 0.9f || LeftJoystickAxis < -0.9f || RightJoystickAxis > 0.9f || RightJoystickAxis < -0.9f) && JoystickReset)
            {
                // QTE stick moved
                if ((QTEkey == "LeftJoystickLeft" && LeftJoystickAxis < -0.9f) ||
                    (QTEkey == "LeftJoystickRight" && LeftJoystickAxis > 0.9f) ||
                    (QTEkey == "RightJoystickLeft" && RightJoystickAxis < -0.9f) ||
                    (QTEkey == "RightJoystickRight" && RightJoystickAxis > 0.9f))
                {
                    QTEPressed = true;
                    JoystickReset = false;
                }
                else
                {
                    QTEFailed();
                }
            }
            else if (LeftJoystickAxis == 0.0f && RightJoystickAxis == 0.0f)
            {
                JoystickReset = true;
            }
            Vector2 NewPosition = StickCircleEndPosition.position;
            if (QTEkey == "LeftJoystickLeft" || QTEkey == "LeftJoystickRight")
                NewPosition.x -= XStickCircleDistance * (1.0f - Mathf.Abs(LeftJoystickAxis));
            else
                NewPosition.x -= XStickCircleDistance * (1.0f - Mathf.Abs(RightJoystickAxis));
            StickCircle.transform.position = NewPosition;
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

        if (bEnemyAttack)
        {
            PlayerRef.SetDamagedPose(iPlayerAnimVal);
            DamageEnemy.SetAttackPose(iEnemyAnimVal);
            Debug.Log("Hurt");
            PlayerRef.DamagePlayer(5);
        }
        else
        {
            PlayerRef.SetAttackPose(iPlayerAnimVal);
            DamageEnemy.SetDeffensePose(iEnemyAnimVal);
        }

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

            PlayerRef.SetDamagedPose(iPlayerAnimVal);
            DamageEnemy.SetAttackPose(iEnemyAnimVal);
        }
        QTEManagerRef.RemoveQTE(gameObject);
        Destroy(gameObject);
    }

    void QTEComplete()
    {        
        StickCircle.transform.position = StickCircleEndPosition.position;
        // QTE effect
        // QTE deal damage to [DamageEnemy]
        // Damage frame/anim player [PlayerRef] 
        if (bEnemyAttack)
        {
            PlayerRef.SetDeffensePose(iPlayerAnimVal);
            DamageEnemy.SetAttackPose(iEnemyAnimVal);
        }
        else
        {
            PlayerRef.SetAttackPose(iPlayerAnimVal);
            DamageEnemy.SetDamagedPose(iEnemyAnimVal);
            DamageEnemy.TakeDamage(5);
        }

        // Remove QTE from manager:
        QTEManagerRef.RemoveQTE(gameObject);
        Destroy(gameObject);
    }
    public void ActivateQTE()
    {
        gameObject.SetActive(true);
    }

    void SetImage()
    {
        if (QTEkey == "LeftJoystickLeft" ||
            QTEkey == "LeftJoystickRight" ||
            QTEkey == "RightJoystickLeft" ||
            QTEkey == "RightJoystickRight")
        {
            if (QTEkey == "LeftJoystickLeft" || QTEkey == "RightJoystickLeft")
            {
                Vector3 Scale = transform.localScale;
                Scale.x = -1;
                transform.localScale = Scale;
                Scale = TextObject.transform.localScale;
                Scale.x = -1;
                TextObject.transform.localScale = Scale;
            }
            GetComponent<Image>().sprite = StickSlider;
            StickCircle.gameObject.SetActive(true);

        }
        else if (QTEkey == "LeftBumper" || QTEkey == "RightBumper")
        {
            GetComponent<Image>().sprite = BumperImage;
        }
        else if (QTEkey == "LeftTrigger" || QTEkey == "RightTrigger")
        {
            GetComponent<Image>().sprite = TriggerImage;
        }
        else
        {
            GetComponent<Image>().sprite = ButtonImage;
        }
    }
}
