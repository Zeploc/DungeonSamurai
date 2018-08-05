using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour {

    public float InitialHealth = 100.0f;
    protected float Health = 100.0f;
    public float QTETimer;
    public int QTEType;
    public int iAttackCount;
    // attacks?
    GameController GameControllerRef;
    protected QTEManager QTEManagerRef;

    public Transform LeftSlap;
    public Transform RightSlap;
    public Transform LeftAttack;
    public Transform RightAttack;

    // Use this for initialization
    protected void Start ()
    {
		Health = InitialHealth;
		GameControllerRef = FindObjectOfType<GameController> ();
        QTEManagerRef = GameControllerRef.QTEManagerRef;
    }
	
	// Update is called once per frame
	protected void Update ()
    {
        if (QTEManagerRef)
            Debug.Log("Is valid");
        else
            Debug.Log("not valid fuck");
    }
    

    public void GenerateQTEAttacks()
    {
        if (!QTEManagerRef)
        {
            Debug.Log("Can't find qtemanager");
            return;
        }
        for (int i = 0; i < iAttackCount; i++)
        {
            QTEType = Random.Range(0, 4);
            if (QTEType == 0)
            {
                QTEManagerRef.AddQTEToQueue("Fire2", 1, "Left", 1, 1, true, LeftSlap.position);
            }
            if (QTEType == 1)
            {
                QTEManagerRef.AddQTEToQueue("Fire1", 1, "Right", 2, 0, true, RightSlap.position);
            }
            if (QTEType == 2)
            {
                QTEManagerRef.AddQTEToQueue("Fire2", 1, "Left", 3, 0, true, LeftAttack.position);
            }
            if (QTEType == 3)
            {
                QTEManagerRef.AddQTEToQueue("Fire1", 1, "Right", 2, 0, true, RightAttack.position);
            }
        }

    }

    public void TakeDamage(float Damage)
    {
        Health -= Damage;
		if (Health <= 0) {
			GameControllerRef.PlayerRef.MoveToDoor ();
		}
        // Damage Effect
        // Damage frame/anim
    }
}
