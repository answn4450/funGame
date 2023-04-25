using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BulletControll : MonoBehaviour
{
	public string MasterTag;
	// ** �Ѿ��� ���ư��� �ӵ�
	public float Speed;
	public GameObject Target;

	public int HP = 5;
	public bool Option;

	// ** �Ѿ��� ������ �Ÿ�
	public Vector3 Mileage;

	// ** ����Ʈȿ�� ����
	public GameObject fxPrefab;

	private GameObject PopText;

	// ** �Ѿ��� ���ư����� ����
	public Vector3 Direction { get; set; }

	private void Awake()
	{
		// ** �ӵ� �ʱⰪ
		if (Speed == 0)
			Speed = Option ? 0.7f : 1.6f;
		
		PopText = Resources.Load("Prefabs/PopText") as GameObject;
	}


	private void Start()
	{
		// ** ������ ����ȭ
		Direction = Direction.normalized;

		float fAngle = getAngle(Vector3.down, Direction);

		transform.eulerAngles = new Vector3(
			0.0f, 0.0f, fAngle);
	}

	void Update()
	{
		if (HP<=0)
			Destroy(this.gameObject);
		// ** �ǽð����� Ÿ���� ��ġ�� Ȯ���ϰ� ������ �����Ѵ�.
		if (Option && Target)
		{
			// ��ũ������ �������� transform�� ��ġ�� ���Ѵ�. 
			Vector3 ScreenTransformPosition=Camera.main.WorldToScreenPoint(transform.position);
			if(Target.name == "Cursor")
				Direction = (Target.transform.position - ScreenTransformPosition).normalized;
			else
				Direction = (Target.transform.position - transform.position).normalized;
		}
		float fAngle = getAngle(Vector3.down, Direction);
		transform.eulerAngles = new Vector3(
		0.0f, 0.0f, fAngle);
		// ** �������� �ӵ���ŭ ��ġ�� ����
		transform.position += Direction * Speed * Time.deltaTime;
		Mileage += Direction * Speed * Time.deltaTime;
	}


	// ** �浹ü�� ���������� ���Ե� ������Ʈ�� �ٸ� �浹ü�� �浹�Ѵٸ� ����Ǵ� �Լ�. 
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// ** collision = �浹�� ���.
		if (collision.transform.tag==MasterTag)
		{
			return;
		}
		// ** �浹�� ����� �����Ѵ�.
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
			// ���� Boss�� ���� ü�� ��� ���� ȿ��
			string str = collision.GetComponent<BossController>().HP.ToString();
			GameObject popText = Instantiate(PopText) as GameObject;
			popText.transform.position = Input.mousePosition;
			popText.transform.position = Camera.main.WorldToScreenPoint(transform.position);
			
			//print(Input.mousePosition);
			popText.GetComponent<Text>().text = str;
		}
		else if(collision.tag == "Bullet")
		{
			HP -= 1;
		}
		else
		{
			// ** ����ȿ���� ������ ������ ����.
			GameObject camera = new GameObject("Camera Test");

			// ** ���� ȿ�� ��Ʈ�ѷ� ����.
			camera.AddComponent<CameraShake>();

			// ** ����Ʈȿ�� ����.
			GameObject Obj = Instantiate(fxPrefab);

			// ** ����Ʈȿ���� ��ġ�� ����
			Obj.transform.position = transform.position;
		}
	}

	public float getAngle(Vector3 from, Vector3 to)
	{
		return Quaternion.FromToRotation(Vector3.down, to-from).eulerAngles.z;
	}
}