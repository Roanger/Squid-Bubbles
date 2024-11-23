using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeciesItemUI : MonoBehaviour
{
    [SerializeField] private Image speciesIcon;
    [SerializeField] private TextMeshProUGUI speciesNameText;
    [SerializeField] private Image backgroundImage;

    private void Awake()
    {
        // Ensure references
        if (speciesIcon == null)
            speciesIcon = transform.Find("SpeciesIcon")?.GetComponent<Image>();
        if (speciesNameText == null)
            speciesNameText = transform.Find("SpeciesName")?.GetComponent<TextMeshProUGUI>();
        if (backgroundImage == null)
            backgroundImage = GetComponent<Image>();
    }

    public void Initialize(string speciesName, Sprite icon)
    {
        if (speciesNameText != null)
            speciesNameText.text = speciesName;
        
        if (speciesIcon != null)
        {
            speciesIcon.sprite = icon;
            speciesIcon.enabled = icon != null;
        }
    }

    public void OnClick()
    {
        // Simple scale animation
        if (gameObject.activeInHierarchy)
            StartCoroutine(PulseAnimation());
    }

    private System.Collections.IEnumerator PulseAnimation()
    {
        Vector3 originalScale = transform.localScale;
        transform.localScale = originalScale * 1.1f;
        yield return new WaitForSeconds(0.1f);
        transform.localScale = originalScale;
    }
}
