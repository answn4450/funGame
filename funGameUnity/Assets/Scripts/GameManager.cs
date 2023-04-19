using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	
	void Update()
    {
		//∞‘¿” ≥°
		if (GameStatus.GetInstance().PlayerHP <= 0)
		{
			GameStatus.GetInstance().GameEndComment = "Fail\n";
			GameStatus.GetInstance().GameEndComment += "Run: ";
			GameStatus.GetInstance().GameEndComment += GameStatus.GetInstance().RunDistance;
			GameStatus.GetInstance().GameEndComment += "M";

			if (UserController.GetInstance().HighScore < GameStatus.GetInstance().RunDistance)
			{
				UserController.GetInstance().HighScore = GameStatus.GetInstance().RunDistance;
				StartCoroutine(UserController.GetInstance().SetHighScore(UserController.GetInstance().HighScore));
			}

			SceneManager.LoadScene("GameEnd");
			GameStatus.GetInstance().ResetGameStatus();
		}
		else if (GameStatus.GetInstance().RunDistance >= GameStatus.GetInstance().DistanceLength)
		{
			GameStatus.GetInstance().GameEndComment = "Win";
			SceneManager.LoadScene("GameEnd");
			GameStatus.GetInstance().ResetGameStatus();
		}

	}
}
