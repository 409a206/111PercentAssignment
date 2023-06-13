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

    //derived data variables
    float distToGround;
    [SerializeField]
    private LayerMask groundLayer;

    //reference variables
    // [SerializeField]
    // private Button jumpButton;
    // [SerializeField]
    // private Button shieldButton;
    // [SerializeField]
    // private Button attackButton;
    
    void Start()
    {
        _col = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();

       
        distToGround = _col.bounds.extents.y;

    }

    void Update()
    {
        
    }

    private bool IsGrounded() {
        return Physics2D.Raycast(transform.position, Vector2.down, distToGround + 0.1f, groundLayer);
    }

    public void Jump() {
            if(IsGrounded()) {
                _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            }
    }
}
