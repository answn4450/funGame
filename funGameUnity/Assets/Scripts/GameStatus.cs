using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStatus : MonoBehaviour
{
    private Text UITravelDistance;
    private Text UIPlayerLV;
    private GameObject Player;

	private void Awake()
	{
		Player = GameObject.Find("Player");
		UITravelDistance = this.transform.GetChild(0).GetComponent<Text>();
		UIPlayerLV = this.transform.GetChild(1).GetComponent<Text>();
	}

    void Update()
    {
        UITravelDistance.text = ((int)Player.GetComponent<PlayerController>().TravelDistance).ToString();
        DrawPlayerLV();
    }

    private void DrawPlayerLV()
    {
        string LVStr = "";
        for (int i = 0; i < 5; ++i)
        {
			LVStr+=ControllerManager.GetInstance().NowLV[i].ToString();
            if (i!=4)
            {
                LVStr += '/';
            }
		}
        UIPlayerLV.text = LVStr;
    }
}
