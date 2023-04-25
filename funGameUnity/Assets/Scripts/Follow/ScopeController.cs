using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;

public class ScopeController : MonoBehaviour
{
	// ** 0~360
	[Range(0.0f, 360.0f)]
	public float Angle;

#if UNITY_EDITOR
	public List<Vector3> PointList = new List<Vector3>();
#endif
    // ** 가상의 선의 개수
	[Range(10, 360)]
    public int Segments = 30;

    // ** 반지름
    [Range(0.0f, 360.0f)]
    public float radius = 5.0f;

	void Start()
    {
        Angle = 45.0f;
        Segments = 8;
        radius = 5.0f;

        float angle = 360 / Segments;

#if UNITY_EDITOR
        for (int i = 0;i<Segments;++i)
        {
            GameObject Object = new GameObject("EditorGizmo");
            MyGizmo2 gizmo = Object.AddComponent<MyGizmo2>();
            Object.transform.position = new Vector3(
                Mathf.Cos((angle * i) * Mathf.Deg2Rad),
                Mathf.Sin((angle * i) * Mathf.Deg2Rad),
                0.0f
            ) * radius;
            
            PointList.Add(Object.transform.position);
        }
#endif
    }

    void Update()
    {
#if UNITY_EDITOR
        foreach(Vector3 element in PointList)
            Debug.DrawRay(transform.position, element, Color.red);
#endif
    }
}
