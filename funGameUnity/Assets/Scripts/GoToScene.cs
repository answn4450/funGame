using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    public void LogIn()
    {
        SceneManager.LoadScene("LogIn");
    }

    public void MainMenu()
    {
		SceneManager.LoadScene("MainMenu");
	}
}
