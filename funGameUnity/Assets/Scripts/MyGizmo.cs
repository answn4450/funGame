using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGizmo : MonoBehaviour
{
	public Color color =Color.blue;

	private void OnDrawGizmos()
	{
		// ** Gizmos 의 색을 변경한다.
		Gizmos.color = color;

		// ** Gizmos 그린다.
		Gizmos.DrawSphere(this.transform.position, 0.2f);
	}
}
