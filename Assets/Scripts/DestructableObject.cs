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
    private int hp = 3;

    //shield에 대항하는 수치
    [SerializeField]
    private float resistence = 3.0f;

    void Start()
    {
        _destructableObject = this.GetComponent<DestructableObject>();
        _rb = this.GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage) {
        Debug.Log("TakeDamage called");
        hp -= damage;
        if(hp <= 0) {
            GetDestroyed();
        }
    }

    //쉴드로 인해 튕겨져 나감
    public void BounceOff(float shieldForce, out bool isCalled) {
        
        Debug.Log("BounceOff called");
        
        isCalled = true;

        float bounceForce = shieldForce - resistence;
        if(bounceForce < 1.0f) bounceForce = 1.0f;
        
        Debug.Log("bounceForce: " + bounceForce);

        _rb.velocity = new Vector2(_rb.velocity.x, bounceForce);
    }

    private void GetDestroyed() {
        Destroy(this.gameObject);
    }

}
