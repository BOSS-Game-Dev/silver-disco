using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CircleCollider2D cc2d;
    private Rigidbody2D rb2d;
    [SerializeField] private float moveSpeed;

    private Vector2 inputVector;

    // Start is called before the first frame update
    void Start()
    {
        cc2d = GetComponent<CircleCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb2d.velocity = inputVector * moveSpeed  * 100 * Time.fixedDeltaTime;
    }

    void Update() 
    {
        inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        inputVector = inputVector.normalized;
    }
}
