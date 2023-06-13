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
    public void BounceOff(float shieldForce) {
        Debug.Log("BounceOff called");
        _rb.velocity = new Vector2(_rb.velocity.x, shieldForce);
    }

    private void GetDestroyed() {
        Destroy(this.gameObject);
    }

}
