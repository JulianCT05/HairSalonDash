using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public string itemName;
    public Sprite itemSprite;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CollectableManager.Instance.CollectItem(itemName, itemSprite);
            Destroy(gameObject);
        }
    }
}