using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
	public void OnPointerUp(PointerEventData eventData)
	{
		SceneManager.LoadScene("GameStart");
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		throw new System.NotImplementedException();
	}

	public void OnDrag(PointerEventData eventData)
	{
		throw new System.NotImplementedException();
	}
}
