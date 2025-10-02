using UnityEngine;

public class RecoleccionFruta : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Reproducir el sonido de recolección
            if (audioSource != null)
            {
                audioSource.Play();
            }

            // Desactivar el objeto de fruta
            gameObject.SetActive(false);
        }
    }
}
