using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayUser : MonoBehaviour
{
    public Text ProgressText; 
    
    void Update()
    {
        ProgressText.text = "";
        ProgressText.text += UserController.GetInstance().HighScore.ToString();
        ProgressText.text += "% ´Þ·È´ø ";
        ProgressText.text += UserController.GetInstance().Nickname;
    }
}
