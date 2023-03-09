using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    public float Speed;
    public GameObject player;

    private Vector3 movemane = new Vector3(0.0f, 0.0f, 0.0f);
	private Vector3 offset = new Vector3(0.0f, 7.5f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        movemane = new Vector3(
            Input.GetAxisRaw("Horizontal") + offset.x,
            player.transform.position.y + offset.y,
            0.0f + offset.z
            );
        transform.position -= movemane * Time.deltaTime * Speed;
    }
}
