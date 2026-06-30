using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static string PlayerName = "";
    public static int HighScore = 0;

    void Start()
    {
        LoadHighScore();
    }

    public static void SaveHighScore(int score)
    {
        if (score > HighScore)
        {
            HighScore = score;

            PlayerPrefs.SetInt("HighScore", HighScore);
            PlayerPrefs.SetString("HighScorePlayer", PlayerName);
            PlayerPrefs.Save();
        }
    }

    public void LoadHighScore()
    {
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public static string GetHighScorePlayer()
    {
        return PlayerPrefs.GetString("HighScorePlayer", "None");
    }
}