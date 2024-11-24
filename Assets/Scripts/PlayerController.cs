using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 180f;
    [SerializeField] private float smoothness = 0.1f;

    private Vector2 movement;
    private Vector2 smoothVelocity;
    private Rigidbody2D rb;
    private Animator animator;
    private CircleCollider2D playerCollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        // Set up collider if it doesn't exist
        playerCollider = GetComponent<CircleCollider2D>();
        if (playerCollider == null)
        {
            playerCollider = gameObject.AddComponent<CircleCollider2D>();
            playerCollider.radius = 0.5f; // Adjust based on your sprite size
            playerCollider.isTrigger = true;
        }
        
        // Set up rigidbody settings
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        
        // Make sure the player has the "Player" tag
        if (gameObject.tag != "Player")
        {
            gameObject.tag = "Player";
        }
    }

    private void Update()
    {
        // Get input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movement = new Vector2(horizontal, vertical).normalized;

        // Handle rotation
        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.Euler(0, 0, angle - 90), // -90 to point sprite upward
                rotationSpeed * Time.deltaTime
            );
        }

        // Update animation
        if (animator != null)
        {
            animator.SetFloat("Speed", movement.magnitude);
        }
    }

    private void FixedUpdate()
    {
        // Apply movement with smoothing
        Vector2 targetVelocity = movement * moveSpeed;
        rb.velocity = Vector2.SmoothDamp(
            rb.velocity,
            targetVelocity,
            ref smoothVelocity,
            smoothness
        );
    }
}
