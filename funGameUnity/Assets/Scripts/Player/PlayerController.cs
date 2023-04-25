using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum ObjectID
{
	BULLET,
	FX,
	BULLET1,
	BULLET2,
	MAX
}


public class PlayerController : MonoBehaviour
{
	public int testLV=3;

	// ** �����̴� �ӵ�
	private float Speed;

	// ** ���� �������� �����ϴ� ����
	private Vector3 HorMovement;
	// ** ���� �������� �����ϴ� ����
	private Vector3 VerMovement;

	// ** �÷��̾��� Animator ������Ҹ� �޾ƿ�������...
	private Animator animator;

	// ** �÷��̾��� SpriteRenderer ������Ҹ� �޾ƿ�������...
	private SpriteRenderer playerRenderer;

	// ** [����üũ]
	private bool onAttack; // ���ݻ���
	private bool onHit; // �ǰݻ���

	// ** ������ �Ѿ��� �������.
	private List<GameObject> Bullets = new List<GameObject>();

	// ** �÷��̾ ���������� �ٶ� ����.
	private float Direction;
	private Vector3 BreakWind = new Vector3(0.0f, 0.0f, 0.0f);
	private Vector3 Gravity = new Vector3(0.0f, -1.0f, 0.0f);

	[Header("����")]
	// ** �÷��̾ �ٶ󺸴� ����

	[Tooltip("����")]
	public bool DirLeft;
	[Tooltip("������")]
	public bool DirRight;

	private void Awake()
	{
		// ** player �� Animator�� �޾ƿ´�.
		animator = this.GetComponent<Animator>();

		// ** player �� SpriteRenderer�� �޾ƿ´�.
		playerRenderer = this.GetComponent<SpriteRenderer>();
	}

	// ** ����Ƽ �⺻ ���� �Լ�
	// ** �ʱⰪ�� ������ �� ���
	void Start()
	{
		GetComponent<BulletPattern>().Speed = 0.1f;
		// ** �ӵ��� �ʱ�ȭ.
		Speed = 5.0f;

		// ** �ʱⰪ ����
		onAttack = false;
		onHit = false;
		Direction = 1.0f;

		DirLeft = false;
		DirRight = false;

	}

	// ** ����Ƽ �⺻ ���� �Լ�
	// ** �����Ӹ��� �ݺ������� ����Ǵ� �Լ�.
	void Update()
	{
		transform.position += Gravity * Time.deltaTime;
		GetComponent<BulletPattern>().ReloadTerm = ControllerManager.GetInstance().Player_BulletTerm;
		Move();
		AutoAttack();
		Climate.GetInstance().PlayerBreakWind = BreakWind;
		GameStatus.GetInstance().RunDistance += (BreakWind + Climate.GetInstance().Wind).x * Time.deltaTime;

		// ** ���� ����ƮŰ�� �Է��Ѵٸ�.....
		if (Input.GetKey(KeyCode.LeftShift))
			// ** �ǰ�
			OnHit();
	}


	private void AutoAttack()
	{
		// ** �ڵ� ���� 
		if (GetComponent<BulletPattern>().ShotEnd)
		{
			// ** ����
			OnAttack();
			BulletPattern.Pattern pattern = ControllerManager.GetInstance().Player_Pattern;
			GetComponent<BulletPattern>().pattern = pattern;
			GetComponent<BulletPattern>().ShotBullet(ControllerManager.GetInstance().Player_PatternLV[pattern]);
		}
	}


