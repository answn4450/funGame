using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
	GameObject Boss;
	int NextWaveIndex = GameStatus.GetInstance().NextWanveIndex; 
	public float[] WavePoints = GameStatus.GetInstance().WavePoints;
	private GameObject Wave0, Wave1, Wave2, Wave3, Wave4, Wave5;
	
	private void Awake()
	{
		Boss = Resources.Load("Prefabs/Boss/Boss") as GameObject;
	}

	private void Start()
	{
		Wave0 = Resources.Load("Prefabs/Waves/Wave0") as GameObject;
		Wave1 = Resources.Load("Prefabs/Waves/Wave1") as GameObject;
		Wave2 = Resources.Load("Prefabs/Waves/Wave2") as GameObject;
		Wave3 = Resources.Load("Prefabs/Waves/Wave3") as GameObject;
		Wave4 = Resources.Load("Prefabs/Waves/Wave4") as GameObject;
		Wave5 = Resources.Load("Prefabs/Waves/Wave5") as GameObject;
	}

	void Update()
    {
		float percent = GameStatus.GetInstance().GetRunPercent();
		if (percent >= WavePoints[NextWaveIndex] 
			&& NextWaveIndex + 1 < Manual.WaveNumber)
		{
			switch(NextWaveIndex)
			{
				case 0:
					GenWave(Wave0);
					break;
				case 1:
					GenWave(Wave1);
					break;
				case 2:
					GenWave(Wave2);
					break;
				case 3:
					GenWave(Wave3);
					break;
				case 4:
					GenWave(Wave4);
					break;
				case 5:
					GenWave(Wave5);
					break;
			}

			NextWaveIndex += 1;
		}
    }

	private void GenWave(GameObject prefab)
	{
		GameObject obj = Instantiate(prefab);
		obj.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
	}
}
