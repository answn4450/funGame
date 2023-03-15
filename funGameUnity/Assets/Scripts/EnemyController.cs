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
    }

    // Update is called once per frame
    void Update()
    {
		// ** 이동정보 셋팅
		Movement = ControllerManager.GetInstance().DirRight ?
			new Vector3(Speed+1, 0.0f, 0.0f) : new Vector3(Speed, 0.0f, 0.0f);

		transform.position -= Movement * Time.deltaTime;
        Anim.SetFloat("Speed", Movement.x);
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
}