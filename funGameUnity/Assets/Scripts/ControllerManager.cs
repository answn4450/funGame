using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ControllerManager
{
	private static ControllerManager Instance = null;

	public float BulletSpeed=10.0f;
	public bool DirLeft;
	public bool DirRight;

	private ControllerManager()
	{

	}

	public static ControllerManager GetInstance()
	{
		if (Instance == null)
		{
			Instance = new ControllerManager();
		}
		return Instance;
	}

	public int Player_HP=100;
	public float Player_Bullet_Timer_Limit = 0.3f;
}