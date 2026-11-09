
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("Interfaz")]
    public TextMeshProUGUI inventoryText;

    [Header("Jugador")]
    public PlayerStats playerStats;

    private int food = 0;
    private int medicine = 0;
    // private int ammo = 0;
    private int supplies = 0;


    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateInventoryUI();
    }

    public void AddItem(string itemName)
    {
        switch (itemName.ToLower())
        {
            case "comida":
                food++;
                break;

            case "botiquin":
            case "botiquín":
                medicine++;
                break;

           // case "municion":
           // case "munición":
            //    ammo++;
             //   break;

            case "suministro":
                supplies++;

                if (MissionManager.Instance != null)
                {
                    MissionManager.Instance.AddSupply();
                }

                break;

            default:
                Debug.Log("Objeto desconocido: " + itemName);
                return;
        }

        Debug.Log("Recogiste: " + itemName);
        UpdateInventoryUI();
    }

    public void UseFood()
    {
        if (food <= 0 || playerStats == null)
            return;

        food--;
        playerStats.RestoreEnergy(25);
        UpdateInventoryUI();
    }

    public void UseMedicine()
    {
        if (medicine <= 0 || playerStats == null)
            return;

        medicine--;
        playerStats.RestoreHealth(30);
        UpdateInventoryUI();
    }

    void UpdateInventoryUI()
    {
        if (inventoryText != null)
        {
            inventoryText.text =
                "INVENTARIO\n" +
                "Comida: " + food + "\n" +
                "Botiquines: " + medicine + "\n" +
                //"Munición: " + ammo + "\n" +
                "Suministros: " + supplies;
        }
    }
}