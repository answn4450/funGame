using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [Tooltip("�׽�Ʈ������ ��� ��")]
    public GameObject test;
    
    private void Awake()
	{
        //test = new GameObject("EmptyObject");
        //test.AddComponent<MyGizmo>();
	}

	void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        test.transform.position = mousePosition;
    }
}
