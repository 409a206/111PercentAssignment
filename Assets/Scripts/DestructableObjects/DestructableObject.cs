using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour
{
    //component variables
    private DestructableObject _destructableObject;
    private Rigidbody2D _rb;
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

    private GameManager gameManager;

    void Awake()
    {
        _destructableObject = this.GetComponent<DestructableObject>();
        _rb = this.GetComponent<Rigidbody2D>();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        currentHp = maxHp;
    }

    public void TakeDamage(int damage) {
        Debug.Log("TakeDamage called");
        currentHp -= damage;
        gameManager.comboManager.TriggerCombo();
        if(currentHp <= 0) {
            GetDestroyed();
        }
    }

    //쉴드로 인해 튕겨져 나감
    public void BounceOff(float shieldForce, out bool isCalled) {
        
        Debug.Log("BounceOff called");
        
        isCalled = true;

        float bounceForce = (shieldForce - resistence) * 50f;
        if(bounceForce < 500f) bounceForce = 500f;
        
        Debug.Log("bounceForce: " + bounceForce);

        _rb.AddForce(Vector2.up * bounceForce);
    }

    private void GetDestroyed() {
        Destroy(this.gameObject);
    }

}
