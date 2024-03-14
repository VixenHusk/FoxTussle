using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
        public GameObject enemy;
        public int numeroElementos;
        public int tiempoEntreSpawn;
        private int contador = 0;
        // Start is called before the first frame update
        void Start()
        {
            InvokeRepeating("Spawn", tiempoEntreSpawn, tiempoEntreSpawn);
        }

        // Update is called once per frame
        void Spawn()
        {
            contador++;
            Instantiate(enemy, transform);
            if (contador >= numeroElementos)
            {
                CancelInvoke("Spawn");
            }
        }
}
