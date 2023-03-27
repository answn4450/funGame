using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ControllerManager
{
	private static ControllerManager Instance = null;

	public int Player_HP = 100;
	public int HitShock=0;
	public float Player_Bullet_Term = 0.3f;

	public float BulletSpeed=10.0f;
	public bool DirLeft;
	public bool DirRight;

	//ÇÇ°Ý
	
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