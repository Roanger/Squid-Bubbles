using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

[RequireComponent(typeof(SpeciesEntry))]
public class SpeciesDiscoveryAnimation : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private float revealDuration = 0.5f;
    [SerializeField] private float textFadeDuration = 0.3f;
    [SerializeField] private float iconScaleDuration = 0.4f;
    [SerializeField] private Ease revealEase = Ease.OutBack;
    
    [Header("Particle Effects")]
    [SerializeField] private ParticleSystem discoveryParticles;
    
    [Header("Sound Effects")]
    [SerializeField] private AudioClip discoverySound;
    
    private SpeciesEntry speciesEntry;
    private RectTransform rectTransform;
    private AudioSource audioSource;

    private void Awake()
    {
        speciesEntry = GetComponent<SpeciesEntry>();
        rectTransform = GetComponent<RectTransform>();
        
        // Add audio source if we have a discovery sound
        if (discoverySound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = discoverySound;
            audioSource.playOnAwake = false;
        }
    }

    public void PlayDiscoveryAnimation()
    {
        // Reset any ongoing animations
        DOTween.Kill(transform);
        
        // Initial setup
        rectTransform.localScale = Vector3.zero;
        
        // Scale animation sequence
        Sequence revealSequence = DOTween.Sequence();
        
        // Main reveal animation
        revealSequence.Append(rectTransform.DOScale(1.2f, revealDuration * 0.6f)
            .SetEase(revealEase));
        
        // Slight bounce back
        revealSequence.Append(rectTransform.DOScale(1f, revealDuration * 0.4f)
            .SetEase(Ease.OutBack));
        
        // Play particle effect if available
        if (discoveryParticles != null)
        {
            discoveryParticles.Play();
        }
        
        // Play sound effect if available
        if (audioSource != null && discoverySound != null)
        {
            audioSource.Play();
        }
    }

    public void PlayTextRevealAnimation(TextMeshProUGUI[] texts)
    {
        foreach (var text in texts)
        {
            if (text != null)
            {
                // Fade in text
                text.alpha = 0f;
                text.DOFade(1f, textFadeDuration).SetEase(Ease.OutQuad);
            }
        }
    }

    public void PlayIconRevealAnimation(Image icon)
    {
        if (icon != null)
        {
            // Initial setup
            icon.transform.localScale = Vector3.zero;
            
            // Scale up with bounce
            icon.transform.DOScale(1f, iconScaleDuration)
                .SetEase(Ease.OutBack);
        }
    }
}
