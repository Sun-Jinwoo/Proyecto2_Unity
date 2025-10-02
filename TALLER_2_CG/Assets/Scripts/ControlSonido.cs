using UnityEngine;

public class ControlSonido : MonoBehaviour
{
public static ControlSonido instancia;
    private AudioSource audioSource;

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void ReproducirSonido(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
