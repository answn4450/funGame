using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.Rendering;

[System.Serializable]
class DataForm
{
    public string name;
    public string age;

    public DataForm(string _name, string _age)
    {
        name= _name;
        age= _age;
    }
}


public class DataManager : MonoBehaviour
{
    private int value;
    private string userName;

    void Start()
    {

        var JsonData = Resources.Load<TextAsset>("saveFile/Data");
        DataForm form = JsonUtility.FromJson<DataForm>(JsonData.ToString());

        print(JsonData.ToString());
        value = int.Parse(form.age);
        userName = form.name;

        /*
        form.name = "ÀÓ²©Á¤";
        form.age = "38";
        string data = JsonUtility.ToJson(form);

        print(data);
        */
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.UpArrow))
        {
            ++value;
            print(value);
        }

		if (Input.GetKeyUp(KeyCode.DownArrow))
		{
            --value;
			print(value);
		}

		if (Input.GetKeyUp(KeyCode.Return))
		{
            SaveData("ÀÌ¸ù·æ", value.ToString());
		}
	}

    void SaveData(string _name, string _age)
    {
        DataForm form = new DataForm(_name, _age);

		string JsonData = JsonUtility.ToJson(form);

        FileStream fileStream = new FileStream(
            Application.dataPath+"/Resources/saveFile/Data.json", FileMode.Create);

        print(Application.dataPath);

        byte[] data = Encoding.UTF8.GetBytes(JsonData);

        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
	}
}