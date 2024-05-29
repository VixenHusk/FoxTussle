using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    public static SpawnerEnemy Instance { get; private set; }

        public void SetActive(bool active)
    {
        // Implementation of the method to set the activity of the spawner
        gameObject.SetActive(active);
    }
}