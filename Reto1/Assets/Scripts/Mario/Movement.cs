using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    enum Direction { Left = -1, None = 0, Right = 1};
    Direction currentDirection = Direction.None;

    public float speed;
    public float acceleration;
    public float maxVelocity;
    public float friction;
    float currentVelocity = 0f;

    public float jumpForce;
    public float maxJumpingTime = 1f;
    public bool isJumping;
    float jumpTimer = 0;
    float defaultGravity;

    public bool isSkidding;


    public Rigidbody2D rb2D;
    Collisions collisions;
    Animations animations;

    public bool inputMoveEnabled = true;
    public GameObject headBox;

    public void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        collisions = GetComponent<Collisions>();
        animations = GetComponent<Animations>();
    }
    // Start is called before the first frame update
    void Start()
    {
        defaultGravity = rb2D.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        headBox.SetActive(false);
        bool grounded = collisions.Grounded();
        animations.Grounded(grounded);

        if (isJumping)
        {
            if (rb2D.velocity.y > 0f)
            {
                headBox.SetActive(true);
                if (Input.GetKey(KeyCode.Space))
                {
                    jumpTimer += Time.deltaTime;
                }
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    if(jumpTimer < maxJumpingTime)
                    {
                        rb2D.gravityScale = defaultGravity * 3f;
                    }
                }
            }
            else
            {
                rb2D.gravityScale = defaultGravity;
                if (grounded)
                {
                    isJumping = false;
                    jumpTimer = 0;
                    animations.Jumping(false);
                }
            }
        }

        currentDirection = Direction.None;

        if (inputMoveEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (grounded)
                {
                    jump();
                }
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                currentDirection = Direction.Left;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                currentDirection = Direction.Right;
            }
        }
      
    }
    private void FixedUpdate()
    {
        isSkidding = false;
        currentVelocity = rb2D.velocity.x;
        
        if(currentDirection > 0)
        {
            if (currentVelocity < 0)
            {
                currentVelocity += ((acceleration + friction) * Time.deltaTime);
                isSkidding = true;
            }
            else if(currentVelocity < maxVelocity)
            {
                currentVelocity += acceleration * Time.deltaTime;
                transform.localScale = new Vector2(1, 1);
            }
        }
        else if(currentDirection < 0)
        {
            if (currentVelocity > 0)
            {
                currentVelocity -= ((acceleration + friction) * Time.deltaTime);
                isSkidding = true;
            }
            else if (currentVelocity > -maxVelocity)
            {
                currentVelocity -= acceleration * Time.deltaTime;
                transform.localScale = new Vector2(-1, 1);
            }
        }
        else
        {
            if (currentVelocity > 1f)
            {
                currentVelocity -= friction * Time.deltaTime;
            }
            else if( currentVelocity < -1f)
            {
                currentVelocity += friction * Time.deltaTime;
            }
            else
            {
                currentVelocity = 0;
            }
        }

        Vector2 velocity = new Vector2(currentVelocity, rb2D.velocity.y);
        rb2D.velocity = velocity;

        animations.Velocity(currentVelocity);
        animations.Skid(isSkidding);
    }

    void jump()
    {
        if (!isJumping)
        {
            AudioManager.Instance.PlayJump();
            isJumping = true;
            Vector2 force = new Vector2(0, jumpForce);
            rb2D.AddForce(force, ForceMode2D.Impulse);
            animations.Jumping(true);
        }
    }

    public void Dead()
    {
        inputMoveEnabled = false;
        rb2D.velocity = Vector2.zero;
        rb2D.gravityScale = 1;
        rb2D.AddForce(Vector2.up*5f,ForceMode2D.Impulse);
    }
    
    public void BounceUp()
    {
        rb2D.velocity = Vector2.zero;
        rb2D.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
    }
}
