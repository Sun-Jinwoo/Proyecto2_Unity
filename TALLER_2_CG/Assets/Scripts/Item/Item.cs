using UnityEngine;

public class Item : MonoBehaviour
{
    public string nombre;
    public int puntos;
    public bool otorgaVida;

    public float tiempoExtra = 5f; // Valor por defecto, puedes cambiarlo en el inspector

    public enum TipoItem { Puntos, RelojPositivo, RelojNegativo }
    public TipoItem tipo = TipoItem.Puntos;

    public GameObject efectoFeedback;

    private void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Player"))
        {
            switch (tipo)
            {
                case TipoItem.Puntos:
                    GameControllerBosque.Instancia.AgregarItem(gameObject.name, puntos);
                    break;

                case TipoItem.RelojPositivo:
                    GameControllerBosque.Instancia.SumarTiempo(tiempoExtra);
                    break;

                case TipoItem.RelojNegativo:
                    GameControllerBosque.Instancia.SumarTiempo(-tiempoExtra);
                    break;
            }
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
}
