using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;


enum LoginStatus
{
	Create,
	LogIn
}


[System.Serializable]
public class MemberForm
{
	public string id;
	public string password;
	public string want;
	public string nickname;

	public MemberForm(string id, string password, string want)
	{
		this.id = id;
		this.password = password;
		this.want = want;
	}
}

// 회원가입
// 로그인


public class LogInManager : MonoBehaviour
{
	public InputField IdInput;
	public InputField PasswordInput;
	public Text TryStatusText;

	string URL = "https://script.google.com/macros/s/AKfycbzgvSq1CkKXmBN7sBrnt-e3GrXxjNe8yHR17gJ-gNL5ueT8IoVafJZ2fvh2y_DPshex/exec";


	public void LogInButton()
	{
		IdInput.GetComponent<InputField>().interactable = false;
		PasswordInput.GetComponent<InputField>().interactable = false;

		string id = IdInput.text;
		string password = PasswordInput.text;
		string want = "logIn";

		MemberForm member = new MemberForm(id, password, want);

		WWWForm form = new WWWForm();
		form.AddField(nameof(member.id), member.id);
		form.AddField(nameof(member.password), member.password);
		form.AddField(nameof(member.want), member.want);
		
		StartCoroutine(TryLogIn(form));
	}
	
	public void RegisterButton()
	{
		IdInput.GetComponent<InputField>().interactable = false;
		PasswordInput.GetComponent<InputField>().interactable = false;

		string id = IdInput.text;
		string password = PasswordInput.text;
		string want = "register";

		MemberForm member = new MemberForm(id, password, want);

		WWWForm form = new WWWForm();
		form.AddField(nameof(member.id), member.id);
		form.AddField(nameof(member.password), member.password);
		form.AddField(nameof(member.want), member.want);

		StartCoroutine(TryRegister(form));
	}


	public void GetNickname()
	{
		string id = IdInput.text;
		string password = PasswordInput.text;
		string want = "getNickname";

		MemberForm member = new MemberForm(id, password, want);

		WWWForm form = new WWWForm();
		form.AddField(nameof(member.id), member.id);
		form.AddField(nameof(member.password), member.password);
		form.AddField(nameof(member.want), member.want);

		StartCoroutine(TryGetNickName(form));
	}


	public IEnumerator TryLogIn(WWWForm form)
	{
		using (UnityWebRequest request = UnityWebRequest.Post(URL, form))
		{
			TryStatusText.text = "로그인 시도 중...";

			yield return request.SendWebRequest();
			if (request.downloadHandler.text == "true")
			{
				TryStatusText.text = "로그인 완료";

				//닉네임 가져오는 동안 아이디비번 인풋 금지
				GetNickname();
			}
			else
			{
				TryStatusText.text = "계정 불일치";
				IdInput.GetComponent<InputField>().interactable = true;
				PasswordInput.GetComponent<InputField>().interactable = true;
			}
		}
	}

	public IEnumerator TryRegister(WWWForm form)
	{
		using (UnityWebRequest request = UnityWebRequest.Post(URL, form))
		{
			TryStatusText.text = "계정 생성 시도 중...";

			yield return request.SendWebRequest();
			if (request.downloadHandler.text == "true")
			{
				TryStatusText.text = "계정 생성 완료";
			}
			else
			{
				TryStatusText.text = "이미 존재하는 계정 id";
			}
			IdInput.GetComponent<InputField>().interactable = true;
			PasswordInput.GetComponent<InputField>().interactable = true;
		}
	}

	public IEnumerator TryGetNickName(WWWForm form)
	{
		using (UnityWebRequest request = UnityWebRequest.Post(URL, form))
		{
			TryStatusText.text = "닉네임 받아오는 중...";

			yield return request.SendWebRequest();
			GameStatus.GetInstance().Nickname = request.downloadHandler.text;
			TryStatusText.text = "hi! " + GameStatus.GetInstance().Nickname;
			IdInput.GetComponent<InputField>().interactable = true;
			PasswordInput.GetComponent<InputField>().interactable = true;
		}
	}

	public void NextScene()
	{
		SceneManager.LoadScene("progressScene");
	}
}
