using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
	public GameObject Enemy;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		print("weaopn hit player");

		if (collision.transform.tag == "Player")
		{
			Enemy.GetComponent<EnemyController>().WeaponHitPlayer = true;
		}
	}
}
