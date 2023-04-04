using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletController : MonoBehaviour
{

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// ** collision = 충돌한 대상.
		// ** 충돌한 대상을 삭제한다. 
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
