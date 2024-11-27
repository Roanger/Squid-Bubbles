using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private Transform target;  // The player to follow
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);  // Offset from the player (especially for Z to keep camera back)
    
    [Header("Follow Settings")]
    [SerializeField] private float smoothSpeed = 5f;  // How smoothly the camera follows
    [SerializeField] private float lookAheadFactor = 0.5f;  // How much to look ahead in the movement direction
    
    [Header("Zoom Settings")]
    [SerializeField] private float minZoom = 3f;  // Maximum zoom in (smaller orthographic size)
    [SerializeField] private float maxZoom = 15f;  // Maximum zoom out (larger orthographic size)
    [SerializeField] private float zoomSpeed = 2f;  // How fast to zoom
    [SerializeField] private float zoomSmoothness = 0.2f;  // How smooth the zoom feels
    
    private Vector3 velocity = Vector3.zero;
    private Vector3 desiredPosition;
    private Camera cam;
    private float currentZoom;
    private float targetZoom;

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
        if (cam != null)
        {
            currentZoom = targetZoom = cam.orthographicSize;
        }
        else
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

    private void Update()
    {
        // Handle zoom input
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
        if (scrollDelta != 0)
        {
            // Zoom in when scrolling up (positive delta) and out when scrolling down (negative delta)
            targetZoom = Mathf.Clamp(targetZoom - scrollDelta * zoomSpeed, minZoom, maxZoom);
        }
        
        // Smoothly interpolate current zoom to target zoom
        if (currentZoom != targetZoom)
        {
            currentZoom = Mathf.Lerp(currentZoom, targetZoom, zoomSmoothness);
            cam.orthographicSize = currentZoom;
        }
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
