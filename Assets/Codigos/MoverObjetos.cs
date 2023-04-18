using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverObjetos : MonoBehaviour
{
    public GameObject pocion;
    private bool enCaldero;
    private Vector2 posicionInicial;
    private GameObject objetoEnCaldero;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private static List<GameObject> objetosEnCaldero = new List<GameObject>();
    //private int contadorObjetosEnCaldero = 0;

    void Start()
    {
        posicionInicial = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        pocion.GetComponent<SpriteRenderer>().enabled = false; // Desactivar el SpriteRenderer de la poción al inicio
    }

    void OnMouseDown()
    {
        // Verificar si el objeto tiene un componente SpriteRenderer
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            // Desactivar el collider y hacer que el objeto siga al mouse
            boxCollider.enabled = false;
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }

    void OnMouseDrag()
    {
        if (!enCaldero)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(transform.position.x, transform.position.y);
            boxCollider.enabled = true;
        }
    }

    void OnMouseUp()
    {
      GetComponent<SpriteRenderer>().sortingOrder = 1;

      RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.zero);

      bool hitCaldero = false;
      foreach (RaycastHit2D hit in hits)
      {
          if (hit.collider.CompareTag("Caldero"))
          {
              hitCaldero = true;
              enCaldero = true;
              objetoEnCaldero = hit.collider.gameObject;
              objetosEnCaldero.Add(gameObject); // añadir objeto a la lista de objetos en el caldero
              spriteRenderer.enabled = false;
              boxCollider.enabled = false;

              if (objetosEnCaldero.Count == 2) // Verificar si se han agregado dos objetos al caldero
              {
                  // Devolver todos los objetos a su posición inicial
                  foreach (GameObject obj in objetosEnCaldero)
                  {
                      obj.GetComponent<SpriteRenderer>().enabled = true;
                      obj.GetComponent<BoxCollider2D>().enabled = true;
                      obj.transform.position = obj.GetComponent<MoverObjetos>().posicionInicial;
                      obj.GetComponent<MoverObjetos>().enCaldero = false;
                  }

                  objetosEnCaldero.Clear(); // Limpiar la lista de objetos en el caldero

                  // Generar la poción
                  transform.position = posicionInicial;
                  pocion.GetComponent<SpriteRenderer>().enabled = true;
              }
          }
      }

      if (!hitCaldero)
      {
          transform.position = posicionInicial;

          // Desactivar el SpriteRenderer de la poción si no se agregaron ambos ingredientes
          if (objetosEnCaldero.Count < 2)
          {
              pocion.GetComponent<SpriteRenderer>().enabled = false;
          }
      }
  }
    public void Devolver()
    {
        transform.position = posicionInicial;
    }
}

