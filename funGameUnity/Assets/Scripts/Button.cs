using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;
using static BulletPattern;

public class Button : MonoBehaviour
{
	public List<GameObject> Images = new List<GameObject>();
	public List<GameObject> Buttons = new List<GameObject>();
	public List<Image> ButtonImages = new List<Image>();

	private ControllerManager controllerManager;

	private float cooldown;
	public enum Skill
	{
		PowerUp,
		DefenceUp,
		SpeedUp,
		Shield,
		Heal
	};

	private void Awake()
	{
		controllerManager = ControllerManager.GetInstance();
	}

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


	public void PushButton(int index, float cool, float duration)
	{
		ButtonImages[index].fillAmount = 0;
		//print(Buttons[index].GetComponent<Button>());
		//Buttons[0].GetComponent<Button>().enabled = false;

		StartCoroutine(PushButton_Coroutine(index, cool));
		ApplyEffect(index);
		StartCoroutine(SkillEffect_Coroutine(index, duration));
	}

	IEnumerator SkillEffect_Coroutine(int index, float cool)
	{
		while (cool > 0)
		{
			print(cool);
			cool -= Time.deltaTime;
			yield return null;
		}
		EraseSomeEffect(index);
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
		controllerManager.BulletSpeed += 0.025f;
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

	private void ApplyEffect(int index)
	{
		switch (index)
		{
			case 0:
				controllerManager.Player_Bullet_Term = 0.15f;
				break;

			case 1:
				controllerManager.Player_HP= Math.Min(
					controllerManager.Player_HP+10, 100
				);
				break;
		}
	}

	//PowerUp, DefenceUp, SpeedUp, Shield, Heal
	private void EraseSomeEffect(int index)
	{
		switch (index)
		{
			case 0:
				ControllerManager.GetInstance().Player_Bullet_Term = 0.3f;
				break;
		}
	}
}