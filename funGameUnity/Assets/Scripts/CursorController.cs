using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CursorController : MonoBehaviour
{
    void Update()
    {
		transform.position = Input.mousePosition;
	}
}
