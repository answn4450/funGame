using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus
{
	public float RunDistance = 0.0f;
	public float DistanceLength = 100.0f;

	private static GameStatus Instance;

	public static GameStatus GetInstance()
	{
		if (Instance == null)
		{
			Instance = new GameStatus();
		}
		return Instance;
	}
}
