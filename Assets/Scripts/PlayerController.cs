using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Field Variables
    //Component variables
    private Collider2D _col;
    private Rigidbody2D _rb;

    //adjustable data variables
    [SerializeField]
    private float jumpForce = 1;
    [SerializeField]
    private int attackForce = 1;
    [SerializeField]
    private float attackRange = 0.1f;
    [SerializeField]
    private float attackCoolTime = 0.5f;

    [SerializeField]
    private float shieldForce = 5.0f;
    [SerializeField]
    private float shieldRange = 0.1f;
    [SerializeField]
    private float shieldCoolTime = 0.5f;
    [Tooltip("쉴드로 인해 플레이어가 아래로 튕기는 수치")]
    [SerializeField]
    [Range(1.0f, 10.0f)]
    private float shieldBounceOff = 3.0f;

    [SerializeField]
    private int dashForce = 100;
    public int DashForce{
        get{return dashForce;}
    }
    //대쉬 지속 시간(초)
    [SerializeField]
    private float dashDuration = 5.0f;
    //대쉬 속도
    [SerializeField]
    private float dashSpeed = 2.0f;
    [SerializeField]
    private int requiredJumpStacksForDash = 3;
    private int currentJumpStacks = 0;

    [Tooltip("궁극기 사용시 공격력 증가 배율")]
    [SerializeField]
    private int overdriveAttackForceIncreaseRate = 2;
    [Tooltip("궁극기 사용시 공격 범위 증가 배율")]
    [SerializeField]
    private float overdriveAttackRangeIncreaseRate = 2.0f;
    [Tooltip("궁극기 사용시 공격 쿨타임 감소 배율")]
    [SerializeField]
    private float overdriveAttackCoolTimeDecreaseRate = 2.0f;
    [Tooltip("궁극기 사용시 점프력 증가 배율")]
    [SerializeField]
    private float overdriveJumpForceIncreaseRate = 2.0f;
    [Tooltip("궁극기 사용시 쉴드력 증가 배율")]
    [SerializeField]
    private float overdriveshieldForceIncreaseRate = 2.0f;
    [Tooltip("궁극기 사용시 쉴드쿨타임 증가 배율")]
    [SerializeField]
    private float overdriveshieldCoolTimeDecreaseRate = 2.0f;

    [SerializeField]
    private float overdriveDuration = 5.0f;

    [Tooltip("궁극기를 쓰기 위해 채워야하는 히트 수")]
    [SerializeField]
    private int requiredHitStacksForOverdrive = 5;
    private int currentHitStacks = 0;
    

    //flag variables
    private bool canAttack = true;
    private bool canShield = true;
    private bool canDash = false;
    private bool canOverdrive = false;

    //derived data variables
    float distToGround;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private LayerMask destructableObjectLayer;

    //reference variables
    //공격 판정 지점
    [SerializeField]
    private Transform attackPoint;
    //쉴드 판정 지점
    [SerializeField]
    private Transform shieldPoint;
    
    private GameManager gameManager;

    #endregion
   
    void Start()
    {
        _col = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();

        distToGround = _col.bounds.extents.y;

    }
    
    private void CheckCanDash()
    {
        if(currentJumpStacks >= requiredJumpStacksForDash) {
            currentJumpStacks = requiredJumpStacksForDash;
            canDash = true;
        } 
    }

    private void CheckCanOverdrive() {
        if(currentHitStacks >= requiredHitStacksForOverdrive) {
            currentHitStacks = requiredHitStacksForOverdrive;
            canOverdrive = true;
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
        //Debug.Log("attack!");
        
        if(canAttack) {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, destructableObjectLayer);

            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("player hit " + enemy.name);
                enemy.transform.gameObject.GetComponent<DestructableObject>().TakeDamage(attackForce);
                currentHitStacks++;
                CheckCanOverdrive();
            }

            canAttack = !canAttack;
           
            StartCoroutine(CheckAttackCoolTime());

            //StartCoroutine(PlayAttackAnim());
        }
    }

    private void OnDrawGizmosSelected() {
        if(attackPoint != null) Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        if(shieldPoint != null) Gizmos.DrawWireSphere(shieldPoint.position, shieldRange);
    }

    private IEnumerator PlayAttackAnim()
    {
        throw new NotImplementedException();
    }

    public void Shield() {
        //Debug.Log("Shield!");
        
        if(canShield) {
            bool isBounceOffFunctionCalled = false;

            Collider2D blockedEnemy = Physics2D.OverlapCircle(shieldPoint.position, shieldRange, destructableObjectLayer);

            Debug.Log("player blocked " + blockedEnemy?.name);
            blockedEnemy?.transform.gameObject.GetComponent<DestructableObject>().BounceOff(shieldForce, out isBounceOffFunctionCalled);
            
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
        _rb.AddForce(Vector2.down * (-shieldBounceOff));
    }

    public void Dash() {
        if(canDash) {
            Debug.Log("Dash!");
            //_rb.velocity = new Vector2(_rb.velocity.x, dashForce);
            GameObject instantiatedDashPrefab = Instantiate(Resources.Load("Prefabs/Dash") as GameObject);
            instantiatedDashPrefab.transform.position = this.transform.position;

            gameManager.mainCamera.GetComponent<CameraSmoothFollow>().Target = instantiatedDashPrefab;

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

            //temporary variable for a smoother step
            float t = elapsedTime / dashDuration;
            t = Mathf.Sin(t * Mathf.PI * 0.5f);

            dashPrefab.transform.Translate(Vector2.up * dashSpeed * t * Time.deltaTime);

            yield return null;
        }

        Destroy(dashPrefab);
        
        gameManager.mainCamera.GetComponent<CameraSmoothFollow>().Target = this.gameObject;
        

    }

    public void Overdrive() {
        if(canOverdrive) {
            Debug.Log("Overdrive!");
            StartCoroutine(OverDriveCoroutine());
            currentHitStacks = 0;
            canOverdrive = !canOverdrive;
        }
    }

    IEnumerator OverDriveCoroutine()
    {
        float elapsedTime = 0f;

        //원래 수치 임시 변수에 저장하기
        int originAttackForce = attackForce;
        float originAttackRange = attackRange;
        float originAttackCoolTime = attackCoolTime;

        float originShieldCoolTime = shieldCoolTime;
        float originShieldForce = shieldForce;

        float originJumpForce = jumpForce;

        //오버드라이브 수치 적용
        attackForce *= overdriveAttackForceIncreaseRate;
        attackRange *= overdriveAttackRangeIncreaseRate;
        attackCoolTime /= overdriveAttackCoolTimeDecreaseRate;

        shieldCoolTime /= overdriveshieldCoolTimeDecreaseRate;
        shieldForce *= overdriveshieldForceIncreaseRate;

        jumpForce *= overdriveJumpForceIncreaseRate;

        while(elapsedTime < overdriveDuration) {
            elapsedTime += Time.deltaTime;
            if(elapsedTime >= overdriveDuration) elapsedTime = overdriveDuration;
            yield return null;
        }

        Debug.Log("End of Overdrive");
        attackForce = originAttackForce;
        attackRange = originAttackRange;
        attackCoolTime = originAttackCoolTime;
        shieldCoolTime = originShieldCoolTime;
        shieldForce = originShieldForce;
        jumpForce = originJumpForce;

    }
}
