using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMovement : MonoBehaviour
{
    public float speed = 1f;
    bool movementPaused;

    Rigidbody2D rb2D;
    SpriteRenderer spriteRenderer;

    Vector2 lastVelocity;
    Vector2 currentDirection;

    float defaultSpeed;

    public bool flipSprite = true;

    float timer = 0;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        rb2D.velocity = new Vector2(speed, rb2D.velocity.y);
        defaultSpeed = Mathf.Abs(speed);
    }

    private void FixedUpdate()
    {
        if (!movementPaused)
        {
            if (rb2D.velocity.x > -0.1f && rb2D.velocity.x < 0.1f)
            {
                if(timer > 0.05)
                {
                    speed = -speed;
                }
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
            }
            rb2D.velocity = new Vector2(speed, rb2D.velocity.y);

            if (flipSprite)
            {
                if (rb2D.velocity.x > 0)
                {
                    spriteRenderer.flipX = true;
                }
                else
                {
                    spriteRenderer.flipX = false;
                }
            }
           
        }
    }
    public void PauseMovement()
    {
        if (!movementPaused)
        {
            currentDirection = rb2D.velocity.normalized;
            lastVelocity = rb2D.velocity;
            movementPaused = true;
            rb2D.velocity = new Vector2(0f, 0f);
        }
    }

    public void ContinueMovement()
    {
        if (movementPaused)
        {
            speed = defaultSpeed*currentDirection.x;
            rb2D.velocity = new Vector2(speed, lastVelocity.y);
            movementPaused = false;
        }
    }

    public void ContinueMovement(Vector2 newVelocity)
    {
        if (movementPaused)
        {
            rb2D.velocity = newVelocity;
            movementPaused = false;
        }
    }
}
