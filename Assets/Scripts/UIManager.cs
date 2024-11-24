using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Fact Panel")]
    [SerializeField] private TextMeshProUGUI factText;
    [SerializeField] private Image factPanel;

    [Header("Discovery Notification")]
    [SerializeField] private TextMeshProUGUI discoveryText;
    [SerializeField] private Image discoveryPanel;
    
    [Header("Discovery Counter")]
    [SerializeField] private TextMeshProUGUI counterText;
    
    [Header("Animation Settings")]
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float factDisplayDuration = 5f;
    [SerializeField] private float discoveryDisplayDuration = 2f;
    
    private HashSet<string> discoveredSpecies = new HashSet<string>();
    private Coroutine factCoroutine;
    private Coroutine discoveryCoroutine;

    private void Start()
    {
        // Ensure UI elements are visible
        if (factPanel != null)
        {
            Canvas factCanvas = factPanel.GetComponentInParent<Canvas>();
            if (factCanvas != null)
            {
                factCanvas.sortingLayerName = "UI";
                factCanvas.sortingOrder = 100;
            }
        }
        
        if (discoveryPanel != null)
        {
            Canvas discoveryCanvas = discoveryPanel.GetComponentInParent<Canvas>();
            if (discoveryCanvas != null)
            {
                discoveryCanvas.sortingLayerName = "UI";
                discoveryCanvas.sortingOrder = 100;
            }
        }

        // Initialize panel alphas
        SetPanelAlpha(factPanel, factText, 0f);
        SetPanelAlpha(discoveryPanel, discoveryText, 0f);
        UpdateDiscoveryCounter();
    }

    public bool ShowFact(string fact, string speciesName)
    {
        bool isNewDiscovery = !discoveredSpecies.Contains(speciesName);
        
        if (isNewDiscovery)
        {
            discoveredSpecies.Add(speciesName);
            UpdateDiscoveryCounter();
            ShowDiscoveryNotification(speciesName);
        }

        // Show the fact
        if (factText != null && factPanel != null)
        {
            factText.text = fact;
            if (factCoroutine != null)
            {
                StopCoroutine(factCoroutine);
            }
            factCoroutine = StartCoroutine(DisplayFactCoroutine());
        }

        return isNewDiscovery;
    }

    private void UpdateDiscoveryCounter()
    {
        if (counterText != null)
        {
            counterText.text = $"Discoveries: {discoveredSpecies.Count}";
        }
    }

    private void ShowDiscoveryNotification(string speciesName)
    {
        if (discoveryText != null && discoveryPanel != null)
        {
            discoveryText.text = $"New Discovery!\n{speciesName}";
            if (discoveryCoroutine != null)
            {
                StopCoroutine(discoveryCoroutine);
            }
            discoveryCoroutine = StartCoroutine(DisplayDiscoveryCoroutine());
        }
    }

    private IEnumerator DisplayFactCoroutine()
    {
        // Fade in
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeDuration;
            SetPanelAlpha(factPanel, factText, t);
            yield return null;
        }
        SetPanelAlpha(factPanel, factText, 1f);

        // Wait for display duration
        yield return new WaitForSeconds(factDisplayDuration);

        // Fade out
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = 1f - (elapsedTime / fadeDuration);
            SetPanelAlpha(factPanel, factText, t);
            yield return null;
        }
        SetPanelAlpha(factPanel, factText, 0f);
    }

    private IEnumerator DisplayDiscoveryCoroutine()
    {
        // Fade in
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeDuration;
            SetPanelAlpha(discoveryPanel, discoveryText, t);
            yield return null;
        }
        SetPanelAlpha(discoveryPanel, discoveryText, 1f);

        // Wait for display duration
        yield return new WaitForSeconds(discoveryDisplayDuration);

        // Fade out
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = 1f - (elapsedTime / fadeDuration);
            SetPanelAlpha(discoveryPanel, discoveryText, t);
            yield return null;
        }
        SetPanelAlpha(discoveryPanel, discoveryText, 0f);
    }

    private void SetPanelAlpha(Image panel, TextMeshProUGUI text, float alpha)
    {
        if (panel != null)
        {
            Color panelColor = panel.color;
            panelColor.a = alpha;
            panel.color = panelColor;
        }
        
        if (text != null)
        {
            Color textColor = text.color;
            textColor.a = alpha;
            text.color = textColor;
        }
    }
}
