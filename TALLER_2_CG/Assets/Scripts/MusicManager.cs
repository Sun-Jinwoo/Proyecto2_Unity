using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;

    // Tus clips de música para cada escena
    public AudioClip musicaMenu;
    public AudioClip musicaPueblo;
    public AudioClip musicaBosque;
      public AudioClip musicaNieve;
    public AudioClip musicaLava;
  
    

    private void Awake()
    {
        // Asegurarse que solo exista un MusicManager
        if (FindObjectsOfType<MusicManager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        // Suscribirse al evento de escena cargada
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "menu":
                CambiarMusica(musicaMenu);
                break;
            case "pueblo":  
                CambiarMusica(musicaPueblo);
                break;
            case "bosque":
                CambiarMusica(musicaBosque);
                break;
            case "nieve":
                CambiarMusica(musicaNieve);
                break;
            case "lava":
                CambiarMusica(musicaLava);
                break;

  
        }
    }

    private void CambiarMusica(AudioClip nuevaMusica)
    {
        if (audioSource.clip == nuevaMusica) return; // No reiniciar si es la misma
        audioSource.clip = nuevaMusica;
        audioSource.Play();
    }
}
