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

    //���Ӿ� �ʱ�ȭ ���� �ε�
    public void Restart()
    {
		SceneManager.LoadScene("GameStart");
	}
}