	private void Move()
	{
		float mazinoX = 0.1f;
		Climate.GetInstance().Slide(gameObject);

		// **  Input.GetAxis =     -1 ~ 1 ������ ���� ��ȯ��. 
		float Hor = Input.GetAxisRaw("Horizontal"); // -1 or 0 or 1 ���߿� �ϳ��� ��ȯ.
		float Ver = Input.GetAxisRaw("Vertical"); // -1 or 0 or 1 ���߿� �ϳ��� ��ȯ.
		
		animator.SetBool("Jump Up", Ver==1);
		animator.SetBool("Jump Down", Ver==-1);
		
		// ** �Է¹��� ������ �÷��̾ �����δ�.
		HorMovement = new Vector3(
			Hor * Time.deltaTime * Speed,
			0.0f,
			0.0f);

		// ** �Է¹��� ������ �÷��̾ �����δ�.
		VerMovement = new Vector3(
			0.0f,
			Ver * Time.deltaTime * Speed,
			0.0f);

		// ** Hor�� 0�̶�� �����ִ� �����̹Ƿ� ����ó���� ���ش�. 
		if (Hor != 0)
			Direction = Hor;

		Climate.GetInstance().PlayerRunHard = false;

		if ((Ver > 0 && transform.position.y < 6.38f) ||
			(Ver < 0 && transform.position.y > -8.38f))
		{ 
			transform.position += VerMovement;
		}

		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
		{
			// ** �÷��̾��� ��ǥ�� 0.0 ���� ������ �÷��̾ �����δ�.
			if (transform.position.x < mazinoX)
			{ 
				transform.position += HorMovement;
				if (transform.position.x > mazinoX)
					transform.position = new Vector3(
						mazinoX,
						transform.position.y,
						transform.position.z
						);
			}
			if (transform.position.x >= mazinoX)
			{
				Climate.GetInstance().PlayerRunHard = true;
				ControllerManager.GetInstance().DirRight = true;
				ControllerManager.GetInstance().DirLeft = false;
			}
		}

		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
		{
			ControllerManager.GetInstance().DirRight = false;
			ControllerManager.GetInstance().DirLeft = true;
			
			// ** �÷��̾��� ��ǥ�� -15.0 ���� Ŭ�� �÷��̾ �����δ�.
			if (transform.position.x > -10.0f)
				// ** ���� �÷��̾ �����δ�.
				transform.position += HorMovement;
		}

		if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow)
			|| Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
		{
			ControllerManager.GetInstance().DirRight = false;
			ControllerManager.GetInstance().DirLeft = false;
		}


		// ** �÷��̾ �ٶ󺸰��ִ� ���⿡ ���� �̹��� ���� ����.
		if (Direction < 0)
		{
			playerRenderer.flipX = DirLeft = true;
		}
		else if (Direction > 0)
		{
			playerRenderer.flipX = false;
			DirRight = true;
		}

		// ** �÷����� �����ӿ� ���� �̵� ����� ���� �Ѵ�.
		animator.SetFloat("Speed", Hor);
	}

	private void OnAttack()
	{
		// ** �̹� ���ݸ���� �������̶��
		if (onAttack)
			// ** �Լ��� �����Ų��.
			return;

		// ** �Լ��� ������� �ʾҴٸ�...
		// ** ���ݻ��¸� Ȱ��ȭ �ϰ�.
		onAttack = true;

		// ** ���ݸ���� ���� ��Ų��.
		animator.SetTrigger("Attack");
	}

	private void SetAttack()
	{
		// ** �Լ��� ����Ǹ� ���ݸ���� ��Ȱ��ȭ �ȴ�.
		// ** �Լ��� �ִϸ��̼� Ŭ���� �̺�Ʈ ���������� ���Ե�.
		onAttack = false;
	}

	private void OnHit()
	{
		// ** �̹� �ǰݸ���� �������̶��
		if (onHit)
			// ** �Լ��� �����Ų��.
			return;

		// ** �Լ��� ������� �ʾҴٸ�...
		// ** �ǰݻ��¸� Ȱ��ȭ �ϰ�.
		onHit = true;

		// ** �ǰݸ���� ���� ��Ų��.
		animator.SetTrigger("Hit");
	}

	private void SetHit()
	{
		// ** �Լ��� ����Ǹ� �ǰݸ���� ��Ȱ��ȭ �ȴ�.
		// ** �Լ��� �ִϸ��̼� Ŭ���� �̺�Ʈ ���������� ���Ե�.
		onHit = false;
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.transform.tag == "wall")
			GameStatus.GetInstance().PlayerHP -= 500 * Time.deltaTime;
	}

	public void LikeJump()
	{
		animator.SetBool("Jump Up", Input.GetKey(KeyCode.Q));
		animator.SetBool("Jump Down", Input.GetKey(KeyCode.E));
	}
}