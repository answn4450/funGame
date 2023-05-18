using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
	public Vector2 mouse;

	private GameObject Text;

    [Serialize]
    public Vector3 a_pos, b_os;

    public float angle;
    public float length;
    public float getAngle;
    public float getRotZ;

    GameObject a, b;
    private void Start()
    {
        angle = 20.0f;
        length = 4.0f;
        a = GameObject.Find("PointA");
        b = GameObject.Find("PointB");
    }

    public void Update()
    {
        angle += Time.deltaTime * 10;
        b.transform.position = a.transform.position + new Vector3(
            Mathf.Cos(Mathf.Deg2Rad * angle) * length,
            Mathf.Sin(Mathf.Deg2Rad * angle) * length,
            0.0f
            );
        a.transform.rotation = new Quaternion(0.0f, 0.0f, angle,1.0f);
        getAngle = Vector2.Angle(a.transform.position, b.transform.position);
        getRotZ = a.transform.rotation.z;
    }


    public void DrawGraph()
    {

    }
}
