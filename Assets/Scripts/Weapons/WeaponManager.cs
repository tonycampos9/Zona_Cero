using UnityEngine;
using TMPro;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;

    [Header("Interfaz")]
    public TextMeshProUGUI ammoText;

    [Header("Municion")]
    public int currentAmmo = 12;
    public int reserveAmmo = 60;
    public int magazineSize = 12;

    [Header("Configuracion")]
    public float reloadTime = 1.5f;

    private bool isReloading = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateAmmoUI();
    }

    public bool CanShoot()
    {
        return currentAmmo > 0 && !isReloading;
    }

    public void UseAmmo()
    {
        if (!CanShoot()) return;

        currentAmmo--;
        UpdateAmmoUI();
    }

    public void AddAmmo(int amount)
    {
        reserveAmmo += amount;
        UpdateAmmoUI();
    }

    public void Reload()
    {
        if (isReloading) return;
        if (currentAmmo >= magazineSize) return;
        if (reserveAmmo <= 0) return;

        StartCoroutine(ReloadRoutine());
    }

    System.Collections.IEnumerator ReloadRoutine()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        int neededAmmo = magazineSize - currentAmmo;
        int ammoToLoad = Mathf.Min(neededAmmo, reserveAmmo);

        currentAmmo += ammoToLoad;
        reserveAmmo -= ammoToLoad;

        isReloading = false;
        UpdateAmmoUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = "MUNICION: " +
                currentAmmo + " / " + reserveAmmo;
        }
    }
}