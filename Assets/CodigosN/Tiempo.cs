using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Tiempo : MonoBehaviour
{
    public Image circuloR;
    public float tiempo = 60f;
    private float ContadorTiempo;

    void Start()
    {
        ContadorTiempo = 0;
    }

    void Update()
    {
        if(ContadorTiempo <= tiempo)
        {
            ContadorTiempo = ContadorTiempo + Time.deltaTime;
            circuloR.fillAmount = ContadorTiempo / tiempo;
        }
    }
}
