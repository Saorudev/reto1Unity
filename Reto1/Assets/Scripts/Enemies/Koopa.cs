using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koopa : Enemy
{
    bool isHidden;
    public float maxStoppedTime;
    float stoppedTimer;

    public float rollSpeed;
    protected override void Update()
    {
        base.Update();
        if (isHidden && rb2D.velocity.x == 0f)
        {
            stoppedTimer += Time.deltaTime;
            if(stoppedTimer >= maxStoppedTime)
            {
                ResetMove();
            }
        }
    }
    public override void Stomped(Transform player)
    {
        AudioManager.Instance.PlayStomp();
        if (!isHidden)
        {
            isHidden = true;
            animator.SetBool("Hidden", isHidden);
            autoMovement.PauseMovement();
        }
        else
        {
            if (Mathf.Abs(rb2D.velocity.x)>0f) {
                autoMovement.PauseMovement();
            }
            else
            {
                if (player.position.x < transform.position.x)
                {
                    autoMovement.speed = rollSpeed;
                }
                else
                {
                    autoMovement.speed = -rollSpeed;
                }
                autoMovement.ContinueMovement(new Vector2(autoMovement.speed, 0f));
            }
            
        }
        gameObject.layer = LayerMask.NameToLayer("OnlyGround");
        Invoke("ResetLayer", 0.1f);
        stoppedTimer = 0;
    }
    void ResetLayer()
    {
        gameObject.layer = LayerMask.NameToLayer("Enemy");
    }
    void ResetMove()
    {
        autoMovement.ContinueMovement();
        isHidden = false;
        animator.SetBool("Hidden", isHidden);
        stoppedTimer = 0;
    }

}
