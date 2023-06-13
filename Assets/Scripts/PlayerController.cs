using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Component variables
    private Collider2D _col;
    private Rigidbody2D _rb;

    //adjustable data variables
    [SerializeField]
    private float jumpForce = 1;
    [SerializeField]
    private int attackForce = 1;

    //derived data variables
    float distToGround;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private LayerMask destructableObjectLayer;

    //reference variables
    //공격 판정 범위 콜라이더
    private Collider2D attackCol;
    //destructable object 콜라이더
    private Collider2D DoCol;
    
    void Start()
    {
        _col = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();

        attackCol = GetComponentInChildren<Collider2D>();

        distToGround = _col.bounds.extents.y;

    }

    void Update()
    {
        Debug.Log(IsTouchingDestructableObject());
    }

    private bool IsGrounded() {
        return Physics2D.Raycast(transform.position, Vector2.down, distToGround + 0.1f, groundLayer);
    }

    public void Jump() {
        if(IsGrounded() && !IsTouchingDestructableObject()) {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
        }
    }

    //Destructable Object와 접촉중인지 확인하는 함수
    private bool IsTouchingDestructableObject() {
        return Physics2D.Raycast(transform.position, Vector2.up, distToGround + 0.1f, destructableObjectLayer);
    }

    public void Attack() {
        if(attackCol.IsTouching(DoCol)) {
            
        }
    }
}
