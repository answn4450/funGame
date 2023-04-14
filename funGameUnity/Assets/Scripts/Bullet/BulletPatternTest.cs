using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPatternTest : MonoBehaviour
{

	public enum Pattern
	{
		Screw,
		DelayScrew,
		Twist, ShotGun,
		Explosion, F,
		GuideBullet
	};

	public Pattern pattern = Pattern.Screw;
	public Sprite sprite;
	public GameObject Target;
	
	private BulletPattern bulletPattern;
	private List<GameObject> BulletList = new List<GameObject>();
	private GameObject BulletPrefab;

	void Start()
	{
		bulletPattern = BulletPattern.GetInstance();
		//Target = new GameObject("Mouse");
		//Target.AddComponent<MyGizmo>();
		Target = GameObject.Find("Target");
		BulletPrefab = Resources.Load("Prefabs/Boss/BossBullet") as GameObject;
		Fire();
	}

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Space))
			bulletPattern.getshot
	}
}
