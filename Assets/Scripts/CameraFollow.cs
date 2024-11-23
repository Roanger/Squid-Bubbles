using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private Transform target;  // The player to follow
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);  // Offset from the player (especially for Z to keep camera back)
    
    [Header("Follow Settings")]
    [SerializeField] private float smoothSpeed = 5f;  // How smoothly the camera follows
    [SerializeField] private float lookAheadFactor = 0.5f;  // How much to look ahead in the movement direction
    
    private Vector3 velocity = Vector3.zero;
    private Vector3 desiredPosition;
    private Camera cam;

    private void Start()
    {
        // If no target is set, try to find the player
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
            else
            {
                Debug.LogError("No player found for camera to follow!");
            }
        }

        cam = GetComponent<Camera>();
        if (cam == null)
        {
            Debug.LogError("Camera component not found!");
        }
    }

    private void LateUpdate()
    {
        if (target == null) return;

        // Get the player's rigidbody to check velocity
        Rigidbody2D targetRb = target.GetComponent<Rigidbody2D>();
        Vector3 lookAheadPos = Vector3.zero;

        if (targetRb != null)
        {
            // Add look-ahead based on player velocity
            lookAheadPos = (Vector3)targetRb.velocity * lookAheadFactor;
        }

        // Calculate desired position with offset and look-ahead
        desiredPosition = target.position + offset + lookAheadPos;
        
        // Keep the z-offset constant
        desiredPosition.z = offset.z;

        // Smoothly move the camera
        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref velocity,
            1f / smoothSpeed
        );
    }

    // Optional: Method to shake the camera when something exciting happens
    public void ShakeCamera(float duration = 0.2f, float magnitude = 0.5f)
    {
        StartCoroutine(ShakeCameraCoroutine(duration, magnitude));
    }

    private System.Collections.IEnumerator ShakeCameraCoroutine(float duration, float magnitude)
    {
        Vector3 originalPos = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(
                originalPos.x + x,
                originalPos.y + y,
                originalPos.z
            );

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPos;
    }

    // Optional: Method to zoom the camera
    public void SetZoom(float targetOrthoSize, float duration = 1f)
    {
        if (cam != null && cam.orthographic)
        {
            StartCoroutine(ZoomCoroutine(targetOrthoSize, duration));
        }
    }

    private System.Collections.IEnumerator ZoomCoroutine(float targetOrthoSize, float duration)
    {
        float startSize = cam.orthographicSize;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            
            cam.orthographicSize = Mathf.Lerp(startSize, targetOrthoSize, t);
            yield return null;
        }

        cam.orthographicSize = targetOrthoSize;
    }
}
