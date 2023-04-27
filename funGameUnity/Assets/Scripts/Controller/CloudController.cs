using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    private void Start()
    {

        GetComponent<SpriteRenderer>().color = new Color(
            255.0f - Climate.GetInstance().RainPercent*255,
            255.0f - Climate.GetInstance().RainPercent*255,
            255.0f - Climate.GetInstance().RainPercent*255
            );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print(collision.name);
    }
}
