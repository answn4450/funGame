using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Aaf
{

}
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

	// ������ �� �� �ִ� ������
	int Size = 5;
	int[] CostRoadMap = { 3, 5, 15, 27, 40, 80 };
	int[] CostRoadMapIndex = new int[6];
	//���� �䱸�Ǵ� cost
	int[] Cost = new int[5];

	private Transform CostMother;
	
	void Start()
	{
		CostMother = transform.GetChild(1);
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
		int money = ControllerManager.GetInstance().Player_Money;

		if (Cost[index] <= money && CostRoadMapIndex[index] < Manual.MaxStatsLV)
		{
			ControllerManager.GetInstance().LV[index] += 1;
			ControllerManager.GetInstance().Player_Money -= Cost[index];
			CostRoadMapIndex[index] += 1;
			Cost[index] = CostRoadMap[CostRoadMapIndex[index]];
		}
	}
}
