using UnityEngine;

public class ZonaCero : MonoBehaviour
{
    [Header("Configuracion de la zona")]
    public float radioInicial = 25f;
    public float radioMinimo = 8f;
    public float tiempoAntesDeCerrar = 20f;
    public float velocidadCierre = 0.5f;

    [Header("Daño")]
    public float danoPorSegundo = 5f;

    [Header("Borde circular")]
    public LineRenderer bordeZona;
    public int segmentos = 100;
    public float grosorBorde = 0.08f;

    private float radioActual;
    private float temporizador;
    private Transform jugador;
    private PlayerStats estadisticas;

    [Header("Niebla toxica")]
    public SpriteRenderer spriteNiebla;

    private Material materialNiebla;

    void Start()
    {
        radioActual = radioInicial;

        GameObject objetoJugador =
            GameObject.FindGameObjectWithTag("Player");

        if (objetoJugador != null)
        {
            jugador = objetoJugador.transform;
            estadisticas = objetoJugador.GetComponent<PlayerStats>();
        }
        if (spriteNiebla != null)
        {
            materialNiebla = spriteNiebla.material;
        }
        if (bordeZona == null)
            bordeZona = GetComponentInChildren<LineRenderer>();

        if (bordeZona != null)
        {
            bordeZona.useWorldSpace = false;
            bordeZona.loop = true;
            bordeZona.startWidth = grosorBorde;
            bordeZona.endWidth = grosorBorde;
        }

        ActualizarBorde();
    }

    void Update()
    {
        temporizador += Time.deltaTime;

        if (temporizador >= tiempoAntesDeCerrar)
        {
            radioActual = Mathf.Max(
                radioMinimo,
                radioActual - velocidadCierre * Time.deltaTime
            );
        }

        ActualizarBorde();
        ComprobarJugador();
        ActualizarNiebla();
    }

    void ActualizarBorde()
    {
        if (bordeZona == null)
            return;

        bordeZona.positionCount = segmentos;

        for (int i = 0; i < segmentos; i++)
        {
            float angulo =
                (float)i / segmentos * Mathf.PI * 2f;

            float x = Mathf.Cos(angulo) * radioActual;
            float y = Mathf.Sin(angulo) * radioActual;

            bordeZona.SetPosition(
                i,
                new Vector3(x, y, 0)
            );
        }
    }

    void ComprobarJugador()
    {
        if (jugador == null || estadisticas == null)
            return;

        float distancia = Vector2.Distance(
            transform.position,
            jugador.position
        );

        if (distancia > radioActual)
        {
            estadisticas.TakeDamage(
                danoPorSegundo * Time.deltaTime
            );
        }
    }

    void ActualizarNiebla()
    {
        if (materialNiebla != null)
        {
            materialNiebla.SetFloat("_Safe_Radius", radioActual);
        }
    }


}