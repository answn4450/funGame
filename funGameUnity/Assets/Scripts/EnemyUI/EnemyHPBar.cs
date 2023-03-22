using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHPBar : MonoBehaviour
{
	// ** ����ٴ� ��ü
    public GameObject Target;
	
	// ** ������ġ ����
	private Vector3 offset;

	private void Awake()
	{
		Target = GameObject.Find("Boss");
	}


	private void Start()
	{
		// ** ��ġ ����
		offset = new Vector3(0.0f,0.6f, 0.0f);
	}


	private void Update()
    {
		// ** WorldToScreenPoint = ���� ��ǥ�� ī�޶� ��ǥ�� ��ȯ
		// ** ����� �ִ� Ÿ���� ��ǥ�� ī�޶� ��ǥ�� ��ȯ�Ͽ� UI�� �����Ѵ�.
		if (Target.IsDestroyed())
			Destroy(transform.gameObject);
		else
			transform.position = Camera.main.WorldToScreenPoint(Target.transform.position + offset);
    }
}
