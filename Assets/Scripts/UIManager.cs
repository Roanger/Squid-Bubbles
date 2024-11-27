using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image factPanel;
    [SerializeField] private TextMeshProUGUI factText;
    [SerializeField] private Image discoveryPanel;
    [SerializeField] private TextMeshProUGUI discoveryText;
    [SerializeField] private TextMeshProUGUI counterText;

    [Header("Animation Settings")]
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float displayDuration = 5f;
    [SerializeField] private AnimationCurve fadeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private HashSet<string> discoveredSpecies = new HashSet<string>();
    private Coroutine fadeCoroutine;

    void Start()
    {
        // Ensure UI starts hidden
        SetPanelAlpha(factPanel, factText, 0);
        SetPanelAlpha(discoveryPanel, discoveryText, 0);
        UpdateCounter();
    }
 
    public bool ShowFact(string fact, string speciesName)
    {
        bool isNewDiscovery = !discoveredSpecies.Contains(speciesName);
        
        if (isNewDiscovery)
        {
            discoveredSpecies.Add(speciesName);
            UpdateCounter();
            discoveryText.text = $"New Discovery!\n{speciesName}";
        }
        else
        {
            discoveryText.text = $"Encountered\n{speciesName}";
        }

        // Show the fact
        factText.text = fact;

        // Stop any existing fade
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        // Start new fade sequence
        fadeCoroutine = StartCoroutine(FadeSequence(isNewDiscovery));

        return isNewDiscovery;
    }

    private IEnumerator FadeSequence(bool isNewDiscovery)
    {
        // Fade in both panels
        float elapsed = 0;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = fadeCurve.Evaluate(elapsed / fadeDuration);
            
            SetPanelAlpha(factPanel, factText, alpha);
            SetPanelAlpha(discoveryPanel, discoveryText, alpha);  // Always fade discovery panel
            
            yield return null;
        }

        // Ensure panels are fully visible
        SetPanelAlpha(factPanel, factText, 1f);
        SetPanelAlpha(discoveryPanel, discoveryText, 1f);  // Always set to full opacity

        // Hold
        yield return new WaitForSeconds(displayDuration);

        // Fade out
        elapsed = 0;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = 1 - fadeCurve.Evaluate(elapsed / fadeDuration);
            
            SetPanelAlpha(factPanel, factText, alpha);
            SetPanelAlpha(discoveryPanel, discoveryText, alpha);  // Always fade out discovery panel
            
            yield return null;
        }

        // Ensure fully hidden
        SetPanelAlpha(factPanel, factText, 0);
        SetPanelAlpha(discoveryPanel, discoveryText, 0);
    }

    private void SetPanelAlpha(Image panel, TextMeshProUGUI text, float alpha)
    {
        if (panel)
        {
            Color color = panel.color;
            color.a = alpha;
            panel.color = color;
            Debug.Log($"[UIManager] Setting {panel.gameObject.name} alpha to {alpha}");
        }

        if (text)
        {
            Color color = text.color;
            color.a = alpha;
            text.color = color;
        }
    }

    private void UpdateCounter()
    {
        if (counterText)
        {
            counterText.text = $"Discoveries: {discoveredSpecies.Count}";
        }
    }

    public bool IsSpeciesDiscovered(string speciesName)
    {
        return discoveredSpecies.Contains(speciesName);
    }
}
