using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
	public Vector2 mouse;

	private GameObject Text;

	private void Start()
	{
		Text = Resources.Load("Prefabs/Text1") as GameObject;
	}
	private void Update()
	{
		mouse = Input.mousePosition;

		if (Input.GetKeyUp(KeyCode.Space))
		{
			GameObject a = Instantiate(Text) as GameObject;
			a.transform.position = mouse;
			a.GetComponent<Text>().text = pos();
		}

		print(pos());
	}

	private string pos()
	{
		string str = "";
		str += mouse.x.ToString();
		str += ":";
		str += mouse.y.ToString();
		return str;
	}
}
