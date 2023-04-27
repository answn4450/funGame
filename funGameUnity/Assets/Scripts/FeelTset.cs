using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeelTset : MonoBehaviour
{
	public GameObject Player;

	//쓸만한 FX
	private GameObject MagicPoof;
	private GameObject FSmoke;
	private GameObject Smoke;
	private GameObject Hit;
	private GameObject SwordCross;
	private GameObject SwordSpiral;
	private GameObject SwordThinSpiral;

	//키보드 키로 일단 플레이 시뮬
	void Update()
    {
		Player.GetComponent<PlayerController>().LikeJump();

		if (Input.GetKeyUp(KeyCode.Z))
			FeelFX(0);
		if (Input.GetKeyUp(KeyCode.X))
			FeelFX(1);
		if (Input.GetKeyUp(KeyCode.C))
			FeelFX(2);
		if (Input.GetKeyUp(KeyCode.V))
			FeelFX(3);
		if (Input.GetKeyUp(KeyCode.B))
			FeelFX(4);
		if (Input.GetKeyUp(KeyCode.N))
			FeelFX(5);
		if (Input.GetKeyUp(KeyCode.M))
			FeelFX(6);

		_Test();
	}


	private void FeelFX(int i)
	{
		print("fx test");
		GameObject[] LikeFX = { MagicPoof, FSmoke, Smoke, Hit, SwordCross, SwordSpiral, SwordThinSpiral };
        GameObject Obj = Instantiate(PrefabManager.Instance.GetPrefabByName("MagicPoof"));

        Obj.transform.position = new Vector3(
			Player.transform.position.x,
			Player.transform.position.y,
			-3.0f
		);
	}

	private void _Test()
	{
		GameObject a = GameObject.Find("yesA");
		//a.child
	}
}
