using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    private static string selectedMode = "Normal";

    public void StartGame(string mode)
    {
        if (mode == "Normal" || mode == "Hard")
        {
            selectedMode = mode;
        }
        else
        {
            Debug.LogError("Invalid mode argument");
            return;
        }

        // 独自でSceneManager.csを作ってしまったので、以下のようにして読み込んでいる
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }

    public static string GetSelectedMode()
    {
        return selectedMode;
    }
}
