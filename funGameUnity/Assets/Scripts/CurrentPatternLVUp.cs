using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CurrentPatternLVUp : MonoBehaviour
{
    public void PressPatternLVUp()
    {
        BulletPattern.Pattern pattern = ControllerManager.GetInstance().Pattern;
        int lv = ControllerManager.GetInstance().Player_PatternLV[pattern];
        if (lv < 5 && ControllerManager.GetInstance().Player_Exp>0)
        {
            ControllerManager.GetInstance().Player_PatternLV[pattern] += 1;
            ControllerManager.GetInstance().Player_Exp -= 1;
		}
    }
}
