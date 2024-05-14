using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAI : MonoBehaviour
{
    public void OnDeathAnimationEnd()
    {
        Destroy(transform.parent.gameObject);
    }
}
