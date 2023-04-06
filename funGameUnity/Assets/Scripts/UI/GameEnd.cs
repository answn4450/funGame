using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEnd : MonoBehaviour
{
    private static GameEnd Instance;
    public GameObject RecordText;

    public static GameEnd GetInstance()
    {
        if (Instance==null)
            Instance = new GameEnd();

        return Instance;
    }

	private void Update()
	{
		RecordText.GetComponent<Text>().text = GameStatus.GetInstance().GameEndComment;
	}

    //게임씬 초기화 없이 로드
    public void Restart()
    {
		SceneManager.LoadScene("GameStart");
	}
}
