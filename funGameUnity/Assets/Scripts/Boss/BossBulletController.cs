using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletController : MonoBehaviour
{

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// ** collision = �浹�� ���.
		// ** �浹�� ����� �����Ѵ�. 
		if (collision.transform.tag == "wall")
			Destroy(this.gameObject);
		else
		{
			if (collision.transform.tag == "Player")
			{
				Destroy(this.gameObject);
				ControllerManager.GetInstance().CommonHit(2);
			}
		}
	}
}
