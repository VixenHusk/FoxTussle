using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemDetector : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    private bool playerEntered = false;
    private string descripcion;
    private string itemName;
    private int price;

    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI itemText;

    public AppleItem appleItem;
    public ExtraLifeItem extraLifeItem;
    public RabbitItem rabbitItem;
    public BreathItem breathItem;
    public ShieldItem shieldItem;
    private GameManager gameManager; // Referencia al GameManager

    public void StartReferences()
    {
        Invoke("AssignReferences", 0.5f);
        Debug.Log($"[{gameObject.name}] Se han repuesto las funciones de la tienda");
    }

    private void AssignReferences()
    {
        // Obtener la referencia al GameManager
        gameManager = FindObjectOfType<GameManager>();
        appleItem = FindObjectOfType<AppleItem>();
        extraLifeItem = FindObjectOfType<ExtraLifeItem>();
        rabbitItem = FindObjectOfType<RabbitItem>();
        breathItem = FindObjectOfType<BreathItem>();
        shieldItem = FindObjectOfType<ShieldItem>();
        Debug.Log($"[{gameObject.name}] Referencias asignadas");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            playerEntered = true;
            Debug.Log($"[{gameObject.name}] Se va a mostrar descripción");

            // Buscar el hijo con el script ItemDescription y obtener la variable descripcion
            ItemDescription itemDescription = GetComponentInChildren<ItemDescription>();
            if (itemDescription != null)
            {
                descripcion = itemDescription.descripcion;
                itemName = itemDescription.itemName;
                price = itemDescription.price; // Obtener el costo del objeto

                // Mostrar la descripción en el TextMeshPro
                itemText.text = itemName;
                descriptionText.text = descripcion;
                Debug.Log($"[{gameObject.name}] Descripción mostrada: {itemName} - {descripcion}");
            }
            else
            {
                Debug.Log($"[{gameObject.name}] No se encontró el componente ItemDescription en el hijo.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            playerEntered = false;
            itemName = null;
            descripcion = null; // Se limpian ambas

            descriptionText.text = string.Empty;
            itemText.text = string.Empty;
            Debug.Log($"[{gameObject.name}] Descripción limpia");
        }
    }

    private void Update()
    {
        if (playerEntered && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log($"[{gameObject.name}] Intento de compra");

            // Verificar si el jugador tiene suficientes monedas para comprar el objeto
            if (gameManager != null && gameManager.CheckMonedas(price))
            {
                // Sustraer las monedas del GameManager
                gameManager.Monedas(-price); // Pasar el costo del objeto como valor negativo

                // Llamar al método Activar() del item correspondiente
                ActivarItem();

                // Destruir todos los hijos del GameObject al que se le hizo trigger
                DestroyChildren(gameObject);
                Debug.Log($"[{gameObject.name}] Todos los hijos destruidos");
                Debug.Log($"[{gameObject.name}] Objeto comprado");
            }
            else
            {
                Debug.Log($"[{gameObject.name}] No tienes suficientes monedas para comprar este objeto. Precio: {price}");
            }
        }
    }

    private void DestroyChildren(GameObject parent)
    {
        // Destruir todos los hijos del GameObject
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void ActivarItem()
    {
        // Aquí determina qué item se debe activar y llama a su método Activar()

        switch (itemName)
        {
            case "Apple of Experience":
                appleItem.Activar();
                Debug.Log("Activando Apple of Experience");
                break;

            case "Fox Masquerade":
                extraLifeItem.Activar();
                Debug.Log("Activando Fox Masquerade");
                break;

            case "Sacred Rabbit":
                rabbitItem.Activar();
                Debug.Log("Activando Sacred Rabbit");
                break;

            case "Chili Pepper":
                breathItem.Activar();
                Debug.Log("Activando Chili Pepper");
                break;

            case "Shield of the Wind":
                shieldItem.Activar();
                Debug.Log("Activando Shield of the Wind");
                break;

            // Agrega más casos para los otros items según sea necesario
            default:
                Debug.LogWarning($"Nombre de item desconocido: {itemName}");
                break;
        }
    }
}