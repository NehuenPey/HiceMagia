﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverObjeto : MonoBehaviour
{
    private bool enCaldero;
    public GameObject pocion;
    private Vector2 posicionInicial;
    private GameObject objetoEnCaldero;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private static List<GameObject> objetosEnCaldero = new List<GameObject>();
    [SerializeField] private float tiempoPocionEnPantalla = 3f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

    }

    private void Start()
    {
        posicionInicial = transform.position;
    }

    private void OnMouseDown()
    {
        // Verificar si el objeto tiene un componente SpriteRenderer
        if (spriteRenderer != null)
        {
            // Desactivar el collider y hacer que el objeto siga al mouse
            boxCollider.enabled = false;
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }

    private void OnMouseDrag()
    {
        if (!enCaldero)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(transform.position.x, transform.position.y);
            boxCollider.enabled = true;
        }
    }

    private void OnMouseUp()
    {
        spriteRenderer.sortingOrder = 3;

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
                Debug.Log("Objeto agregado a la lista de objetos en el caldero. Total de objetos en caldero: " + objetosEnCaldero.Count);

                spriteRenderer.enabled = false;
                boxCollider.enabled = false;

                if (objetosEnCaldero.Count == 2) // Verificar si se han agregado dos objetos al caldero
                {
                    // Devolver todos los objetos a su posición inicial
                    foreach (GameObject obj in objetosEnCaldero)
                    {
                        obj.GetComponent<SpriteRenderer>().enabled = true;
                        obj.GetComponent<BoxCollider2D>().enabled = true;
                        obj.transform.position = obj.GetComponent<MoverObjeto>().PosicionInicial;
                        obj.GetComponent<MoverObjeto>().enCaldero = false;
                    }

                    // Generar la poción
                    transform.position = posicionInicial;
                    pocion.GetComponent<SpriteRenderer>().enabled = true;
                    Debug.Log("Hola que hace");
                    objetosEnCaldero.Clear(); // Limpiar la lista de objetos en el caldero

                    PocionManager pocionManager = FindObjectOfType<PocionManager>();
                    pocionManager.IncrementarPuntuacion(2);
                    Invoke("DesactivarPocionGenerada", tiempoPocionEnPantalla);
                }
            }
        }

        if (!hitCaldero && objetosEnCaldero.Count < 2)
        {
            transform.position = posicionInicial;
        }
    }

    public void Devolver()
    {
        transform.position = posicionInicial;
        enCaldero = false;
        spriteRenderer.enabled = true;
        boxCollider.enabled = true;
    }

    public Vector2 PosicionInicial {
        get { return posicionInicial; }
    }
    private void DesactivarPocionGenerada()
    {
        pocion.GetComponent<SpriteRenderer>().enabled = false;
    }
}
