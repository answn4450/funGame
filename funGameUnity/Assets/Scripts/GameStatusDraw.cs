using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameStatusDraw : MonoBehaviour
{
    private Text UITravelDistance;
    private Text UIPlayerLV;
    private Text UIPlayerExp;
    private Text UIPlayerHP;
    private Transform UICourse;
    private float UICourseStartX = 480.0f, UICourseEndX = 1457.0f;

    private void Awake()
	{
		UITravelDistance = this.transform.GetChild(0).GetComponent<Text>();
		UIPlayerLV = this.transform.GetChild(1).GetComponent<Text>();
        UICourse = this.transform.GetChild(2);
        UIPlayerExp = this.transform.GetChild(3).GetComponent<Text>();
        UIPlayerHP = this.transform.GetChild(4).GetChild(0).GetComponent<Text>();
    }

    void Update()
    {
        UITravelDistance.text = ((int)GameStatus.GetInstance().RunDistance).ToString();
        DrawPlayerLV();
        DrawPaceUI();
        DrawPlayerExp();
        DrawPlayerHP();
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
		Transform currentPoint=UICourse.Find("CurrentPoint");
        Transform wavePoint = UICourse.Find("WavePoint");


		float width = UICourseEndX - UICourseStartX;
        float pointY = UICourse.Find("CourseBar").GetChild(0).transform.position.y;
        
        currentPoint.transform.position = new Vector3(
            width * GameStatus.GetInstance().GetRunPercent()/100.0f + UICourseStartX,
            pointY,
            0.0f
            );

        // run percent 글자 채움
        currentPoint.GetChild(0).GetComponent<Text>().text = GameStatus.GetInstance().GetRunPercent().ToString() + "%";
        currentPoint.GetChild(1).GetComponent<Text>().text = GameStatus.GetInstance().GetRunPercent().ToString() + "%";

        for (int i =0;i<GameStatus.GetInstance().WaveNumber;++i)
        {
            wavePoint.GetChild(i).transform.position = new Vector3(
                UICourseStartX + width * GameStatus.GetInstance().WavePoints[i] / 100,
                pointY,
                0.0f
            );
        }
    }

    private void DrawPlayerExp()
	{
        UIPlayerExp.text = ControllerManager.GetInstance().PlayerExp.ToString();
    }

    private void DrawPlayerHP()
    {
		UIPlayerHP.text =  ((int)GameStatus.GetInstance().PlayerHP).ToString();
	}
}
