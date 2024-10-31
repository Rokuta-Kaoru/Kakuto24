using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video; // VideoPlayerを使用するために追加

public class GameOverChecker : MonoBehaviour
{
    public float overXmax = 13f;
    public float overYmax = 6f;
    public float overXmin = -13f;
    public float overYmin = -6f;

    public GameObject gekitui_player; // gekitui_player オブジェクトを追加
    public VideoPlayer videoPlayer; // VideoPlayer を追加

    private bool isGameOver = false; // ゲームオーバー状態を管理

    void Start()
    {
        if (gekitui_player != null)
        {
            gekitui_player.SetActive(false); // 初期状態で非表示
        }
        if (videoPlayer != null)
        {
            videoPlayer.Stop(); // 初期状態で再生を停止
            videoPlayer.loopPointReached += OnVideoEnd; // ビデオ終了時に呼び出されるイベントを追加
        }
    }

    void Update()
    {
        if (!isGameOver && IsOutOfBounds())
        {
            isGameOver = true; // ゲームオーバー状態に変更

            // ボーダーを超えた位置と回転を設定してgekitui_playerを表示
            SetGekituiPlayerPositionAndRotation();

            // VideoPlayerを再生
            if (videoPlayer != null)
            {
                videoPlayer.Play();
            }
        }
    }

    // ボーダーを超えた位置と回転を設定するメソッド
    void SetGekituiPlayerPositionAndRotation()
    {
        if (gekitui_player == null) return;

        Vector3 newPosition = transform.position;
        float rotationZ = 0f;

        // 右のボーダーを超えた場合
        if (transform.position.x > overXmax)
        {
            newPosition = new Vector3(4.42f, -0.4f, 0f);
            rotationZ = 180f;
        }
        // 左のボーダーを超えた場合
        else if (transform.position.x < overXmin)
        {
            newPosition = new Vector3(-3.88f, -0.4f, 0f);
            rotationZ = 180f;
        }
        // 上のボーダーを超えた場合
        else if (transform.position.y > overYmax)
        {
            newPosition = new Vector3(transform.position.x, -0.4f, 0f);
            rotationZ = 90f;
        }
        // 下のボーダーを超えた場合
        else if (transform.position.y < overYmin)
        {
            newPosition = new Vector3(transform.position.x, -0.4f, 0f);
            rotationZ = -90f;
        }

        gekitui_player.transform.position = newPosition;
        gekitui_player.transform.rotation = Quaternion.Euler(0, 0, rotationZ);
        gekitui_player.SetActive(true); // gekitui_playerを表示
    }

    // ビデオが終了したら呼び出されるメソッド
    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene("ResultScene");
    }

    // ボーダー範囲外かを判定するメソッド
    bool IsOutOfBounds()
    {
        return transform.position.x > overXmax || transform.position.y > overYmax ||
               transform.position.x < overXmin || transform.position.y < overYmin;
    }
}
