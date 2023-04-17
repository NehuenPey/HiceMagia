using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverObjetos : MonoBehaviour
{
    private bool enCaldero;
    private Vector2 posicionInicial;
    private GameObject objetoEnCaldero;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private static List<GameObject> objetosEnCaldero = new List<GameObject>();

    void Start()
    {
        posicionInicial = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
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

                // Si hay más de un objeto en el caldero, devolver todos los objetos a su posición inicial
                if (objetosEnCaldero.Count > 1)
                {
                    foreach (GameObject obj in objetosEnCaldero)
                    {
                        obj.GetComponent<SpriteRenderer>().enabled = true;
                        obj.GetComponent<BoxCollider2D>().enabled = true;
                        obj.transform.position = obj.GetComponent<MoverObjetos>().posicionInicial;
                        obj.GetComponent<MoverObjetos>().enCaldero = false;
                    }

                    objetosEnCaldero.Clear(); // Limpiar la lista de objetos en el caldero
                }
            }
        }

        if (!hitCaldero)
        {
            transform.position = posicionInicial;
        }
    }
}
