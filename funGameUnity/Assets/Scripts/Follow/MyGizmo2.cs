using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGizmo2 : MonoBehaviour
{
	public GameObject Target=null;

	private void Start()
	{
		if (Target == null)
			Target = this.gameObject;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(Target.transform.position, 0.2f);
	}
}
