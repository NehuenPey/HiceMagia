using UnityEngine;

[CreateAssetMenu(fileName = "Nueva Pocion", menuName = "Pocion")]
public class Pocion : ScriptableObject
{
    public string nombre;
    public Color color;
    public Sprite imagen;
    public Ingrediente ingrediente1;
    public Ingrediente ingrediente2;

    public bool ComprobarIngredientes(Ingrediente i1, Ingrediente i2)
    {
        if ((i1 == ingrediente1 && i2 == ingrediente2) || (i1 == ingrediente2 && i2 == ingrediente1))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}