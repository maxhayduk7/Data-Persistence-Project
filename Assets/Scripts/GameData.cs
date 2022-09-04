using System.IO;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }

    private const string DefaultName = "Player";

    private readonly SaveData data = new SaveData { Name = DefaultName };
    private string currentName;
    private int score;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadSessionData();
    }

    #region Score

    public void SetScore(int score) => this.score = score;

    public int GetScore() => score;

    #endregion

    #region SaveData

    public void SetBestScore()
     {
        if (IsNewBestScore())
        {
            this.data.BestScore = this.score;
            SaveSessionData();
        }
    }

    public int GetBestScore() => this.data.BestScore;

    public string GetBestScoreText() => $"Best Score: {this.data.Name}: {this.data.BestScore}";

    public void SetName(string name) => this.currentName = string.IsNullOrWhiteSpace(name)? DefaultName : name;

    #endregion

    private void SaveSessionData()
    {
        this.data.Name = this.currentName;
        string json = JsonUtility.ToJson(this.data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    private void LoadSessionData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            this.data.Name = data.Name;
            this.data.BestScore = data.BestScore;
        }
    }

    private bool IsNewBestScore() => this.data.BestScore < this.score;

    [System.Serializable]
    class SaveData
    {
        public string Name;
        public int BestScore;
    }
}
