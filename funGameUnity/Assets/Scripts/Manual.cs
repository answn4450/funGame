using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Manual : MonoBehaviour
{
	static public readonly int WaveNumber = 6;

	// 플레이어 Stat LV 제한. 레벨은 0부터 MaxLV까지
	static public readonly int MaxStatsLV = 4;

	// Bullet Pattern LV 제한.
	static public readonly int MaxPatternLV = 5;

    static public readonly float[] WavePoints = { 
        0.5f, 20.0f, 50.0f, 60.0f, 90.0f, 95.0f 
    };
}
