using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
	private void OnTriggerStay2D(Collider2D collision)
	{
		//print(collision);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		print(collision);
		if (collision.tag == "Player")
			print("from collisionTest yes");
	}
}
