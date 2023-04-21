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
using System.Text.RegularExpressions;
using UnityEditor.VersionControl;
using System.Text;
using System.Security.Cryptography;

enum LoginStatus
{
	Create,
	LogIn
}


[System.Serializable]
public class MemberForm
{
	public string email;
	public string password;
	public string order;
	public string nickname;
	public string highScore;

	public MemberForm(string email, string password, string order)
	{
		this.email = email;
		this.password = password;
		this.order = order;
		this.highScore = "0";
	}
}

// ȸ������
// �α���


public class LoginManager : MonoBehaviour
{
	private string emailPattern = @"^[\w-.]+@([\w-]+.)+[\w-]{2,4}$";

	public InputField EmailInput;
	public InputField PasswordInput;
	public Button MainMenuButton;
	public Button LogInButton;
	public Button RegisterButton;
	public Text Message;

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
				Message.text = EndGetNumber.ToString() + "/" + MaxGetNumber.ToString() + "���� ���� ��..";
			}
			else
			{
				BlockInput(false);
				Message.text = "ȯ��!" + UserController.GetInstance().Nickname.ToString();
				TryGet= false;
				EndGetNumber = 0;
			}
		}
	}


	public void PressLogin()
	{
		string email = EmailInput.text;

		if (Regex.IsMatch(email, emailPattern))
		{
			// ** true
			string password = Security(PasswordInput.text);

			// ** login
			if (string.IsNullOrEmpty(password))
			{
				// ** true
				Message.text = "password�� �ʼ� �Է� ���Դϴ�.";
			}
			else
			{
				// ** ����.(login)
				Login();
			}
		}
		else
		{
			// ** false
			Message.text = "email ������ �ٽ� Ȯ���ϼ���!";
		}
	}
	
	public void PressRegister()
	{
		string email = EmailInput.text;
		if (Regex.IsMatch(email, emailPattern))
		{

			// ** true
			string password = Security(PasswordInput.text);

			// ** login
			if (string.IsNullOrEmpty(password))
			{
				// ** true
				Message.text = "password�� �ʼ� �Է� ���Դϴ�.";
			}
			else
			{
				Register();
			}
		}
		else
		{
			// ** false
			Message.text = "email ������ �ٽ� Ȯ���ϼ���!";
		}
	}

	private void Login()
	{
		BlockInput(true);

		string email = EmailInput.text;
		string password = PasswordInput.text;
		string order = "login";

		MemberForm member = new MemberForm(email, Security(password), order);

		WWWForm form = new WWWForm();
		form.AddField(nameof(member.email), member.email);
		form.AddField(nameof(member.password), member.password);
		form.AddField(nameof(member.order), member.order);
		
		StartCoroutine(TryLogin(form, email, password));
	}


	private void Register()
	{
		BlockInput(true);

		string email = EmailInput.text;
		string password = Security(PasswordInput.text);
		string order = "register";

		MemberForm member = new MemberForm(email, password, order);

		WWWForm form = new WWWForm();
		form.AddField(nameof(member.email), member.email);
		form.AddField(nameof(member.password), member.password);
		form.AddField(nameof(member.order), member.order);

		StartCoroutine(TryRegister(form));
	}

	public void GetNickname()
	{
		string order = "getNickname";

		MemberForm member = new MemberForm(
			UserController.GetInstance().Email, 
			Security(UserController.GetInstance().Password), 
			order);

		WWWForm form = new WWWForm();
		form.AddField(nameof(member.email), member.email);
		form.AddField(nameof(member.password), member.password);
		form.AddField(nameof(member.order), member.order);

		StartCoroutine(TryGetNickName(form));
	}

	public void GetHighScore()
	{
		string order = "getHighScore";

		MemberForm member = new MemberForm(
			UserController.GetInstance().Email, 
			Security(UserController.GetInstance().Password), 
			order);

		WWWForm form = new WWWForm();
		form.AddField(nameof(member.email), member.email);
		form.AddField(nameof(member.password), member.password);
		form.AddField(nameof(member.order), member.order);

		StartCoroutine(TryGetHighScore(form));
	}


	public IEnumerator TryLogin(WWWForm form, string email, string password)
	{
		using (UnityWebRequest request = UnityWebRequest.Post(URL, form))
		{
			Message.text = "�α��� �õ� ��...";

			yield return request.SendWebRequest();
			BlockInput(false);
			if (request.downloadHandler.text == "true")
			{
				Message.text = "�α��� �Ϸ�";
				UserController.GetInstance().Email = email;
				UserController.GetInstance().Password = password;

				TryGet = true;
				EndGetNumber = 0;
				//�ʿ��� ���� ������ MaxGetNumber ��ŭ�� coroutine���� �����´�.
				GetNickname();
				GetHighScore();
			}
			else
			{
				print(request.downloadHandler.text);
				Message.text = "���� ����ġ";
			}
		}
	}
	

	public IEnumerator TryRegister(WWWForm form)
	{
		using (UnityWebRequest request = UnityWebRequest.Post(URL, form))
		{
			Message.text = "���� ���� �õ� ��...";

			yield return request.SendWebRequest();
			BlockInput(false);
			if (request.downloadHandler.text == "true")
			{
				Message.text = "���� ���� �Ϸ�";
			}
			else
			{
				print(request.downloadHandler.text);
				Message.text = "�̹� �����ϴ� ���� email";
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
		EmailInput.GetComponent<InputField>().interactable = !block;
		PasswordInput.GetComponent<InputField>().interactable = !block;
	}

	public void NextScene()
	{
		SceneManager.LoadScene("progressScene");
	}

	string Security(string password)
	{
		if (string.IsNullOrEmpty(password))
		{
			// ** true
			Message.text = "password�� �ʼ� �Է� �� �Դϴ�.";
			return "null";
		}
		else
		{
			// ** ��ȣȭ & ��ȣȭ
			// ** false
			SHA256 sha = new SHA256Managed();
			byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(password));
			StringBuilder stringBuilder = new StringBuilder();

			foreach (byte b in hash)
			{
				stringBuilder.AppendFormat("{0:x2}", b);
			}
			return stringBuilder.ToString();
		}
	}
}
