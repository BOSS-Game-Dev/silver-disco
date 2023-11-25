using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    public Vector2 InputVector { get; set; }

    private Rigidbody2D rb2d;
    private Animator animator;


    public void Start()
    {
        // Get reference to the player's Rigidbody2D and animator
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void FixedUpdate()
    {
        // Update velocity
        rb2d.velocity = InputVector * speed * Time.fixedDeltaTime;
    }


    // Get input using Unity's "new" input system
    public void Move(InputAction.CallbackContext callbackContext)
    {
        InputVector = callbackContext.ReadValue<Vector2>().normalized;
    }
}
