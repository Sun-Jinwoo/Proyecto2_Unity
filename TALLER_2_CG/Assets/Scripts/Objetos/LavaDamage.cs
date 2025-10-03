using UnityEngine;

public class LavaDamage : MonoBehaviour
{
    [SerializeField] private int da�oPorLava = 10; // Da�o que causa la lava al tocarla
    [SerializeField] private float tiempoEntreDa�o = 0.5f; // Tiempo entre aplicaciones de da�o
    private float tiempoUltimoDa�o;
    private VidaJugador vidaJugador;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            vidaJugador = collision.gameObject.GetComponent<VidaJugador>();
            if (vidaJugador != null && Time.time >= tiempoUltimoDa�o + tiempoEntreDa�o)
            {
                vidaJugador.TomarDa�o(da�oPorLava);
                tiempoUltimoDa�o = Time.time;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (vidaJugador != null && Time.time >= tiempoUltimoDa�o + tiempoEntreDa�o)
            {
                vidaJugador.TomarDa�o(da�oPorLava);
                tiempoUltimoDa�o = Time.time;
            }
        }
    }
}