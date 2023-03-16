using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    private Text ui;
    public GameObject player;
    public GameObject test;
    
    // Start is called before the first frame update
    void Start()
    {
        ui= GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position,test.transform.position);
        ui.text = distance.ToString();

        if (distance > 0.5f)
        {
            test.GetComponent<MyGizmo>().color = Color.green;
        }
        else
        { 
            test.GetComponent<MyGizmo>().color = Color.red;
        }
    }
}
