//���� �ȿ� ������ ��Ÿ�� ������ �Ϲݰ��� ���
//������ ������� ���� �ð� ������ ��ų(���Ÿ�) ����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float Speed;
    public int HP;
	public bool closeToPlayer;

	private float Skilltimer;
    private Animator Anim;
    private Vector3 Movement;

	private void Awake()
	{
		Anim = GetComponent<Animator>();
		ResetSkillTimer();
	}

	// Start is called before the first frame update
	void Start()
    {
		if (EnemyManager.GetInstance.Distance >=10.0f)
		{
			print("10.0f");
		}

        Speed = 0.2f;
		HP = 3;
		closeToPlayer= false;
		Anim.SetFloat("SkillTimer", 4000);
    }

    // Update is called once per frame
    void Update()
    {
		// ** �̵����� ����
		Movement = ControllerManager.GetInstance().DirRight ?
			new Vector3(Speed+1, 0.0f, 0.0f) : new Vector3(Speed, 0.0f, 0.0f);

		transform.position -= Movement * Time.deltaTime;
        Anim.SetFloat("Speed", Movement.x);
		
		Anim.SetFloat("AttackTimer",
			Mathf.Max(0, Anim.GetFloat("AttackTimer") - Time.deltaTime));

		if (SkillTimer == 0)
		{
			Anim.SetTrigger("Skill");
		}
		else
		{
			if (closeToPlayer)
			{
				Anim.SetTrigger("Attack");
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

	private void SkillEnd()
	{
		ResetSkillTimer();
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

	private void ResetSkillTimer()
	{
		Anim.SetFloat("SkillTimer", Random.Range(300.0f,1000.0f));
	}

	 private void checkNear()
	 {
			//����� üũ
	 }
}