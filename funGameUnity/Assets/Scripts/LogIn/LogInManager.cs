using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


enum LoginStatus
{
	Create,
	LogIn,
	SaveLogOut
}


[System.Serializable]
public class MemberForm
{
	string id;
	string password;
	string nickname;
	string favourite;
	int highScore;

}

// 회원가입
// 로그인


public class LogInManager : MonoBehaviour
{
	public InputField IdInput;
	public InputField PasswordInput;
	public Text StatusText;

	string URL = "https://script.google.com/macros/s/AKfycby0sgdU2frCHSu9_OqZiWZBTfJ8mLYBu5gCGmwTwK0/dev";

	IEnumerator Start()
	{
		// ** 요청을 하기위한 작업.
		StatusText.text = "asdf";
		string a, b, c;
		a = IdInput.text;
		b = IdInput.text;
		print(a);
		print(StatusText.text);
		yield return new WaitForSeconds(2.0f);
		/*
		using (UnityWebRequest request = UnityWebRequest.Get(URL))
		{
			yield return request.SendWebRequest();
			MemberForm json = JsonUtility.FromJson<MemberForm>(request.downloadHandler.text);

			print(json);
		}
		*/
	}

	public IEnumerator TryLogIn()
	{
		using (UnityWebRequest request = UnityWebRequest.Get(URL))
		{
			yield return request.SendWebRequest();
			MemberForm json = JsonUtility.FromJson<MemberForm>(request.downloadHandler.text);

			print(json);
		}
	}

	public void NextScene()
	{
		SceneManager.LoadScene("progressScene");
	}
}
