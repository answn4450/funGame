using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackController : MonoBehaviour
{
    Vector3 Direction;
    public void Start()
    {
        Direction = new Vector3(1.0f, 0.0f, 0.0f).normalized;
        float fAngle = getAngle(Direction);
        transform.eulerAngles = new Vector3(
            0.0f, 0.0f, -fAngle);
    }

    public void Update()
    {
        transform.position += Direction;    
    }

    public float getAngle(Vector3 to)
    {
        return Quaternion.FromToRotation(Vector3.down, to).eulerAngles.z;
    }
}
