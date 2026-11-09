
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public string itemName = "Comida";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (InventoryManager.Instance != null)
            {
                InventoryManager.Instance.AddItem(itemName);
                Destroy(gameObject);
            }
        }
    }
}
