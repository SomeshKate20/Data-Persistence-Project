using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartMenu : MonoBehaviour
{
    public TMP_InputField nameInput;

    public void StartGame()
    {
        GameData.PlayerName = nameInput.text;
        SceneManager.LoadScene("MainGame");
    }
}