using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerVelocity = 3.0f;
    public LayerMask groundLayer;
    public float jumpPower = 5f; 
    Rigidbody2D rigidbody2D;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        rigidbody2D.velocity = new Vector2(playerVelocity * moveX, rigidbody2D.velocity.y);

        
        if (moveX > 0) { // if player wants move right
            transform.localScale = new Vector3(1, 1, 1);
            anim.SetBool("Is Moving", true);
        } else if (moveX < 0) { // if player wants move left
            transform.localScale = new Vector3(-1, 1, 1);
            anim.SetBool("Is Moving", true);
        } else {
            anim.SetBool("Is Moving", false);
        }

        Jump();

        if (rigidbody2D.velocity.y < 0) {
            anim.SetBool("Is Falling", true);
        } else {
            anim.SetBool("Is Falling", false);
        }
    }

    private bool touchingGround;
    private bool hasDoubleJumped;
    void Jump()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);
        if (hit.collider != null) {
            touchingGround = true;
        } else {
            touchingGround = false;
        }
        
        bool jumpInput = Input.GetKeyDown(KeyCode.Space);
        if (jumpInput) {

            if (touchingGround || !hasDoubleJumped) {
                // rigidbody2D.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpPower);
                hasDoubleJumped = true;
                anim.SetTrigger("Has Jumped");
            }
        }

        if (touchingGround) {
            hasDoubleJumped = false;
        }

    }
}
