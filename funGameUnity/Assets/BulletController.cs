using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	// ** �Ѿ��� ���󰡴� �ӵ�
	private float Speed;

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
	}
	// Update is called once per frame
	void Update()
    {

		// ** �������� �ӵ���ŭ ��ġ�� ����
		transform.position += Direction * Speed * Time.deltaTime;
	}

	// ** �浹ü�� ���������� ���Ե� ������Ʈ�� �ٸ� �浹ü�� �浹 �Ѵٸ� ����Ǵ� �Լ�
	private void OnTriggerEnter2D (Collider2D other)
	{
		DestroyObject(this.gameObject);
	}
}
