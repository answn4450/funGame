using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climate
{
	public bool PlayerRunHard = false;
	public Vector3 PlayerBreakWind = new Vector3(0.5f, 0.0f, 0.0f);
	public Vector3 BossBreakWind = new Vector3(1.5f, 0.0f, 0.0f);
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
			gameObject.transform.position -= Wind * Time.deltaTime;
		}
		else if (gameObject.tag=="Boss")
		{
			gameObject.transform.position = new Vector3();
			//Vector3.Max(Wind*Time.deltaTime-new vec)
		}
		else
		{
			if (PlayerRunHard)
				gameObject.transform.position -= (Wind + PlayerBreakWind) * Time.deltaTime;
			else
				gameObject.transform.position -= Wind * Time.deltaTime;
		}
	}
}
