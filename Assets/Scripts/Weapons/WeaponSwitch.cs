

using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public GameObject[] weapons;

    private int currentWeapon = 0;

    void Start()
    {
        SelectWeapon(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SelectWeapon(0);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            SelectWeapon(1);
    }

    void SelectWeapon(int index)
    {
        if (weapons == null || index < 0 ||
            index >= weapons.Length) return;

        currentWeapon = index;

        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] != null)
                weapons[i].SetActive(i == currentWeapon);
        }
    }
}