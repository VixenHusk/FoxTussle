using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireOrbDamage : MonoBehaviour
{
    [Header("Daño infringido en cada lapso de tiempo")]
    public int danyo;
    [Header("Tiempo transcurrido entre cada incremento/decremento daño")]
    public float frecuencia;
    private bool isAI=false;
    private Transform transformPlayer;

    public void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("Enemy")) 
        {
            transformPlayer = c.gameObject.transform;
            InvokeRepeating("HacerDanyo", 0, frecuencia);
            isAI = false;

        }  else if (c.gameObject.CompareTag("EnemyAI")){
            transformPlayer = c.gameObject.transform;
            InvokeRepeating("HacerDanyo", 0, frecuencia);
            isAI = true;
        }
    }

    public void OnTriggerExit(Collider c)
    {
        if (c.gameObject.CompareTag("Enemy")||c.gameObject.CompareTag("EnemyAI")) 
        {
            CancelInvoke("HacerDanyo");
        }
    }

    private void HacerDanyo()
    {
        if (isAI){
            if (transformPlayer != null) // Verificar si el transformPlayer aún existe
            {
                var healthManagerAI = transformPlayer.gameObject.GetComponent<EnemigoHealthManagerAI>();
                if (healthManagerAI != null) // Verificar si el componente EnemigoHealthManager aún existe
                {
                    healthManagerAI.HacerPupa(danyo);
                }
            }
            else
            {
                CancelInvoke("HacerDanyo"); // Cancelar la invocación si el objeto ha sido destruido
            }

        }else{

            if (transformPlayer != null) // Verificar si el transformPlayer aún existe
            {
                var healthManager = transformPlayer.gameObject.GetComponent<EnemigoHealthManager>();
                if (healthManager != null) // Verificar si el componente EnemigoHealthManager aún existe
                {
                    healthManager.HacerPupa(danyo);
                }
            }
            else
            {
                CancelInvoke("HacerDanyo"); // Cancelar la invocación si el objeto ha sido destruido
            }

        }
    }
}
