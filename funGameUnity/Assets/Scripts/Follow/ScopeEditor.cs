using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScopeController))]
public class ScopeEditor : Editor
{
	// ** 0~360
	private void OnSceneGUI()
	{
		ScopeController targetComponent = (ScopeController)target;

		float Angle = targetComponent.Angle * 0.5f;

		float leftAngle = -targetComponent.Angle * 0.5f;
		float rightAngle = targetComponent.Angle * 0.5f;

		Vector3 leftPoint = new Vector3(
			Mathf.Sin(leftAngle),
			0.0f,
			Mathf.Cos(leftAngle)
			);

		Vector3 rightPoint = new Vector3(
			Mathf.Sin(rightAngle),
			0.0f,
			Mathf.Cos(rightAngle)
			);

		Handles.DrawWireArc(targetComponent.transform.position, Vector3.up, Vector3.forward, 360.0f, targetComponent.radius);
		Handles.DrawLine(targetComponent.transform.position,
			targetComponent.transform.position + leftPoint * targetComponent.radius);
		Handles.DrawLine(targetComponent.transform.position,
			targetComponent.transform.position + rightPoint * targetComponent.radius);

		float Segments = targetComponent.Angle / targetComponent.Segments;
		for (int i = 0;i<targetComponent.Segments+1;++i)
		{
			Vector3 anglePoint = new Vector3(
				Mathf.Sin(Segments * i),
				0.0f,
				Mathf.Cos(Segments * i)
				);
			Handles.DrawLine(targetComponent.transform.position,
			targetComponent.transform.position + leftPoint * targetComponent.radius);
			Handles.DrawLine(targetComponent.transform.position,
				targetComponent.transform.position + rightPoint * targetComponent.radius);
		}
	}
}