using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climate
{
	public Vector3 PlayerBreakWind = new Vector3(0.5f, 0.0f, 0.0f);
	public Vector3 Wind = new Vector3(1.0f, 0.0f, 0.0f);
	
	private static Climate Instance = null;

	public static Climate GetInstance()
	{
		if (Instance == null)
		{
			Instance = new Climate();
		}
		return Instance;
	}

	public void Slide(GameObject gameObject)
	{
		if (gameObject.name == "Player")
		{
			gameObject.transform.position -= Wind*Time.deltaTime;
		}
		else
		{
			gameObject.transform.position -= (Wind+PlayerBreakWind) * Time.deltaTime;
		}
	}
}
