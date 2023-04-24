using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameStatusDraw : MonoBehaviour
{
    public GameObject Player;

    private Transform UITravelDistance;
    private Transform UIPlayerLV;
    private Transform UIPlayerMoney;
    private Transform UIPlayerHP;
    private Transform UICourse;
    private Transform UIBulletPattern;
    private Transform UIBulletPatternPick;
    private Transform UITarget;
    
    private float UICourseStartX = 480.0f, UICourseEndX = 1457.0f;
    private int PatternIndex = 0;

    private void Awake()
	{
        Player = GameObject.Find("Player");

		UITravelDistance = this.transform.GetChild(0);
		UIPlayerLV = this.transform.GetChild(1);
        UICourse = this.transform.GetChild(2);
        UIPlayerMoney = this.transform.GetChild(3);
        UIPlayerHP = this.transform.GetChild(4);
        UIBulletPattern = this.transform.GetChild(5);
        UIBulletPatternPick = this.transform.GetChild(6);
        UITarget = this.transform.GetChild(7);
    }

    void Update()
    {
        UITravelDistance.GetComponent<Text>().text = ((int)GameStatus.GetInstance().RunDistance).ToString();
        DrawPlayerLV();
        DrawCourseUI();
        DrawPlayerMoney();
        DrawPlayerHP();
        DrawBulletPattern();
        DrawPickBulletPattern();
        DrawTarget();
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
        UIPlayerLV.GetComponent<Text>().text = LVStr;
    }


    //수동으로 시작점이랑 끝점을 정한 후 움직였음.
    private void DrawCourseUI()
    {
		Transform currentPoint=UICourse.Find("CurrentPoint");
        Transform wavePoint = UICourse.Find("WavePoint");

		float width = UICourseEndX - UICourseStartX;
		//float pointY = UICourse.Find("CourseBar").GetChild(0).transform.position.y;
        float pointY = 76;
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

    private void DrawPlayerMoney()
	{
        UIPlayerMoney.GetChild(0).GetComponent<Text>().text = ControllerManager.GetInstance().Player_Money.ToString();
    }

    private void DrawPlayerHP()
    {
		UIPlayerHP.GetChild(0).GetComponent<Text>().text =  ((int)GameStatus.GetInstance().PlayerHP).ToString();
	}

    private void DrawBulletPattern()
    {
        BulletPattern.Pattern pattern = ControllerManager.GetInstance().Player_Pattern;
		UIBulletPattern.GetComponent<Text>().text = pattern.ToString();
        int lv = ControllerManager.GetInstance().Player_PatternLV[pattern];
		UIBulletPattern.GetComponent<Text>().text += " - " + lv.ToString();
    }
    
    private void DrawPickBulletPattern()
    {
        int scroll = (int)(Input.mouseScrollDelta.y);
        int patternNumber = ControllerManager.GetInstance().Player_Patterns.Count;
		PatternIndex = (PatternIndex+scroll+patternNumber) % patternNumber;
		BulletPattern.Pattern pattern = ControllerManager.GetInstance().Player_Patterns[PatternIndex];
		string patternInfo = PatternIndex.ToString() + "번째 ";
		patternInfo += pattern.ToString();
		UIBulletPatternPick.GetComponent<Text>().text = patternInfo;
	}

    private void DrawTarget()
    {
        UITarget.position = Player.GetComponent<BulletPattern>().Target.transform.position;
    }

	public void PatternButton()
    {
		ControllerManager.GetInstance().SetPlayerPattern(PatternIndex);
	}
}
