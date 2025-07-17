using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CollectableManager : MonoBehaviour
{
    public static CollectableManager Instance;

    [Header("Scene UI Slots (auto-rebound)")]
    [SerializeField] private Image[] collectedItemSlots; 
    private readonly List<Sprite> collectedSprites = new List<Sprite>();

    [Header("Required Collectables")]
    public int requiredItemCount = 2;

    private readonly HashSet<string> collectedItems = new HashSet<string>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (Instance == this)
            SceneManager.sceneLoaded -= OnSceneLoaded;
    }

   
    public void CollectItem(string itemName, Sprite itemSprite)
    {
        if (string.IsNullOrEmpty(itemName)) itemName = $"Item_{collectedItems.Count}";

        if (!collectedItems.Contains(itemName))
        {
            collectedItems.Add(itemName);
            collectedSprites.Add(itemSprite);
            UpdateUI();
            Debug.Log("Collected: " + itemName);
        }
        else
        {
            Debug.Log("Duplicate collectable ignored: " + itemName);
        }
    }

    
    public void RegisterUISlots(Image[] newSlots)
    {
        collectedItemSlots = newSlots;
        UpdateUI(); 
    }

    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
      
        var binder = FindFirstObjectByType<CollectableUISlotsBinder>();
        if (binder != null)
        {
            RegisterUISlots(binder.slots);
        }
        else
        {
          
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        if (collectedItemSlots == null || collectedItemSlots.Length == 0)
            return;

        for (int i = 0; i < collectedItemSlots.Length; i++)
        {
            Image img = collectedItemSlots[i];
            if (!img) continue; // destroyed scene ref

            if (i < collectedSprites.Count)
            {
                img.sprite = collectedSprites[i];
                img.enabled = true;
            }
            else
            {
                img.enabled = false;
            }
        }
    }


    public bool HasRequiredItems() => collectedItems.Count >= requiredItemCount;

    public void ResetCollectables()
    {
        collectedItems.Clear();
        collectedSprites.Clear();
        UpdateUI();
    }
}
