using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    Animator animator;
    Collisions collisions;
    Movement movement;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    public void Grounded(bool isGrounded)
    {
        animator.SetBool("Grounded", isGrounded);
    }
    public void Velocity(float velocityX)
    {
        animator.SetFloat("VelocityX", Mathf.Abs(velocityX));
    }
    public void Jumping(bool isJumping)
    {
        animator.SetBool("Jumping", isJumping);
    }
    public void Skid(bool isSkidding)
    {
        animator.SetBool("Skid", isSkidding);
    }
    public void Dead()
    {
        animator.SetTrigger("Dead");
    }
    public void NewState(int state)
    {
        animator.SetInteger("State", state);
    }
    public void PowerUp()
    {
        animator.SetTrigger("PowerUp");
    }
    public void Hit()
    {
        animator.SetTrigger("Hit");
    }
}
