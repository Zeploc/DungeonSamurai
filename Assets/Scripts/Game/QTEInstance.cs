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
    Text TextObject;

	// Use this for initialization
	void Start ()
    {
        QTEManagerRef = FindObjectOfType<QTEManager>();
        PlayerRef = FindObjectOfType<GameController>().GetPlayer();
        
    }

    public void SetQTEInit(string button, float damage, string Text)
    {
        QTEkey = button;
        gameObject.GetComponentInChildren<Text>().text = Text;
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
        }
    }

    void QTEComplete()
    {
        // QTE effect
        // QTE deal damage to [DamageEnemy]
        // Damage frame/anim player [PlayerRef] 

        // Remove QTE from manager:
        QTEManagerRef.RemoveQTE(gameObject);
        Destroy(gameObject);
    }
}
