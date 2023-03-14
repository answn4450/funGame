using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	private EnemyManager() { }
	private static EnemyManager instance = null;

	private GameObject Prefab;

	public static EnemyManager GetInstance
	{
		get
		{
			if (instance == null)
				return null;
			return instance;
		}
	}


	private void Awake()
	{
		if (instance==null)
		{
			instance = this;

			// **���� ����Ǿ ��� ������ �� �ְ� ���ش�.
			DontDestroyOnLoad(gameObject);

			Prefab = Resources.Load("Prefabs/Enemy/Enemy") as GameObject;
		}
	}

	private IEnumerator Start()
	{
		while (true)
		{
			GameObject Obj = Instantiate(Prefab);
			Obj.transform.position = new Vector3(
				18.0f, Random.Range(-8.2f, 5.5f), 0.0f);
			Obj.transform.name = "Test";

			yield return new WaitForSeconds(1.5f);
		}
	}
}
