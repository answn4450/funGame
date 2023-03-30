using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;
using static BulletPattern;

enum Skill
{
	PowerUp,
	DefenceUp,
	SpeedUp,
	Shield,
	Heal
};

public class SkillController: MonoBehaviour
{
	public List<GameObject> Images = new List<GameObject>();
	public List<GameObject> Buttons = new List<GameObject>();
	public List<Image> ButtonImages = new List<Image>();

	private float cooldown;

	private void Start()
	{
		GameObject SkillsObj = GameObject.Find("Skills");

		for (int i = 0; i < SkillsObj.transform.childCount; ++i)
			Images.Add(SkillsObj.transform.GetChild(i).gameObject);

		for (int i = 0; i < Images.Count; ++i)
			Buttons.Add(Images[i].transform.GetChild(0).gameObject);

		for (int i = 0; i < Buttons.Count; ++i)
			ButtonImages.Add(Buttons[i].GetComponent<Image>());

		cooldown = 0.0f;
	}

	//키보드로 숫자로 스킬 적용
	private void Update()
	{
		
	}

	public void PushButton(int index, float cool, float skillduration)
	{
		ButtonImages[index].fillAmount = 0;
		//print(Buttons[index].GetComponent<Button>());
		//Buttons[0].GetComponent<Button>().enabled = false;
		
		StartCoroutine(PushButton_Coroutine(index, cool));
		ControllerManager.GetInstance().GoTrial(index,skillduration);
	}

	IEnumerator PushButton_Coroutine(int index, float cool)
	{
		while (ButtonImages[index].fillAmount != 1)
		{
			ButtonImages[index].fillAmount += Time.deltaTime * cool;
			yield return null;
		}

		//Buttons[0].GetComponent<Button>().enabled = true;
	}

	public void PushButton1()
	{
		PushButton(0, 0.5f, 3.0f);
		print("테스트 메세지 1 입니다.");
	}

	public void PushButton2()
	{
		PushButton(1, 0.5f,2.5f);
		print("테스트 메세지 2 입니다.");
	}

	public void PushButton3()
	{
		PushButton(2, 0.5f, 2.5f);
		print("테스트 메세지 3 입니다.");
	}

	public void PushButton4()
	{
		PushButton(3, 0.5f, 2.5f);
		print("테스트 메세지 4 입니다.");
	}

	public void PushButton5()
	{
		PushButton(4, 0.5f, 2.5f);
		print("테스트 메세지 5 입니다.");
	}
}