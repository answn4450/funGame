using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
	// ** 따라다닐 객체
    public GameObject Target;
	private Slider HPBar;

	// ** 세부위치 조정
	private Vector3 offset;

	private void Awake()
	{
		HPBar = GetComponent<Slider>();
		// ** 복제된 UI를 캔버스에 위치시킨다. 
		transform.SetParent(GameObject.Find("EnemyHPCanvas").transform);
	}


	private void Start()
	{
		// ** 위치 셋팅
		//offset = new Vector3(0.0f,0.6f, 0.0f);
		HPBar.maxValue = Target.GetComponent<EnemyController>().MaxHP;
	}


	private void Update()
    {
		// ** WorldToScreenPoint = 월드 좌표를 카메라 좌표로 변환
		// ** 월드상에 있는 타겟의 좌표를 카메라 좌표로 변환하여 UI에 셋팅한다.
		HPBar.value = Target.GetComponent<EnemyController>().HP;
		//if (Target.IsDestroyed())
		if (HPBar.value <= 0.0f)
			Destroy(transform.gameObject);
		else
			HPBar.value = Target.GetComponent<EnemyController>().HP;
			transform.position = Camera.main.WorldToScreenPoint(Target.transform.position + offset);
    }
}
