using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	// ** �Ѿ��� ���󰡴� �ӵ�
	private float Speed;
	
	private int hp;

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
		--hp;

		GameObject Obj = Instantiate(fxPrefab);

		GameObject camera = new GameObject("Camera Test");
		camera.AddComponent<CameraShake>();

		Obj.transform.position = transform.position;

		//print("Enter");
		DestroyObject(collision.transform.gameObject);
		if (hp==0)
		{
			DestroyObject(this.transform.gameObject);
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
