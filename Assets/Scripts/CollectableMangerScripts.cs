using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableManager : MonoBehaviour
{
    public static CollectableManager Instance;

    [Header("UI Settings")]
    public Image[] collectedItemSlots; // Assign in Inspector
    private List<Sprite> collectedSprites = new List<Sprite>();

    [Header("Required Collectables")]
    public int requiredItemCount = 2;

    private HashSet<string> collectedItems = new HashSet<string>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CollectItem(string itemName, Sprite itemSprite)
    {
        if (!collectedItems.Contains(itemName))
        {
            collectedItems.Add(itemName);
            collectedSprites.Add(itemSprite);
            UpdateUI();
            Debug.Log("Collected: " + itemName);
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < collectedItemSlots.Length; i++)
        {
            if (i < collectedSprites.Count)
            {
                collectedItemSlots[i].sprite = collectedSprites[i];
                collectedItemSlots[i].enabled = true;
            }
            else
            {
                collectedItemSlots[i].enabled = false;
            }
        }
    }

    public bool HasRequiredItems()
    {
        return collectedItems.Count >= requiredItemCount;
    }

    public void ResetCollectables()
    {
        collectedItems.Clear();
        collectedSprites.Clear();
        UpdateUI();
    }
}


