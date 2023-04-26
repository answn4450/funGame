using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager
{
	public enum Status {
		BulletPower,
		Defence,
		BulletTerm,
		ImmortalChance,
		HPRegenSize
	}

	private static ControllerManager Instance = null;

	public float[,] LVTable =
	{
		//BulletPower int로 바꿔서 써야 함
		{ 1.0f, 2.0f, 4.0f, 6.0f, 10.0f, 20.0f},
		//Defence 
		{ 0.0f, 1.0f, 4.0f, 9.0f, 10.0f, 20.0f},
		//BulletTerm
		{ 2.0f, 1.0f, 0.5f, 0.3f, 0.2f, 0.1f},
		//ImmortalChance 백분위
		{ 1.0f, 3.0f, 5.0f, 7.0f, 8.0f, 9.0f},
		//HPRegenSize
		{ 0.0f, 1.0f, 2.0f, 2.5f, 3.0f, 4.0f}
	};

	public bool[] DoTrial = { false, false, false, false, false };
	public float[] TrialTimer = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };

	public int[] TrialLV = { 0, 0, 0, 0, 0 };
	public int[] NowLV = { 0, 0, 0, 0, 0 };
	public int[] LV = { 0, 0, 0, 0, 0 };

	public float Player_BulletPower = 1.0f;
	public float Player_Defence = 0.0f;
	public float Player_BulletTerm = 2.0f;
	public float Player_ImmortalChance = 1.0f;
	public float Player_HPRegenSize = 0.0f;
	public float Player_BulletMileage = 5.0f;
	public int Player_Money = 3;
	public int Player_Exp = 0;

	public bool PlayerRunHard = false;

	public Dictionary<BulletPattern.Pattern, int> Player_PatternLV = new Dictionary<BulletPattern.Pattern, int>
	{
		{ BulletPattern.Pattern.Screw, 0 },
		{ BulletPattern.Pattern.DelayScrew, 0 },
		{ BulletPattern.Pattern.GuideBullet, 0 },
		{ BulletPattern.Pattern.Explosion, 0 },
	};


	public List<BulletPattern.Pattern> Player_Patterns = new List<BulletPattern.Pattern> 
		{
			BulletPattern.Pattern.Screw,
			BulletPattern.Pattern.DelayScrew,
			BulletPattern.Pattern.GuideBullet,
			BulletPattern.Pattern.Explosion
		};

	public BulletPattern.Pattern Player_Pattern = BulletPattern.Pattern.Screw;

	public int HitShock = 0;

	public float BulletSpeed = 1.0f;
	public bool DirLeft;
	public bool DirRight;

	public float[] DangerPercent = { 10.0f, 20.0f, 40.0f };

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
		SetPlayerStatus();
		PlayerHPRegen();
	}

	public void PlayerHPRegen()
	{
		float regen = Time.deltaTime * LVTable[(int)Status.HPRegenSize, NowLV[(int)Status.HPRegenSize]];
		GameStatus.GetInstance().PlayerHP = Math.Min(
				GameStatus.GetInstance().PlayerHP+regen,
				GameStatus.GetInstance().MaxPlayerHP
			);
	}

	public void SetPlayerStatus()
	{
		for (int i = 0; i < 5; ++i)
		{
			if (DoTrial[i])
			{
				NowLV[i] = TrialLV[i];
				TrialTimer[i] -= Time.deltaTime;
				if (TrialTimer[i] <= 0.0f)
				{
					DoTrial[i] = false;
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
		Player_HPRegenSize = LVTable[(int)Status.HPRegenSize, NowLV[(int)Status.HPRegenSize]];
	}

	public void GoTrial(int LVIndex, float t)
	{
		DoTrial[LVIndex] = true;
		TrialTimer[LVIndex] = t;
		if (NowLV[LVIndex] <= Manual.MaxStatsLV)
		{
			TrialLV[LVIndex] = LV[LVIndex] + 1;
		}
	}

	public void CommonHit(float damage)
	{
		float defence = LVTable[(int)Status.Defence, NowLV[(int)Status.Defence]];
		damage = Math.Max(0.0f, damage - defence);
		if (UnityEngine.Random.Range(0, 100) > Player_ImmortalChance)
		{
			GameStatus.GetInstance().PlayerHP -= damage;
		}
	}

	
	public void SetPlayerPattern(int index)
	{
		Player_Pattern = Player_Patterns[index];
	}


	public void AddPlayerPattern(BulletPattern.Pattern pattern)
	{
		ControllerManager.GetInstance().Player_Patterns.Add(pattern);
		ControllerManager.GetInstance().Player_PatternLV[pattern] = 0;
	}
}
