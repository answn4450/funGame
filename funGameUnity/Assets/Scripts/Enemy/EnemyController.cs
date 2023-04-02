//범위 안에 들어오면 쿨타임 가지고 일반공격 모션
//범위에 상관없이 일정 시간 지나면 스킬(원거리) 공격
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
	public GameObject HPBar;

	public float Speed;
	public int HP;
	public int MaxHP;
	public bool CloseToPlayer;
	public bool DoAttack;
	public bool DoSkill;
	public bool WeaponHitPlayer;

	private int Damage=2;
	private float SkillTimer;
	private Animator Anim;
	private Vector3 Movement;
	private Object Battery;

	private void Awake()
	{
		Anim = GetComponent<Animator>();
		ResetSkillTimer();
		WeaponHitPlayer= false;
		CloseToPlayer = false;
		DoAttack = false;
		DoSkill = false;
		Battery = Resources.Load("/Prefabs/Battery1");
	}

	// Start is called before the first frame update
	void Start()
	{
		Speed = 0.2f;
		CloseToPlayer = false;
		MaxHP = 3;
		HP = MaxHP;
	}

	// Update is called once per frame
	void Update()
	{
		Anim.SetFloat("Speed", Movement.x);
		if (!DoSkill)
		{
			if (DoAttack)
			{
				if (WeaponHitPlayer)
				{
					print("asfas");
					ControllerManager.GetInstance().SmallHit(Damage);
					WeaponHitPlayer = false;
				}
			}
			else
			{
				Walk();
				if (CloseToPlayer)
				{
					Anim.SetTrigger("Attack");
					DoAttack = true;
				}
			}
			

			SkillTimer -= Time.deltaTime;
			if (SkillTimer == 0)
			{
				Anim.SetTrigger("Skill");
				DoSkill = true;
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Bullet")
		{
			HP-=(int)ControllerManager.GetInstance().Player_BulletPower;

			if (HP <= 0)
			{
				Anim.SetTrigger("Die");
				GetComponent<CapsuleCollider2D>().enabled = false;
			}
		}
	}

	private void Walk()
	{
		// ** 이동정보 셋팅
		//Movement = ControllerManager.GetInstance().DirRight ?
		//	new Vector3(Speed + 0, 0.0f, 0.0f) : new Vector3(Speed, 0.0f, 0.0f);
		//Movement = 
		Climate.GetInstance().Slide(gameObject);
		//transform.position -= Movement * Time.deltaTime;
	}

	private void AttackEnd()
	{
		DoAttack=false;
	}

	private void SkillEnd()
	{
		DoSkill = false;
		ResetSkillTimer();
	}

	private void ResetSkillTimer()
	{
		SkillTimer=Random.Range(300.0f, 1000.0f);
	}

	private void DestroyEnemy()
	{
		ControllerManager.GetInstance().PlayerExp += 1;
		Destroy(gameObject, 0.016f);
		//Canvas dropItemCanvas = Canvas.find Find("DropItemCanvas");
	}

	private void DropItem()
	{
		
	}
}