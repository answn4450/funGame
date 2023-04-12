using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoppingText
{
	private static PoppingText Instance = null;

	public static PoppingText GetInstance()
	{
		if (Instance == null)
		{
			Instance = new PoppingText();
		}

		return Instance;
	}
	void Start()
    {
        
    }

    void Update()
    {
        
    }
}
