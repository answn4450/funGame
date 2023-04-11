using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

[System.Serializable]
public class MemberForm
{
	public string name;
	public int age;
	public int index;
	public int gender;

	public MemberForm(string name, int age, int index, int gender)
	{
		this.name = name;
		this.age = age;
		this.index = index;
		this.gender = gender;
	}
}

// ȸ������
// �α���


public class LogInManager : MonoBehaviour
{
	string URL = "https://script.google.com/macros/s/AKfycbzgvSq1CkKXmBN7sBrnt-e3GrXxjNe8yHR17gJ-gNL5ueT8IoVafJZ2fvh2y_DPshex/exec";

	IEnumerator Start()
	{
		// ** ��û�� �ϱ����� �۾�.


		using (UnityWebRequest request = UnityWebRequest.Get(URL))
		{
			yield return request.SendWebRequest();
			MemberForm json = JsonUtility.FromJson<MemberForm>(request.downloadHandler.text);

			print(json.name);
			// ** ���信 ���� �۾�.
			//print(request.downloadHandler.text);
		}
	}

	public void NextScene()
	{
		SceneManager.LoadScene("progressScene");
	}
}
