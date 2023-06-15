using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour
{
    //component variables
    private DestructableObject _destructableObject;
    private Rigidbody2D _rb;
    private Collider2D _col;
    //시스템 조정 데이터
    [SerializeField]
    private int maxHp = 3;
    private int currentHp;
    public int CurrentHp{
        get{return currentHp;}
    }

    //shield에 대항하는 수치
    [SerializeField]
    private float resistence = 3.0f;

    //reference variables
    private GameManager gameManager;
    //derived variables
    float distToGround;
    private LayerMask groundLayer;
    //flag variables
    bool isBoomPlayed = false;

    void Awake()
    {
        _destructableObject = this.GetComponent<DestructableObject>();
        _rb = this.GetComponent<Rigidbody2D>();
        _col = this.GetComponent<Collider2D>();
        groundLayer = LayerMask.GetMask("Ground");
        //Debug.Log(groundLayer);

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        currentHp = maxHp;
        distToGround = _col.bounds.extents.y;
    }

    private void Update() {
        CheckIsGrounded();
    }

    private void CheckIsGrounded()
    {
        if(!isBoomPlayed && IsGrounded()) {
            Debug.Log("enemy is on ground");
            isBoomPlayed = true;
            Boom();
        }
    }

    public void TakeDamage(int damage) {
        //Debug.Log("TakeDamage called");
        currentHp -= damage;
        gameManager.comboManager.TriggerCombo();

        int randomInt = UnityEngine.Random.Range(1,3);

        gameManager.soundManager.PlaySE("snd_hit_" + randomInt);

        if(currentHp <= 0) {
            GetDestroyed();
        }
    }

    //쉴드로 인해 튕겨져 나감
    public void BounceOff(float shieldForce, out bool isCalled) {
        
        //Debug.Log("BounceOff called");
        
        isCalled = true;

        float bounceForce = (shieldForce - resistence) * 50f;
        if(bounceForce < 500f) bounceForce = 500f;
        
        //Debug.Log("bounceForce: " + bounceForce);

        _rb.AddForce(Vector2.up * bounceForce);
    }

    private void GetDestroyed() {
        gameManager.soundManager.PlaySE("snd_enemy_down");
        gameManager.enemiesSlain++;
        gameManager.uIController.scoreCounter.UpdateScore();
        Destroy(this.gameObject);
    }

    private void Boom() {
        Debug.Log("Boom!");
        gameManager.soundManager.PlaySE("snd_boom");
    }
    private bool IsGrounded() {
        return Physics2D.Raycast(transform.position, Vector2.down, distToGround + 0.1f, groundLayer);
    }

}
