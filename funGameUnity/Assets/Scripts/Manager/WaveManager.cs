using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
	int NextWaveIndex = GameStatus.GetInstance().NextWanveIndex;
    public GameObject Parent;

    private void Start()
    {
        Parent = new GameObject("WaveList");
    }

	void Update()
    {
		float percent = GameStatus.GetInstance().GetRunPercent();
		if (percent >= Manual.WavePoints[NextWaveIndex] 
			&& NextWaveIndex + 1 < Manual.WaveNumber)
		{
            GenWave("Wave" + NextWaveIndex.ToString());
			NextWaveIndex += 1;
		}
    }

	private void GenWave(string _waveName)
	{
        GameObject obj = Instantiate(PrefabManager.Instance.GetPrefabByName(_waveName));
        obj.transform.SetParent(Parent.transform);
        obj.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
	}
}
