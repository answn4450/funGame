using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.UI;

public class Popping : MonoBehaviour
{
	private GameObject Canvas;
	private Color FadeOutColor = new Color(0.0f, 0.0f, 0.0f, -3f);

	private void Awake()
	{
		Canvas = GameObject.Find("EffectCanvas");
		//Destroy(gameObject, Time.deltaTime*20);
		gameObject.layer = 5;
	}

	private void Start()
	{
		transform.SetParent(Canvas.transform);
	}

	void Update()
    {
		transform.GetComponent<Text>().color += Time.deltaTime*FadeOutColor; 
    }
}
