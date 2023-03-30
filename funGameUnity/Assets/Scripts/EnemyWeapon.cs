using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
	public GameObject Enemy;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		print(collision.transform.tag);
		if (collision.transform.tag=="Untagged")
		{
			print(collision.gameObject.name);
		}
		if (collision.transform.tag == "Player")
		{
			print("hit");
			Enemy.GetComponent<EnemyController>().WeaponHitPlayer = true;
		}
	}
}
