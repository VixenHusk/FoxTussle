using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDetector : MonoBehaviour
{
    // El objeto que queremos activar cuando se produce el evento OnTriggerEnter
    public GameObject targetObject;

    // Referencia al script PlayerAttack
    private PlayerAttack playerAttack;
    private bool playerInTrigger = false;

    // Referencia al objeto Frostmourne
    private GameObject frostmourne;
    public GameObject sword2;

    void Start()
    {
        // Encontrar el objeto del jugador y obtener el componente PlayerAttack
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerAttack = player.GetComponent<PlayerAttack>();
            // Buscar el objeto Frostmourne en los hijos del jugador
            frostmourne = player.transform.Find("Frostmourne").gameObject;
            
            Transform sword2Transform = transform.parent.Find("Sword2");
            if (sword2Transform != null)
            {
                sword2 = sword2Transform.gameObject;
            }
        }
    }

    // Este método se llama cuando otro collider entra en el trigger de este GameObject
    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el objeto con el que colisiona tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;

            // Activa el objeto objetivo
            if (targetObject != null)
            {
                targetObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            targetObject.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if (playerAttack != null)
            {
                playerAttack.dobleAtaque = true;
            }

            if (frostmourne != null)
            {
                frostmourne.SetActive(true);
            }

            if (sword2 != null)
            {
                Destroy(sword2);
                Destroy(targetObject);
                Destroy(gameObject);
            }
        }
    }
}