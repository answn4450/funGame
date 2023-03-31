using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStatus : MonoBehaviour
{
    private Text UITravelDistance;
    private Text UIPlayerLV;
    private GameObject Player;

    private GameObject Pace;
    private GameObject CurrentPace;

	private void Awake()
	{
		Player = GameObject.Find("Player");
		UITravelDistance = this.transform.GetChild(0).GetComponent<Text>();
		UIPlayerLV = this.transform.GetChild(1).GetComponent<Text>();
        Pace = GameObject.Find("Pace");
        CurrentPace = GameObject.Find("CurrentPace");
        
	}

    void Update()
    {
        UITravelDistance.text = ((int)Player.GetComponent<PlayerController>().TravelDistance).ToString();
        DrawPlayerLV();
        DrawPaceUI();
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


    private void DrawPaceUI()
    {
        // 烙矫
        //float PassedPace = 2.2f;
        //林青 / 醚 林青 *  (pace width-current width) + pace x
        //CurrentPace.transform.position =;
    }
}
