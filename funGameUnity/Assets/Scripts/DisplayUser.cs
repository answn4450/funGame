using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayUser : MonoBehaviour
{
    public Text ProgressText; 
    
    void Update()
    {
        ProgressText.text = "�� �޸��� " + GameStatus.GetInstance().Nickname;
    }
}
