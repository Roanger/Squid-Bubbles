using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI factText;
    [SerializeField] private CanvasGroup factPanel;
    [SerializeField] private TextMeshProUGUI discoveryCountText;
    [SerializeField] private CanvasGroup discoveryPopupPanel;
    [SerializeField] private TextMeshProUGUI discoveryPopupText;
    [SerializeField] private DiscoveryCollectionUI collectionUI;
    
    [Header("Animation Settings")]
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float factDisplayDuration = 5f;
    [SerializeField] private float discoveryPopupDuration = 2f;
    [SerializeField] private AnimationCurve fadeInCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] private AnimationCurve fadeOutCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);
    
    private HashSet<string> discoveredSpecies = new HashSet<string>();
    private Coroutine factCoroutine;
    private Coroutine discoveryPopupCoroutine;

    private void Start()
    {
        if (factPanel != null)
        {
            factPanel.alpha = 0f;
        }
        if (discoveryPopupPanel != null)
        {
            discoveryPopupPanel.alpha = 0f;
        }
        UpdateDiscoveryCount();
    }

    public bool ShowFact(string fact, string speciesName)
    {
        bool isNewDiscovery = false;
        if (!discoveredSpecies.Contains(speciesName))
        {
            discoveredSpecies.Add(speciesName);
            isNewDiscovery = true;
            UpdateDiscoveryCount();
            ShowDiscoveryPopup(speciesName);
            
            // Update collection UI
            if (collectionUI != null)
            {
                collectionUI.UpdateDiscovery(speciesName);
            }
        }

        if (factText != null)
        {
            if (factCoroutine != null)
            {
                StopCoroutine(factCoroutine);
            }
            factCoroutine = StartCoroutine(DisplayFactCoroutine(fact));
        }

        return isNewDiscovery;
    }

    private void ShowDiscoveryPopup(string speciesName)
    {
        if (discoveryPopupPanel != null && discoveryPopupText != null)
        {
            discoveryPopupText.text = $"New Discovery!\n{speciesName}";
            if (discoveryPopupCoroutine != null)
            {
                StopCoroutine(discoveryPopupCoroutine);
            }
            discoveryPopupCoroutine = StartCoroutine(DisplayDiscoveryPopupCoroutine());
        }
    }

    private void UpdateDiscoveryCount()
    {
        if (discoveryCountText != null)
        {
            discoveryCountText.text = $"Discoveries: {discoveredSpecies.Count}";
        }
    }

    private IEnumerator DisplayFactCoroutine(string fact)
    {
        factText.text = fact;
        
        // Fade in
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeDuration;
            factPanel.alpha = fadeInCurve.Evaluate(t);
            yield return null;
        }
        factPanel.alpha = 1f;

        // Wait for display duration
        yield return new WaitForSeconds(factDisplayDuration);

        // Fade out
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeDuration;
            factPanel.alpha = fadeOutCurve.Evaluate(t);
            yield return null;
        }
        factPanel.alpha = 0f;
        
        factCoroutine = null;
    }

    private IEnumerator DisplayDiscoveryPopupCoroutine()
    {
        // Fade in
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeDuration;
            discoveryPopupPanel.alpha = fadeInCurve.Evaluate(t);
            yield return null;
        }
        discoveryPopupPanel.alpha = 1f;

        // Wait for display duration
        yield return new WaitForSeconds(discoveryPopupDuration);

        // Fade out
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeDuration;
            discoveryPopupPanel.alpha = fadeOutCurve.Evaluate(t);
            yield return null;
        }
        discoveryPopupPanel.alpha = 0f;
        
        discoveryPopupCoroutine = null;
    }
}
