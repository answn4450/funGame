using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test2 : MonoBehaviour
{
	private float a = 0;
    void Update()
    {
		a += Time.deltaTime;
		print(a);
		print(SceneManager.GetActiveScene().name);
	}
}
