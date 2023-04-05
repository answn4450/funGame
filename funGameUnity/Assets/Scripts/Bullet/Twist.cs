using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Twist : MonoBehaviour
{
	public float Angle;
	public float Speed;
	public Sprite sprite;

	private GameObject[] Bullet = new GameObject[2];


	private void Start()
	{
		Bullet[0] = new GameObject("TwistBullet");
		Bullet[0].AddComponent<MyGizmo>();

		Bullet[0].transform.position = new Vector3(
			transform.position.x,
			transform.position.y + 2.5f,
			0.0f
		);

		Bullet[0].transform.SetParent(transform);

		Bullet[1] = new GameObject("TwistBullet");
		Bullet[1].AddComponent<MyGizmo>();

		Bullet[1].transform.position = new Vector3(
			transform.position.x,
			transform.position.y - 2.5f,
			0.0f
		);
		Bullet[1].transform.SetParent(transform);
	}
	void Update()
    {
		print("a");
		Angle += 2.5f;
		Bullet[0].transform.position += new Vector3(
			Mathf.Cos(Angle * Mathf.Deg2Rad), 1.0f, 0.0f)*Speed*Time.deltaTime;

		Bullet[0].transform.position += new Vector3(
			Mathf.Cos(Angle * Mathf.Deg2Rad), 1.0f, 0.0f) * Speed * Time.deltaTime;
	}
}
