using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	// ** ī�޶��� ���� �ð�
	private float shakeTime;
	// ** ī�޶��� ���� ����
	private Vector3 offset = new Vector3(0.025f, 0.025f, 0.0f);

	// ** ī�޶��� ����ȿ���� �ֱ��� ī�޶� ��ġ�� �޾ƿ´�. 
	private Vector3 oldPosition;
	// ** �ڷ�ƾ �Լ� ����
	IEnumerator Start()
    {
		// ** ī�޶��� ����ȿ���� �ֱ��� ī�޶� ��ġ�� �޾ƿ´�. 
		oldPosition = new Vector3(0.0f, -0.7f, -10f);
	// ** 0.15�� ���� ����
	shakeTime = 0.25f;
        while(shakeTime > 0.0f)
        {
            shakeTime -= Time.deltaTime;

			// ** �ݺ����� ����Ǵ� ���� �ݺ������� ȣ��.
			yield return null;

			// ** ī�޶� ���� ���� ��ŭ ������Ų��.
			Camera.main.transform.position = new Vector3(
			Random.Range(oldPosition.x - offset.x, oldPosition.x + offset.x),
			Random.Range(oldPosition.y - offset.y, oldPosition.y + offset.x),
			-10.0f
			);
		}

		// ** �ݺ����� ����Ǹ� ī�޶���ġ�� �ٽ� ������ ���´�. 
		Camera.main.transform.position = oldPosition;

		// ** Ŭ������ �����Ѵ�. 
		Destroy(this.gameObject);
	}
}
