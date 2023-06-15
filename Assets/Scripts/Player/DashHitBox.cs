using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashHitBox : MonoBehaviour
{
    private PlayerController playerController;
    private DestructableObject attackTarget;
    private int currentDashForceLeft;

    private void Awake() {
        playerController = GameObject.FindObjectOfType<PlayerController>();
        currentDashForceLeft = playerController.DashForce;
        Debug.Log("currentDashForceLeft" + currentDashForceLeft);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "DestructableObject") {
            Debug.Log("destructable object: " + other.gameObject.name);
            attackTarget = other.gameObject.GetComponent<DestructableObject>();
            attackTarget.TakeDamage(currentDashForceLeft);
            //change needed
            currentDashForceLeft -= attackTarget.CurrentHp;
            Debug.Log("CurrentDashForceLeft: " + currentDashForceLeft);
        }
    }
    
}
