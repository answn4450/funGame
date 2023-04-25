using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallEnemyController : MonoBehaviour
{
	private Animator Anim;
	private int HP = 3;
	private int Damage = 1;
	private Color Color1;
	private Color Color2;
	private Renderer SelfRenderer;
	private bool Changed = false;

	private void Awake()
	{
		Anim = GetComponent<Animator>();
		SelfRenderer = GetComponent<Renderer>();
		Color1 = SelfRenderer.material.color;
		Color2 = new Color(12, 85, 66);
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
			Changed = !Changed;
			SelfRenderer.material.color = Changed ? Color1: Color2;
		}

		if (HP <= 0)
			Anim.SetTrigger("Die");
	}


	public void Die()
	{
		Destroy(gameObject);
	}
}
