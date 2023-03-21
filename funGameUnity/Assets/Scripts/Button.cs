using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
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


	public void PushButton(int index, float cool)
	{
		ButtonImages[index].fillAmount = 0;
		print(Buttons[index].GetComponent<Button>());
		//Buttons[0].GetComponent<Button>().enabled = false;

		StartCoroutine(PushButton_Coroutine(index, cool));
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
		ControllerManager.GetInstance().BulletSpeed += 0.025f;
		PushButton(0, 0.5f);
		print("�׽�Ʈ �޼��� 1 �Դϴ�.");
	}

	public void PushButton2()
	{
		PushButton(1, 0.5f);
		print("�׽�Ʈ �޼��� 2 �Դϴ�.");
	}

	public void PushButton3()
	{
		PushButton(2, 0.5f);
		print("�׽�Ʈ �޼��� 3 �Դϴ�.");
	}

	public void PushButton4()
	{
		PushButton(3, 0.5f);
		print("�׽�Ʈ �޼��� 4 �Դϴ�.");
	}

	public void PushButton5()
	{
		PushButton(4, 0.5f);
		print("�׽�Ʈ �޼��� 5 �Դϴ�.");
	}
}