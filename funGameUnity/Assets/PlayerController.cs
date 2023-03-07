using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.Accessibility;

public class PlayerController : MonoBehaviour
{
    private float Speed;
    private Vector3 Movement=new Vector3(0f,0f,0f);

    public Animator animator;

    private bool onAttack;
    private bool onHit;

    private bool rollNow;

    // Start is called before the first frame update
    void Start()
    {
        Speed = 5.0f;

        // ** player�� Animator�� �޾ƿ´�.
        animator = this.GetComponent<Animator>();

        onAttack = false;
        onHit = false;

        rollNow = false;
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
        Movement=new Vector3(
            Hor * Time.deltaTime * Speed, 
            Ver * Time.deltaTime * Speed
            , 0.0f);

        if(Input.GetKey(KeyCode.LeftControl))
        {
            OnAttack();
		}
		if (Input.GetKey(KeyCode.LeftShift))
		{
			OnHit();
		}

		if (Input.GetKey(KeyCode.F))
		{
            animator.SetTrigger("Roll Begin");
		}

		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
		{
			animator.SetTrigger("Climbing");
		}


		animator.SetFloat("Speed", Hor);
		transform.position += Movement;
	}

	private void FixedUpdate()
	{
		//transform.position += Movement;
	}

    private void OnAttack()
    {
        if (onAttack)
        {
            return;
        }

        onAttack = true;
        animator.SetTrigger("Attack");
    }

	private void OnHit()
	{
		if (onHit)
		{
			return;
		}

		onHit = true;
		animator.SetTrigger("Hit");
	}

 
	private void SetAttack()
    {
        onAttack = false;
    }
    
    private void SetHit()
    {
        onHit = false;
    }
}
