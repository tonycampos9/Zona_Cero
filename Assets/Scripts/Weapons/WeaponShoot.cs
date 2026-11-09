using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    [Header("Configuracion del disparo")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 0.25f;
    public MuzzleFlash muzzleFlash;
    private float nextFireTime = 0f;

    void Update()
    {
        if (Time.timeScale == 0) return;

        if (Input.GetMouseButton(0) &&
            Time.time >= nextFireTime)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (projectilePrefab == null || firePoint == null) return;
        if (WeaponManager.Instance == null) return;
        if (!WeaponManager.Instance.CanShoot()) return;

        WeaponManager.Instance.UseAmmo();

        GameObject projectile = Instantiate(
            projectilePrefab,
            firePoint.position,
            firePoint.rotation
        );
        if (muzzleFlash != null)
        {
            muzzleFlash.PlayFlash();

        }
        Projectile2D projectileScript =
            projectile.GetComponent<Projectile2D>();

        if (projectileScript != null)
        {
            projectileScript.SetDirection(firePoint.right);
        }

        nextFireTime = Time.time + fireRate;
    }
}