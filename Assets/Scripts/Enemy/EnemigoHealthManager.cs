using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemigoHealthManager : MonoBehaviour
{
    [Header("Objeto que se va a instanciar cuando el enemigo 'muera'")]
    public GameObject prefabObjetoReemplazo;
    public EnemyAnimatorController animatorController;
    public GameManager gameManager;
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
    public void HacerPupa(int pupa){
        salud-=pupa;
        sliderSalud.value = salud;
        if (salud<=0){
                gameManager.GanarExperiencia(100);
                animatorController.Die();
                isDead=true;
        }
    }

}
