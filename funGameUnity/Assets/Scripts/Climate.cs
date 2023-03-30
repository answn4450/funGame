using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Climate
{
    public Vector3 Wind = new Vector3(0.01f,0.0f,0.0f);

	private static Climate Instance = null;

	private Climate()
    {

    }

	public static Climate GetInstance()
	{
		if (Instance == null)
		{
			Instance = new Climate();
		}
		return Instance;
	}
}
