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

	// ** Enemy 담아둘 상위 객체
	private GameObject Parrent;

	//** 플레이어의 누적 이동 거리
	public float Distance;

	private void Awake()
	{
		if (instance==null)
		{
			instance = this;

			Distance = 0.0f;
		}
	}
	
	private void Start()
	{
		// ** 생성되는 Enemy를 담아둘 상위 객체
		Parrent = new GameObject("EnemyList");
		Player = GameObject.Find("Player");
		StartCoroutine(Generate());
	}

	// ** 시작하자마자 Start 함수를 코루틴 함수로 실행
	private IEnumerator Generate()
	{
		while (true)
		{
            // ** Enemy 원형 객체를 복제한다.
            GameObject Obj = Instantiate(PrefabManager.Instance.GetPrefabByName("Enemy/Enemy"));

            // ** 클론의 위치를 초기화.
            Obj.transform.position = new Vector3(
				18.0f, Random.Range(-8.2f, 5.5f), 0.0f);
				//10.0f, 0.0f, 0.0f);

			// ** 클론의 이름 초기화.
			Obj.transform.name = "Enemy";
			// ** 클론의 계층구조 설정.
			Obj.transform.parent = Parrent.transform;

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
		//return 1;
		return Mathf.Max(2-GameStatus.GetInstance().GetRunPercent()/10, 1.5f);
	}
}
