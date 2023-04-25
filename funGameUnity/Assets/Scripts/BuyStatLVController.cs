using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyStatLVController : MonoBehaviour
{
	public enum Status
	{
		BulletPower,
		Defence,
		BulletTerm,
		ImmortalChance,
		HPRegenSize
	}

	private int MaxPureLV;
	// 레벨업 할 수 있는 가짓수
	int Size = 5;
	int[] CostRoadMap = { 3, 5, 15, 27, 40, 80 };
	int[] CostRoadMapIndex = new int[6];
	//현재 요구되는 cost
	int[] Cost = new int[5];

	private Transform CostMother;
	
	void Start()
	{
		CostMother = transform.GetChild(1);
		MaxPureLV = ControllerManager.GetInstance().MaxPureLV;
		for (int b = 0; b < Size; ++b)
		{
			CostRoadMapIndex[b] = 0;
			Cost[b] = CostRoadMap[CostRoadMapIndex[b]];
		}
	}

	private void Update()
	{
		DrawCost();
	}

	private void DrawCost()
	{
		for (int i = 0; i < Size; ++i)
		{
			CostMother.GetChild(i).GetComponent<Text>().text = Cost[i].ToString();
		}
	}

	public void BuyStatLV(int index)
	{
		print(index);
		int money = ControllerManager.GetInstance().Player_Money;

		if (Cost[index] <= money && CostRoadMapIndex[index] < MaxPureLV)
		{
			ControllerManager.GetInstance().LV[index] += 1;
			ControllerManager.GetInstance().Player_Money -= Cost[index];
			CostRoadMapIndex[index] += 1;
			Cost[index] = CostRoadMap[CostRoadMapIndex[index]];
		}
	}
}
