using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGizmo : MonoBehaviour
{
	public GameObject player;
	public GameObject test;

	// Start is called before the first frame update
	private void Start()
	{
		player = GameObject.Find("Player");
		//player = test;
	}

	// Update is called once per frame
	void Update()
	{
		float distance = Vector3.Distance(player.transform.position, test.transform.position);

		if (distance > 2.0f)
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
