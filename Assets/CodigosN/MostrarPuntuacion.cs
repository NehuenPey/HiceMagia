using UnityEngine;
using UnityEngine.UI;

public class MostrarPuntuacion : MonoBehaviour
{
    public Text textoPuntuacion;

    private void Start()
    {
        int puntuacion = PlayerPrefs.GetInt("Puntuacion");
        textoPuntuacion.text = "Puntuación: " + puntuacion.ToString();
    }
}
