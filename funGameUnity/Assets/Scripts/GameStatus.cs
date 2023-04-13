using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameStatus
{
	public string id = "guest";
	public string GameEndComment;
	public List<int> LV = Enumerable.Repeat(0,5).ToList();
	public int MaxPlayerHP = 100;
	public float PlayerHP = 100;
	public float RunDistance = 0.0f;
	public float DistanceLength = 300.0f;
	//public float currentWavePercent = 0.0f;
	private static GameStatus Instance;

	public int CurrentWanveIndex = 0;

	public int WaveNumber = 6;
	public float[] WavePoints = { 3.0f, 20.0f, 50.0f, 60.0f, 90.0f, 95.0f };

	public static GameStatus GetInstance()
	{
		if (Instance == null)
		{
			Instance = new GameStatus();
		}
		return Instance;
	}

	// 게임이 끝날 때 손수 리셋
	public void ResetGameStatus()
	{
		MaxPlayerHP = 100;
		PlayerHP = 100;
		RunDistance = 0.0f;
		DistanceLength = 300.0f;
		CurrentWanveIndex = 0;
		LV = Enumerable.Repeat(0, 5).ToList();
	}

	public float GetRunPercent()
	{
		return 100 * RunDistance / DistanceLength;
	}
}
