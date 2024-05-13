using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace SanBlasNPC
{
    public class NPCPatrullajeManager : MonoBehaviour
    {
        public List<Transform> targets;
        public bool patrullajeAleatorio = true;

        [Range(0, 10)]
        public float tiempoEspera;
        private int nextTarget = 0;
        private NavMeshAgent navMeshAgent;

        private bool esperandoAsignacion = false;
        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            if (patrullajeAleatorio)
            {
                nextTarget = Random.Range(0, targets.Count);
            }
            navMeshAgent.destination = targets[nextTarget].position;
        }
        void Update()
        {
            DeterminarSiguienteTarget();
        }
        private void DeterminarSiguienteTarget()
        {
            if (esperandoAsignacion) return;
            if (!navMeshAgent.pathPending)
            {
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                {
                    if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                    {
                        if (patrullajeAleatorio)
                        {
                            esperandoAsignacion = true;
                            Invoke("AsignarSiguienteTargetAleatorio", tiempoEspera);
                        }
                        else
                        {
                            esperandoAsignacion = true;
                            Invoke("AsignarSiguienteTargetSecuencial", tiempoEspera);
                        }
                    }
                }
            }
        }
        private void AsignarSiguienteTargetAleatorio()
        {
            esperandoAsignacion = false;
            nextTarget = Random.Range(0, targets.Count);
            navMeshAgent.destination = targets[nextTarget].position;
        }
        private void AsignarSiguienteTargetSecuencial()
        {
            esperandoAsignacion = false;
            nextTarget++;
            if (nextTarget == targets.Count) nextTarget = 0;
            navMeshAgent.destination = targets[nextTarget].position;
        }
    }
}