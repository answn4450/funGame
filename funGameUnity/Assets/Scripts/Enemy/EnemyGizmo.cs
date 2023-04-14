using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGizmo : MonoBehaviour
{
	public GameObject player;
	public GameObject test;

	private void Start()
	{
		player = GameObject.Find("Player");
	}

	void Update()
	{
		//if ()
		float distance = Vector3.Distance(player.transform.position, test.transform.position);

		if (distance > 1.5f)
		{
			test.GetComponent<MyGizmo>().color = Color.green;
			this.GetComponent<EnemyController>().CloseToPlayer = false;
		}
		else
		{
			test.GetComponent<MyGizmo>().color = Color.red;
			this.GetComponent<EnemyController>().CloseToPlayer = true;
		}
	}
}
