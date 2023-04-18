using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class ChangePlayerPattern : MonoBehaviour
{
	private void ButtonChange(int index)
	{
		ControllerManager.GetInstance().SetPlayerPattern(index);
	}
}
