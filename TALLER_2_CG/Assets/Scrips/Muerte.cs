using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Muerte : MonoBehaviour
{

    TextMesh textMesh;
    
    public void LoadNumber()
    {
        SceneManager.LoadScene(1);
        if (GameManager.Instance == null)
        {
            GameObject gameManagerObject = new GameObject("GameManager");
            gameManagerObject.AddComponent<GameManager>();
        }
    }
    public void PrimerNivel()
    {
        SceneManager.LoadScene(2);
    }
    public void SegundoNivelBosque()
    {
        SceneManager.LoadScene(3);
    }
    public void TercerNivelNieve()
    {
        SceneManager.LoadScene(4);
    }
    public void UltimoNuvelLava()
    {
        SceneManager.LoadScene(5);
    }
}
