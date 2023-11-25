using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
  private Animator animator;
  private Rigidbody2D rb2d;

  private int spriteDirection = 1; // 1 means facing right: (right is positive)
                                   // -1 means facing left (left is negative)

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
      // If we are facing right...
      if (rb2d.velocity.x > 0)
      {
        spriteDirection = 1;
      }

      // If we are facing left...
      else if (rb2d.velocity.x < 0)
      {
        spriteDirection = -1;
      }

      animator.Play("PlayerWalkAnimation");
    }
    else
    {
      animator.Play("PlayerIdleAnimation");
    }

    Vector3 currentTransform = transform.localScale; // Transform transform
    currentTransform.x = spriteDirection;
    transform.localScale = currentTransform;

  }
}
