using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    private float Radius;


    private void Start()
    {
        // gameObject가 2D 에서 원 모양이라고 가정
        Radius = GetComponent<Transform>().localScale.x;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        Vector3 distance = transform.position - collision.transform.position;
        float power = 4 * (Radius - distance.magnitude) / Radius;
        
        collision.transform.position += (
            Time.deltaTime * distance.normalized * power
            );
    }
}
