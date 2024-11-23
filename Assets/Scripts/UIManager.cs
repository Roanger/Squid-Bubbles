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
        if (factPanel != null) factPanel.color = new Color(factPanel.color.r, factPanel.color.g, factPanel.color.b, 0f);
        if (discoveryPanel != null) discoveryPanel.color = new Color(discoveryPanel.color.r, discoveryPanel.color.g, discoveryPanel.color.b, 0f);
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
            Color panelColor = factPanel.color;
            panelColor.a = t;
            factPanel.color = panelColor;
            yield return null;
        }
        SetPanelAlpha(factPanel, 1f);

        // Wait for display duration
        yield return new WaitForSeconds(factDisplayDuration);

        // Fade out
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = 1f - (elapsedTime / fadeDuration);
            Color panelColor = factPanel.color;
            panelColor.a = t;
            factPanel.color = panelColor;
            yield return null;
        }
        SetPanelAlpha(factPanel, 0f);
    }

    private IEnumerator DisplayDiscoveryCoroutine()
    {
        // Fade in
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeDuration;
            Color panelColor = discoveryPanel.color;
            panelColor.a = t;
            discoveryPanel.color = panelColor;
            yield return null;
        }
        SetPanelAlpha(discoveryPanel, 1f);

        // Wait for display duration
        yield return new WaitForSeconds(discoveryDisplayDuration);

        // Fade out
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = 1f - (elapsedTime / fadeDuration);
            Color panelColor = discoveryPanel.color;
            panelColor.a = t;
            discoveryPanel.color = panelColor;
            yield return null;
        }
        SetPanelAlpha(discoveryPanel, 0f);
    }

    private void SetPanelAlpha(Image panel, float alpha)
    {
        Color color = panel.color;
        color.a = alpha;
        panel.color = color;
    }
}
