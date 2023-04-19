using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.SocialPlatforms.Impl;
using JetBrains.Annotations;
using System.Threading;

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
	public string highScore;

	public MemberForm(string id, string password, string want)
	{
		this.id = id;
		this.password = password;
		this.want = want;
		this.highScore = "0";
	}
}

// 회원가입
// 로그인


public class LogInManager : MonoBehaviour
{
	public InputField IdInput;
	public InputField PasswordInput;
	public Button MainMenuButton;
	public Button LogInButton;
	public Button RegisterButton;
	public Text TryStatusText;

	string URL = "https://script.google.com/macros/s/AKfycbzgvSq1CkKXmBN7sBrnt-e3GrXxjNe8yHR17gJ-gNL5ueT8IoVafJZ2fvh2y_DPshex/exec";

	private const int MaxGetNumber = 2;
	private int EndGetNumber = MaxGetNumber;
	private bool TryGet = false;


	private void Update()
	{
		if (TryGet)
		{
			if (EndGetNumber < MaxGetNumber)
			{
				BlockInput(true);
				TryStatusText.text = EndGetNumber.ToString() + "/" + MaxGetNumber.ToString() + "정보 오는 중..";
			}
			else
			{
				BlockInput(false);
				TryStatusText.text = "환영!" + UserController.GetInstance().Nickname.ToString();
				TryGet= false;
				EndGetNumber = 0;
			}
		}
	}


	public void PressLogIn()
	{
		BlockInput(true);

		string id = IdInput.text;
		string password = PasswordInput.text;
		string want = "logIn";

		MemberForm member = new MemberForm(id, password, want);

		WWWForm form = new WWWForm();
		form.AddField(nameof(member.id), member.id);
		form.AddField(nameof(member.password), member.password);
		form.AddField(nameof(member.want), member.want);
		
		StartCoroutine(TryLogIn(form, id, password));
	}
	
	public void PressRegister()
	{
		BlockInput(true);

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
		string want = "getNickname";

		MemberForm member = new MemberForm(
			UserController.GetInstance().Id, 
			UserController.GetInstance().Password, 
			want);

		WWWForm form = new WWWForm();
		form.AddField(nameof(member.id), member.id);
		form.AddField(nameof(member.password), member.password);
		form.AddField(nameof(member.want), member.want);

		StartCoroutine(TryGetNickName(form));
	}

	public void GetHighScore()
	{
		string want = "getHighScore";

		MemberForm member = new MemberForm(
			UserController.GetInstance().Id, 
			UserController.GetInstance().Password, 
			want);

		WWWForm form = new WWWForm();
		form.AddField(nameof(member.id), member.id);
		form.AddField(nameof(member.password), member.password);
		form.AddField(nameof(member.want), member.want);

		StartCoroutine(TryGetHighScore(form));
	}


	public IEnumerator TryLogIn(WWWForm form, string id, string password)
	{
		using (UnityWebRequest request = UnityWebRequest.Post(URL, form))
		{
			TryStatusText.text = "로그인 시도 중...";

			yield return request.SendWebRequest();
			BlockInput(false);
			if (request.downloadHandler.text == "true")
			{
				TryStatusText.text = "로그인 완료";
				UserController.GetInstance().Id = id;
				UserController.GetInstance().Password = password;

				TryGet = true;
				EndGetNumber = 0;
				//필요한 유저 정보를 MaxGetNumber 만큼의 coroutine으로 가져온다.
				GetNickname();
				GetHighScore();
			}
			else
			{
				TryStatusText.text = "계정 불일치";
			}
		}
	}
	

	public IEnumerator TryRegister(WWWForm form)
	{
		using (UnityWebRequest request = UnityWebRequest.Post(URL, form))
		{
			TryStatusText.text = "계정 생성 시도 중...";

			yield return request.SendWebRequest();
			BlockInput(false);
			if (request.downloadHandler.text == "true")
			{
				TryStatusText.text = "계정 생성 완료";
			}
			else
			{
				TryStatusText.text = "이미 존재하는 계정 id";
			}
		}
	}


	public IEnumerator TryGetNickName(WWWForm form)
	{
		using (UnityWebRequest request = UnityWebRequest.Post(URL, form))
		{
			yield return request.SendWebRequest();
			UserController.GetInstance().Nickname = request.downloadHandler.text;
			EndGetNumber += 1;
		}
	}


	public IEnumerator TryGetHighScore(WWWForm form)
	{
		using (UnityWebRequest request = UnityWebRequest.Post(URL, form))
		{
			yield return request.SendWebRequest();
			UserController.GetInstance().HighScore = float.Parse(request.downloadHandler.text);
			EndGetNumber += 1;
			UserController.GetInstance().SetHighScore(10);
		}
	}


	private void BlockInput(bool block)
	{
		LogInButton.enabled = !block;
		RegisterButton.enabled = !block;
		IdInput.GetComponent<InputField>().interactable = !block;
		PasswordInput.GetComponent<InputField>().interactable = !block;
	}

	public void NextScene()
	{
		SceneManager.LoadScene("progressScene");
	}
}
