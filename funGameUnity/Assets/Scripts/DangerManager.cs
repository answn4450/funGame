using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DangerManager : MonoBehaviour
{
    private GameObject Penguin;
	private void Awake()
	{
        Penguin = Resources.Load("Penguin") as GameObject;
	}

	void Start()
    {
        
    }

    void Update()
    {
        PenguinSliderGen();
    }

    private void PenguinSliderGen()
    {

    }
}
