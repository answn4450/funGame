using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BossController : MonoBehaviour
{
    const int STATE_WALK = 1;
    const int STATE_ATTACK = 2;
    const int STATE_SLIDE = 3;
	private GameObject Target;

    private SpriteRenderer renderer;
    private Animator Anim;

    private Vector3 Movement;
    private Vector3 EndPoint;
    
    private float CoolDown;
    private float Speed;
    private int HP;

    private bool SkillAttack;
    private bool Attack;
    private bool active;
    private bool Walk;

    private int choice;

	private void Awake()
	{
        Target = GameObject.Find("Player");
        Anim= GetComponent<Animator>();

        renderer= GetComponent<SpriteRenderer>();
	}

	void Start()
    {
        Movement=new Vector3(0.0f,0.0f,0.0f);
        CoolDown = 1.5f;
        Speed = 0.5f;
        HP = 30000;
        SkillAttack= false;
        Attack= false;
        Walk = false;
        choice = 0;

        active = true;
		StartCoroutine(onCooldown());
	}

    void Update()
    {
        if (ControllerManager.GetInstance().DirRight)
        {
			transform.position -= new Vector3(Speed, 0.0f, 0.0f) * Time.deltaTime;
		}

		float result = Target.transform.position.x - transform.position.x;
        
        if (result <0.0f)
        {
            renderer.flipX = true;
        }
        else if (result > 0.0f)
        {
            renderer.flipX = false;
        }

        if (active)
        {
			switch (onController())
			{
				case 0:
					Anim.SetTrigger("Attack");
					onAttack();
					break;

				case 1:
					Anim.SetFloat("Speed", Movement.x);
					onWalk();
					break;

				case 2:
					Anim.SetTrigger("Slide_jump");
					onSlide();
					break;
			}
		}
        else
        {
            StartCoroutine(onCooldown());
        }
    }

    private void onAttack()
    {
        print("Attack");
        active = true;
	}

	private void onWalk()
	{
        print("onWalk");
        Walk = true;

        //** 목적지에 도착할 때까지
        float Distance=Vector3.Distance(EndPoint, transform.position);
        if (Distance > 0.5f)
        {
            Vector3 Direction = (EndPoint-transform.position).normalized;

            Movement = new Vector3(
                Speed*Distance, 
                Speed*Distance, 
                0.0f 
                );
            transform.position += Movement * Time.deltaTime;
            Anim.SetFloat("Speed",Speed);
        }
        else
            active = false;
	}

	private void onSlide()
	{
        {
		    choice = 0;
		    print("Slide");
		    StartCoroutine(onCooldown());
        }
        active= true;
	}

    private int onController()
    {
        // ** 행동 패턴에 대한 내용

        {
			// ** 초기화
            if (Walk)
			{
                Anim.SetFloat("Speed", Movement.x);
				Movement = new Vector3(0.0f, 0.0f, 0.0f);
				Walk = false;
            }
            if (Attack)
            {
                Attack = false;
            }
            if (SkillAttack)
            {
                SkillAttack= false;
            }
		}
        // ** 로직

        //** 어디로 움직일지 정하는 시점에 플레이어의 위치를 도착점으로 설정
        EndPoint=Target.transform.position;

        return Random.Range(STATE_WALK,STATE_SLIDE+1);
		// * [return]
		// * 0: 공격      Attack
		// * 1: 이동      Walk
		// * 2: 슬라이딩  Slide

	}

	private IEnumerator onCooldown()
	{
        float fTime = CoolDown;

        while(fTime>0.0f)
        {
            fTime -= Time.deltaTime;
            yield return null;
        }

		active = false;
		choice = onController();
	}
}