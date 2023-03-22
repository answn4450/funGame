using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    public GameObject SkillCanvas;
    public bool SkillCanvasActive;

	public void Awake()
	{
        SkillCanvas = GameObject.Find("SkillCanvas");
	}

	// Start is called before the first frame update
	void Start()
    {
        SkillCanvasActive = true;
        SkillCanvas.SetActive(SkillCanvasActive);
    }

    public void onSkillCanvasActive()
    {
        SkillCanvasActive = !SkillCanvasActive;
        SkillCanvas.SetActive(SkillCanvasActive);
    }
}
