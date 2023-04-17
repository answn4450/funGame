using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPatternTest : MonoBehaviour
{
	public GameObject Target;

	public BulletPattern.Pattern pattern2;
	
	private List<GameObject> BulletList = new List<GameObject>();
	private GameObject BulletPrefab;
	
	void Start()
	{
		Target = GameObject.Find("Target");
		BulletPrefab = Resources.Load("Prefabs/Bullet") as GameObject;
		GetComponent<BulletPattern>().BulletPrefab = BulletPrefab;
		GetComponent<BulletPattern>().Target = Target;
	}

	private void Update()
	{
		GetComponent<BulletPattern>().pattern = pattern2;
		//MyBulletPattern2.position = transform.position;

		if (Input.GetKeyUp(KeyCode.Space))
		{
			GetComponent<BulletPattern>().ShotBullet();
		}
		if (this.GetComponent<BulletPattern>().ShotEnd)
		{
			this.GetComponent<BulletPattern>().ShotEnd = false;
			this.GetComponent<BulletPattern>().ShotBullet();
		}
		//print(GetComponent<BulletPattern>().ShotEnd);
	}
}
