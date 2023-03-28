using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStatus : MonoBehaviour
{
    private Text UITravelDistance;
    private Text UILevel;
    private GameObject Player;

	private void Awake()
	{
		Player = GameObject.Find("Player");
		UITravelDistance = this.transform.GetChild(0).GetComponent<Text>();
		UILevel = this.transform.GetChild(1).GetComponent<Text>();
	}

	void Start()
    {
           
    }

    void Update()
    {
        UITravelDistance.text = ((int)Player.GetComponent<PlayerController>().TravelDistance).ToString();
        DrawUILevel();
    }

    private void DrawUILevel()
    {
        string str = "";
        str += ControllerManager.GetInstance().LV_BulletTerm.ToString();
        str += '/';
        str += ControllerManager.GetInstance().LV_HealRegen.ToString();
        UILevel.text = str;
    }
}
