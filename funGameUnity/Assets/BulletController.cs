using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	// ** 총알이 날라가는 속도
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

	// ** 총알이 날라가는 방향
	public Vector3 Direction { get; set; }

	private void Start()
	{
		// ** 속도 초기값
		Speed = 6.0f;
	}
	// Update is called once per frame
	void Update()
    {

		// ** 방향으로 속도만큼 위치를 변경
		transform.position += Direction * Speed * Time.deltaTime;
	}

	// ** 충돌체와 물리엔진이 포함된 오브젝트가 다른 충돌체와 충돌 한다면 실행되는 함수
	private void OnTriggerEnter2D (Collider2D other)
	{
		DestroyObject(this.gameObject);
	}
}
