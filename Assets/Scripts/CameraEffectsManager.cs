using UnityEngine;
using System.Collections;

public class CameraEffectsManager : MonoBehaviour
{
    [Header("Zoom Settings")]
    [SerializeField] private float defaultOrthographicSize = 5f;
    [SerializeField] private float zoomInSize = 3f;
    [SerializeField] private float zoomDuration = 0.5f;
    [SerializeField] private AnimationCurve zoomCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    
    [Header("Discovery Effect")]
    [SerializeField] private float timeSlowdownFactor = 0.5f;
    [SerializeField] private float effectDuration = 2f;
    
    private Camera mainCamera;
    private float originalTimeScale;
    private Coroutine discoveryEffectCoroutine;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
        originalTimeScale = Time.timeScale;
    }

    public void TriggerDiscoveryEffect(Transform discoveredSpecies)
    {
        Debug.Log("[CameraEffects] TriggerDiscoveryEffect called");
        // Stop any ongoing discovery effect
        if (discoveryEffectCoroutine != null)
        {
            Debug.Log("[CameraEffects] Stopping previous discovery effect");
            StopCoroutine(discoveryEffectCoroutine);
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

        // Calculate target position (keeping z-coordinate unchanged)
        Vector3 targetPosition = new Vector3(
            discoveredSpecies.position.x,
            discoveredSpecies.position.y,
            transform.position.z
        );

        // Slow down time
        Time.timeScale = timeSlowdownFactor;

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

        // Reset time scale
        Time.timeScale = originalTimeScale;
        Debug.Log("[CameraEffects] Discovery effect complete");
        discoveryEffectCoroutine = null;
    }

    private void OnDestroy()
    {
        // Ensure time scale is reset when the script is destroyed
        Time.timeScale = originalTimeScale;
    }
}
