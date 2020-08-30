using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator playerAnimator;
    public GroundCheck ground;
    [Tooltip("Move Speed is set static in crouch after experimentation")]
    public float moveSpeed, jumpForce;
    float horizontalValue, verticalValue;
    private Vector2 originalColliderSize, originalOffset;

    private Rigidbody2D rgbd;
    private BoxCollider2D PlayerCollider;
    private bool isFacingRight = true;

    private void Awake()
    {
        rgbd = GetComponent<Rigidbody2D>();
        PlayerCollider = GetComponent<BoxCollider2D>();
        originalColliderSize = PlayerCollider.size;
        originalOffset = PlayerCollider.offset;

    }
    // Update is called once per frame
    void Update()
    {
        GetHorizontalInput();
        GetVerticalInput();
        InputForCrouch();
        SetAnimatorSpeedValue();
    }
    private void FixedUpdate()
    {
        if (verticalValue > 0)
            JumpPlayer(verticalValue);
        if (horizontalValue != 0)
            MovePlayer(horizontalValue);
    }

    public void GetHorizontalInput()
    {
        horizontalValue = Input.GetAxisRaw("Horizontal");
    }

    public void GetVerticalInput()
    {
        verticalValue = Input.GetAxisRaw("Jump");
    }

    private void SetAnimatorSpeedValue()
    {
        CheckFlipped();
        playerAnimator.SetFloat("Speed", Mathf.Abs(horizontalValue));
    }

    private void CheckFlipped()
    {
        if (horizontalValue < 0 && isFacingRight)
        {
            Flip();
        }
        if (horizontalValue > 0 && !isFacingRight)
        {
            Flip();
        }

    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        if (isFacingRight)
            scale.x = 1 * Mathf.Abs(scale.x);
        else
            scale.x = -1 * Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
    private void MovePlayer(float horizontal)
    {
        Vector2 pos = transform.position;
        pos.x = pos.x + horizontal * moveSpeed * Time.deltaTime;
        transform.position = pos;
    }

    private void JumpPlayer(float vertical)
    {
        if (ground.isGrounded == false) return;

        playerAnimator.SetTrigger("Jump");

        if (vertical > 0)
        {
            rgbd.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    private void InputForCrouch()
    {
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            playerAnimator.SetBool("isCrouch", true);
            PlayerCollider.size = new Vector2(PlayerCollider.size.x, 1.2f);
            PlayerCollider.offset = new Vector2(PlayerCollider.offset.x, .6f);
            moveSpeed = 6; // moves slow when crouching
        }
        else
        {
            playerAnimator.SetBool("isCrouch", false);
            PlayerCollider.size = originalColliderSize;
            PlayerCollider.offset = originalOffset;
            moveSpeed = 8;
        }
    }
}
