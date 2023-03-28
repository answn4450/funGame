using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;



public class ControllerManager
{
	private static ControllerManager Instance = null;

	public float[] BulletTermTable = { 0.7f, 0.3f, 0.2f, 0.1f, 0.0f};
	public float[] HealRegenTermTable = { 0.7f, 0.3f, 0.2f, 0.1f, 0.0f};

	public int LV_BulletTerm = 0;
	public int LV_HealRegen = 0;

	public int Trial_LV_BulletTerm = 0;
	public int Trial_LV_HealRegen = 0;

	public float TrialBulletTermTimer = 0.0f;

	public bool TrialBulletTerm = false;
	public bool TrialHealRegen = false;

	public int Player_HP = 100;
	public float Player_BulletTerm = 0.3f;
	
	public int HitShock=0;

	public float BulletSpeed=10.0f;
	public bool DirLeft;
	public bool DirRight;

	public float Shield;
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

	public void RegularUpdate()
	{
		//Trial on/off
		if (TrialBulletTerm)
		{
			Player_BulletTerm = BulletTermTable[Trial_LV_BulletTerm];
			TrialBulletTermTimer -= Time.deltaTime;
			if (TrialBulletTermTimer<=0.0f)
			{
				TrialBulletTerm = false;
			}
		}
		else
			Player_BulletTerm = BulletTermTable[LV_BulletTerm];	
	}

	public void GoTrialBulletTerm(float t)
	{
		TrialBulletTerm = true;
		TrialBulletTermTimer = t;
		Trial_LV_BulletTerm = LV_BulletTerm + 1;
	}

	public void EndTrialBulletTerm()
	{
		TrialBulletTerm = false;
	}

	public void BeginTrialHealRegen()
	{
	}

	public void EndTrialHealRegen()
	{
	}

	public void WeakHit(float damage)
	{
		//ControllerManager.GetInstance()
	}

	public void BigHeal()
	{
		Player_HP = Math.Min(
			Player_HP + 10, 100
		);
	}
}