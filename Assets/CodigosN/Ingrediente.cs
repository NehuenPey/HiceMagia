using UnityEngine;

[CreateAssetMenu(fileName = "Nuevo Ingrediente", menuName = "Ingrediente")]
public class Ingrediente : ScriptableObject
{
    public string nombre;
    public Color color;
    public Sprite imagen;
}