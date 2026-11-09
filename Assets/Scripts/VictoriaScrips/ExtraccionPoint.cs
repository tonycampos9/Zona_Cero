
using UnityEngine;

public class ExtractionPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (MissionManager.Instance == null)
            return;

        if (MissionManager.Instance.MissionCompleted)
        {
            MissionManager.Instance.WinGame();
        }
    }
}
