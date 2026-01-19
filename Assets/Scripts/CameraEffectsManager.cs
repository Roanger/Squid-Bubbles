using UnityEngine;
using System.Collections;

public class CameraEffectsManager : MonoBehaviour
{
    [Header("Zoom Settings")]
    [SerializeField] private float defaultOrthographicSize = 5f;
    [SerializeField] private float zoomInSize = 4f;  // Less extreme zoom
    [SerializeField] private float zoomDuration = 1.5f;  // Slower, cinematic zoom
    [SerializeField] private AnimationCurve zoomCurve = AnimationCurve.EaseInOut(0, 0, 1, 1); // Smooth ease-in-out
    
    [Header("Discovery Effect")]
    [SerializeField] private float timeSlowdownFactor = 0.7f;  // Less extreme slowdown
    [SerializeField] private float effectDuration = 1f;  // Shorter duration
    [SerializeField] private float maxCameraOffset = 2f;  // Limit how far camera can move
    [SerializeField] private bool enableSlowMotion = false;  // Option to disable slow motion
    
    private Camera mainCamera;
    private float originalTimeScale;
    private Coroutine discoveryEffectCoroutine;
    private CameraFollow cameraFollow;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
        originalTimeScale = Time.timeScale;
        cameraFollow = GetComponent<CameraFollow>();
    }

    public void TriggerDiscoveryEffect(Transform discoveredSpecies)
    {
        Debug.Log("[CameraEffects] TriggerDiscoveryEffect called");
        // Stop any ongoing discovery effect
        if (discoveryEffectCoroutine != null)
        {
            Debug.Log("[CameraEffects] Stopping previous discovery effect");
            StopCoroutine(discoveryEffectCoroutine);
            // Reset time scale if we're interrupting an effect
            if (enableSlowMotion)
            {
                Time.timeScale = originalTimeScale;
            }
        }
        
        discoveryEffectCoroutine = StartCoroutine(PlayDiscoveryEffect(discoveredSpecies));
        Debug.Log("[CameraEffects] Started new discovery effect coroutine");
    }

    private IEnumerator PlayDiscoveryEffect(Transform discoveredSpecies)
    {
        Debug.Log("[CameraEffects] Starting discovery effect");
        // Store original camera position and size
        Vector3 originalCameraPosition = transform.position;
        float originalOrthoSize = mainCamera.orthographicSize;

        // Calculate direction to target
        Vector2 directionToTarget = (discoveredSpecies.position - transform.position);
        directionToTarget = Vector2.ClampMagnitude(directionToTarget, maxCameraOffset);
        
        // Calculate target position (keeping z-coordinate unchanged)
        Vector3 targetPosition = new Vector3(
            originalCameraPosition.x + directionToTarget.x,
            originalCameraPosition.y + directionToTarget.y,
            transform.position.z
        );

        // Slow down time if enabled
        if (enableSlowMotion)
        {
            Time.timeScale = timeSlowdownFactor;
        }

        // Zoom in and move to target
        float elapsedTime = 0f;
        while (elapsedTime < zoomDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float t = zoomCurve.Evaluate(elapsedTime / zoomDuration);

            // Lerp camera position and size
            transform.position = Vector3.Lerp(originalCameraPosition, targetPosition, t);
            mainCamera.orthographicSize = Mathf.Lerp(originalOrthoSize, zoomInSize, t);

            yield return null;
        }

        // Hold the zoom
        yield return new WaitForSecondsRealtime(effectDuration);

        // Zoom back out
        elapsedTime = 0f;
        while (elapsedTime < zoomDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float t = zoomCurve.Evaluate(elapsedTime / zoomDuration);

            // Lerp back to original position and size
            transform.position = Vector3.Lerp(targetPosition, originalCameraPosition, t);
            mainCamera.orthographicSize = Mathf.Lerp(zoomInSize, originalOrthoSize, t);

            yield return null;
        }

        // Reset time scale if slow motion was enabled
        if (enableSlowMotion)
        {
            Time.timeScale = originalTimeScale;
        }
        Debug.Log("[CameraEffects] Discovery effect complete");
        discoveryEffectCoroutine = null;
    }

    private void OnDestroy()
    {
        // Ensure time scale is reset when the script is destroyed
        Time.timeScale = originalTimeScale;
    }
}
