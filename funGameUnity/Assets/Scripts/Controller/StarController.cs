using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    public GameObject canvas;

    private Vector3 movement;

    void Start()
    {
        canvas = GameObject.Find("StarCanvas");
        transform.SetParent(canvas.transform);
        transform.position = new Vector3(
            1000.0f,
            Random.Range(-491.0f,495.0f),
            0.0f
            );

        movement = new Vector3(-1.0f, 0.0f, 0.0f);
    }

    void Update()
    {
        transform.position += movement * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
            ControllerManager.GetInstance().CommonHeal(10);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }
}
