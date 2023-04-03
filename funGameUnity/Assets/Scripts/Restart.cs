using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
