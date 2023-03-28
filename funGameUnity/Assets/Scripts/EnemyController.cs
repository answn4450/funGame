//���� �ȿ� ������ ��Ÿ�� ������ �Ϲݰ��� ���
//������ ������� ���� �ð� ������ ��ų(���Ÿ�) ����
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
		// ** �̵����� ����
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

		// ** ������ �Ѿ��� ��ġ�� ���� �÷��̾��� ��ġ�� �ʱ�ȭ�Ѵ�.
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
			//����� üũ
	 }
}