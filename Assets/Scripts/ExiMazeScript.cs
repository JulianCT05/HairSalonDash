using UnityEngine;

public class ExitPortal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (CollectableManager.Instance.HasRequiredItems())
            {
                CollectableManager.Instance.ResetCollectables();
                RandomLevelLoader.Instance.LoadRandomLevel();
            }
            else
            {
                Debug.Log("You need more items before exiting!");
            }
        }
    }
}
