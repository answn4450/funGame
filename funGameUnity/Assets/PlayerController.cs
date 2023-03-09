using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.Accessibility;

using System;
using UnityEditor.Experimental.GraphView;

public class PlayerController : MonoBehaviour
{
	// ** �����̴� �ӵ�
	private float Speed;
	// ** �������� �����ϴ� ����
	private Vector3 Movement=new Vector3(0f,0f,0f);

	// ** �÷��̾��� Animator ���� ��Ҹ� �޾ƿ��� ����..
	public Animator animator;
    // **�÷��̾��� SpriteRenderer �������
    private SpriteRenderer playerRenderer;

	// ** [����üũ]
	private bool onAttack;
    private bool onHit;

    private bool rPressed;

	// ** ������ �Ѿ� ����
	public GameObject BulletPrefab;
	public GameObject[] StageBack = new GameObject[7];

	// ** ������ fx ����
	public GameObject FxPrefab;

	private List<GameObject> Bullets = new List<GameObject>();

    private float Direction;

	private void Awake()
	{
		// ** player�� Animator�� �޾ƿ´�.
		animator = this.GetComponent<Animator>();

		// ** player�� SpriteRenderer�� �޾ƿ´�.
		playerRenderer = this.GetComponent<SpriteRenderer>();
	}

	// Start is called before the first frame update
	void Start()
    {
		// ** �ӵ��� �ʱ�ȭ
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
    // ** �����Ӹ��� �ݺ������� ����Ǵ� �Լ�.
    void Update()
    {
        // ** �Ǽ� ���� IEEE754
        // ** Input.GetAxis = -1 ~ 1 ������ ���� ��ȯ��.
        // ** Input.GetAxisRaw = -1, 0, 1 �� �ϳ��� ���� ��ȯ��.
        float Hor = Input.GetAxisRaw("Horizontal");
        float Ver = Input.GetAxisRaw("Vertical");

        // ** Hor �� 0�̶�� �����ִ� �����̹Ƿ� ����ó��
        if (Hor != 0)
			Direction = Hor;

		// ** �÷��̾ �ٶ󺸰� �ִ� ���⿡ ���� �̹��� �ø� ����
		if (Direction<0)
        {
			playerRenderer.flipX = true;
		}
        else if (Direction>0)
        {
            playerRenderer.flipX = false;
        }

		// ** �Է¹��� ������ �÷��̾ �����δ�. 
		Movement = new Vector3(
     //     Hor * Time.deltaTime * Speed, 
		0,
            Ver * Time.deltaTime * Speed
            , 0.0f);

		// ** ���� ��Ʈ��Ű�� �Է��Ѵٸ�.....
		if (Input.GetKey(KeyCode.LeftControl))
		{
			// ** ����
			OnAttack();
		}

		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
		{
			animator.SetTrigger("Climb");
		}

		// ** ���� ����ƮŰ�� �Է��Ѵٸ�
		if (Input.GetKey(KeyCode.LeftShift))
		{
			// ** �ǰ�
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

		// ** �����̽��ٸ� �Է��Ѵٸ�..
		if (Input.GetKeyDown(KeyCode.Space))
		{
			// ** ����
			OnAttack();

			// ** �Ѿ˿����� �����Ѵ�.
			GameObject Obj = Instantiate(BulletPrefab);

			// ** ������ �Ѿ��� ��ġ�� ���� �÷��̾��� ��ġ�� �ʱ�ȭ�Ѵ�.
			Obj.transform.position = transform.position;

			// ** �Ѿ��� BullerController ��ũ��Ʈ�� �޾ƿ´�.
			BulletController Controller = Obj.AddComponent<BulletController>();
			
			Controller.fxPrefab = FxPrefab;

			// ** �Ѿ� ��ũ��Ʈ������ ���� ������ ���� �÷��̾��� ���� ������ ���� �Ѵ�.
			Controller.Direction = new Vector3(Direction, 0.0f, 0.0f);

			// ** �Ѿ��� SpriteRenderer�� �޾ƿ´�.
			SpriteRenderer buleltRenderer = Obj.GetComponent<SpriteRenderer>();

			// ** �Ѿ��� �̹��� ���� ���¸� �÷��̾��� �̹��� ���� ���·� �����Ѵ�.
			buleltRenderer.flipY = playerRenderer.flipX;

			// ** ��� ������ ����Ǿ��ٸ� ����ҿ� �����Ѵ�.
			Bullets.Add(Obj);

		}

		//** �÷��̾��� �����ӿ� ���� �ִϸ��̼� ����
		animator.SetFloat("Speed", Hor);

		//** ���� �÷��̾ �����δ�.
		transform.position += Movement;
	}

    private void OnAttack()
	{
		//** �̹� ���ݸ���� �������̶��
		if (onAttack)
		{
			//**�Լ��� �����Ѵ�.
			return;
        }

		//** �Լ��� ������� �ʾҴٸ�...
		//**���ݻ��¸� Ȱ��ȭ �ϰ�
		onAttack = true;

		//**���� ����� ���� ��Ų��.
		animator.SetTrigger("Attack");
    }

	private void OnHit()
	{
		//** �̹� �ǰݸ���� �������̶��
		if (onHit)
		{
			//**�Լ��� �����Ѵ�.
			return;
		}

		//** �Լ��� ������� �ʾҴٸ�...
		//**�ǰݻ��¸� Ȱ��ȭ �ϰ�
		onHit = true;

		//**�ǰ� ����� ���� ��Ų��.
		animator.SetTrigger("Hit");
	}

	private void SetAttack()
	{
		//** �Լ��� ����Ǹ� ���� ����� ��Ȱ��ȭ �ȴ�.
		//** �Լ��� �ִϸ��̼� Ŭ���� �̺�Ʈ ���������� ���Ե�.
		onAttack = false;
    }

	private void SetHit()
	{
		//** �Լ��� ����Ǹ� �ǰ� ����� ��Ȱ��ȭ �ȴ�.
		//** �Լ��� �ִϸ��̼� Ŭ���� �̺�Ʈ ���������� ���Ե�.
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
