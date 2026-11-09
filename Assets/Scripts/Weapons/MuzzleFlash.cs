using UnityEngine;
using System.Collections;

public class MuzzleFlash : MonoBehaviour
{
    public GameObject flashObject;
    public float flashDuration = 0.06f;

    public void PlayFlash()
    {
        if (flashObject == null) return;

        StopAllCoroutines();
        StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        flashObject.SetActive(true);

        yield return new WaitForSeconds(flashDuration);

        flashObject.SetActive(false);
    }
}