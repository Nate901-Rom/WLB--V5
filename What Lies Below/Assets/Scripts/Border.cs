using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Border : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Hit the wall!");
        }
    }
}

