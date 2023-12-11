using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask lavaLayer;

    public Vector2 InputVector { get; set; }

    private Rigidbody2D rb2d;
    private BoxCollider2D bc2d;
    private Animator animator;
    private Vector2 playerSize;

    #region Terrain
        private bool onGround;
        private bool onLava;
    #endregion

    private bool isDead;
    

    public void Start()
    {
        // Get reference to the player's Rigidbody2D and animator
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bc2d = GetComponent<BoxCollider2D>();

        playerSize = new Vector2(bc2d.size.x, bc2d.size.y + bc2d.edgeRadius);
    }

    public void Update() {
        CheckTerrain();
    }

    private void CheckTerrain() {
        onGround = Physics2D.BoxCast(transform.position, transform.localScale, 0f, Vector2.zero, 0.0f, groundLayer);
        onLava = Physics2D.BoxCast(transform.position, transform.localScale, 0f, Vector2.zero, 0.0f, lavaLayer);

        if (!onGround && onLava) {
            StartCoroutine(Die());
        }
    }

    public void FixedUpdate()
    {
        // Update velocity
        rb2d.velocity = InputVector * speed * Time.fixedDeltaTime;
    }


    // Get input using Unity's "new" input system
    public void Move(InputAction.CallbackContext callbackContext)
    {
        if (isDead)
            return;
        
        InputVector = callbackContext.ReadValue<Vector2>().normalized;
    }

    private IEnumerator Die() {
        isDead = true;
        InputVector = Vector2.zero;
        yield return StartCoroutine(FadeToDeath(1f));
        gameObject.SetActive(false);
    }

    private IEnumerator FadeToDeath(float duration) {
        float timeElapsed = 0f;

        while (timeElapsed < duration) {
            timeElapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(1, 0, timeElapsed / duration);
            
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            Color spriteColor = sr.sharedMaterial.color;
            sr.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, newAlpha);

            yield return null;
        }
    }
}
