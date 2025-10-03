using UnityEngine;

public class LavaDamage : MonoBehaviour
{
    [SerializeField] private int dañoPorLava = 10; // Daño que causa la lava al tocarla
    [SerializeField] private float tiempoEntreDaño = 0.5f; // Tiempo entre aplicaciones de daño
    private float tiempoUltimoDaño;
    private VidaJugador vidaJugador;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            vidaJugador = collision.gameObject.GetComponent<VidaJugador>();
            if (vidaJugador != null && Time.time >= tiempoUltimoDaño + tiempoEntreDaño)
            {
                vidaJugador.TomarDaño(dañoPorLava);
                tiempoUltimoDaño = Time.time;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (vidaJugador != null && Time.time >= tiempoUltimoDaño + tiempoEntreDaño)
            {
                vidaJugador.TomarDaño(dañoPorLava);
                tiempoUltimoDaño = Time.time;
            }
        }
    }
}