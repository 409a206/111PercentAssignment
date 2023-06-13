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
    [SerializeField]
    private float shieldForce = 5.0f;

    [Tooltip("쉴드로 인해 플레이어가 아래로 튕기는 수치(양수여야함)")]
    [SerializeField]
    private float shieldBounceOff = 3.0f;

    //derived data variables
    float distToGround;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private LayerMask destructableObjectLayer;

    //reference variables
    private HitBox hitBox;
   
    void Start()
    {
        _col = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();

        hitBox = GetComponentInChildren<HitBox>();

        distToGround = _col.bounds.extents.y;

    }

    void Update()
    {
        //Debug.Log(IsTouchingDestructableObject());
        //Debug.Log(IsGrounded());
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
        // Debug.Log("attack!");
        hitBox.AttackTarget?.TakeDamage(attackForce);
    }

    public void Shield() {
        //Debug.Log("Shield!");
        bool isBounceOffFunctionCalled = false;

        hitBox.AttackTarget?.BounceOff(shieldForce, out isBounceOffFunctionCalled);

        if(isBounceOffFunctionCalled) ShieldBounceOff();
    }

    //shield로 인해 아래로 튕겨지는 것을 구현한 함수
    private void ShieldBounceOff() {
        Debug.Log("ShieldBounceOff Called");
        _rb.velocity = new Vector2(_rb.velocity.x, -shieldBounceOff);
    }
}
