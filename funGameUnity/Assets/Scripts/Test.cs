using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Test : MonoBehaviour
{
    [Tooltip("테스트용으로 띄울 것")]
    public GameObject test;
    
    private void Awake()
	{
        //test = new GameObject("EmptyObject");
        //test.AddComponent<MyGizmo>();
	}

	void Update()
    {
        if (Input.GetMouseButtonDown(0))
            print("mouse down");
	}
}
