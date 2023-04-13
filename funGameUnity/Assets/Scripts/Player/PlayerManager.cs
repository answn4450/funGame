using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
	private void Awake()
	{
		//print(ControllerManager.GetInstance().BulletTermTable[0]);
	}

	void Update()
    {
		ControllerManager.GetInstance().Update();
    }
}
