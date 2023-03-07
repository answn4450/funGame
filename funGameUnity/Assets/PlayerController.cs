using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.Accessibility;

using System;

public class PlayerController : MonoBehaviour
{
    private float Speed;
    private Vector3 Movement=new Vector3(0f,0f,0f);

    public Animator animator;

    private bool onAttack;
    private bool onClimb;
    private bool rollBegin;

    private bool rPressed;

    // Start is called before the first frame update
    void Start()
    {
        Speed = 5.0f;

        // ** player�� Animator�� �޾ƿ´�.
        animator = this.GetComponent<Animator>();

        onAttack = false;
        onClimb = false;
        rollBegin = false;

        rPressed = false;
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

		if (Input.GetKey(KeyCode.F))
		{
            animator.SetTrigger("Roll Begin");
		}

		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
		{
            OnClimb();
		}

        if (Input.GetKey(KeyCode.Q))
		{
            animator.SetTrigger("Hit");
		}

        if (Input.GetKey(KeyCode.R))
		{
            if (!rPressed)
			{
                
			}
            rPressed = true;
            animator.SetTrigger("Roll");
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

    private void OnClimb()
    {
        if (onClimb)
        {
            return;
        }

        onClimb = true;
        animator.SetTrigger("Climb");
    }


    private void SetAttack()
    {
        onAttack = false;
    }
    
    private void SetClimb()
    {
        onClimb = false;
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
