using UnityEngine;
using UnityEngine.SceneManagement;

public class ReStartButton : MonoBehaviour
{
    public void BackStartScene()
    {
        // 独自でSceneManager.csを作ってしまったので、以下のようにして読み込んでいる
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartScene");
    }
}
