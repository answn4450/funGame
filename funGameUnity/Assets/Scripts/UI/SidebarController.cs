using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidebarController : MonoBehaviour
{
    public GameObject sidebar;

    public bool check;

    private Animator Anim;

	private void Awake()
	{
		Anim=sidebar.GetComponent<Animator>();
	}


	void Start()
    {
        check= false;
    }

	private void Update()
	{/*
		if(check)
        {
            sidebar.transform.position = Vector3.Lerp(
                //sidebar.transform.position,
                new Vector3(Screen.width + 150.0f, Screen.height*0.5f, 0.0f),
                new Vector3(Screen.width - 150.0f, Screen.height*0.5f, 0.0f),
            );
        }
        else
        {
			sidebar.transform.position = Vector3.Lerp(
				new Vector3(1920 - 150.0f, 1080.0f * 0.5f, 0.0f),
				new Vector3(1920 + 150.0f, 1080.0f * 0.5f, 0.0f),
			);
		}
        */
	}

	public void ClickButton()
    {
        check = !check;
        print("click");
        Anim.SetBool("Move", check);
    }
}
