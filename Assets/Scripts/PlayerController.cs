using System;
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
    private float attackCoolTime = 0.5f;
    [SerializeField]
    private float shieldForce = 5.0f;
    [SerializeField]
    private float shieldCoolTime = 0.5f;
    [Tooltip("쉴드로 인해 플레이어가 아래로 튕기는 수치(양수여야 함)")]
    [SerializeField]
    private float shieldBounceOff = 3.0f;
    [SerializeField]
    private float dashForce = 100f;
    //대쉬 지속 시간(초)
    [SerializeField]
    private float dashDuration = 5.0f;
    //대쉬 속도
    [SerializeField]
    private float dashSpeed = 2.0f;
    [SerializeField]
    private int requiredJumpStacksForDash = 3;
    private int currentJumpStacks = 0;


    //flag variables
    private bool canAttack = true;
    private bool canShield = true;
    private bool canDash = false;

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
    {}

    private void CheckCanDash()
    {
        if(currentJumpStacks >= requiredJumpStacksForDash) {
            currentJumpStacks = requiredJumpStacksForDash;
            canDash = true;
        } 
    }

    private IEnumerator CheckShieldCoolTIme()
    {
        float elapsedTime = 0f;
        
        while(!canShield) {
            elapsedTime += Time.deltaTime;
            //Debug.Log(elapsedTime);

            if(elapsedTime >= shieldCoolTime) {
                elapsedTime = shieldCoolTime;
                canShield = !canShield;
                
            }

            yield return null;
        }
    }

    private IEnumerator CheckAttackCoolTime()
    {
        float elapsedTime = 0f;
        
        while(!canAttack) {
            elapsedTime += Time.deltaTime;
            //Debug.Log(elapsedTime);

            if(elapsedTime >= attackCoolTime) {
                elapsedTime = attackCoolTime;
                canAttack = !canAttack;
            }

            yield return null;
        }
    }

    private bool IsGrounded() {
        return Physics2D.Raycast(transform.position, Vector2.down, distToGround + 0.1f, groundLayer);
    }

    public void Jump() {
        if(IsGrounded() && !IsTouchingDestructableObject()) {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            currentJumpStacks++;
            CheckCanDash();
            Debug.Log("CurrentJumpStacks: " + currentJumpStacks);
        }
    }

    //Destructable Object와 접촉중인지 확인하는 함수
    private bool IsTouchingDestructableObject() {
        return Physics2D.Raycast(transform.position, Vector2.up, distToGround + 0.1f, destructableObjectLayer);
    }

    public void Attack() {
         Debug.Log("attack!");
        if(canAttack) {
            hitBox.AttackTarget?.TakeDamage(attackForce);
            canAttack = !canAttack;
            StartCoroutine(CheckAttackCoolTime());
            //StartCoroutine(PlayAttackAnim());
        }
    }

    private IEnumerator PlayAttackAnim()
    {
        throw new NotImplementedException();
    }

    public void Shield() {
        Debug.Log("Shield!");
        if(canShield) {
            bool isBounceOffFunctionCalled = false;

            hitBox.AttackTarget?.BounceOff(shieldForce, out isBounceOffFunctionCalled);

            if(isBounceOffFunctionCalled) ShieldBounceOff();

            canShield = !canShield;
            StartCoroutine(CheckShieldCoolTIme());
            //StartCoroutine(PlayShieldAnim());
        }
    }

    private string PlayShieldAnim()
    {
        throw new NotImplementedException();
    }

    //shield의 반작용으로 인해 아래로 튕겨지는 것을 구현한 함수
    private void ShieldBounceOff() {
        Debug.Log("ShieldBounceOff Called");
        _rb.velocity = new Vector2(_rb.velocity.x, -shieldBounceOff);
    }

    public void Dash() {
        if(canDash) {
            Debug.Log("Dash!");
            //_rb.velocity = new Vector2(_rb.velocity.x, dashForce);
            GameObject instantiatedDashPrefab = Instantiate(Resources.Load("Prefabs/Dash") as GameObject);
            instantiatedDashPrefab.transform.position = this.transform.position;

            StartCoroutine(DashCoroutine(instantiatedDashPrefab));

            currentJumpStacks = 0;
            canDash = !canDash;
        }
    }

    IEnumerator DashCoroutine(GameObject dashPrefab)
    {
        float elapsedTime = 0f;

        while(elapsedTime < dashDuration) {
            elapsedTime += Time.deltaTime;
            if(elapsedTime >= dashDuration) elapsedTime = dashDuration;

            //variable for a smoother step
            float t = elapsedTime / dashDuration;
            t = Mathf.Sin(t * Mathf.PI * 0.5f);

            dashPrefab.transform.Translate(Vector2.up * dashSpeed * t * Time.deltaTime);

            yield return null;
        }

        Destroy(dashPrefab);
    }
}
