using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletPattern2 : MonoBehaviour
{
	private static BulletPattern2 Instance = null;

	public static BulletPattern2 GetInstance()
	{
		if (Instance == null)
		{
			Instance = new BulletPattern2();
		}
		return Instance;
	}

	public enum Pattern
	{
		SingleOne,
		SingleTwo,
		SingleFour,
		Screw,
		DelayScrew,
		Twist, ShotGun,
		Explosion,
		GuideBullet
	};

	public Pattern pattern = Pattern.Screw;
	public Sprite sprite;
	public GameObject Target;

	private List<GameObject> BulletList = new List<GameObject>();
	private GameObject BulletPrefab;

	void Start()
	{
		//Target = new GameObject("Mouse");
		//Target.AddComponent<MyGizmo>();
		Target = GameObject.Find("Target");
		BulletPrefab = Resources.Load("Prefabs/Boss/BossBullet") as GameObject;
		ShotBullet();
	}

	private void Update()
	{
		//Target.transform.position = Input.mousePosition;
		if (Input.GetKeyUp(KeyCode.Space))
			ShotBullet();
	}

	public void ShotBullet()
	{
		switch (pattern)
		{
			case Pattern.Screw:
				GetScrewPattern(20);
				break;

			case Pattern.DelayScrew:
				StartCoroutine(GetDelayScrewPattern());
				break;

			case Pattern.Twist:
				StartCoroutine(TwistPattern());
				break;

			case Pattern.ShotGun:
				GetShotGunPattern(15);
				break;

			case Pattern.Explosion:
				StartCoroutine(ExplosionPattern(10));
				break;

			case Pattern.GuideBullet:
				GuideBulletPattern();
				break;
		}
	}

	public void GetShotGunPattern(int _count)
	{
		for (int i = 0; i < _count; ++i)
		{
			GameObject Obj = Instantiate(BulletPrefab);
			BulletControll controller = Obj.GetComponent<BulletControll>();
			Vector3 offSet = new Vector3(
				Random.Range(-1.0f, 1.0f),
				Random.Range(-1.0f, 1.0f),
				0.0f
				) * 0.3f;
			float speed = Random.Range(10.0f, 20.0f);
			controller.Direction = speed * (offSet + (Target.transform.position - transform.position).normalized);
			Obj.transform.position = transform.position;
			BulletList.Add(Obj);
		}
	}

	public void GetScrewPattern(int _count, bool _option = false)
	{
		float _angle = 0.0f;
		for (int i = 0; i < _count; ++i)
		{
			GameObject Obj = Instantiate(BulletPrefab);
			BulletControll controller = Obj.GetComponent<BulletControll>();

			controller.Option = _option;

			_angle += 360.0f / _count;

			controller.Direction = new Vector3(
				Mathf.Cos(_angle * 3.141592f / 180),
				Mathf.Sin(_angle * 3.141592f / 180),
				0.0f) * 5 + transform.position;

			Obj.transform.position = transform.position;

			BulletList.Add(Obj);
		}
	}


	public IEnumerator GetDelayScrewPattern()
	{
		int iCount = 12;

		float fAngle = 360.0f / iCount;

		int i = 0;

		while (i < (iCount) * 3)
		{
			GameObject Obj = Instantiate(BulletPrefab);
			BulletControll controller = Obj.GetComponent<BulletControll>();

			controller.Option = false;

			fAngle += 30.0f;

			controller.Direction = new Vector3(
				Mathf.Cos(fAngle * Mathf.Deg2Rad),
				Mathf.Sin(fAngle * Mathf.Deg2Rad),
				0.0f) * 5 + transform.position;

			Obj.transform.position = transform.position;

			BulletList.Add(Obj);
			++i;
			yield return new WaitForSeconds(0.25f);
		}
	}


	public IEnumerator TwistPattern()
	{
		float fTime = 3.0f;

		while (fTime > 0)
		{
			fTime -= Time.deltaTime;

			GameObject obj = Instantiate(Resources.Load("Prefabs/Twist")) as GameObject;

			yield return null;
		}
	}


	public IEnumerator ExplosionPattern(int _count, bool _option = false)
	{
		float _angle = 0.0f;
		GameObject ParentObj = new GameObject("Bullet");

		SpriteRenderer renderer = ParentObj.AddComponent<SpriteRenderer>();
		renderer.sprite = sprite;

		BulletControll controll = ParentObj.AddComponent<BulletControll>();

		controll.Option = false;

		controll.Direction = Target.transform.position - transform.position;

		ParentObj.transform.position = transform.position;

		yield return new WaitForSeconds(1.5f);

		Vector3 pos = ParentObj.transform.position;

		Destroy(ParentObj);

		for (int i = 0; i < _count; ++i)
		{
			GameObject Obj = Instantiate(BulletPrefab);

			BulletControll controller = Obj.GetComponent<BulletControll>();

			controller.Option = _option;

			_angle += 360.0f / _count;

			controller.Direction = new Vector3(
				Mathf.Cos(_angle * 3.141592f / 180),
				Mathf.Sin(_angle * 3.141592f / 180),
				0.0f) * 5 + transform.position;

			Obj.transform.position = pos;

			BulletList.Add(Obj);
		}
	}

	public void GuideBulletPattern()
	{
		GameObject Obj = Instantiate(BulletPrefab);
		BulletControll controller = Obj.GetComponent<BulletControll>();

		controller.Target = Target;
		controller.Option = true;

		Obj.transform.position = transform.position;
	}
}