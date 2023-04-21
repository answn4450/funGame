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
	private List<GameObject> BulletList = new List<GameObject>();
	private GameObject BulletPrefab;

	private void Awake()
	{
		LV = 3;
	}

	void Start()
	{
		Target = GameObject.Find("Target");
		BulletPrefab = Resources.Load("Prefabs/Bullet") as GameObject;
		GetComponent<BulletPattern>().BulletPrefab = BulletPrefab;
		GetComponent<BulletPattern>().Target = Target;
	}

	private void Update()
	{
		GetComponent<BulletPattern>().pattern = pattern;
		LVUpDown();
		if (Input.GetKeyUp(KeyCode.Space))
		{
			GetComponent<BulletPattern>().ShotBullet(0);
		}
		if (this.GetComponent<BulletPattern>().ShotEnd)
		{
			this.GetComponent<BulletPattern>().ShotEnd = false;
			this.GetComponent<BulletPattern>().ShotBullet(LV);
		}

		Text.text = LV.ToString();
	}

	private void LVUpDown()
	{
		LV += (int)Input.mouseScrollDelta.y;
		LV = Mathf.Min(4,Mathf.Max(0,LV));
	}
}
