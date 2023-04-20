using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletControll : MonoBehaviour
{
	public string MasterTag;
	// ** 총알이 날아가는 속도
	public float Speed;
	public GameObject Target;

	public int HP = 5;
	public bool Option;
	public float Angnle;
	// ** 총알이 움직인 거리
	public Vector3 Mileage;

	// ** 이펙트효과 원본
	public GameObject fxPrefab;

	private GameObject Text;

	// ** 총알이 날아가야할 방향
	public Vector3 Direction { get; set; }

	private void Awake()
	{
		// ** 속도 초기값
		Speed = Option ? 0.35f : 0.8f;
		Text = Resources.Load("Prefabs/Text1") as GameObject;
	}


	private void Start()
	{
		// ** 벡터의 정규화
		//Direction=(Target.transform.position - transform.position).normalized;
		Direction.Normalize();

		float fAngle = getAngle(Vector3.down, Direction);

		transform.eulerAngles = new Vector3(
			0.0f, 0.0f, fAngle);
	}

	void Update()
	{
		if (HP<=0)
			Destroy(this.gameObject);
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
		Mileage += Direction * Speed * Time.deltaTime;
	}

	// ** 충돌체와 물리엔진이 포함된 오브젝트가 다른 충돌체와 충돌한다면 실행되는 함수. 
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// ** collision = 충돌한 대상.
		if (collision.transform.tag==MasterTag)
		{
			return;
		}
		// ** 충돌한 대상을 삭제한다.
		else if (collision.transform.tag == "wall")
		{
			GameObject Obj = Instantiate(fxPrefab);
			Obj.transform.position = transform.position;
			Destroy(this.gameObject);
		}
		else if(collision.tag=="Player")
		{
			ControllerManager.GetInstance().CommonHit(1);
			Destroy(this.gameObject);
		}
		else if (collision.tag == "Boss")
		{
			// 남은 체력 잠깐 띄우는 효과
			string str = collision.GetComponent<BossController>().HP.ToString();
			GameObject a = Instantiate(Text) as GameObject;
			a.transform.position = Input.mousePosition;
			a.transform.position = transform.position;
			
			//print(Input.mousePosition);
			a.GetComponent<Text>().text = str;
		}
		else if(collision.tag=="Bullet")
		{
			HP -= 1;
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