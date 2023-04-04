using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
	GameObject Boss;
	int CurrentWaveIndex = GameStatus.GetInstance().CurrentWanveIndex;
	public int WaveNumber = GameStatus.GetInstance().WaveNumber;
	public float[] WavePoints = GameStatus.GetInstance().WavePoints;

	private void Awake()
	{
		//Boss = Resources.Load("Prefabs/Enemy/Enemy") as GameObject;
		Boss = Resources.Load("Prefabs/Boss/Boss") as GameObject;
	}

	
	void Update()
    {
		float percent = GameStatus.GetInstance().GetRunPercent();
		if (percent > WavePoints[CurrentWaveIndex])
		{
			//GenBoss(CurrentWaveIndex);
			if (CurrentWaveIndex+1<= WaveNumber)
			{
				CurrentWaveIndex += 1;
			}
		}
    }

	private void GenBoss(int number)
	{
		print("gen boss");
		for (int i =0;i < number; i++)
		{
			GameObject boss = Instantiate(Boss);
			boss.name = "boss";
			//boss.transform.position = new Vector3(1280.0f, 460.0f, 0.0f);
			boss.transform.position = new Vector3(18.0f, Random.Range(-8.2f, 5.5f), 0.0f);
		}
	}
}
