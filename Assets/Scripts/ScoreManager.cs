using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int BestScore = 0;
    public string Name;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadScore();
    }
    
    [System.Serializable]
    class SaveData
    {
        public int BestScore;
        public string Name;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.BestScore = BestScore;
        data.Name = Name;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            BestScore = data.BestScore;
            Name = data.Name;
        }
    }

    public string ShowBestText()
    {
        return $"Best Score: {Name}: {BestScore}";
    }
}
