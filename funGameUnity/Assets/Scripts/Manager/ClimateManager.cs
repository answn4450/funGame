using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climate : MonoBehaviour
{
	public Vector3 PlayerBreakWind = new Vector3(0.5f, 0.0f, 0.0f);
	public Vector3 BossBreakWind = new Vector3(1.5f, 0.0f, 0.0f);
	public Vector3 Wind = new Vector3(1.0f, 0.0f, 0.0f);
	public List<string> Seasons = new List<string>
	{
		"!_Mountain",
		"2_Desert",
		"3_Graveyard",
		"4_Snow"
	};
	public List<string> Climates = new List<string>
	{
        "Good",
		"Normal",
		"Bad"
	};

    public float RainPercent = 0.0f;

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
		bool playerRunHard = ControllerManager.GetInstance().PlayerRunHard;
		if (gameObject.name == "Player")
		{
			gameObject.transform.position -= Wind * Time.deltaTime;
		}
		else if (gameObject.tag=="Boss")
		{
			gameObject.transform.position = new Vector3();
			//Vector3.Max(Wind*Time.deltaTime-new vec)
		}
		else if (gameObject.tag=="Enemy")
		{
			if (playerRunHard)
				gameObject.transform.position -= (Wind + PlayerBreakWind) * Time.deltaTime;
			else
				gameObject.transform.position -= Wind * Time.deltaTime;
		}
        else
		{
            
            gameObject.transform.position -= (Wind + PlayerBreakWind) * Time.deltaTime;
		}
	}

	public Vector3 GetBackgroundWind()
	{
		return Wind + PlayerBreakWind;
	}
}
