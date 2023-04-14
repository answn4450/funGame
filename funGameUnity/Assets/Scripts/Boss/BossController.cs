using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static BulletPattern;

public class BossController : MonoBehaviour
{
	public enum Pattern
	{
		Screw,
		DelayScrew,
		ShotGun,
		Explosion,
		GuideBullet
	};

	public int MaxHP = 300;
	public int HP = 300;

	public Pattern pattern = Pattern.Screw;
	public Sprite sprite;

	private List<GameObject> BulletList = new List<GameObject>();
	private GameObject BulletPrefab;

	const int STATE_WALK = 1;
	const int STATE_ATTACK = 2;
	const int STATE_SLIDE = 3;

	private GameObject Target;

	private Animator Anim;

	private SpriteRenderer renderer;

	private Vector3 Movement;
	private Vector3 EndPoint;

	private float CoolDown;
	private float Speed;

	private bool Attack;
	private bool Walk;
	private bool active;

	private int choice;

	private void Awake()
	{
		Target = GameObject.Find("Player");

		Anim = GetComponent<Animator>();

		renderer = GetComponent<SpriteRenderer>();
	}

	void Start()
	{
		BulletPrefab = Resources.Load("Prefabs/Boss/BossBullet") as GameObject;

		CoolDown = 1.5f;
		Speed = 1.3f;

		Attack = false;

		active = true;

		Attack = false;
		Walk = false;
	}

	void Update()
	{
		if (HP <= 0)
		{
			//Anim.SetTrigger("Die");
			GetComponent<CapsuleCollider2D>().enabled = false;
			Destroy(gameObject);
		}

		//체력에 비례해서 어둡게->원본으로 색을 바꿈.
		//막힘 rgb 상수가 잘 적용 안됨
		float rgb = (1.0f - (float)HP/MaxHP) * 255.0f;
		//print(rgb);
		//print(transform.GetComponent<SpriteRenderer>().color);
		renderer.color = new Color(
			rgb,rgb,rgb);

		float result = EndPoint.x - transform.position.x;

		if (result < 0.0f)
			renderer.flipX = true;
		else if (result > 0.0f)
			renderer.flipX = false;

		if (active)
		{
			active = false;
			choice = onController();
			StartCoroutine(onCooldown());
		}
		else
		{
			switch (choice)
			{
				case STATE_WALK:
					onWalk();
					break;

				case STATE_ATTACK:
					onAttack();
					break;

				case STATE_SLIDE:
					onSlide();
					break;
			}
		}
	}

	private int onController()
	{
		// ** 행동 패턴에 대한 내용을 추가 합니다.

		{
			// ** 초기화
			if (Walk)
			{
				Movement = new Vector3(0.0f, 0.0f, 0.0f);
				Anim.SetFloat("Speed", Movement.x);
				Walk = false;
			}

			if (Attack)
			{
				Attack = false;
			}
		}

		// ** 로직

		// ** 어디로 움직일지 정하는 시점에 플레이어의 위치를 도착지점으로 셋팅.
		EndPoint = Target.transform.position;

		// * [return]
		// * 1 : 이동         STATE_WALK
		// * 2 : 공격         STATE_ATTACK
		// * 3 : 슬라이딩     STATE_SLIDE

		return Random.Range(STATE_WALK, STATE_SLIDE + 1); ;
		//print("1:이동 2:공격 3:슬라이딩"+c.ToString());
	}


	private IEnumerator onCooldown()
	{
		float fTime = CoolDown;

		while (fTime > 0.0f)
		{
			fTime -= Time.deltaTime;
			yield return null;
		}
	}

	private void onAttack()
	{
		Anim.SetTrigger("Attack");
		//애니로 ShotBullet
		active = true;
	}

	private void onWalk()
	{
		Walk = true;

		// ** 목적지에 도착할 때까지......
		float Distance = Vector3.Distance(EndPoint, transform.position);

		if (Distance > 0.5f)
		{
			Vector3 Direction = (EndPoint - transform.position).normalized;

			Movement = new Vector3(
				Speed * Direction.x,
				Speed * Direction.y,
				0.0f);

			transform.position += Movement * Time.deltaTime;
			Anim.SetFloat("Speed", Mathf.Abs(Movement.x));
		}
		else
			active = true;
	}

	private void onSlide()
	{
		Anim.SetBool("Slide", true);
		Vector3 Direction = (EndPoint - transform.position).normalized;
		Movement = new Vector3(
			Speed * Direction.x * Time.deltaTime * 5,
			0.0f,
			0.0f);

		if (Mathf.Abs(transform.position.x - EndPoint.x) > 1)
		{
			transform.position += Movement;
		}
		else
		{
			active = true;
			Anim.SetBool("Slide", false);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Bullet")
		{
			HP -= (int)ControllerManager.GetInstance().Player_BulletPower;
		}
		else if (collision.tag=="Player")
		{
			ControllerManager.GetInstance().CommonHit(10);
		}
	}
	
	public void ShotBullet()
	{
		pattern = (Pattern)Random.Range(0, System.Enum.GetValues(typeof(Pattern)).Length);
		//print(pattern);
		switch (pattern)
		{
			case Pattern.Screw:
				GetScrewPattern(20);
				break;

			case Pattern.DelayScrew:
				StartCoroutine(GetDelayScrewPattern());
				break;

			case Pattern.ShotGun:
				GetShotGunPattern(12);
				break;

			case Pattern.Explosion:
				StartCoroutine(ExplosionPattern(10));
				break;

			case Pattern.GuideBullet:
				GuideBulletPattern();
				break;
		}
	}
	///////////////////////BulletPattern.cs 복붙////////////////////////////

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
			controller.Direction = 0.5f * speed * (offSet + (Target.transform.position - transform.position).normalized);
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