using System.Collections.Generic;
using UnityEngine;

public class Caldero : MonoBehaviour
{
    public List<Ingrediente> ingredientesEnCaldero = new List<Ingrediente>();
    public int tiempoRestante;

    private PocionManager pocionManager;

    private void Start()
    {
        pocionManager = FindObjectOfType<PocionManager>();
    }

    public void AgregarIngrediente(Ingrediente ingrediente)
    {
        ingredientesEnCaldero.Add(ingrediente);
        pocionManager.ActualizarUI();
    }

    public void VaciarCaldero()
    {
        ingredientesEnCaldero.Clear();
        pocionManager.ActualizarUI();
    }

    public bool ComprobarPocion()
    {
        // Comprobamos si hay dos ingredientes en el caldero
        if (ingredientesEnCaldero.Count != 2)
        {
            return false;
        }

        // Comprobamos si la combinación de ingredientes es válida
        if (CombinacionInvalida())
        {
            return false;
        }

        // Si llegamos aquí, la poción es válida
        return true;
    }

    private bool CombinacionInvalida()
    {
        bool ingredienteRojo = false;
        bool ingredienteAmarillo = false;
        bool ingredienteAzul = false;
        bool ingredienteBlanco = false;

        // Recorremos la lista de ingredientes en el caldero para comprobar su combinación
        foreach (Ingrediente ingrediente in ingredientesEnCaldero)
        {
            if (ingrediente.color == Color.red)
            {
                ingredienteRojo = true;
            }
            else if (ingrediente.color == Color.yellow)
            {
                ingredienteAmarillo = true;
            }
            else if (ingrediente.color == Color.blue)
            {
                ingredienteAzul = true;
            }
            else if (ingrediente.color == Color.white)
            {
                ingredienteBlanco = true;
            }
        }

        // Comprobamos si hay una combinación inválida de ingredientes
        if (ingredienteRojo && ingredienteAmarillo && !ingredienteAzul && !ingredienteBlanco)
        {
            // Amarillo + rojo = explosión
            pocionManager.Explotar();
            return true;
        }
        else if (ingredienteAmarillo && ingredienteBlanco && !ingredienteRojo && !ingredienteAzul)
        {
            // Amarillo + blanco = gatitos
            pocionManager.Gatos();
            return true;
        }
        else if (ingredienteBlanco && ingredienteAzul && !ingredienteRojo && !ingredienteAmarillo)
        {
            // Blanco + azul = mariposas
            pocionManager.Mariposas();
            return true;
        }

        // La combinación es válida
        return false;
    }
}
