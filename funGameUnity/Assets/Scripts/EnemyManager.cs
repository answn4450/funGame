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

	// ** Enemy 담아둘 상위 객체
	private GameObject Parrent;

	// ** Enemy로 사용할 원형 객체
	private GameObject Prefab;
	private GameObject HPPrefab;

	//** 플레이어의 누적 이동 거리
	public float Distance;

	private void Awake()
	{
		if (instance==null)
		{
			instance = this;

			Distance = 0.0f;

			// ** 씬이 변경되어도 계속 유지될 수 있게 해준다.
			DontDestroyOnLoad(gameObject);

			// ** 생성되는 Enemy를 담아둘 상위 객체
			Parrent = new GameObject("EnemyList");

			// ** Enemy로 사용할 원형 객체
			Prefab = Resources.Load("Prefabs/Enemy/Enemy") as GameObject;
			HPPrefab = Resources.Load("Prefabs/HP") as GameObject;

		}
	}

	private void Reset()
	{
		print("from enemy manager Reset");
	}

	// ** 시작하자마자 Start 함수를 코루틴 함수로 실행
	private IEnumerator Start()
	{
		while (true)
		{
			// ** Enemy 원형 객체를 복제한다.
			GameObject Obj = Instantiate(Prefab);
			
			// ** Enemy HP UI 복제
			GameObject Bar = Instantiate(HPPrefab);

			// ** 복제된 UI를 캔버스에 위치시킨다. 
			Bar.transform.parent = GameObject.Find("EnemyHPCanvas").transform;
			
			// ** Enemy 작동 스크립트 포함.
			//Obj.AddComponent<EnemyController>();

			// ** 클론의 위치를 초기화.
			Obj.transform.position = new Vector3(
	//	18.0f, Random.Range(-8.2f, 5.5f), 0.0f);
				10.0f, Random.Range(-8.2f, 5.5f), 0.0f);

			// ** 클론의 이름 초기화.
			Obj.transform.name = "Enemy";

			// ** 클론의 계층구조 설정.
			Obj.transform.parent = Parrent.transform;

			// ** UI 객체가 들고 있는 스크립트에 접근.
			EnemyHPBar enemyHPBar = Bar.GetComponent<EnemyHPBar>();

			enemyHPBar.Target = Obj;

			// ** 1.5초 휴식.
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
