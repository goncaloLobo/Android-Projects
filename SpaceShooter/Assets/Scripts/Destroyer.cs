using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    void DestroyGameObject()
    {
        // destroi a explosão, ou seja, o último frame
        Destroy(gameObject);
    }
}
