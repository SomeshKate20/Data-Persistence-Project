using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public TMP_Text ScoreText;
    public TMP_Text PlayerNameText;
    public TMP_Text HighScoreText;

    public GameObject GameOverText;

    private bool m_Started = false;
    private int m_Points;
    private bool m_GameOver = false;

    private InputAction m_LaunchAction;

    void Awake()
    {
        m_LaunchAction = new InputAction("Launch", InputActionType.Button, "<Keyboard>/space");
    }

    void OnEnable()
    {
        m_LaunchAction.Enable();
    }

    void OnDisable()
    {
        m_LaunchAction.Disable();
    }

    void Start()
    {
        // Show player name
        PlayerNameText.text = "Player: " + GameData.PlayerName;

        // Load saved high score
        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);
        string savedPlayer = PlayerPrefs.GetString("HighScorePlayer");

        HighScoreText.text = "High Score: " + savedHighScore;

        // Create bricks
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };

        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);

                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (m_LaunchAction.WasPressedThisFrame())
            {
                m_Started = true;

                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (m_LaunchAction.WasPressedThisFrame())
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = "Score : " + m_Points;

        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (m_Points > highScore)
        {
            PlayerPrefs.SetInt("HighScore", m_Points);
            PlayerPrefs.SetString("HighScorePlayer", GameData.PlayerName);
            PlayerPrefs.Save();

            HighScoreText.text = "High Score: " + m_Points;
        }
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }
}
