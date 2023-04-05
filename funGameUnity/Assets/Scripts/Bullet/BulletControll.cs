using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControll : MonoBehaviour
{
	public string MasterTag;
	// ** �Ѿ��� ���ư��� �ӵ�
	public float Speed;
	public GameObject Target;

	public bool Option;
	public float Angnle;

	// ** ����Ʈȿ�� ����
	public GameObject fxPrefab;

	// ** �Ѿ��� ���ư����� ����
	public Vector3 Direction { get; set; }

	private void Start()
	{
		// ** �ӵ� �ʱⰪ
		//Speed = ControllerManager.GetInstance().BulletSpeed;
		Speed = Option ? 0.35f : 1.0f;

		// ** ������ ����ȭ
		//Direction=(Target.transform.position - transform.position).normalized;
		Direction.Normalize();

		float fAngle = getAngle(Vector3.down, Direction);

		transform.eulerAngles = new Vector3(
			0.0f, 0.0f, fAngle);
	}

	void Update()
	{
		// ** �ǽð����� Ÿ���� ��ġ�� Ȯ���ϰ� ������ �����Ѵ�.
		if (Option && Target)
		{
			Direction = (Target.transform.position - transform.position).normalized;
		}
		float fAngle = getAngle(Vector3.down, Direction);
		transform.eulerAngles = new Vector3(
		0.0f, 0.0f, fAngle);
		// ** �������� �ӵ���ŭ ��ġ�� ����
		transform.position += Direction * Speed * Time.deltaTime;

	}

	// ** �浹ü�� ���������� ���Ե� ������Ʈ�� �ٸ� �浹ü�� �浹�Ѵٸ� ����Ǵ� �Լ�. 
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// ** collision = �浹�� ���.
		// ** �浹�� ����� �����Ѵ�. 
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