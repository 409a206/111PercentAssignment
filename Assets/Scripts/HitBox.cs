using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    private DestructableObject attackTarget;

    public DestructableObject AttackTarget{
        get{return attackTarget;}
        set{attackTarget = value;}
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "DestructableObject") {
            Debug.Log("destructable object: " + other.gameObject.name);
            attackTarget = other.gameObject.GetComponent<DestructableObject>();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        attackTarget = null;
    }
    
}