using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    class SaveData
    {
        public string savedName;
        public int savedScore;
    }

    public void SaveGame()
    {
        highScore = playerScore;
        highScoreName = playerName;

        SaveData data = new SaveData();
        data.savedName = playerName;
        data.savedScore = highScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName = data.savedName;
            highScore = data.savedScore;
        }
    }

    public void ClearSave()
    {
        highScore = 0;
        highScoreName = "Best";

        SaveData data = new SaveData();
        data.savedName = playerName;
        data.savedScore = highScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

        Debug.Log("save has been removed");
    }

    public static GameManager gm;

    //public GameObject clearSaveButton;

    public string highScoreName;
    public int highScore;

    public string playerName;
    public int playerScore;

    // Start is called before the first frame update
    void Awake()
    {

        if (gm != null)
        {
            Destroy(gameObject);
            return;
        }

        gm = this;
        DontDestroyOnLoad(gameObject);
        highScoreName = "Best";
        LoadGame();
    }

    void Start()
    {
        playerScore = 0;
    }

    public void AddScore(int score)
    {
        playerScore += score;
    }
}
