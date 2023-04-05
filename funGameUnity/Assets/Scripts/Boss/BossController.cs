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
		Twist, D,
		Explosion, F,
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

	// ** �÷��̾��� SpriteRenderer ������Ҹ� �޾ƿ�������...
	private SpriteRenderer renderer;

	private Vector3 Movement;
	private Vector3 EndPoint;

	private float CoolDown;
	private float Speed;
	//private int HP;

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
			Anim.SetTrigger("Die");
			GetComponent<CapsuleCollider2D>().enabled = false;
			Destroy(gameObject);
		}

		//ü�¿� ����ؼ� ��Ӱ�->�������� ���� �ٲ�.
		//���� rgb ����� �� ���� �ȵ�
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
			print(choice);
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
		// ** �ൿ ���Ͽ� ���� ������ �߰� �մϴ�.

		{
			// ** �ʱ�ȭ
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

		// ** ����

		// ** ���� �������� ���ϴ� ������ �÷��̾��� ��ġ�� ������������ ����.
		EndPoint = Target.transform.position;

		// * [return]
		// * 1 : �̵�         STATE_WALK
		// * 2 : ����         STATE_ATTACK
		// * 3 : �����̵�     STATE_SLIDE
		return 3;
		//return Random.Range(STATE_WALK, STATE_SLIDE + 1);
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
		//�ִϷ� ShotBullet
		active = true;
	}

	private void onWalk()
	{
		Walk = true;

		// ** �������� ������ ������......
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
		Anim.SetTrigger("Slide");
		Anim.SetBool("SlideEnd", false);
		float timer = 6;
		Vector3 Direction = (EndPoint - transform.position).normalized;

		Movement = new Vector3(
			Speed * Direction.x,
			0.0f,
			0.0f);

		while (Mathf.Abs(transform.position.x-EndPoint.x)>0.01 && timer>0)
		{
			timer-=Time.deltaTime;
			transform.position += Movement * Time.deltaTime * Speed;
		}

		print(Mathf.Abs(transform.position.x - EndPoint.x));
		//Anim.SetBool("SlideEnd", true);
		active= true;
	}

	#region pragma BossBullet Pattern

	public void ShotBullet()
	{
		pattern = (Pattern)Random.Range(0, System.Enum.GetValues(typeof(Pattern)).Length);
		switch (pattern)
		{
			case Pattern.Screw:
				GetScrewPattern(5.0f, (int)(360 / 5.0f));
				break;

			case Pattern.DelayScrew:
				StartCoroutine(GetDelayScrewPattern());

				break;

			case Pattern.Twist:
				StartCoroutine(TwistPattern());
				break;

			case Pattern.D:

				break;

			case Pattern.Explosion:
				StartCoroutine(ExplosionPattern(1));
				break;

			case Pattern.F:

				break;

			case Pattern.GuideBullet:
				GuideBulletPattern();
				break;
		}
	}

	private void GetScrewPattern(float _angle, int _count, bool _option = false)
	{
		for (int i = 0; i < _count; ++i)
		{
			GameObject Obj = Instantiate(BulletPrefab);
			BulletControll controller = Obj.GetComponent<BulletControll>();

			controller.Option = _option;

			_angle += 5.0f;

			controller.Direction = new Vector3(
				Mathf.Cos(_angle * 3.141592f / 180),
				Mathf.Sin(_angle * 3.141592f / 180),
				0.0f) * 5 + transform.position;

			Obj.transform.position = transform.position;

			BulletList.Add(Obj);
		}
	}

	private IEnumerator GetDelayScrewPattern()
	{
		float fAngle = 30.0f;

		int iCount = (int)(360 / fAngle);

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
			yield return new WaitForSeconds(0.025f);
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
				Mathf.Cos(_angle * Mathf.Deg2Rad),
				Mathf.Sin(_angle * Mathf.Deg2Rad),
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
	#endregion pragma BossBullet Pattern

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Bullet")
		{
			HP -= (int)ControllerManager.GetInstance().Player_BulletPower;
		}
	}
}