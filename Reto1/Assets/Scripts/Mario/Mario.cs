using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : MonoBehaviour
{
    enum State { Default = 0 ,Super = 1,Fire = 2};
    State currentState = State.Default;

    Movement movement;
    Collisions collisions;
    Animations animations;
    Rigidbody2D rb2D;
    public GameObject stompBox;

    bool isDead;
    private void Awake()
    {
        movement = GetComponent<Movement>();
        collisions = GetComponent<Collisions>();
        animations = GetComponent<Animations>();
        rb2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(rb2D.velocity.y < 0 && !isDead)
        {
            stompBox.SetActive(true);
        }
        else
        {
            stompBox.SetActive(false);
        }
    }
    public void Hit()
    {
        if(currentState == State.Default)
        {
            Dead();
        }
        else
        {
            AudioManager.Instance.PlayPowerDown();
            Time.timeScale = 0;
            animations.Hit();
        }
        
    }

    public void Dead()
    {
        if (!isDead)
        {
            AudioManager.Instance.PlayDie();
            isDead = true;
            collisions.Dead();
            movement.Dead();
            animations.Dead();
        }
        
    }
    public void ChangeState(int newState)
    {
        currentState = (State)newState;
        animations.NewState(newState);
        Time.timeScale = 1;
    }
    public void CatchItem(ItemType type)
    {
        switch (type)
        {
            case ItemType.MagicMushroom:
                AudioManager.Instance.PlayPowerUp();
                if (currentState == State.Default)
                {
                    animations.PowerUp();
                    Time.timeScale = 0;
                }
                break;
            case ItemType.FireFlower:
                AudioManager.Instance.PlayPowerUp();
                if (currentState != State.Fire)
                {
                    animations.PowerUp();
                    Time.timeScale = 0;
                }
                break;
            case ItemType.Coin:
                AudioManager.Instance.PlayCoin();
                Debug.Log("COIN");
                break;
            case ItemType.Life:
                break;
            case ItemType.Star:
                AudioManager.Instance.PlayPowerUp();
                break;
            default:
                break;
        }
    }
}
