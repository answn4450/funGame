using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStatusDraw : MonoBehaviour
{
    private Text UITravelDistance;
    private Text UIPlayerLV;
    private Text UIPlayerExp;
    private Transform Course;

    private GameObject Player;

    private void Awake()
	{
		Player = GameObject.Find("Player");
		UITravelDistance = this.transform.GetChild(0).GetComponent<Text>();
		UIPlayerLV = this.transform.GetChild(1).GetComponent<Text>();
        Course = this.transform.GetChild(2);
        UIPlayerExp = this.transform.GetChild(3).GetComponent<Text>();
    }

    void Update()
    {
        UITravelDistance.text = ((int)GameStatus.GetInstance().RunDistance).ToString();
        DrawPlayerLV();
        DrawPaceUI();
        DrawPlayerExp();
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


    //수동으로 시작점이랑 끝점을 정한 후 움직였음.
    private void DrawPaceUI()
    {
        Transform currentPoint=Course.Find("CurrentPoint");
        float startX = 480.0f;
        float endX = 1457.0f;
        float width = endX - startX;
        float percent = GameStatus.GetInstance().RunDistance / GameStatus.GetInstance().DistanceLength;
        
        currentPoint.transform.position = new Vector3(
            width * percent + startX,
            57.0f,
            0.0f
            );
    }

    private void DrawPlayerExp()
	{
        UIPlayerExp.text = ControllerManager.GetInstance().PlayerExp.ToString();
	}
}
