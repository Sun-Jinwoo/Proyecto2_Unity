using UnityEngine;

public class AutoDestruir : MonoBehaviour
{
    public float duracion = 0.5f;

    void Start()
    {
        Destroy(gameObject, duracion);
    }
}
