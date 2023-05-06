using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puntuacion : MonoBehaviour
{
    public Text textoPuntuacion;

    public int puntuacion = 0;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void IncrementarPuntuacion(int cantidad)
    {
        puntuacion += cantidad;
        ActualizarUI();
    }

    public void ReducirPuntuacion(int cantidad)
    {
        puntuacion -= cantidad;
        ActualizarUI();
    }

    public void ActualizarUI()
    {
        textoPuntuacion.text = "Puntuación: " + puntuacion;
    }

}

