using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PriceDetector : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemPrice1;
    [SerializeField] private TextMeshProUGUI itemPrice2;
    [SerializeField] private TextMeshProUGUI itemPrice3;

    public void StartPrices()
    {
        StartCoroutine(DelayedPriceUpdate());
        Debug.Log("Se han repuesto los precios de la tienda");
    }

    IEnumerator DelayedPriceUpdate()
    {
        yield return new WaitForSeconds(0.1f); // Espera 0.5 segundos

        // Encontrar los componentes ItemDescription de los objetos correspondientes y asignarlos a las variables
        ItemDescription itemDesc1 = GameObject.Find("Item1").GetComponentInChildren<ItemDescription>();
        if (itemDesc1 != null)
        {
            itemPrice1.text = "Price: " + itemDesc1.price.ToString(); // Convertir int a string

        }
        else
        {
            Debug.Log("Error: No se encontró ItemDescription en Item1");
        }

        ItemDescription itemDesc2 = GameObject.Find("Item2").GetComponentInChildren<ItemDescription>();
        if (itemDesc2 != null)
        {
            itemPrice2.text = "Price: " + itemDesc2.price.ToString(); // Convertir int a string

        }
        else
        {
            Debug.Log("Error: No se encontró ItemDescription en Item2");
        }

        ItemDescription itemDesc3 = GameObject.Find("Item3").GetComponentInChildren<ItemDescription>();
        if (itemDesc3 != null)
        {
            itemPrice3.text = "Price: " + itemDesc3.price.ToString(); // Convertir int a string

        }
        else
        {
            Debug.Log("Error: No se encontró ItemDescription en Item3");
        }
    }
}