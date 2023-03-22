using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    public GameObject ui;
    public RectTransform uiTranspos;

    public float sizeX;
    public float sizeY;
    private float fTime;
    void Awake()
    {
        uiTranspos = ui.GetComponent<RectTransform>();
    }

	private void Start()
	{
        sizeX = 2000.0f;
        sizeY = 1500.0f;
		StartCoroutine(EffectUI());
	}

    private void OnEnable()
    {
        
    }

	private void OnDisable()
	{
        uiTranspos.sizeDelta = new Vector2(10.0f, 5.0f);
	}

	IEnumerator EffectUI()
    {
        float fTime = 0.0f;
        while (uiTranspos.sizeDelta.y < sizeY)
        {
            fTime += Time.deltaTime * 1.0f;
            uiTranspos.sizeDelta = Vector2.Lerp(
                new Vector2(10.0f,5.0f),
                new Vector2(10.0f,5.0f+sizeY),
                fTime
                );
            yield return null;
        }
        
		fTime = 0.0f;
		while (uiTranspos.sizeDelta.x < sizeX)
		{
			fTime += Time.deltaTime * 1.5f;
			uiTranspos.sizeDelta = Vector2.Lerp(
				new Vector2(5.0f, sizeY),
				new Vector2(5.0f+sizeX, sizeY),
				fTime
				);
			yield return null;
		}
	}
}
