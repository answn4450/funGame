using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBar : MonoBehaviour
{
	private Slider HPBar;
	
	private void Awake()
	{
		HPBar= GetComponent<Slider>();
	}

	private void Start()
	{
		HPBar.maxValue = GameStatus.GetInstance().PlayerHP;
		HPBar.value=HPBar.maxValue;
	}

	private void Update()
	{	
		HPBar.value = GameStatus.GetInstance().PlayerHP;
	}
}
