using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SanBlasNPC;

public class EnemigoHealthManagerAI : MonoBehaviour
{
    public NPCSmartEnemy SmartEnemy;
    public GameManager gameManager;
    public GameObject itemPrefab; // El prefab que se instanciará cuando el enemigo muera
    public float dropProbability = 0.5f; // La probabilidad de que el ítem se deje caer (0.0f - 1.0f)

    public int salud=100;
    public bool isDead = false;

    private Slider sliderSalud;

    void Start(){
        sliderSalud = GetComponentInChildren<Slider>();
        GameObject gameManagerObject = GameObject.Find("GameManager");
        if (gameManagerObject != null)
        {
            gameManager = gameManagerObject.GetComponent<GameManager>();
            if (gameManager == null)
            {
                Debug.LogError("El GameObject GameManager no tiene el componente GameManager adjunto.");
            }
        }
        else
        {
            Debug.LogError("No se ha encontrado el GameObject GameManager en la escena.");
        }
    }
    void ActualizarSliderSalud()
    {
        sliderSalud.value = salud;
    }

    public void HacerPupa(int pupa)
    {
        if (isDead) return; // No entra si el enemigo esta muerto

        salud -= pupa;
        ActualizarSliderSalud();

        if (salud <= 0)
        {
            salud = 0; // evitamos que sea negativa
            isDead = true; // Marca como muerto
            GanarExperienciaYDropItem();
        }
    }

    private void GanarExperienciaYDropItem()
    {
        gameManager.GanarExperiencia(100);
        SmartEnemy.Die();
        DropItem();
    }

    private void DropItem()
    {
        float randomValue = Random.value;
        if (randomValue < dropProbability)
        {
            Instantiate(itemPrefab, transform.position, Quaternion.identity);
        }
    }
}
