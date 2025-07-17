using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public string itemName;
    public Sprite itemSprite;
    public AudioClip collectSound; 
    public float soundVolume = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
          
            CollectableManager.Instance.CollectItem(itemName, itemSprite);

            
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position, soundVolume);
            }

            
            Destroy(gameObject);
        }
    }
}
