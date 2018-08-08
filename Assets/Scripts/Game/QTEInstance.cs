﻿using System.Collections;
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

    // References
    QTEManager QTEManagerRef;
    BaseEnemy DamageEnemy;
    Player PlayerRef;
    GameController GameControllerRef;


    // Animation values

    int iPlayerAnimVal = 0;
    int iEnemyAnimVal = 0;
    bool bEnemyAttack = false;
    
    GameObject QTEObjectPostition;
    Vector2 VecOffset;

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

        GameControllerRef = FindObjectOfType<GameController>();
        XStickCircleDistance = StickCircleEndPosition.position.x - StickCircle.transform.position.x;

    }

    public void SetQTEInit(string button, float damage, string Text, int PlayerAnimVal, int EnemyAnimVal, bool EnemyAttack, GameObject ObjectPosition, Vector2 Offset)
    {
        VecOffset = Offset;
        QTEkey = button;
        gameObject.GetComponentInChildren<Text>().text = Text;
        iPlayerAnimVal = PlayerAnimVal;
        iEnemyAnimVal = EnemyAnimVal;
        bEnemyAttack = EnemyAttack;
        QTEObjectPostition = ObjectPosition;
        transform.position = QTEObjectPostition.transform.position + (Vector3)VecOffset;
        SetImage();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = QTEObjectPostition.transform.position + (Vector3)VecOffset;

		if(QTEObjectPostition != null)
        transform.position = QTEObjectPostition.transform.position;
        CurrentTime += Time.deltaTime;
        if (CurrentTime > Timer)
        {
            // Failed QTE
            if (bEnemyAttack) QTEFailed();
            else QTEMissed();
        }
        
        // If QTE is Axis Input
        if (JoystickInput)
        {
            float LeftJoystickAxis = Input.GetAxis("LeftJoystickHorizontal");
            float RightJoystickAxis = Input.GetAxis("RightJoystickHorizontal");
            float LeftTriggerAxis = Mathf.Abs(Input.GetAxis("LeftTrigger"));
            float RightTriggerAxis = Mathf.Abs(Input.GetAxis("RightTrigger"));

            // Visual Element
            Vector2 NewPosition = StickCircleEndPosition.position;
            if (QTEManagerRef.GetJoystickReset())
            {
                if (QTEkey == "LeftJoystickLeft")
                    NewPosition.x -= XStickCircleDistance * (1.0f - Mathf.Abs(Mathf.Clamp(LeftJoystickAxis, -1, 0)));
                else if (QTEkey == "LeftJoystickRight")
                    NewPosition.x -= XStickCircleDistance * (1.0f - Mathf.Abs(Mathf.Clamp(LeftJoystickAxis, 0, 1)));
                else if (QTEkey == "RightJoystickLeft")
                    NewPosition.x -= XStickCircleDistance * (1.0f - Mathf.Abs(Mathf.Clamp(RightJoystickAxis, -1, 0)));
                else
                    NewPosition.x -= XStickCircleDistance * (1.0f - Mathf.Abs(Mathf.Clamp(RightJoystickAxis, 0, 1)));
            }
            else
            {
                NewPosition.x -= XStickCircleDistance;
            }

            StickCircle.transform.position = NewPosition;
        }        
    }

    public void InputPressed(string Input)
    {
        if (Input == QTEkey)
        {
            QTEComplete();
        }
        else
        {
            QTEFailed();
        }
        QTEManagerRef.timer = 0.0f;
    }

    void QTEFailed() //wrong key pressed
    {
        //Debug.Log("QTE Failed");
        // Loose time

        if (bEnemyAttack)
        {
			FindObjectOfType<AudioManager>().PlaySound("PlayerHurt");
            PlayerRef.SetDamagedPose(1);
            DamageEnemy.SetAttackPose(iEnemyAnimVal);
            //Debug.Log("Hurt");
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

    void QTEMissed() //Missed the button press
    {
        // Missed particle effect
        if (bEnemyAttack == true)
        {
            Debug.Log("Hurt");
			FindObjectOfType<AudioManager>().PlaySound("PlayerHurt");
            PlayerRef.DamagePlayer(5);

            PlayerRef.SetDamagedPose(1);
            DamageEnemy.SetAttackPose(iEnemyAnimVal);
        }
        else
        {
            GameControllerRef.TimeTillBombu -= 20.0f;
        }
        QTEManagerRef.RemoveQTE(gameObject);
        Destroy(gameObject);
    }

    void QTEComplete()
    {
        //Debug.Log("QTE Complete");
        StickCircle.transform.position = StickCircleEndPosition.position;
        // QTE effect
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
		if(gameObject != null)
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
            JoystickInput = true;
        }
        else if (QTEkey == "LeftBumper" || QTEkey == "RightBumper")
        {
            GetComponent<Image>().sprite = BumperImage;
        }
        else if (QTEkey == "LeftTrigger" || QTEkey == "RightTrigger")
        {
            GetComponent<Image>().sprite = TriggerImage;
            JoystickInput = true;
            GetComponent<RectTransform>().sizeDelta = new Vector2(50, 200);
            return;
            //GetComponent<RectTransform>() = NewRect;
        }
        else
        {
            GetComponent<Image>().sprite = ButtonImage;
        }
    }
}
