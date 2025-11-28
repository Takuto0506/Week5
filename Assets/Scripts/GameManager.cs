using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject goalUI;   // ← ゴールメッセージUI

    void Awake()
    {
        instance = this;
    }

    public void Goal()
    {
        // ゴールUIを表示
        if (goalUI != null)
            goalUI.SetActive(true);

        // 必要ならゲーム停止
        Time.timeScale = 0f;
    }
}
