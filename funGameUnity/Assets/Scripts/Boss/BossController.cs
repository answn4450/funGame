using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TerrainUtils;
using static BulletPattern;

public class BossController : MonoBehaviour
{
	public int MaxHP = 300;
	public int HP = 300;
	public int PatternLV = 2;

	public Sprite sprite;
	private BulletPattern.Pattern Pattern = BulletPattern.Pattern.ShotGun;

	const int STATE_WALK = 1;
	const int STATE_ATTACK = 2;
	const int STATE_SLIDE = 3;

	private GameObject Target;

	private Animator Anim;

	private SpriteRenderer renderer;

	private Vector3 Movement;
	private Vector3 EndPoint;

	private float Speed;

	private bool Walk;
	private bool active=false;

    private bool WaitShot = true;
    private int choice=STATE_ATTACK;

	private void Awake()
	{
		Target = GameObject.Find("Player");
		Anim = GetComponent<Animator>();

		renderer = GetComponent<SpriteRenderer>(); 
	}

	void Start()
	{
		GameObject.Find("Player").GetComponent<BulletPattern>().Target = gameObject;
		
		Speed = 1.3f;

		active = true;

		Walk = false;

	}
    
	private void Update()
	{
		if (HP <= 0)
		{
			//Anim.SetTrigger("Die");
			GetComponent<CapsuleCollider2D>().enabled = false;
			Destroy(gameObject);
		}

		//체력에 비례해서 어둡게->원본으로 색을 바꿈. 
		float rgb = (1.0f - (float)HP/MaxHP) * 255.0f;
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
		}

		// ** 로직

		// ** 어디로 움직일지 정하는 시점에 플레이어의 위치를 도착지점으로 셋팅.
		EndPoint = Target.transform.position;

        // ** 공격을 할지 정하는 시점에 공격 애니메이션 도중 공격 여부를 참으로 셋팅. 
        WaitShot = true;

        // * [return]
        // * 1 : 이동         STATE_WALK
        // * 2 : 공격         STATE_ATTACK
        // * 3 : 슬라이딩     STATE_SLIDE

        return Random.Range(STATE_WALK, STATE_SLIDE + 1);
	}

	private void onAttack()
	{
        //애니로 AttackShot
        Anim.SetTrigger("Attack");

        if (WaitShot)
        {
            Pattern = (Pattern)Random.Range(0, System.Enum.GetValues(typeof(BulletPattern.Pattern)).Length);
            GetComponent<BulletPattern>().pattern = Pattern;
            GetComponent<BulletPattern>().Target = Target;
        }
        else
        {
            if (Pattern == Pattern.DelayScrew)
            {
                if (GetComponent<BulletPattern>().ShotEnd)
                    active = true;  
            }
            else
                active = true;
        }
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
			Anim.SetFloat("Speed", Movement.magnitude);
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
		if (collision.tag == "Bullet" 
			&& collision.GetComponent<BulletControll>().MasterTag!="Boss")
		{
			// 플레이어가 보스를 죽였고 보스 패턴을 안갖고 있으면 패턴 선물
			if (HP <= 0)
			{
				ControllerManager.GetInstance().Player_Exp += 1;
				if (!ControllerManager.GetInstance().Player_Patterns.Contains(Pattern))
				{
					ControllerManager.GetInstance().AddPlayerPattern(Pattern);
				}
			}
		}
		else if (collision.tag=="Player")
		{
			ControllerManager.GetInstance().CommonHit(10);
		}
	}

	public void AttackShot()
	{
        if (WaitShot && !active && choice == STATE_ATTACK)
        {
            GetComponent<BulletPattern>().ShotBullet(PatternLV);
            WaitShot = false;
        }
    }
}