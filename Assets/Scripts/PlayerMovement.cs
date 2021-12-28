using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    

    [Header("移动参数")]
    [SerializeField] private float speed = 8f;
    [SerializeField] private float crouchSpeedDivisor = 3f;

    [Header("跳跃参数")] 
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private float jumpHoldForce = 1.9f;
    [SerializeField] private float jumpHoldDuration = 0.1f;
    [SerializeField] private float crouchJumpBoost = 2.5f;
    [SerializeField] private int jumpCountConfig = 2;
    
    [Header("碰撞体参数")]
    [SerializeField] private Vector2 standSize;
    [SerializeField] private Vector2 standOffset;
    [SerializeField] private Vector2 crouchSize;
    [SerializeField] private Vector2 crouchOffset;
    
    [Header("输入")]
    public float xVelocity;
    [SerializeField] private bool jumpPressed;
    [SerializeField] private bool jumpHeld;
    [SerializeField] private bool crouchHeld;

    [Header("状态")] 
    public bool isCrouch;
    public bool isOnGround;
    public bool isJump;
    [SerializeField] private int jumpCount;
    [SerializeField] private bool isHeadBlocked;
    // private float jumpTime;

    [Header("环境检测")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector2 leftFootOffset = new Vector2(-0.3f,0.05f);
    [SerializeField] private Vector2 rightFootOffset = new Vector2(0.3f,0.05f);
    [SerializeField] private float headClearance = 0.5f;
    [SerializeField] private float groundDistance = 0.1f;


    public Transform groundCheck;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();

        standSize = coll.size;
        standOffset = coll.offset;
        crouchSize = new Vector2(coll.size.x, coll.size.y / 2);
        crouchOffset = new Vector2(coll.offset.x, coll.offset.y / 2);
    }

    // Update is called once per frame
    void Update()
    {
        xVelocity = isCrouch ? Input.GetAxis("Horizontal")/crouchSpeedDivisor :  Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && jumpCount > 0 && !isHeadBlocked)
        {
            jumpPressed = true;
        }
        
        jumpHeld = Input.GetButton("Jump");
        crouchHeld = Input.GetButton("Crouch");
    }

    private void FixedUpdate()
    {
        StateCheck();
        GroundMovement();
        Jump();
    }

    void Jump()//跳跃
    {
        if (isOnGround)
        {
            jumpCount = jumpCountConfig;//可跳跃数量
            isJump = false;
        }

        if (jumpPressed)
        {
            if (isOnGround)
            {
                isJump = true;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpCount--;
                jumpPressed = false;
                
                AudioManager.PlayJumpAudio();
            }
            // 多段跳
            else if (jumpCount > 0 && isJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpCount--;
                jumpPressed = false;
                
                AudioManager.PlayJumpAudio();
            }
        }
    }
    // 状态init
    void StateCheck()
    {
        RaycastHit2D leftCheck = Raycast(leftFootOffset, Vector2.down, groundDistance, groundLayer);
        RaycastHit2D rightCheck = Raycast(rightFootOffset, Vector2.down, groundDistance, groundLayer);
        
        RaycastHit2D headCheck = Raycast(new Vector2(0f,coll.size.y), Vector2.up, headClearance, groundLayer);
        // Debug.Log((bool)leftCheck);
        // Debug.Log((bool)rightCheck);
        if (leftCheck || rightCheck)
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }

        if (headCheck)
        {
            isHeadBlocked = true;
        }
        else
        {
            isHeadBlocked = false;
        }

        if (crouchHeld || !crouchHeld && isCrouch && isHeadBlocked)
        {
            Crouch();
        }
        else
        {
            StandUp();
        }
        // Screen.SetResolution();
        
        // Debug.DrawRay(pos + offset , Vector3.down,Color.green,0.2f);
        // isOnGround = coll.IsTouchingLayers(groundLayer);
        // isOnGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        // isCrouch = crouchHeld;
    }

    void GroundMovement()
    {
        rb.velocity = new Vector2(xVelocity * speed, rb.velocity.y);
        FilpDirction();
    }

    void FilpDirction()
    {
        if (xVelocity < 0)
        {
            transform.localScale = new Vector3(-1, 1,1);
        }
        else if(xVelocity > 0)
        {
            transform.localScale = new Vector3(1, 1 , 1);
        }
    }

    void StandUp()
    {
        isCrouch = false;
        coll.size = standSize;
        coll.offset = standOffset;
        // print("standup");
    }

    void Crouch()
    {
        isCrouch = true;
        coll.size = crouchSize;
        coll.offset = crouchOffset;
        // print("crouch");
    }
    
    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length, LayerMask layer)
    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDirection, length, layer);
        Color color = hit ? Color.red : Color.green;
        
        Debug.DrawRay(pos + offset , rayDirection * length , color);
        
        return hit;
    }
}
