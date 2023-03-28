//범위 안에 들어오면 쿨타임 가지고 일반공격 모션
//범위에 상관없이 일정 시간 지나면 스킬(원거리) 공격
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	public GameObject Player;
	public float Speed;
    public int HP;
	public bool CloseToPlayer;
	public bool DoSkill;

	private float SkillTimer;

	private GameObject EnemyBulletPrefab;

	private Animator Anim;
    
	private Vector3 Movement;

	private void Awake()
	{
		EnemyBulletPrefab = Resources.Load("Prefabs/Enemy/EnemyBullet") as GameObject;
		Anim = GetComponent<Animator>();
		ResetSkillTimer();
		CloseToPlayer = false;
		DoSkill= false;
	}

	// Start is called before the first frame update
	void Start()
    {
		Player = GameObject.Find("Player");
		CloseToPlayer= false;
		
		Speed = 0.2f;
		HP = 3;
		Anim.SetFloat("SkillTimer", 4000);
    }

    // Update is called once per frame
    void Update()
    {
        Anim.SetFloat("Speed", Movement.x);
		
		Anim.SetFloat("AttackTimer",
			Mathf.Max(0, Anim.GetFloat("AttackTimer") - Time.deltaTime));

		//print(SkillTimer);
		if (!DoSkill)
		{
			//Walk();

			if (CloseToPlayer)
			{
				Anim.SetTrigger("Attack");
			}

			SkillTimer -= Time.deltaTime;
			if (SkillTimer <= 0)
			{
				Anim.SetTrigger("Skill");
				DoSkill = true;
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag ==	"Bullet")
		{
			--HP;

			if(HP<=0)
			{
				Anim.SetTrigger("Die");
				GetComponent<CapsuleCollider2D>().enabled = false;
			}
		}
	}

	private void Walk()
	{
		// ** 이동정보 셋팅
		Movement = ControllerManager.GetInstance().DirRight ?
			new Vector3(Speed + 1, 0.0f, 0.0f) : new Vector3(Speed, 0.0f, 0.0f);

		transform.position -= Movement * Time.deltaTime;
	}

	private void DestroyEnemy()
	{
		Destroy(gameObject, 0.016f);
	}

	private void AttackPlayer()
	{
		GameObject player = GameObject.Find("Player");
		if (player != null)
		{
			
		}
	}

	private void SpawnEnemyBullet()
	{
		GameObject Obj = Instantiate(EnemyBulletPrefab);

		// ** 복제된 총알의 위치를 현재 플레이어의 위치로 초기화한다.
		Obj.transform.position = Player.transform.position;
	}

	private void SkillEnd()
	{
		DoSkill = false;
		ResetSkillTimer();
	}

	private void ResetSkillTimer()
	{
		Anim.SetFloat("SkillTimer", Random.Range(300.0f,1000.0f));
	}

	 private void checkNear()
	 {
			//기즈모 체크
	 }
}