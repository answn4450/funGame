using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiCloud : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Cloud")
            Destroy(gameObject);
    }
}
