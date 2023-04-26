using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PocionManager : MonoBehaviour
{
    // public float tiempoTotal = 120f;
    // private float tiempoRestante;

    // public Text textoTiempo;
    public Text textoPuntuacion;
    public Text textoMensaje;

    public int puntuacion = 0;

    public bool gameOver = false;

    private void Start()
    {
        // tiempoRestante = tiempoTotal;
        ActualizarUI();
    }

    private void Update()
    {
        if (!gameOver)
        {
            // tiempoRestante -= Time.deltaTime;
            ActualizarUI();

            // if (tiempoRestante <= 0)
            // {
                // gameOver = true;
                // textoMensaje.text = "Se acabó el tiempo! Tu puntuación final es: " + puntuacion;
            // }
        }
    }

    public void AumentarPuntuacion(int cantidad)
    {
        puntuacion += cantidad;
        ActualizarUI();
    }

    public void ReducirTiempo(float cantidad)
    {
        // tiempoRestante -= cantidad;
        ActualizarUI();
    }

    public void ActualizarUI()
    {
        // textoTiempo.text = "Tiempo: " + Mathf.RoundToInt(tiempoRestante);
        textoPuntuacion.text = "Puntuación: " + puntuacion;
    }

    public void Explotar()
    {
        // ReducirTiempo(3f);
        textoMensaje.text = "¡Oh no! La poción explotó...";
    }

    public void Gatos()
    {
        // tiempoRestante -= 10f;
        textoMensaje.text = "¡Maldición! ¡Aparecieron unos gatitos!";
    }

    public void Mariposas()
    {
        // ReducirTiempo(2f);
        textoMensaje.text = "¡Oh, qué bonitas mariposas!";
    }
}

