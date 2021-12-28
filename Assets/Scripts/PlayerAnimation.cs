using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        movement = GetComponentInParent<PlayerMovement>();
        rb = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("speed" , Mathf.Abs(movement.xVelocity));
        anim.SetBool("isOnGround" , movement.isOnGround);
        anim.SetBool("isJumping" , movement.isJump);
        anim.SetBool("isCrouching" , movement.isCrouch);
        anim.SetFloat("verticalVelocity" , rb.velocity.y);
        
        // print(rb.velocity.y);
    }

    public void StepAudio()
    {
        AudioManager.PlayFootstepAudio();
    }

    public void CrouchStepAudio()
    {
        AudioManager.PlayCrouchFootstepAudio();
    }
}
