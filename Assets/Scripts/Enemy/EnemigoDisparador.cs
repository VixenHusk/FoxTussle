using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoDisparador : MonoBehaviour
{
    public Transform puntoDisparo;
    private Animator animator;
    public GameObject prefabProyectil;
    public float fuerzaDisparo;
    public float distanciaDisparo;
    public float minCadenciaDisparo = 1.0f; // Cadencia mínima de disparo en segundos
    public float maxCadenciaDisparo = 3.0f; // Cadencia máxima de disparo en segundos
    public string targetTag;
    private Transform targetTransform;
    private bool isADistancia = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator not found!");
        }

        targetTransform = GameObject.FindGameObjectWithTag(targetTag).transform;
        if (targetTransform == null)
        {
            Debug.LogError("Target not found with tag: " + targetTag);
        }

        DispararConCadenciaAleatoria();
    }

    void Update()
    {
        if (targetTransform == null) return;

        isADistancia = (targetTransform.position - puntoDisparo.position).magnitude < distanciaDisparo;
        Vector3 direccionCalamari = new Vector3(targetTransform.position.x, transform.position.y, targetTransform.position.z);
        transform.LookAt(direccionCalamari);
    }

    void Disparar()
    {
        if (!isADistancia) return;

        GameObject proyectil = Instantiate(prefabProyectil, puntoDisparo.position, puntoDisparo.rotation);
        proyectil.GetComponent<Rigidbody>().AddForce(puntoDisparo.forward * fuerzaDisparo);
        animator.SetTrigger("Attack");

        // Invocar el próximo disparo con una cadencia aleatoria
        DispararConCadenciaAleatoria();
    }

    void DispararConCadenciaAleatoria()
    {
        float cadenciaDisparoAleatoria = Random.Range(minCadenciaDisparo, maxCadenciaDisparo);
        Invoke("Disparar", cadenciaDisparoAleatoria);
    }
}