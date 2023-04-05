using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SidebarController : MonoBehaviour
{
    public GameObject sidebar;

    public bool check;

    private Animator Anim;

	private int MaxPureLV;
	int StatNumber;
	// ��ų�� �� �� 5�� ���ȸ� ��ų�� ����
	public GameObject[] StatButton = new GameObject[1000];
	int[] CostRoadMap = { 3, 5, 10, 15, 20 , 0};
	//statnumber (�ϴ� 6��) ��ŭ
	int[] CostRoadMapIndex = new int[1000];
	//���� �䱸�Ǵ� cost
    int[] Cost = new int[1000];

	private void Awake()
	{
		Anim=sidebar.GetComponent<Animator>();
	}


	void Start()
    {
        check= false;
		MaxPureLV = ControllerManager.GetInstance().MaxPureLV;
		//X ��ư ���� ����
		//���� �ִ� ��ư 5���� ������.
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
		//print(index);
		int exp = ControllerManager.GetInstance().PlayerExp;

		if (Cost[index] <= exp && CostRoadMapIndex[index] < MaxPureLV)
		{
			if (index <= 4)
			{
				ControllerManager.GetInstance().LV[index] += 1;
				ControllerManager.GetInstance().PlayerExp -= Cost[index];
				CostRoadMapIndex[index] += 1;
				Cost[index] = CostRoadMap[index];
			}

		}
	}
}
