using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritesAnimation : MonoBehaviour
{
    public Sprite[] sprites;
    public float frameTime = 0.1f;

    SpriteRenderer spriteRenderer;

    //float timer = 0f;
    int animationFrame = 0;
    public bool stop;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Animation());
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    timer += Time.deltaTime;
    //    if(timer>= frameTime)
    //    {
    //        animationFrame++;

    //        if ( animationFrame >= sprites.Length)
    //        {
    //            animationFrame = 0;
    //        }
    //        spriteRenderer.sprite = sprites[animationFrame];
    //        timer = 0;
    //    }
    //}
    IEnumerator Animation()
    {
        while (!stop)
        {
            spriteRenderer.sprite = sprites[animationFrame];
            animationFrame++;
            if(animationFrame>= sprites.Length)
            {
                animationFrame = 0;
            }
            yield return new WaitForSeconds(frameTime);
        }
    }
}
