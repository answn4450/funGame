//범위 안에 들어오면 쿨타임 가지고 일반공격 모션
//범위에 상관없이 일정 시간 지나면 스킬(원거리) 공격
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float Speed;
    public int HP;

    private Animator Anim;
    private Vector3 Movement;

	private void Awake()
	{
		Anim = GetComponent<Animator>();
	}

	// Start is called before the first frame update
	void Start()
    {
        Speed = 0.2f;
		HP = 3;
		Anim.SetFloat("AttackTimer", 1000);
		Anim.SetFloat("SkillTimer", 4000);
    }

    // Update is called once per frame
    void Update()
    {
		// ** 이동정보 셋팅
		Movement = ControllerManager.GetInstance().DirRight ?
			new Vector3(Speed+1, 0.0f, 0.0f) : new Vector3(Speed, 0.0f, 0.0f);

		transform.position -= Movement * Time.deltaTime;
        Anim.SetFloat("Speed", Movement.x);
		
		Anim.SetFloat("AttackTimer",
			Mathf.Max(0, Anim.GetFloat("AttackTimer") - Time.deltaTime));
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

	private void ResetAttackTimer()
	{
		Anim.SetFloat("AttackTimer", 100.0f);
	}
}