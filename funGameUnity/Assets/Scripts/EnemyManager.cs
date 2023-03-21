using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	private EnemyManager() { }
	private static EnemyManager instance = null;

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
	private GameObject HPPrefab;

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

			// ** �����Ǵ� Enemy�� ��Ƶ� ���� ��ü
			Parrent = new GameObject("EnemyList");

			// ** Enemy�� ����� ���� ��ü
			Prefab = Resources.Load("Prefabs/Enemy/Enemy") as GameObject;
			HPPrefab = Resources.Load("Prefabs/HP") as GameObject;

		}
	}

	private void Reset()
	{
		print("from enemy manager Reset");
	}

	// ** �������ڸ��� Start �Լ��� �ڷ�ƾ �Լ��� ����
	private IEnumerator Start()
	{
		while (true)
		{
			// ** Enemy ���� ��ü�� �����Ѵ�.
			GameObject Obj = Instantiate(Prefab);
			
			// ** Enemy HP UI ����
			GameObject Bar = Instantiate(HPPrefab);

			// ** ������ UI�� ĵ������ ��ġ��Ų��. 
			Bar.transform.parent = GameObject.Find("EnemyHPCanvas").transform;
			
			// ** Enemy �۵� ��ũ��Ʈ ����.
			//Obj.AddComponent<EnemyController>();

			// ** Ŭ���� ��ġ�� �ʱ�ȭ.
			Obj.transform.position = new Vector3(
	//	18.0f, Random.Range(-8.2f, 5.5f), 0.0f);
				10.0f, Random.Range(-8.2f, 5.5f), 0.0f);

			// ** Ŭ���� �̸� �ʱ�ȭ.
			Obj.transform.name = "Enemy";

			// ** Ŭ���� �������� ����.
			Obj.transform.parent = Parrent.transform;

			// ** UI ��ü�� ��� �ִ� ��ũ��Ʈ�� ����.
			EnemyHPBar enemyHPBar = Bar.GetComponent<EnemyHPBar>();

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
		return 1.5f;
	}
}
