using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	private EnemyManager() { }
	private static EnemyManager instance = null;
	private GameObject Player;

	public static EnemyManager GetInstance
	{
		get
		{
			if (instance == null)
				return null;
			return instance;
		}
	}

	// ** Enemy ��Ƶ� ���� ��ü
	private GameObject Parrent;

	// ** Enemy�� ����� ���� ��ü
	private GameObject Prefab;

	//** �÷��̾��� ���� �̵� �Ÿ�
	public float Distance;

	private void Awake()
	{
		if (instance==null)
		{
			instance = this;

			Distance = 0.0f;

			// ** ���� ����Ǿ ��� ������ �� �ְ� ���ش�.
			DontDestroyOnLoad(gameObject);
		}
	}
	
	private void Start()
	{
		// ** �����Ǵ� Enemy�� ��Ƶ� ���� ��ü
		Parrent = new GameObject("EnemyList");
		Player = GameObject.Find("Player");
		// ** Enemy�� ����� ���� ��ü
		Prefab = Resources.Load("Prefabs/Enemy/Enemy") as GameObject;
		StartCoroutine(Generate());
	}

	// ** �������ڸ��� Start �Լ��� �ڷ�ƾ �Լ��� ����
	private IEnumerator Generate()
	{
		while (true)
		{
			// ** Enemy ���� ��ü�� �����Ѵ�.
			//GameObject Obj = Instantiate(Prefab);
			GameObject Obj = Instantiate(Prefab);

			GameObject HPBar = Obj.GetComponent<EnemyController>().HPBar;
			// ** ������ UI�� ĵ������ ��ġ��Ų��. 
			HPBar.transform.SetParent(GameObject.Find("EnemyHPCanvas").transform);
			//HPBar.transform.parent = GameObject.Find("EnemyHPCanvas").transform;

			// ** Ŭ���� ��ġ�� �ʱ�ȭ.
			Obj.transform.position = new Vector3(
				//18.0f, Random.Range(-8.2f, 5.5f), 0.0f);
				10.0f, 0.0f, 0.0f);

			// ** Ŭ���� �̸� �ʱ�ȭ.
			Obj.transform.name = "Enemy";

			// ** Ŭ���� �������� ����.
			Obj.transform.parent = Parrent.transform;

			// ** UI ��ü�� ��� �ִ� ��ũ��Ʈ�� ����.
			EnemyHPBar enemyHPBar = HPBar.GetComponent<EnemyHPBar>();

			enemyHPBar.Target = Obj;

			// ** 1.5�� �޽�.
			yield return new WaitForSeconds(LevelDesign());
		}
	}

	private void Update()
	{
		if (ControllerManager.GetInstance().DirRight)
		{
			Distance += Input.GetAxisRaw("Horizontal") * Time.deltaTime;
		}
	}

	private float LevelDesign()
	{
		return 1;
		//return Mathf.Max(2-GameStatus.GetInstance().GetRunPercent()/10, 0.5f);
	}
}
