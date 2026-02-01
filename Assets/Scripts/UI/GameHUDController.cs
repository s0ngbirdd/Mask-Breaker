using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
using System.Collections.Generic;

public class GameHUDController : MonoBehaviour
{
    [Header("UI Document")]
    [SerializeField] private UIDocument uiDocument;
    
    [Header("Heart Textures")]
    [SerializeField] private Texture2D heartFilledTexture;
    [SerializeField] private Texture2D heartEmptyTexture;
    
    [Header("Settings")]
    [SerializeField] private int maxHearts = 3;
    
    // Cached UI elements
    private List<VisualElement> hearts = new List<VisualElement>();
    private VisualElement progressFill;
    private VisualElement progressContainer;
    
    // State
    private int currentHearts;
    private float currentProgress;
    void OnEnable()
    {
        // Wait for UI to be ready
        StartCoroutine(InitializeUI());
    }
    
    IEnumerator InitializeUI()
    {
        // Wait one frame for UI to load
        yield return null;
        
        var root = uiDocument.rootVisualElement;
        HideHUD();
        // Cache heart elements
        hearts.Clear();
        for (int i = 1; i <= maxHearts; i++)
        {
            var heart = root.Q<VisualElement>($"heart-{i}");
            if (heart != null)
            {
                hearts.Add(heart);
            }
        }
        
        // Cache progress bar elements
        progressFill = root.Q<VisualElement>("progress-fill");
        progressContainer = root.Q<VisualElement>("progress-container");
        
        // Set initial state
        currentHearts = maxHearts;
        SetProgress(0f);
        
        Debug.Log($"HUD Initialized: {hearts.Count} hearts, progress bar found: {progressFill != null}");
    }

    public void ShowHUD()
    {
        var root = uiDocument.rootVisualElement;
        root.style.display = DisplayStyle.Flex;
    }

    public void HideHUD()
    {
        var root = uiDocument.rootVisualElement;
        root.style.display = DisplayStyle.None;
    }
    
    public void SetHearts(int filledHearts)
    {
        currentHearts = Mathf.Clamp(filledHearts, 0, maxHearts);
        
        for (int i = 0; i < hearts.Count; i++)
        {
            bool isFilled = i < currentHearts;
            
            hearts[i].RemoveFromClassList("heart-filled");
            hearts[i].RemoveFromClassList("heart-empty");
            hearts[i].AddToClassList(isFilled ? "heart-filled" : "heart-empty");
        
        }
    }
    public void LoseHeart()
    {
        SetHearts(currentHearts - 1);
        
        if (currentHearts >= 0 && currentHearts < hearts.Count)
        {
            StartCoroutine(HeartDamageAnimation(hearts[currentHearts]));
        }
    }

    public void GainHeart()
    {
        SetHearts(currentHearts + 1);
    }
    
    public void SetProgress(float percent)
    {
        currentProgress = Mathf.Clamp(percent, 0f, 100f);
    
        if (progressFill != null)
        {
            float minHeight = 8f;
            float maxHeight = 85f; 
            
            float fillHeight = Mathf.Lerp(minHeight, maxHeight, currentProgress / 100f);
            progressFill.style.height = new Length(fillHeight, LengthUnit.Percent);
        }
    }
    
    public void SetProgressAnimated(float targetPercent, float duration = 0.5f)
    {
        StartCoroutine(AnimateProgress(targetPercent, duration));
    }
    
    IEnumerator AnimateProgress(float targetPercent, float duration)
    {
        float startPercent = currentProgress;
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            
            // Ease out
            t = 1f - Mathf.Pow(1f - t, 3f);
            
            SetProgress(Mathf.Lerp(startPercent, targetPercent, t));
            yield return null;
        }
        
        SetProgress(targetPercent);
    }
    
    IEnumerator HeartDamageAnimation(VisualElement heart)
    {
        heart.AddToClassList("heart-damaged");
        yield return new WaitForSeconds(0.1f);
        heart.RemoveFromClassList("heart-damaged");
    }

    void OnDisable()
    {
        // Clean up UI references
        progressFill = null;
        hearts.Clear();
    }
}
