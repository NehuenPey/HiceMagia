using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tiempo : MonoBehaviour
{
    public Image imagenCirculo;
    public float duracion;
    private float tiempoRestante;
    public static bool tiempoTerminado = false;

    public Puntuacion puntuacion;

    private void Start()
    {
        tiempoRestante = duracion;
        tiempoTerminado = false;

        puntuacion = FindObjectOfType<Puntuacion>();
    }

    private void Update()
    {
        if (!tiempoTerminado)
        {
            tiempoRestante -= Time.deltaTime;
            imagenCirculo.fillAmount = tiempoRestante / duracion;
            if (tiempoRestante <= 0)
            {
                tiempoTerminado = true;
                Collider[] colliders = FindObjectsOfType<Collider>();
                foreach (Collider collider in colliders)
                {
                    collider.enabled = false;
                }

                // Mostrar mensaje de juego finalizado y puntuación
                puntuacion.textoPuntuacion.text = "Puntuación: " + puntuacion.puntuacion.ToString();

                CambioEscena();

            }
        }
    }
    public void ReducirTiempo(float valor)
{
    tiempoRestante -= valor;
    if (tiempoRestante < 0)
    {
        tiempoRestante = 0;
        tiempoTerminado = true;
        Collider[] colliders = FindObjectsOfType<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }

    }
    else
    {
        imagenCirculo.fillAmount = tiempoRestante / duracion;
    }
}


    private void CambioEscena()
    {
        SceneManager.LoadScene("PantallaFinal", LoadSceneMode.Single);
        PlayerPrefs.SetInt("Puntuacion", puntuacion.puntuacion);
    }

}
