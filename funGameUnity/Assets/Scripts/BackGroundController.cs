using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    // ** ������ BackGround�� ���ִ� ���������� �ֻ��� ��ü
    private Transform parent;

	// ** Sprite�� �����ϰ� �ִ� �������
	private SpriteRenderer spriteRenderer;

	// ** �̹���
	private Sprite sprite;

	// ** ��������
	private float endPoint;

	// ** ���� ����
	private float exitPoint;

	// ** �̹��� �̵� �ӵ�
	public float Speed;

	// ** �÷��̾� ����
	public GameObject player;
//rivate SpriteController playerController;

	// ** ������ ����
	private Vector3 movemane = new Vector3(0.0f, 0.0f, 0.0f);

	// ** �̹����� �߾� ��ġ�� ���������� ����� �� �ֵ��� �ϱ� ���� ���� ����
	private Vector3 offset = new Vector3(0.0f, 5.0f, 0.0f);

	private void Awake()
	{

		// ** �÷��̾��� �⺻������ �޾ƿ´�. 
		player = GameObject.Find("Player").gameObject;

		// ** �θ�ü�� �޾ƿ´�. 
		parent = GameObject.Find("BackGround").transform;

		// ** �̹����� ��� �ִ� ������Ҹ� �޾ƿ´�. 
		spriteRenderer = GetComponent<SpriteRenderer>();

		// ** �÷��̾� �̹����� ��� �ִ� ������Ҹ� �޾ƿ´�. 
//playerController = GetComponent<>();
	}

	// Start is called before the first frame update
	void Start()
	{
		// ** ������ҿ� ���Ե� �̹����� �޾ƿ´�. 
		sprite = spriteRenderer.sprite;
		
		// ** ���������� ����
		endPoint = sprite.bounds.size.x * 0.5f + transform.position.x;

		// ** ���������� ����
		exitPoint = -sprite.bounds.size.x * 0.5f + player.transform.position.x;
	}

    // Update is called once per frame
    void Update()
	{
		SpriteRenderer playerRenderer = player.GetComponent<SpriteRenderer>();
		// ** �̵����� ����
		movemane = new Vector3(
				// ** singleton 
				Input.GetAxisRaw("Horizontal") * Time.deltaTime * Speed + offset.x,
				player.transform.position.y + offset.y,
				0.0f + offset.z
				);

		// ** �÷��̾ �ٶ󺸰� �ִ� ���⿡ ���� �б��.
		if (ControllerManager.GetInstance().DirLeft)
		{// ** ���� �̵�
			endPoint -= movemane.x;
		}
		
		if (ControllerManager.GetInstance().DirRight)
		{// ** ���� �̵�
			transform.position -= movemane;
		}

		// ** ������ �̹��� ����
		if (player.transform.position.x + (sprite.bounds.size.x * 0.5f) + 1 > endPoint)
		{
			// ** �̹����� �����Ѵ�.
			GameObject Obj = Instantiate(this.gameObject);

			// ** ������ �̹����� �θ� �����Ѵ�. 
			Obj.transform.parent = transform.parent;

			// ** ������ �̹����� �̸� �����Ѵ�. 
			Obj.transform.name = transform.name;

			// ** ������ �̹����� ��ġ�� �����Ѵ�.
			Obj.transform.position = new Vector3(
                player.transform.position.x + 50.0f,
                transform.position.y, 0.0f);

			// ** ���������� �����Ѵ�.
			endPoint += 25.0f;
		}

		// ** ���������� �����ϸ� �����Ѵ�. 
		if (transform.position.x+(sprite.bounds.size.x*0.5f) - 2 < exitPoint)
        {
            Destroy(this.gameObject);
        }
    }
}
