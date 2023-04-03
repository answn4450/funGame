using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerManager
{
	public enum Status {
		BulletPower,
		Defence,
		BulletTerm,
		ImmortalChance,
		HPRegenSpeed
	}

	private static ControllerManager Instance = null;
	
	//List<List<int>> LVTable = new List<List<int>>();
	//플레이어가 스킬 없이 이룰 수 있는 LV 제한. 레벨은 0부터 MaxLV까지
	public int MaxPureLV=3;
	//크기는 MaxPureLV + 1
	public float[,] LVTable = 
	{
		//BulletPower int로 바꿔서 써야 함
		{ 1.0f, 2.0f, 4.0f, 6.0f, 10.0f},
		//Defence 
		{ 0.0f, 1.0f, 4.0f, 9.0f, 20.0f},
		//BulletTerm
		{ 0.5f, 0.3f, 0.2f, 0.1f, 0.0f},
		//ImmortalChance 백분위
		{ 1.0f, 3.0f, 5.0f, 7.0f, 10.0f},
		//HPRegenSpeed 
		{ 5.0f, 4.6f, 4.3f, 4.0f, 3.5f}
	};

	public bool[] Trial = { false, false, false, false, false };
	public float[] TrialTimer = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };

	public int[] TrialLV = { 0, 0, 0, 0, 0 };
	public int[] NowLV = { 0, 0, 0, 0, 0 };
	public int[] LV = { 0, 0, 0, 0, 0 };

	public float Player_BulletPower = 0.0f;
	public float Player_Defence = 0.0f;
	public float Player_BulletTerm = 0.0f;
	public float Player_ImmortalChance = 0.0f;
	public float PlayerHPRegenSpeed = 0.0f;

	public int PlayerHP = 100;
	public int PlayerExp = 0;
	public int HitShock=0;

	public float BulletSpeed=10.0f;
	public bool DirLeft;
	public bool DirRight;

	public float[] DangerPercent = {10.0f,20.0f,40.0f};
	//피격

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

	public void Update()
	{
		//게임 끝
		if (PlayerHP<=0)
		{
			GameEnd.GetInstance().Record = "Fail";
			GameEnd.GetInstance().Record = "Run: ";
			GameEnd.GetInstance().Record += GameStatus.GetInstance().RunDistance;
			GameEnd.GetInstance().Record = "M";

			SceneManager.LoadScene("GameEnd");
		}
		if (GameStatus.GetInstance().RunDistance>=GameStatus.GetInstance().DistanceLength)
		{
			GameEnd.GetInstance().Record = "Win";
			GameEnd.GetInstance().Record = "M";

			SceneManager.LoadScene("GameEnd");
		}

		SetPlayerStatus();
	}

	public void SetPlayerStatus()
	{
		for (int i = 0; i < 5; ++i)
		{
			if (Trial[i])
			{
				NowLV[i] = TrialLV[i];
				TrialTimer[i] -= Time.deltaTime;
				if (TrialTimer[i] <= 0.0f)
				{
					Trial[i] = false;
				}
			}
			else
			{
				NowLV[i] = LV[i];
			}
		}
		Player_BulletPower = LVTable[(int)Status.BulletPower, NowLV[(int)Status.BulletPower]];
		Player_Defence = LVTable[(int)Status.Defence, NowLV[(int)Status.Defence]];
		Player_BulletTerm = LVTable[(int)Status.BulletTerm, NowLV[(int)Status.BulletTerm]];
		Player_ImmortalChance = LVTable[(int)Status.ImmortalChance, NowLV[(int)Status.ImmortalChance]];
		Player_ImmortalChance = LVTable[(int)Status.HPRegenSpeed, NowLV[(int)Status.HPRegenSpeed]];
	}

	public void GoTrial(int LVIndex, float t)
	{
		Trial[LVIndex] = true;
		TrialTimer[LVIndex] = t;
		if (NowLV[LVIndex]<MaxPureLV)
		{
			TrialLV[LVIndex] = NowLV[LVIndex] + 1;
		}
	}

	//잡몹한테 얻어 맞았을 때
	public void SmallHit(int damage)
	{
		if (UnityEngine.Random.Range(0, 100)>Player_ImmortalChance)
		{
			PlayerHP -= damage;
		}
	}

	public void BigHeal()
	{
		PlayerHP = Math.Min(
			PlayerHP + 10, 100
		);
	}
}