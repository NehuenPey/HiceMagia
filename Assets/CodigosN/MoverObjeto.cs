using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverObjeto : MonoBehaviour
{
    private bool enCaldero;
    private Tiempo timer;
    public GameObject pocionRosa;
    public GameObject pocionVerde;
    public GameObject pocionVioleta;
    public GameObject pocionNaranja;
    public GameObject prefabMariposas;
    public GameObject prefabExplosion;
    public GameObject prefabGatitos;
    private Vector2 posicionInicial;
    private GameObject objetoEnCaldero;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private static List<GameObject> objetosEnCaldero = new List<GameObject>();
    [SerializeField] private float tiempoPocionEnPantalla = 3f;
    public AK.Wwise.Event magiapocion;
    public AK.Wwise.Event tirarencaldero;
    public AK.Wwise.Event agarrar;

    private void Awake()
    {
        timer = GetComponent<Tiempo>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

    }

    private void Start()
    {
        posicionInicial = transform.position;
        prefabMariposas.SetActive(false);
        prefabExplosion.SetActive(false);
        prefabGatitos.SetActive(false);
    }

    private void OnMouseDown()
    {
        // Verificar si el objeto tiene un componente SpriteRenderer y si el tiempo ha terminado
        if (spriteRenderer != null && !Tiempo.tiempoTerminado)
        {
            // Desactivar el collider y hacer que el objeto siga al mouse
            boxCollider.enabled = false;
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            agarrar.Post(gameObject);
        }
    }

    private void OnMouseDrag()
    {
        if (!enCaldero && !Tiempo.tiempoTerminado)
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
            Debug.Log("Total de objetos en caldero: " + objetosEnCaldero.Count);
            tirarencaldero.Post(gameObject);

            spriteRenderer.enabled = false;
            boxCollider.enabled = false;

                // Verificar si se han agregado dos objetos al caldero
                if (objetosEnCaldero.Count == 2)
                {
                    // Verificar qué objetos se han combinado para generar la poción correspondiente
                    if (objetosEnCaldero[0].CompareTag("Rojo") && objetosEnCaldero[1].CompareTag("Blanco") ||
                        objetosEnCaldero[0].CompareTag("Blanco") && objetosEnCaldero[1].CompareTag("Rojo"))
                    {
                        // Generar la poción Rosa
                        transform.position = posicionInicial;
                        magiapocion.Post(gameObject);
                        pocionRosa.GetComponent<SpriteRenderer>().enabled = true;

                        Puntuacion pocionManager = FindObjectOfType<Puntuacion>();
                        pocionManager.IncrementarPuntuacion(5);
                        Invoke("DesactivarPocionGenerada",tiempoPocionEnPantalla);
                    }
                else if (objetosEnCaldero[0].CompareTag("Rojo") && objetosEnCaldero[1].CompareTag("Azul") ||
                    objetosEnCaldero[0].CompareTag("Azul") && objetosEnCaldero[1].CompareTag("Rojo")) // Verificar si se han combinado otros objetos para generar otras pociones
                {
                    // Generar la poción violeta
                    transform.position = posicionInicial;
                    magiapocion.Post(gameObject);
                    pocionVioleta.GetComponent<SpriteRenderer>().enabled = true;

                    Puntuacion pocionManager = FindObjectOfType<Puntuacion>();
                    pocionManager.IncrementarPuntuacion(5);
                    Invoke("DesactivarPocionGenerada",tiempoPocionEnPantalla);
                
                }
                else if (objetosEnCaldero[0].CompareTag("Rojo") && objetosEnCaldero[1].CompareTag("Amarillo")) // Verificar si se han combinado otros objetos para generar otras pociones
                {
                    // Generar la poción naranja
                    transform.position = posicionInicial;
                    magiapocion.Post(gameObject);
                    pocionNaranja.GetComponent<SpriteRenderer>().enabled = true;

                    Puntuacion pocionManager = FindObjectOfType<Puntuacion>();
                    pocionManager.IncrementarPuntuacion(5);
                    Invoke("DesactivarPocionGenerada",tiempoPocionEnPantalla);
                
                }
                else if (objetosEnCaldero[0].CompareTag("Amarillo") && objetosEnCaldero[1].CompareTag("Azul") ||
                    objetosEnCaldero[0].CompareTag("Azul") && objetosEnCaldero[1].CompareTag("Amarillo")) // Verificar si se han combinado otros objetos para generar otras pociones
                {
                    // Generar la poción verde
                    transform.position = posicionInicial;
                    magiapocion.Post(gameObject);
                    pocionVerde.GetComponent<SpriteRenderer>().enabled = true;

                    Puntuacion pocionManager = FindObjectOfType<Puntuacion>();
                    pocionManager.IncrementarPuntuacion(5);
                    Invoke("DesactivarPocionGenerada",tiempoPocionEnPantalla);
                
                }
                else if (objetosEnCaldero[0].CompareTag("Amarillo") && objetosEnCaldero[1].CompareTag("Rojo")) // Verificar si se han combinado otros objetos para generar otras pociones
                {
                    // Generar la poción explosion
                    transform.position = posicionInicial;
                    magiapocion.Post(gameObject);
                    prefabExplosion.SetActive(true);

                    Puntuacion pocionManager = FindObjectOfType<Puntuacion>();
                    pocionManager.ReducirPuntuacion(3);
                    Invoke("DesactivarPocionGenerada",tiempoPocionEnPantalla);
                
                }
                else if (objetosEnCaldero[0].CompareTag("Blanco") && objetosEnCaldero[1].CompareTag("Azul") ||
                    objetosEnCaldero[0].CompareTag("Azul") && objetosEnCaldero[1].CompareTag("Blanco")) // Verificar si se han combinado otros objetos para generar otras pociones
                {
                    // Generar la poción mariposas
                    transform.position = posicionInicial;
                    magiapocion.Post(gameObject);
                    prefabMariposas.SetActive(true);

                    Puntuacion pocionManager = FindObjectOfType<Puntuacion>();
                    pocionManager.ReducirPuntuacion(2);
                    Invoke("DesactivarPocionGenerada",tiempoPocionEnPantalla);
                
                }
                else if (objetosEnCaldero[0].CompareTag("Blanco") && objetosEnCaldero[1].CompareTag("Amarillo") ||
                    objetosEnCaldero[0].CompareTag("Amarillo") && objetosEnCaldero[1].CompareTag("Blanco")) // Verificar si se han combinado otros objetos para generar otras pociones
                {
                    // Generar la poción gatitos
                    transform.position = posicionInicial;
                    magiapocion.Post(gameObject);
                    prefabGatitos.SetActive(true);

                    Tiempo tiempoManager = FindObjectOfType<Tiempo>();
                    tiempoManager.ReducirTiempo(5);
                    Invoke("DesactivarPocionGenerada",tiempoPocionEnPantalla);
                
                }
                // Si no se han combinado objetos que generen una poción, devolver los objetos a su posición inicial
                        foreach (GameObject obj in objetosEnCaldero)
                        {
                            obj.GetComponent<SpriteRenderer>().enabled = true;
                            obj.GetComponent<BoxCollider2D>().enabled = true;
                            obj.transform.position = obj.GetComponent<MoverObjeto>().PosicionInicial;
                            obj.GetComponent<MoverObjeto>().enCaldero = false;
                            Debug.Log("bro acá toy");
                        }
                        transform.position = posicionInicial;
                        objetosEnCaldero.Clear(); // Limpiar la lista de objetos en el caldero
                    
                }
            }
            if (!hitCaldero && objetosEnCaldero.Count < 2)
            {
                transform.position = posicionInicial;
            }
        }
    }

    public Vector2 PosicionInicial {
        get { return posicionInicial; }
    }

    private void DesactivarPocionGenerada()
    {
        pocionRosa.GetComponent<SpriteRenderer>().enabled = false;
        pocionVioleta.GetComponent<SpriteRenderer>().enabled = false;
        pocionVerde.GetComponent<SpriteRenderer>().enabled = false;
        pocionNaranja.GetComponent<SpriteRenderer>().enabled = false;
    }
    void ActivarPrefabConAnimacion()
{
    // Activamos el Prefab
    prefabMariposas.SetActive(true);
    prefabExplosion.SetActive(true);
    prefabGatitos.SetActive(true);

    // Obtenemos el componente Animator del Prefab
    Animator anim = prefabMariposas.GetComponent<Animator>();
    Animator anima = prefabExplosion.GetComponent<Animator>();
    Animator animat = prefabGatitos.GetComponent<Animator>();

    // Reproducimos la animación
    anim.Play("lluvia de mariposas");
    anima.Play("explosión (1)");
    anima.Play("lluvia de gatitos");
}

}
