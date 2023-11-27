using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb2d;

    private int spriteDirection = 1;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb2d.velocity != Vector2.zero)
        {
            animator.Play("PlayerWalkAnimation");

            if (rb2d.velocity.x > 0)
                spriteDirection = 1;

            else if (rb2d.velocity.x < 0)
                spriteDirection = -1;
        }
        else
        {
            animator.Play("PlayerIdleAnimation");
        }

        Vector3 currentScale = transform.localScale;
        currentScale.x = spriteDirection;
        transform.localScale = currentScale;
    }
}
