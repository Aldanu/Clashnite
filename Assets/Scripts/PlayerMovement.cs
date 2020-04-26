using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public float speed = 3f;
    public float jumpSpeed = 500;
    public Animator playerAnimator;

    private bool _isGrounded = true;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        playerRb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, playerRb.velocity.y);

        if (Input.GetAxis("Horizontal") == 0)
        {
             playerAnimator.SetBool($"isWalking", false);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            playerAnimator.SetBool($"isWalking", true);
            _spriteRenderer.flipX = true;
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            playerAnimator.SetBool($"isWalking", true);
            _spriteRenderer.flipX = false;
        }
        
        Debug.Log("gg");
        
        if (!_isGrounded) return;
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        playerRb.AddForce(Vector2.up * jumpSpeed);
        _isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag($"Ground"))
        {
            _isGrounded = true;
        }
    }
}
