using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoGauntlet : MonoBehaviour
{
    public float speed;
    [Header("Tag del GameObject al que va a seguir")]
    public string targetTag = "Player";
    private Transform transformPlayer; 
    private EnemigoHealthManager vidaEnemigo;
    void Start()
    {
        transformPlayer = GameObject.FindGameObjectWithTag(targetTag).transform;
        vidaEnemigo = GetComponent<EnemigoHealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (vidaEnemigo.isDead == true) return;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        transform.LookAt(transformPlayer);
    }
}
