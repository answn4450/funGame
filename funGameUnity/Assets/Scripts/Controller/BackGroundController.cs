using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
	// ** BackGround 가 모여있는 계층구조의 최상위 객체(부모)
	private Transform parent;

	// ** Sprite를 포함하고 있는 구성요소
	private SpriteRenderer spriteRenderer;

	// ** 이미지
	private Sprite sprite;

	// ** 생성지점
	private float endPoint;

	// ** 삭제 지점.
	private float exitPoint;

	// ** 이미지 이동속도
	public float Speed;

	// ** 플레이어 정보
	private GameObject player;
	private PlayerController playerController;

	// ** 움직임 정보
	private Vector3 movemane;

	// ** 뒤에 있는지 여부
	private bool Tail = true;

	private void Awake()
	{
		// ** 플레이어의 기본정보를 받아온다.
		player = GameObject.Find("Player").gameObject;

		// ** 부모객체를 받아온다.
		parent = GameObject.Find("BackGround").transform;

		// ** 현재 이미지를 담고있는 구성요소를 받아온다.
		spriteRenderer = GetComponent<SpriteRenderer>();

		// ** 플레이어 이미지를 담고있는 구성요소를 받아온다.
		playerController = player.GetComponent<PlayerController>();
	}

	void Start()
	{
		// ** 구성요소에 포함된 이미지를 받아온다.
		sprite = spriteRenderer.sprite;

		// ** 시작지점과 종료 지점을 설정. 최초 sprite의 x가 0 이라 가정.  
		endPoint = sprite.bounds.size.x * 0.5f;
		exitPoint = -sprite.bounds.size.x * 0.5f;
	}

	void Update()
	{
		// ** 이동정보 셋팅
		movemane = new Vector3(
			Input.GetAxisRaw("Horizontal") * Time.deltaTime * Speed,
			0.0f, 0.0f);

		// ** singleton
		if (ControllerManager.GetInstance().DirRight)
		{
			transform.position -= movemane;
		}

		if (transform.gameObject.tag=="Cloud")
		{
			Vector3 wind = Climate.GetInstance().Wind;
			transform.position -= wind * Time.deltaTime;
		}

		// ** 동일한 이미지 복사
		if (Tail && transform.position.x + (sprite.bounds.size.x * 0.5f) <= endPoint)
		{
			// ** 이미지를 복제한다.
			GameObject Obj = Instantiate(this.gameObject);

			// ** 본제된 이미지의 부모를 설정한다.
			Obj.transform.parent = parent.transform;

			// ** 복제된 이미지의 이름을 설정한다.
			Obj.transform.name = transform.name;

			// ** 복제된 이미지의 위치를 설정한다.
			Obj.transform.position = transform.position + new Vector3(
				sprite.bounds.size.x, 0.0f, 0.0f
				);
			Tail = false;
		}

		// ** 종료지점에 도달하면 삭제한다.
		if (transform.position.x + (sprite.bounds.size.x * 0.5f) <= exitPoint)
			Destroy(this.gameObject);
	}
}