using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
	// ** ����ٴ� ��ü
    public GameObject Target;
	private Slider HPBar;

	// ** ������ġ ����
	private Vector3 offset;

	private void Awake()
	{
		HPBar = GetComponent<Slider>();
		// ** ������ UI�� ĵ������ ��ġ��Ų��. 
		transform.SetParent(GameObject.Find("EnemyHPCanvas").transform);
	}


	private void Start()
	{
		// ** ��ġ ����
		//offset = new Vector3(0.0f,0.6f, 0.0f);
		HPBar.maxValue = Target.GetComponent<EnemyController>().MaxHP;
	}


	private void Update()
    {
		// ** WorldToScreenPoint = ���� ��ǥ�� ī�޶� ��ǥ�� ��ȯ
		// ** ����� �ִ� Ÿ���� ��ǥ�� ī�޶� ��ǥ�� ��ȯ�Ͽ� UI�� �����Ѵ�.
		HPBar.value = Target.GetComponent<EnemyController>().HP;
		//if (Target.IsDestroyed())
		if (HPBar.value <= 0.0f)
			Destroy(transform.gameObject);
		else
			HPBar.value = Target.GetComponent<EnemyController>().HP;
			transform.position = Camera.main.WorldToScreenPoint(Target.transform.position + offset);
    }
}
