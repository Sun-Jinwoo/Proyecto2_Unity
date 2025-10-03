using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private float globalTime;

    public int scoreApple = 0;
    public int scoreBanana = 0;


    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

    }


    public int player;


}
