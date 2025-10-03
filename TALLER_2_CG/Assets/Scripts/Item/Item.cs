using UnityEngine;

public class Item : MonoBehaviour
{
    public string nombre;
    public int puntos;
    public bool otorgaVida;
    public GameObject efectoFeedback; 

    private void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Player"))
        {
            if (otorgaVida)
            {
                GameControllerBosque.Instancia.SumarVidaJugador(1);
            }

            GameControllerBosque.Instancia.AgregarItem(nombre, puntos);

            
            if (efectoFeedback != null)
            {
                Instantiate(efectoFeedback, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
