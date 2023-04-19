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

	public string Id = "guest";
	public string Password = "1234";
	public string Nickname = "guest";
	public float HighScore = 0.0f;

	enum want
	{
		login
	}
	[System.Serializable]
	public class MemberForm
	{
		public string id;
		public string password;
		public string want;
		public string nickname;
		public string highScore;

		public MemberForm(string id, string password)
		{
			this.id = id;
			this.password = password;
		}
	}

	private MemberForm UserForm;
	public static UserController Instance;

	public static UserController GetInstance()
	{
		if (Instance == null)
		{
			Instance = new UserController();
		}
		return Instance;
	}

	public void Login(string id, string password)
	{
		UserForm = new MemberForm(id, password);
	}

	public WWWForm GetUserWWWForm()
	{
		WWWForm form = new WWWForm();
		form.AddField(nameof(UserForm.id), UserForm.id);
		form.AddField(nameof(UserForm.password), UserForm.password);
		form.AddField(nameof(UserForm.want), UserForm.want);
		form.AddField(nameof(UserForm.highScore), UserForm.highScore);

		return form;
	}

	public IEnumerator SetHighScore(float score)
	{
		UserForm.want = "setHighScore";
		UserForm.highScore = score.ToString();
		WWWForm form = GetUserWWWForm();
		using (UnityWebRequest request = UnityWebRequest.Post(URL, form))
		{
			yield return request.SendWebRequest();
		}
	}
}
