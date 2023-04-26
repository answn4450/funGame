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

	// ** 움직이는 속도
	private float Speed;

	// ** 가로 움직임을 저장하는 벡터
	private Vector3 HorMovement;
	// ** 세로 움직임을 저장하는 벡터
	private Vector3 VerMovement;

	// ** 플레이어의 Animator 구성요소를 받아오기위해...
	private Animator animator;

	// ** 플레이어의 SpriteRenderer 구성요소를 받아오기위해...
	private SpriteRenderer playerRenderer;

	// ** [상태체크]
	private bool onAttack; // 공격상태
	private bool onHit; // 피격상태

	// ** 복제된 총알의 저장공간.
	private List<GameObject> Bullets = new List<GameObject>();

	// ** 플레이어가 마지막으로 바라본 방향.
	private float Direction;
	private Vector3 BreakWind = new Vector3(0.0f, 0.0f, 0.0f);

	[Header("방향")]
	// ** 플레이어가 바라보는 방향

	[Tooltip("왼쪽")]
	public bool DirLeft;
	[Tooltip("오른쪽")]
	public bool DirRight;

	private void Awake()
	{
		// ** player 의 Animator를 받아온다.
		animator = this.GetComponent<Animator>();

		// ** player 의 SpriteRenderer를 받아온다.
		playerRenderer = this.GetComponent<SpriteRenderer>();
	}

	// ** 유니티 기본 제공 함수
	// ** 초기값을 설정할 때 사용
	void Start()
	{
		GetComponent<BulletPattern>().Speed = 0.1f;
		// ** 속도를 초기화.
		Speed = 5.0f;

		// ** 초기값 셋팅
		onAttack = false;
		onHit = false;
		Direction = 1.0f;

		DirLeft = false;
		DirRight = false;

	}

	// ** 유니티 기본 제공 함수
	// ** 프레임마다 반복적으로 실행되는 함수.
	void Update()
	{
		GetComponent<BulletPattern>().ReloadTerm = ControllerManager.GetInstance().Player_BulletTerm;
		Move();
		AutoAttack();
		Climate.GetInstance().PlayerBreakWind = BreakWind;
        if (ControllerManager.GetInstance().PlayerRunHard)
            GameStatus.GetInstance().RunDistance += (BreakWind + Climate.GetInstance().Wind).x * Time.deltaTime;
        
        // ** 좌측 쉬프트키를 입력한다면.....
        if (Input.GetKey(KeyCode.LeftShift))
			// ** 피격
			OnHit();
	}


	private void AutoAttack()
	{
		// ** 자동 공격 
		if (GetComponent<BulletPattern>().ShotEnd)
		{
			// ** 공격
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

		// **  Input.GetAxis =     -1 ~ 1 사이의 값을 반환함. 
		float Hor = Input.GetAxisRaw("Horizontal"); // -1 or 0 or 1 셋중에 하나를 반환.
		float Ver = Input.GetAxisRaw("Vertical"); // -1 or 0 or 1 셋중에 하나를 반환.
		
		animator.SetBool("Jump Up", Ver==1);
		animator.SetBool("Jump Down", Ver==-1);
		
		// ** 입력받은 값으로 플레이어를 움직인다.
		HorMovement = new Vector3(
			Hor * Time.deltaTime * Speed,
			0.0f,
			0.0f);

		// ** 입력받은 값으로 플레이어를 움직인다.
		VerMovement = new Vector3(
			0.0f,
			Ver * Time.deltaTime * Speed,
			0.0f);

		// ** Hor이 0이라면 멈춰있는 상태이므로 예외처리를 해준다. 
		if (Hor != 0)
			Direction = Hor;

		ControllerManager.GetInstance().PlayerRunHard = false;

		if ((Ver > 0 && transform.position.y < 6.38f) ||
			(Ver < 0 && transform.position.y > -8.38f))
		{ 
			transform.position += VerMovement;
		}

		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
		{
			// ** 플레이어의 좌표가 0.0 보다 작을때 플레이어만 움직인다.
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
				ControllerManager.GetInstance().PlayerRunHard = true;
				ControllerManager.GetInstance().DirRight = true;
				ControllerManager.GetInstance().DirLeft = false;
			}
		}

		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
		{
			ControllerManager.GetInstance().DirRight = false;
			ControllerManager.GetInstance().DirLeft = true;
			
			// ** 플레이어의 좌표가 -15.0 보다 클때 플레이어만 움직인다.
			if (transform.position.x > -10.0f)
				// ** 실제 플레이어를 움직인다.
				transform.position += HorMovement;
		}

		if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow)
			|| Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
		{
			ControllerManager.GetInstance().DirRight = false;
			ControllerManager.GetInstance().DirLeft = false;
		}


		// ** 플레이어가 바라보고있는 방향에 따라 이미지 반전 설정.
		if (Direction < 0)
		{
			playerRenderer.flipX = DirLeft = true;
		}
		else if (Direction > 0)
		{
			playerRenderer.flipX = false;
			DirRight = true;
		}

		// ** 플레이의 움직임에 따라 이동 모션을 실행 한다.
		animator.SetFloat("Speed", Hor);
	}

	private void OnAttack()
	{
		// ** 이미 공격모션이 진행중이라면
		if (onAttack)
			// ** 함수를 종료시킨다.
			return;

		// ** 함수가 종료되지 않았다면...
		// ** 공격상태를 활성화 하고.
		onAttack = true;

		// ** 공격모션을 실행 시킨다.
		animator.SetTrigger("Attack");
	}

	private void SetAttack()
	{
		// ** 함수가 실행되면 공격모션이 비활성화 된다.
		// ** 함수는 애니매이션 클립의 이벤트 프레임으로 삽입됨.
		onAttack = false;
	}

	private void OnHit()
	{
		// ** 이미 피격모션이 진행중이라면
		if (onHit)
			// ** 함수를 종료시킨다.
			return;

		// ** 함수가 종료되지 않았다면...
		// ** 피격상태를 활성화 하고.
		onHit = true;

		// ** 피격모션을 실행 시킨다.
		animator.SetTrigger("Hit");
	}

	private void SetHit()
	{
		// ** 함수가 실행되면 피격모션이 비활성화 된다.
		// ** 함수는 애니매이션 클립의 이벤트 프레임으로 삽입됨.
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