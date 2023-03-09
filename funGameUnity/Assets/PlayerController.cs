using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.Accessibility;

using System;
using UnityEditor.Experimental.GraphView;

public class PlayerController : MonoBehaviour
{
	// ** 움직이는 속도
	private float Speed;
	// ** 움직임을 저장하는 벡터
	private Vector3 Movement=new Vector3(0f,0f,0f);

	// ** 플레이어의 Animator 구성 요소를 받아오기 위해..
	public Animator animator;
    // **플레이어의 SpriteRenderer 구성요소
    private SpriteRenderer playerRenderer;

	// ** [상태체크]
	private bool onAttack;
    private bool onHit;

    private bool rPressed;

	// ** 복제할 총알 원본
	public GameObject BulletPrefab;
	public GameObject[] StageBack = new GameObject[7];

	// ** 복제할 fx 원본
	public GameObject FxPrefab;

	private List<GameObject> Bullets = new List<GameObject>();

    private float Direction;

	private void Awake()
	{
		// ** player의 Animator를 받아온다.
		animator = this.GetComponent<Animator>();

		// ** player의 SpriteRenderer를 받아온다.
		playerRenderer = this.GetComponent<SpriteRenderer>();
	}

	// Start is called before the first frame update
	void Start()
    {
		// ** 속도를 초기화
		Speed = 5.0f;

        onAttack = false;
        onHit = false;

        rPressed = false;

		Direction = 0;

		for (int i = 0; i < 7; ++i)
		{
			StageBack[i] = GameObject.Find(i.ToString());
		}
	}

    // Update is called once per frame
    // ** 프레임마다 반복적으로 실행되는 함수.
    void Update()
    {
        // ** 실수 연산 IEEE754
        // ** Input.GetAxis = -1 ~ 1 사이의 값을 반환함.
        // ** Input.GetAxisRaw = -1, 0, 1 중 하나의 값을 반환함.
        float Hor = Input.GetAxisRaw("Horizontal");
        float Ver = Input.GetAxisRaw("Vertical");

        // ** Hor 이 0이라면 멈춰있는 상태이므로 예외처리
        if (Hor != 0)
			Direction = Hor;

		// ** 플레이어가 바라보고 있는 방향에 따라 이미지 플립 설정
		if (Direction<0)
        {
			playerRenderer.flipX = true;
		}
        else if (Direction>0)
        {
            playerRenderer.flipX = false;
        }

		// ** 입력받은 값으로 플레이어를 움직인다. 
		Movement = new Vector3(
     //     Hor * Time.deltaTime * Speed, 
		0,
            Ver * Time.deltaTime * Speed
            , 0.0f);

		// ** 좌측 컨트롤키를 입력한다면.....
		if (Input.GetKey(KeyCode.LeftControl))
		{
			// ** 공격
			OnAttack();
		}

		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
		{
			animator.SetTrigger("Climb");
		}

		// ** 좌측 쉬프트키를 입력한다면
		if (Input.GetKey(KeyCode.LeftShift))
		{
			// ** 피격
			OnHit();
		}

		if (Input.GetKey(KeyCode.R))
		{
            if (!rPressed)
			{
				animator.SetTrigger("Roll Begin");
			}
            rPressed = true;
		}
        else
        {
            rPressed = false;
            animator.SetTrigger("Roll End");
        }

		// ** 스페이스바를 입력한다면..
		if (Input.GetKeyDown(KeyCode.Space))
		{
			// ** 공격
			OnAttack();

			// ** 총알원본을 본제한다.
			GameObject Obj = Instantiate(BulletPrefab);

			// ** 복제된 총알의 위치를 현재 플레이어의 위치로 초기화한다.
			Obj.transform.position = transform.position;

			// ** 총알의 BullerController 스크립트를 받아온다.
			BulletController Controller = Obj.AddComponent<BulletController>();
			
			Controller.fxPrefab = FxPrefab;

			// ** 총알 스크립트내부의 방향 변수를 현재 플레이어의 방향 변수로 설정 한다.
			Controller.Direction = new Vector3(Direction, 0.0f, 0.0f);

			// ** 총알의 SpriteRenderer를 받아온다.
			SpriteRenderer buleltRenderer = Obj.GetComponent<SpriteRenderer>();

			// ** 총알의 이미지 반전 상태를 플레이어의 이미지 반전 상태로 설정한다.
			buleltRenderer.flipY = playerRenderer.flipX;

			// ** 모든 설정이 종료되었다면 저장소에 보관한다.
			Bullets.Add(Obj);

		}

		//** 플레이어의 움직임에 따라 애니메이션 설정
		animator.SetFloat("Speed", Hor);

		//** 실제 플레이어를 움직인다.
		transform.position += Movement;
	}

    private void OnAttack()
	{
		//** 이미 공격모션이 진행중이라면
		if (onAttack)
		{
			//**함수를 종료한다.
			return;
        }

		//** 함수가 종료되지 않았다면...
		//**공격상태를 활성화 하고
		onAttack = true;

		//**공격 모션을 실행 시킨다.
		animator.SetTrigger("Attack");
    }

	private void OnHit()
	{
		//** 이미 피격모션이 진행중이라면
		if (onHit)
		{
			//**함수를 종료한다.
			return;
		}

		//** 함수가 종료되지 않았다면...
		//**피격상태를 활성화 하고
		onHit = true;

		//**피격 모션을 실행 시킨다.
		animator.SetTrigger("Hit");
	}

	private void SetAttack()
	{
		//** 함수가 실행되면 공격 모션이 비활성화 된다.
		//** 함수는 애니메이션 클립의 이벤트 프레임으로 삽입됨.
		onAttack = false;
    }

	private void SetHit()
	{
		//** 함수가 실행되면 피격 모션이 비활성화 된다.
		//** 함수는 애니메이션 클립의 이벤트 프레임으로 삽입됨.
		onHit = false;
	}

	private void SetDie()
	{
        animator.SetTrigger("Die");
	}

    private void Rebirth()
	{
        animator.SetTrigger("Rebirth");
	}
}
