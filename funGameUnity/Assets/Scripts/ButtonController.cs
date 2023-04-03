using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler 
{
    private Text text;
	private RectTransform rectTransform;
	private Color OldColor;

	private void Awake()
	{
		text = GetComponent<Text>();
		rectTransform = GetComponent<RectTransform>();
	}

	private void Start()
    {
		text.text = transform.name;
    }

	public void pushButton()
    {

    }

	public void OnPointerUp(PointerEventData eventData)
	{
		SceneManager.LoadScene(text.text);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
	}

	public void OnDrag(PointerEventData eventData)
	{
		print("drag");
		OldColor = text.color;
		text.color = Color.white;
	}

	public void RestartGameScene()
	{

	}
}
