using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SidebarController : MonoBehaviour
{
	public enum Status
	{
		BulletPower,
		Defence,
		BulletTerm,
		ImmortalChance,
		HPRegenSize
	}

	public GameObject sidebar;

    public bool check;

    private Animator Anim;

	private int MaxPureLV;
	int StatNumber;
	// 스킬들 중 앞 5개만 스킬로 적용
	public GameObject[] StatButton = new GameObject[1000];
	int[] CostRoadMap = { 3, 5, 15, 27, 40 , 80};
	//statnumber (일단 6개) 만큼
	int[] CostRoadMapIndex = new int[1000];
	//현재 요구되는 cost
    int[] Cost = new int[1000];

	private void Awake()
	{
		Anim=sidebar.GetComponent<Animator>();
	}


	void Start()
    {
        check= false;
		MaxPureLV = ControllerManager.GetInstance().MaxPureLV;
		//X 버튼 포함 안함
		//위에 있는 버튼 5개는 고정임.
		StatNumber = sidebar.transform.childCount-1;
		for (int b = 0; b < StatNumber; ++b)
		{
			StatButton[b] = sidebar.transform.GetChild(b+1).gameObject;
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
		for (int i =0;i<StatNumber;++i)
		{
			StatButton[i].transform.GetChild(0).GetComponent<Text>().text = Cost[i].ToString();
		}
	}

	public void ClickButton()
    {
        check = !check;
        Anim.SetBool("Move", check);
    }

	public void BuyStatLV(int index)
	{
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
