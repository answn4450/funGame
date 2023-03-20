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
}