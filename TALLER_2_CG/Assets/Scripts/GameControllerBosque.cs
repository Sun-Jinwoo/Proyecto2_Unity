using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System;

public class GameControllerBosque : MonoBehaviour
{
    public static GameControllerBosque Instancia;

    [SerializeField] private TextMeshProUGUI textoPuntaje;
    [SerializeField] private TextMeshProUGUI textoTiempo;
    [SerializeField] private TextMeshProUGUI textoVidas;


    private int puntaje = 0;
    private int vidas = 3;
    private float tiempo = 0f;

    private List<string> items = new List<string>();

    private void Awake()
    {
        Instancia = this;
    }

    void Update()
    {
        tiempo += Time.deltaTime;
        textoTiempo.text = "Tiempo: " + tiempo.ToString("F1");
    }



    public void AgregarItem(string nombre, int puntos)
    {
        items.Add(nombre);
        puntaje += puntos;
        textoPuntaje.text = "Puntaje: " + puntaje;
    }

    public void SumarVidaJugador(int cantidad)
    {
        vidas += cantidad;
        textoVidas.text = "Vidas: " + vidas;
    }

    public void QuitarVidaJugador(int cantidad)
    {
        vidas -= cantidad;
        textoVidas.text = "Vidas: " + vidas;
    }

    public void SumarTiempo(float segundos)
{
    tiempo += segundos;
    if (tiempo < 0) tiempo = 0; // Evita que el tiempo quede en negativo
    textoTiempo.text = "Tiempo: " + tiempo.ToString("F1");
}


    internal void PlayerTakeDamage(int v)
    {
        throw new NotImplementedException();
    }

    internal void SumarTiempo(object tiempoExtra)
    {
        throw new NotImplementedException();
    }
}
