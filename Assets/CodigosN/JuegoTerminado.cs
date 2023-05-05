using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JuegoTerminado : MonoBehaviour
{
    public void VolverAJugar(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void Salir(){
        Application.Quit();
    }
}
