using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{   
    [SerializeField] private Transform spawnpoint;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask lavaLayer;
    [SerializeField] private Rigidbody2D movingPlatform;

    public Vector2 InputVector { get; set; }

    private Rigidbody2D rb2d;
    private SpriteRenderer sr;
    private Animator animator;

    Color spriteColor;

    #region Terrain
        private bool onGround;
        private bool onLava;
        public bool OnMovingPlatform { get; set; }
    #endregion

    private bool isDead;
    

    public void Start()
    {
        // Get reference to the player's Rigidbody2D and animator
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        spriteColor = sr.sharedMaterial.color;
    }

    public void Update() {
        CheckTerrain();
    }

    private void CheckTerrain() {
        onGround = Physics2D.OverlapBox(transform.position, transform.localScale, 0f, groundLayer);
        onLava = Physics2D.OverlapBox(transform.position, transform.localScale, 0f, lavaLayer);

        if (!onGround && onLava) {
            StartCoroutine(Die());
        }
    }

    public void FixedUpdate()
    {
        // Update velocity
        if (OnMovingPlatform)
            rb2d.velocity = movingPlatform.velocity + InputVector * speed * Time.fixedDeltaTime;
        else
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

        yield return new WaitForSeconds(1f);
        Respawn();
    }

    private void Respawn() {
        isDead = false;
        transform.position = spawnpoint.position;
        sr.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, 1);
    }

    private IEnumerator FadeToDeath(float duration) {
        float timeElapsed = 0f;

        while (timeElapsed < duration) {
            timeElapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(1, 0, timeElapsed / duration);
            sr.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, newAlpha);

            yield return null;
        }
    }
}
