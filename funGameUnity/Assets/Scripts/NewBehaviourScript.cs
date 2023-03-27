using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
	private enum Skill
	{
		PowerUp,
		DefenceUp,
		SpeedUp,
		Shield,
		Heal
	};
	// Start is called before the first frame update
	void Start()
    {
		print("new behaviour---------");
		print(0==Skill.PowerUp);
		print("---------new behaviour");
    }


    // Update is called once per frame
    void Update()
    {
		print(ControllerManager.GetInstance().Player_Bullet_Term);
    }
}
