using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStatusDraw : MonoBehaviour
{
    private Text UITravelDistance;
    private Text UIPlayerLV;
    private Transform Course;

    private GameObject Player;


    private void Awake()
	{
		Player = GameObject.Find("Player");
		UITravelDistance = this.transform.GetChild(0).GetComponent<Text>();
		UIPlayerLV = this.transform.GetChild(1).GetComponent<Text>();
        Course = this.transform.GetChild(2);
    }

    void Update()
    {
        UITravelDistance.text = ((int)GameStatus.GetInstance().RunDistance).ToString();
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
        //GameObject currentPace = 
        //Transform currentPace = PaceBar.transform.GetChild(0);
        //currentPace.position = new Vector3();
        Transform currentPoint=Course.Find("CurrentPoint");
        Transform DistanceBar =Course.Find("DistanceBar");
        float startX = DistanceBar.transform.position.x;
        float width = 100.0f;
            //startX+DistanceBar. GetComponent<Rect>().width;
        float percent = GameStatus.GetInstance().RunDistance / GameStatus.GetInstance().DistanceLength;
        percent = 0.0f;
        currentPoint.transform.position = new Vector3(
            width*percent+startX,
            0.0f,
            0.0f
            )+ DistanceBar.position;
        //Image currentPoint = Course.getc
        //CurrentPace.transform.position = new Vector3(startX+ GameStatus.GetInstance().RunDistance / GameStatus.GetInstance().DistanceLength * (endX - startX),0.0f,0.0f);
    }
}
