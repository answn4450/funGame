using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletPatternTest : MonoBehaviour
{
	public GameObject Target;
	public Text Text;
	public BulletPattern.Pattern pattern;
	public int LV = 0;

	private void Awake()
	{
		LV = 3;
	}

	void Start()
	{
		Target = GameObject.Find("Target");
		GetComponent<BulletPattern>().Target = Target;
	}

	private void Update()
	{
		LVUpDown();
		if (GetComponent<BulletPattern>().ShotEnd)
		{
		    GetComponent<BulletPattern>().pattern = pattern;
			GetComponent<BulletPattern>().ShotBullet(LV );
		}

		Text.text = LV.ToString();
	}

	private void LVUpDown()
	{
		LV += (int)Input.mouseScrollDelta.y;
		LV = Mathf.Min(4,Mathf.Max(0,LV));
	}
}
