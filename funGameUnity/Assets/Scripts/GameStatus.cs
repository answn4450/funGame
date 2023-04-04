using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus
{
	public int[] LV = { 0, 0, 0, 0, 0 };

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

	public float GetRunPercent()
	{
		return 100 * RunDistance / DistanceLength;
	}
}
