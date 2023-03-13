using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	// ** �Ѿ��� ���󰡴� �ӵ�
	private float Speed;
	
	// ** �Ѿ��� �浹�� Ƚ��
	private int hp;


	// ** ����Ʈȿ�� ����
	public GameObject fxPrefab;
	//overflow!
	//public Vector3 Direction
	//{
	//	get
	//	{
	//		return Direction;
	//	}
	//	set
	//	{
	//		Direction = value;
	//	}
	//}

	// ** �Ѿ��� ���󰡴� ����
	public Vector3 Direction { get; set; }

	private void Start()
	{
		// ** �ӵ� �ʱⰪ
		Speed = 6.0f;

		// ** �浹 Ƚ���� 3���� �����Ѵ�. 
		hp = 3;
	}
	// Update is called once per frame
	void Update()
    {
		// ** �������� �ӵ���ŭ ��ġ�� ����
		transform.position += Direction * Speed * Time.deltaTime;
	}

	// ** �浹ü�� ���������� ���Ե� ������Ʈ�� �ٸ� �浹ü�� �浹 �Ѵٸ� ����Ǵ� �Լ�
	private void OnTriggerEnter2D (Collider2D collision)
	{
		// ** �浹 Ƚ�� ����. 
		--hp;

		// ** ����Ʈȿ�� ����.
		GameObject Obj = Instantiate(fxPrefab);

		// ** ����ȿ���� ������ ������ ����.
		GameObject camera = new GameObject("Camera Test");

		// **���� ȿ�� ��Ʈ�ѷ� ����.
		camera.AddComponent<CameraShake>();

		// ** ����Ʈȿ���� ��ġ�� ����.
		Obj.transform.position = transform.position;

		// ** collision = �浹�� ���
		// ** �浹�� ����� �����Ѵ�. 
		if(collision.transform.tag != "wall")
		{
			Destroy(collision.transform.gameObject);
		}
		else
		{
			Destroy(this.gameObject);
		}

		// ** �Ѿ��� �浹 Ƚ���� 0�� �Ǹ� ���� ����. 
		if (hp==0)
		{
			Destroy(this.transform.gameObject);
		}
	}

	/*
	private void OnTriggerStay2D(Collider2D collision)
	{
		print("Stay");
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		print("Exit");
	}
	*/
}
