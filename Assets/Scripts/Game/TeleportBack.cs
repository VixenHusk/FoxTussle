using UnityEngine;

public class TeleportBack : MonoBehaviour
{
    public string shrineDoorName = "ShrineDoor"; // Nombre del GameObject que actúa como puerta del santuario
    public GameObject spawners;
    public GameObject calamari;

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra en el trigger es el jugador
        if (other.CompareTag("Player"))
        {
            // Buscar todos los objetos con el componente ItemDetector en la escena
            ItemDetector[] itemDetectors = FindObjectsOfType<ItemDetector>();

            // Recorrer todos los objetos con ItemDetector y destruir sus hijos
            foreach (ItemDetector itemDetector in itemDetectors)
            {
                DestroyChildren(itemDetector.gameObject);
            }

            //activa spawners
            spawners.SetActive(true);
            //activa calamari
            calamari.SetActive(true);

            // Buscar el GameObject con el nombre "ShrineDoor"
            GameObject shrineDoor = GameObject.Find(shrineDoorName);
            if (shrineDoor != null)
            {
                // Obtener la posición de la puerta del santuario y desplazarla
                Vector3 teleportPosition = shrineDoor.transform.position;
                Destroy(shrineDoor);
                other.transform.position = teleportPosition;

                // Descongelar los GameObjects hijos del GameObject "-- Enemies"
                GameObject enemiesParent = GameObject.Find("-- Enemies");
                if (enemiesParent != null)
                {
                    foreach (Transform enemy in enemiesParent.transform)
                    {
                        UnfreezeGameObject(enemy.gameObject);
                    }
                }
                else
                {
                    Debug.LogError("No se encontró un objeto con el nombre: -- Enemies");
                }
            }
            else
            {
                Debug.LogError("No se encontró un objeto con el nombre: " + shrineDoorName);
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

    private void UnfreezeGameObject(GameObject obj)
    {
        // Reactivar el Rigidbody si existe
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints.None;
        }

        // Reactivar scripts de movimiento específicos (ejemplo)
        MonoBehaviour[] scripts = obj.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = true;
        }
    }
}
