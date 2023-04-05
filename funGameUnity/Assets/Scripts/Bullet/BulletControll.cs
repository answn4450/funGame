using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControll : MonoBehaviour
{
	public string MasterTag;
	// ** 총알이 날아가는 속도
	public float Speed;
	public GameObject Target;

	public bool Option;
	public float Angnle;

	// ** 이펙트효과 원본
	public GameObject fxPrefab;

	// ** 총알이 날아가야할 방향
	public Vector3 Direction { get; set; }

	private void Start()
	{
		// ** 속도 초기값
		//Speed = ControllerManager.GetInstance().BulletSpeed;
		Speed = Option ? 0.35f : 1.0f;

		// ** 벡터의 정규화
		//Direction=(Target.transform.position - transform.position).normalized;
		Direction.Normalize();

		float fAngle = getAngle(Vector3.down, Direction);

		transform.eulerAngles = new Vector3(
			0.0f, 0.0f, fAngle);
	}

	void Update()
	{
		// ** 실시간으로 타겟의 위치를 확인하고 방향을 갱신한다.
		if (Option && Target)
		{
			Direction = (Target.transform.position - transform.position).normalized;
		}
		float fAngle = getAngle(Vector3.down, Direction);
		transform.eulerAngles = new Vector3(
		0.0f, 0.0f, fAngle);
		// ** 방향으로 속도만큼 위치를 변경
		transform.position += Direction * Speed * Time.deltaTime;

	}

	// ** 충돌체와 물리엔진이 포함된 오브젝트가 다른 충돌체와 충돌한다면 실행되는 함수. 
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// ** collision = 충돌한 대상.
		// ** 충돌한 대상을 삭제한다. 
		print(collision.transform.tag);
		if (collision.transform.tag == "wall")
		{
			GameObject Obj = Instantiate(fxPrefab);
			Obj.transform.position = transform.position;
			Destroy(this.gameObject);
		}
		else if(collision.transform.tag=="Player")
		{
			print("boss bullet player hit");
			ControllerManager.GetInstance().CommonHit(1);
			Destroy(this.gameObject);
		}
		else
		{
			// ** 진동효과를 생성할 관리자 생성.
			GameObject camera = new GameObject("Camera Test");

			// ** 진동 효과 컨트롤러 생성.
			camera.AddComponent<CameraShake>();

			// ** 이펙트효과 복제.
			GameObject Obj = Instantiate(fxPrefab);

			// ** 이펙트효과의 위치를 지정
			Obj.transform.position = transform.position;
		}
	}

	public float getAngle(Vector3 from, Vector3 to)
	{
		return Quaternion.FromToRotation(Vector3.down, to-from).eulerAngles.z;
	}
}