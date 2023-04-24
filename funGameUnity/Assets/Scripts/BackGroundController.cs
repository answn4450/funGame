using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
	// ** BackGround �� ���ִ� ���������� �ֻ��� ��ü(�θ�)
	private Transform parent;

	// ** Sprite�� �����ϰ� �ִ� �������
	private SpriteRenderer spriteRenderer;

	// ** �̹���
	private Sprite sprite;

	// ** ��������
	private float endPoint;

	// ** ���� ����.
	private float exitPoint;

	// ** �̹��� �̵��ӵ�
	public float Speed;

	// ** �÷��̾� ����
	private GameObject player;
	private PlayerController playerController;

	// ** ������ ����
	private Vector3 movemane;

	// ** �ڿ� �ִ��� ����
	private bool Tail = true;

	private void Awake()
	{
		// ** �÷��̾��� �⺻������ �޾ƿ´�.
		player = GameObject.Find("Player").gameObject;

		// ** �θ�ü�� �޾ƿ´�.
		parent = GameObject.Find("BackGround").transform;

		// ** ���� �̹����� ����ִ� ������Ҹ� �޾ƿ´�.
		spriteRenderer = GetComponent<SpriteRenderer>();

		// ** �÷��̾� �̹����� ����ִ� ������Ҹ� �޾ƿ´�.
		playerController = player.GetComponent<PlayerController>();
	}

	void Start()
	{
		// ** ������ҿ� ���Ե� �̹����� �޾ƿ´�.
		sprite = spriteRenderer.sprite;

		// ** ���������� ���� ������ ����. ���� sprite�� x�� 0 �̶� ����.  
		endPoint = sprite.bounds.size.x * 0.5f;
		exitPoint = -sprite.bounds.size.x * 0.5f;
	}

	void Update()
	{
		// ** �̵����� ����
		movemane = new Vector3(
			Input.GetAxisRaw("Horizontal") * Time.deltaTime * Speed,
			0.0f, 0.0f);

		// ** singleton
		if (ControllerManager.GetInstance().DirRight)
		{
			transform.position -= movemane;
		}

		if (transform.gameObject.name=="Sky")
		{
			Vector3 wind = Climate.GetInstance().Wind;
			transform.position -= wind * Time.deltaTime;
		}

		// ** ������ �̹��� ����
		if (Tail && transform.position.x + (sprite.bounds.size.x * 0.5f) <= endPoint)
		{
			// ** �̹����� �����Ѵ�.
			GameObject Obj = Instantiate(this.gameObject);

			// ** ������ �̹����� �θ� �����Ѵ�.
			Obj.transform.parent = parent.transform;

			// ** ������ �̹����� �̸��� �����Ѵ�.
			Obj.transform.name = transform.name;

			// ** ������ �̹����� ��ġ�� �����Ѵ�.
			Obj.transform.position = transform.position + new Vector3(
				sprite.bounds.size.x, 0.0f, 0.0f
				);
			Tail = false;
		}

		// ** ���������� �����ϸ� �����Ѵ�.
		if (transform.position.x + (sprite.bounds.size.x * 0.5f) <= exitPoint)
			Destroy(this.gameObject);
	}
}