using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	// ** 총알이 충돌한 횟수
	private int hp;

	// ** 이펙트효과 원본
	public GameObject fxPrefab;

	// ** 총알이 날아가야할 방향
	public Vector3 Direction { get; set; }
	private Color FadeOutColor = new Color(0.0f, 0.0f, 0.0f, -5f);

	private void Start()
	{
		// ** 충돌 횟수를 3으로 지정한다.
		hp = 3;
	}

	void Update()
	{
		if (GetComponent<BulletControll>().Mileage.magnitude>ControllerManager.GetInstance().Player_BulletMileage)
		{
			transform.gameObject.GetComponent<SpriteRenderer>().color += Time.deltaTime*FadeOutColor;
			if (transform.gameObject.GetComponent<SpriteRenderer>().color.a<=0)
				Destroy(this.gameObject);
		}
	}

	// ** 충돌체와 물리엔진이 포함된 오브젝트가 다른 충돌체와 충돌한다면 실행되는 함수. 
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// ** 충돌횟수 차감.
		--hp;
		// ** collision = 충돌한 대상.
		// ** 충돌한 대상을 삭제한다. 
		if (collision.transform.tag == "wall")
			Destroy(this.gameObject);
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

		// ** 총알의 충돌 횟수가 0이 되면 총알 삭제.
		if (hp == 0)
			Destroy(this.gameObject, 0.016f);
	}
}