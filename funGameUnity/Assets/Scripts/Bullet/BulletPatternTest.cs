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
		//Target.transform.position = Input.mousePosition;
		print(Input.mousePosition.x);
		if (Input.GetKeyUp(KeyCode.Space))
			Fire();
	}

	public void Fire()
	{
		switch (pattern)
		{
			case Pattern.Screw:
				bulletPattern.GetScrewPattern(20);
				break;

			case Pattern.DelayScrew:
				bulletPattern.GetDelayScrewPattern();
				break;

			case Pattern.Twist:
				bulletPattern.TwistPattern();
				break;

			case Pattern.ShotGun:
				bulletPattern.GetShotGunPattern(15);
				break;

			case Pattern.Explosion:
				bulletPattern.ExplosionPattern(10);
				break;

			case Pattern.F:

				break;

			case Pattern.GuideBullet:
				bulletPattern.GuideBulletPattern();
				break;
		}
	}
}
