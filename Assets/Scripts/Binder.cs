using UnityEngine;
using UnityEngine.UI;

public class CollectableUISlotsBinder : MonoBehaviour
{
    [Tooltip("Ordered UI slots that show collected item sprites.")]
    public Image[] slots;

    void Start()
    {
        if (CollectableManager.Instance != null)
        {
            CollectableManager.Instance.RegisterUISlots(slots);
        }
        else
        {
            Debug.LogWarning("CollectableUISlotsBinder: No CollectableManager found in scene.");
        }
    }
}
