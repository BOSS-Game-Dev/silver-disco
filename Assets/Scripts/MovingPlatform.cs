using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField] private Transform leftBound;
    [SerializeField] private Transform rightBound;
    [SerializeField] private float moveSpeed;
    [SerializeField] private PlayerController player;

    private Rigidbody2D rb2d;
    private SpriteRenderer sr;
    private float length;

    private bool movingLeft;

    // Start is called before the first frame update
    void Start()
    {
        movingLeft = false;
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        length = sr.bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.x - length / 2 <= leftBound.position.x)
            movingLeft = false;
        else if (transform.position.x + length / 2 >= rightBound.position.x)
            movingLeft = true;

        if (movingLeft)
            MoveLeft();
        else   
            MoveRight();
    }

    private void MoveLeft() {
        rb2d.velocity = new Vector2(-moveSpeed, 0f);
    }

    private void MoveRight() {
        rb2d.velocity = new Vector2(moveSpeed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player") {
            player.OnMovingPlatform = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        if (col.tag == "Player") {
            player.OnMovingPlatform = false;
        }
    }
}
