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
		//ControllerManager.GetInstance().Player_Bullet_Term = 1.9f;
	}


	void Start()
    {
        check= false;
    }


	public void ClickButton()
    {
        check = !check;
        //print(check);
        Anim.SetBool("Move", check);
    }
}
