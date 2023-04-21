using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Windows;

public class UserController
{
	private string URL = "https://script.google.com/macros/s/AKfycbzgvSq1CkKXmBN7sBrnt-e3GrXxjNe8yHR17gJ-gNL5ueT8IoVafJZ2fvh2y_DPshex/exec";

	public string Email = "guest@funGame";
	public string Password = "1234";
	public string Nickname = "guest";
	public float HighScore = 0.0f;

	enum order
	{
		login
	}

	[System.Serializable]
	public class MemberForm
	{
		public string email;
		public string password;
		public string order;
		public string nickname;
		public string highScore;

		public MemberForm(string email, string password)
		{
			this.email = email;
			this.password = password;
		}
	}

	private MemberForm UserForm = new MemberForm("guest@funGame", "1234");
	public static UserController Instance;

	public static UserController GetInstance()
	{
		if (Instance == null)
		{
			Instance = new UserController();
		}
		return Instance;
	}

	public IEnumerator SetHighScore(float score)
	{
		UserForm.order = "setHighScore";
		UserForm.highScore = score.ToString();
		WWWForm form = new WWWForm();
		form.AddField(nameof(UserForm.email), UserForm.email);
		form.AddField(nameof(UserForm.password), UserForm.password);
		form.AddField(nameof(UserForm.order), UserForm.order);
		form.AddField(nameof(UserForm.highScore), UserForm.highScore);

		using (UnityWebRequest request = UnityWebRequest.Post(URL, form))
		{
			yield return request.SendWebRequest();
		}
	}
}
