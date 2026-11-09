
using UnityEngine;
using TMPro;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance;

    [Header("Interfaz")]
    public TextMeshProUGUI missionText;
    public GameObject victoryPanel;

    [Header("Extraccion")]
    public GameObject extractionPoint;

    [Header("Mision")]
    public int requiredSupplies = 3;

    private int collectedSupplies = 0;
    private bool missionCompleted = false;
    private bool gameWon = false;

    public bool MissionCompleted
    {
        get { return missionCompleted; }
    }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (extractionPoint != null)
            extractionPoint.SetActive(false);

        if (victoryPanel != null)
            victoryPanel.SetActive(false);

        UpdateMissionUI();
    }

    public void AddSupply()
    {
        if (missionCompleted)
            return;

        collectedSupplies++;

        if (collectedSupplies >= requiredSupplies)
        {
            collectedSupplies = requiredSupplies;
            missionCompleted = true;

            Debug.Log("¡MISION COMPLETADA!");

            if (extractionPoint != null)
                extractionPoint.SetActive(true);
        }

        UpdateMissionUI();
    }

    public void WinGame()
    {
        if (!missionCompleted || gameWon)
            return;

        gameWon = true;

        if (victoryPanel != null)
            victoryPanel.SetActive(true);

        Time.timeScale = 0f;

        Debug.Log("¡HAS GANADO!");
    }

    void UpdateMissionUI()
    {
        if (missionText == null)
            return;

        if (missionCompleted)
        {
            missionText.text =
                "MISION COMPLETADA\nVe al punto de extraccion";
        }
        else
        {
            missionText.text =
                "MISION\nReunir suministros: " +
                collectedSupplies + "/" + requiredSupplies;
        }
    }
}
