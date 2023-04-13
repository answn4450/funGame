using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    void Start()
    {
		// ** 씬이 변경되어도 계속 유지될 수 있게 해준다.
		DontDestroyOnLoad(gameObject);
	}
}
