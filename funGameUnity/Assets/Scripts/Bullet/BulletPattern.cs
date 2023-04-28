using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class BulletPattern : MonoBehaviour
{
	public enum Pattern
	{
		Screw,
		DelayScrew,
		ShotGun,
		Explosion,
		GuideBullet
	};

	public Pattern pattern = Pattern.Screw;
	public Dictionary<Pattern, List<int>> LVTable = new Dictionary<Pattern, List<int>>();
	public Dictionary<Pattern, int> LVT = new Dictionary<Pattern, int>();

	public GameObject Target;
	public GameObject BulletPrefab;
	public Vector3 TargetDir;
	public bool ShotEnd = true;
	public float Speed = 3.0f;
	public float ReloadTerm = 2.0f;
	private List<GameObject> BulletList = new List<GameObject>();


	private void Awake()
	{
		LVTable.Add(Pattern.Screw, new List<int> { 3,4,8,11,15,30});
		LVTable.Add(Pattern.DelayScrew, new List<int> { 3,4,8,11,15,30});
		LVTable.Add(Pattern.ShotGun, new List<int> { 1, 4, 8, 11, 15, 30 });
		LVTable.Add(Pattern.Explosion, new List<int> { 1, 4, 8, 11, 15, 30 });
		LVTable.Add(Pattern.GuideBullet, new List<int> { 2, 4, 4, 5, 6, 7 });
		Target = GameObject.Find("Cursor");
		ShotEnd = true;
		Speed = 3.0f;
	}

	private void Update()
	{
		Speed = 4.0f;
		if (Target == null)
			Target = GameObject.Find("Cursor");
	}

    private int testCount = 0;
	public void ShotBullet(int _lv)
	{
		if (ShotEnd)
		{
			ShotEnd = false;
            
            List<Color> colors = new List<Color>
            {
                Color.red,
                Color.green,
                Color.blue,
            };
            Color randColor = colors[testCount++ % 3];
            BulletPrefab.GetComponent<SpriteRenderer>().color = randColor;

			switch (pattern)
			{
				case Pattern.Screw:
					GetScrewPattern(_lv);
					break;

				case Pattern.DelayScrew:
					StartCoroutine(GetDelayScrewPattern(_lv));
					break;

				case Pattern.ShotGun:
					GetShotGunPattern(_lv);
					break;

				case Pattern.Explosion:
					StartCoroutine(ExplosionPattern(_lv));
					break;

				case Pattern.GuideBullet:
					GuideBulletPattern(_lv);
					break;
			}
		}
	}

	public void GetShotGunPattern(int _lv)
	{
		int _count = LVTable[Pattern.ShotGun][_lv];
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
		StartCoroutine(DelayShotEnd(ReloadTerm));
	}

	public void GetScrewPattern(int _lv, bool _option = false)
	{
		int _count = LVTable[Pattern.Screw][_lv];
		float _angle = 0.0f;
		for (int i = 0; i < _count; ++i)
		{
			GameObject Obj = Instantiate(BulletPrefab);
			BulletControll controller = Obj.GetComponent<BulletControll>();
			controller.Option = _option;

			_angle += 360.0f / _count;
            controller.Direction = new Vector3(
                Mathf.Cos(_angle * Mathf.Deg2Rad),
                Mathf.Sin(_angle * Mathf.Deg2Rad),
                0.0f);
			Obj.transform.position = transform.position;

			BulletList.Add(Obj);
		}
		StartCoroutine(DelayShotEnd(ReloadTerm));
	}

	public IEnumerator GetDelayScrewPattern(int _lv)
	{
        int iCount = LVTable[Pattern.DelayScrew][_lv];

		float fAngle = 0.0f;

		int i = 0;

        while (i < (iCount) * 3)
		{
			GameObject Obj = Instantiate(BulletPrefab);
			BulletControll controller = Obj.GetComponent<BulletControll>();
            
			controller.Option = false;

			fAngle += 360.0f / iCount;
			controller.Direction = new Vector3(
				Mathf.Cos(fAngle * Mathf.Deg2Rad),
				Mathf.Sin(fAngle * Mathf.Deg2Rad),
				0.0f) * Speed;

			Obj.transform.position = transform.position;

			BulletList.Add(Obj);
			++i;
			yield return new WaitForSeconds(0.25f);
		}
		ShotEnd = true;
	}


	public IEnumerator TwistPattern()
	{
		float fTime = 3.0f;

		while (fTime > 0)
		{
			fTime -= Time.deltaTime;
            GameObject obj = Instantiate(PrefabManager.Instance.GetPrefabByName("Twist"));

			yield return null;
		}
		ShotEnd = true;
	}


	public IEnumerator ExplosionPattern(int _lv, bool _option = false)
	{
		int _count = LVTable[Pattern.Explosion][_lv];
		float _angle = 0.0f;
		GameObject ParentObj = new GameObject("Bullet");

		SpriteRenderer renderer = ParentObj.AddComponent<SpriteRenderer>();
		renderer.sprite = BulletPrefab.GetComponent<SpriteRenderer>().sprite;

		BulletControll controll = ParentObj.AddComponent<BulletControll>();

		controll.Option = false;

		if (Target != null)
		{
			if (Target.name=="Cursor")
			{
				Vector3 ScreenTransformPosition = Camera.main.WorldToScreenPoint(transform.position);
				controll.Direction = Target.transform.position - ScreenTransformPosition;
			}
			else
			{
				controll.Direction = Target.transform.position - transform.position;
			}
		}
		else
			controll.Direction = TargetDir;

		controll.Speed = 3.3f;
		ParentObj.transform.position = transform.position;
        ParentObj.transform.localScale = BulletPrefab.transform.localScale * 2;

		yield return new WaitForSeconds(1.5f);
		Vector3 pos = ParentObj.transform.position;
		Destroy(ParentObj);

        _count = 10;
		for (int i = 0; i < _count; ++i)
		{
			GameObject Obj = Instantiate(BulletPrefab);

			BulletControll controller = Obj.GetComponent<BulletControll>();

			controller.Option = _option;
			//controller.Speed = 2.0f + _lv/2.0f;
			_angle += 360.0f / _count;
			controller.Direction = new Vector3(
				Mathf.Cos(_angle * Mathf.Deg2Rad),
				Mathf.Sin(_angle * Mathf.Deg2Rad),
				0.0f);

			Obj.transform.position = pos;

			BulletList.Add(Obj);
		}
		ShotEnd = true;
	}

	public void GuideBulletPattern(int _lv)
	{
		GameObject Obj = Instantiate(BulletPrefab);
		BulletControll controller = Obj.GetComponent<BulletControll>();

		controller.Target = Target;
		controller.Option = true;
        controller.Speed = LVTable[Pattern.GuideBullet][_lv];

		Obj.transform.position = transform.position;
		StartCoroutine(DelayShotEnd(1.5f));
	}

	private IEnumerator DelayShotEnd(float t)
	{
		yield return new WaitForSeconds(t);
		ShotEnd = true;
	}
}