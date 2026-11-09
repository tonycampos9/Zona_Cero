
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Velocidades")]
    public float speed = 5f;
    public float runSpeed = 10f;

    [Header("Energia")]
    public float energyDrainPerSecond = 20f;
    public float energyRecoveryPerSecond = 12f;

    private Rigidbody2D rb;
    private PlayerStats playerStats;
    private Animator animator;

    private Vector2 movement;
    private bool wantsToRun;
    private bool exhausted;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();

        // Busca el Animator dentro del jugador o sus hijos.
        animator = GetComponentInChildren<Animator>();

        if (animator == null)
        {
            Debug.LogWarning("No se encontro el Animator del jugador.");
        }
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement = movement.normalized;

        wantsToRun = Input.GetKey(KeyCode.LeftShift);

        // Actualizar direccion para las animaciones.
        if (animator != null)
        {
            animator.SetFloat("MoveX", movement.x);
            animator.SetFloat("MoveY", movement.y);
        }
    }

    void FixedUpdate()
    {
        if (playerStats == null) return;

        // Comprobar agotamiento.
        if (playerStats.CurrentEnergy <= 0)
        {
            exhausted = true;
        }

        // Recuperar energia para volver a correr.
        if (exhausted && playerStats.CurrentEnergy >= 20)
        {
            exhausted = false;
        }

        bool isMoving = movement.sqrMagnitude > 0;

        bool isRunning =
            wantsToRun &&
            isMoving &&
            !exhausted;

        float currentSpeed = isRunning ? runSpeed : speed;

        // Movimiento del jugador.
        rb.MovePosition(
            rb.position +
            movement * currentSpeed * Time.fixedDeltaTime
        );

        // Actualizar velocidad de animacion.
        if (animator != null)
        {
            bool hasHorizontalMovement =
                Mathf.Abs(movement.x) > 0.01f;

            float animationSpeed = 0f;

            if (hasHorizontalMovement)
            {
                animationSpeed = isRunning ? 2f : 1f;
            }

            animator.SetFloat("Speed", animationSpeed);
        }

        // Consumo y recuperacion de energia.
        if (isRunning)
        {
            playerStats.UseEnergy(
                energyDrainPerSecond * Time.fixedDeltaTime
            );
        }
        else
        {
            playerStats.RestoreEnergy(
                energyRecoveryPerSecond * Time.fixedDeltaTime
            );
        }
    }
}
