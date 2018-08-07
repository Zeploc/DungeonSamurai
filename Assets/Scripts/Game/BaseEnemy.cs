using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour {

    public float InitialHealth = 100.0f;
    protected float Health = 100.0f;
    public float QTETimer;
    public int QTEType;
    public int iAttackCount;
    
    GameController GameControllerRef;
    protected QTEManager QTEManagerRef;
    protected Player PlayerRef;

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
        PlayerRef = GameControllerRef.PlayerRef;
    }
	
	// Update is called once per frame
	protected void Update ()
    {
        CheckRef();
    }
    

    public virtual void GenerateQTEAttacks()
    {
        Debug.Log("Base Attacks");
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

    public void CheckRef()
    {
        if (QTEManagerRef)
            Debug.Log("Is valid");
        else
            Debug.Log("not valid fuck");
    }
}
