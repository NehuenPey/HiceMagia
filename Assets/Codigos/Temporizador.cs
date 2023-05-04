using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temporizador : MonoBehaviour
{
    public float tiempoTotal = 60f;
    private float tiempoRestante;
    public Text textoTemporizador;
    public bool tiempoAcabado = false;

    void Start()
    {
        tiempoRestante = tiempoTotal;
    }

    void Update()
    {
        if (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;
            ActualizarTemporizador();
        }
        else
        {
            tiempoRestante = 0;
            tiempoAcabado = true;
            textoTemporizador.text = "Tiempo acabado";
        }
    }

    void ActualizarTemporizador()
    {
        int minutos = Mathf.FloorToInt(tiempoRestante / 60);
        int segundos = Mathf.FloorToInt(tiempoRestante % 60);

        textoTemporizador.text = string.Format("Tiempo restante: {0:0}:{1:00}", minutos, segundos);
    }

    public void ReiniciarTemporizador()
    {
        tiempoRestante = tiempoTotal;
        tiempoAcabado = false;
    }
}
