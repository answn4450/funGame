using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallEnemyController : MonoBehaviour
{
	private Animator Anim;
	private int HP = 3;
	private int Damage = 1;

	private void Awake()
	{
		Anim = GetComponent<Animator>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Bullet")
		{
			HP -= (int)ControllerManager.GetInstance().Player_BulletPower;
		}
		else if (collision.tag == "Player")
		{
			ControllerManager.GetInstance().CommonHit(Damage);
		}

		if (HP <= 0)
			Anim.SetTrigger("Die");
	}

	public void Die()
	{
		Destroy(gameObject, 0.2f);
	}
}
