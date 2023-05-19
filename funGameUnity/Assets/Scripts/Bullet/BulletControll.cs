using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BulletControll : MonoBehaviour
{
	public string MasterTag;
	// ** 총알이 날아가는 속도
	public float Speed;
	public GameObject Target;

	public int HP = 5;
	public int Damage = 5;
	public bool Option;

	// ** 총알이 움직인 거리
	public Vector3 Mileage;

	// ** 이펙트효과 원본
	public GameObject fxPrefab;

	// ** 총알이 움직일 방향을 정하는 각도
    public float Angle;
	// ** 총알의 움직임
	private Vector3 Movement { get; set; }
    public Canvas Canvas;

	private void Awake()
	{
		// ** 속도 초기값
		if (Speed == 0)
			Speed = Option ? 0.7f : 1.6f;
	}


	private void Start()
	{
        setByAngle(Angle);
    }

	void Update()
	{
		if (HP<=0)
			Destroy(this.gameObject);
		// ** 실시간으로 타겟의 위치를 확인하고 방향을 갱신한다.
		if (Option && Target)
		{
			// 스크린에서 비춰지는 transform의 위치를 구한다. 
			Vector3 ScreenTransformPosition=Camera.main.WorldToScreenPoint(transform.position);
            if (Target.name == "Cursor")
                Angle = getAngle(ScreenTransformPosition, Target.transform.position);
            else
                Angle = getAngle(transform.position, Target.transform.position);

            setByAngle(Angle);
        }

        // ** 방향으로 속도만큼 위치를 변경
        transform.position += Movement * Time.deltaTime;
		Mileage += Movement * Time.deltaTime;
	}


	// ** 충돌체와 물리엔진이 포함된 오브젝트가 다른 충돌체와 충돌한다면 실행되는 함수. 
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// ** collision = 충돌한 대상.
        // 본인 편인 물체와 충돌하면 무시
		if (collision.transform.tag==MasterTag)
		    return;

        if(collision.tag == "Bullet"
            && collision.GetComponent<BulletControll>().MasterTag != MasterTag && tag!=collision.tag)
		{
			HP -= 1;
		}
		// ** 충돌한 대상을 삭제한다.
		else if (collision.transform.tag == "wall")
		{
			GameObject Obj = Instantiate(PrefabManager.Instance.GetPrefabByName(fxPrefab.name));
            
			Obj.transform.position = transform.position;

			Destroy(this.gameObject);
		}
		else if(collision.tag=="Player")
		{
			ControllerManager.GetInstance().CommonHit(Damage);
			Destroy(this.gameObject);
		}
		else if (collision.tag == "Boss")
		{
			// 맞은 Boss의 남은 체력 잠깐 띄우는 효과
			string str = collision.GetComponent<BossController>().HP.ToString();
			GameObject popText = Instantiate(PrefabManager.Instance.GetPrefabByName("PopText"));
			popText.transform.position = Input.mousePosition;
			popText.transform.position = Camera.main.WorldToScreenPoint(transform.position);


            collision.transform.GetComponent<BossController>().HP -= Damage;
            //print(Input.mousePosition);
            popText.GetComponent<Text>().text = str;
		}
		else
		{
			// ** 진동효과를 생성할 관리자 생성.
			GameObject camera = new GameObject("Camera Test");

			// ** 진동 효과 컨트롤러 생성.
			camera.AddComponent<CameraShake>();

			// ** 이펙트효과 복제.
			GameObject Obj = Instantiate(fxPrefab);

            GameObject canvas = GameObject.Find("EffectCanvas");
            Obj.transform.SetParent(canvas.transform);
            // ** 이펙트효과의 위치를 지정
            Obj.transform.position = transform.position;
		}
	}

    private GameObject CanvasFx(Vector3 _localPosition)
    {
        GameObject Obj = Instantiate(fxPrefab);
        if (Canvas)
        {
            Obj.transform.position = Camera.main.WorldToScreenPoint(_localPosition);
            Obj.transform.SetParent(Canvas.transform);
        }

        return Obj;
    }

    public static float getAngle(Vector3 from, Vector3 to)
    {
        Vector3 v = from - to;

        return (180 + Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg) % 360;
    }

    // 각도로부터 총알의 움직임과 sprite 각도를 정함
    private void setByAngle(float angle)
    {
        Movement = new Vector3(
            Mathf.Cos(Mathf.Deg2Rad * Angle),
            Mathf.Sin(Mathf.Deg2Rad * Angle)) * Speed;

        transform.eulerAngles = new Vector3(
            0.0f, 0.0f, Angle + 90.0f);
    }
}