using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoAmount = 24;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (WeaponManager.Instance != null)
        {
            WeaponManager.Instance.AddAmmo(ammoAmount);
            Destroy(gameObject);
        }
    }
}