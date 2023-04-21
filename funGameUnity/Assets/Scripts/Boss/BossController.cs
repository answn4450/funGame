using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static BulletPattern;

public class BossController : MonoBehaviour
{
	public int MaxHP = 300;
	public int HP = 300;

	public Sprite sprite;
	private BulletPattern.Pattern Pattern = BulletPattern.Pattern.ShotGun;

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
		GameObject.Find("Player").GetComponent<BulletPattern>().Target = gameObject;
		
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

		//return Random.Range(STATE_WALK, STATE_SLIDE + 1); ;
		return 2;
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
			// �÷��̾ ������ �׿��� ���� ������ �Ȱ��� ������ ���� ����
			if (HP <= 0 && !ControllerManager.GetInstance().Player_Patterns.Contains(Pattern))
				ControllerManager.GetInstance().Player_Patterns.Add(Pattern);
		}
		else if (collision.tag=="Player")
		{
			ControllerManager.GetInstance().CommonHit(10);
		}
	}

	public void ShotBullet()
	{
		Pattern = (BulletPattern.Pattern)Random.Range(0, System.Enum.GetValues(typeof(BulletPattern.Pattern)).Length);
		Pattern = Pattern.ShotGun;
		GetComponent<BulletPattern>().pattern = Pattern;
		GetComponent<BulletPattern>().Target = Target;
		GetComponent<BulletPattern>().ShotBullet(2);
	}
}