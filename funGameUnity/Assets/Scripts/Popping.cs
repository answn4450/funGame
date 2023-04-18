using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.UI;

public class Popping : MonoBehaviour
{
	private GameObject Canvas;
    private Vector3 dir = new Vector3(0.0f, 1.0f);
	private Color FadeOutColor = new Color(0.0f, 0.0f, 0.0f, -3f);
	private void Awake()
	{
		Canvas = GameObject.Find("EffectCanvas");
		Destroy(gameObject, 1f);
		gameObject.layer = 5;
		transform.SetParent(Canvas.transform);
	}

	void Update()
    {
		transform.GetComponent<Text>().color += Time.deltaTime*FadeOutColor;
		transform.position += dir;    
    }
}
