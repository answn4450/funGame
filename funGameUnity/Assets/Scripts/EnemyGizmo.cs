using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGizmo : MonoBehaviour
{
	public GameObject player;
	public GameObject test;
	public GameObject weapon;

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
		float weapon_distance = Vector3.Distance(player.transform.position, weapon.transform.position);

		if (distance > 1.0f)
		{

			test.GetComponent<MyGizmo>().color = Color.green;
			this.GetComponent<EnemyController>().CloseToPlayer = false;
		}
		else
		{
			test.GetComponent<MyGizmo>().color = Color.red;
			this.GetComponent<EnemyController>().CloseToPlayer = true;
		}

		if (weapon_distance > 0.5f)
		{

			weapon.GetComponent<MyGizmo>().color = Color.green;
			ControllerManager.GetInstance().HitShock += 1;
		}
		else
		{
			//test.GetComponent<MyGizmo>().color = Color.red;
		}
	}
}
