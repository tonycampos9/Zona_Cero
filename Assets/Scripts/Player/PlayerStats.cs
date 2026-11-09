
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("Valores del jugador")]
    public float maxHealth = 100f;
    public float maxEnergy = 100f;

    private float currentHealth;
    private float currentEnergy;

    public float CurrentEnergy => currentEnergy;

    [Header("Interfaz")]
    public Slider healthSlider;
    public Slider energySlider;

    void Start()
    {
        currentHealth = maxHealth;
        currentEnergy = maxEnergy;

        if (healthSlider != null)
        {
            healthSlider.minValue = 0;
            healthSlider.maxValue = maxHealth;
        }

        if (energySlider != null)
        {
            energySlider.minValue = 0;
            energySlider.maxValue = maxEnergy;
        }

        UpdateUI();
    }

    public void TakeDamage(float amount)
    {
        currentHealth = Mathf.Clamp(
            currentHealth - amount, 0, maxHealth
        );

        UpdateUI();
    }

    public void RestoreHealth(float amount)
    {
        currentHealth = Mathf.Clamp(
            currentHealth + amount, 0, maxHealth
        );

        UpdateUI();
    }

    public void UseEnergy(float amount)
    {
        currentEnergy = Mathf.Clamp(
            currentEnergy - amount, 0, maxEnergy
        );

        UpdateUI();
    }

    public void RestoreEnergy(float amount)
    {
        currentEnergy = Mathf.Clamp(
            currentEnergy + amount, 0, maxEnergy
        );

        UpdateUI();
    }

    void UpdateUI()
    {
        if (healthSlider != null)
            healthSlider.value = currentHealth;

        if (energySlider != null)
            energySlider.value = currentEnergy;
    }



}
