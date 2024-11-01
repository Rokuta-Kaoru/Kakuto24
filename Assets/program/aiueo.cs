using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video; // VideoPlayerを使用するために追加

public class aiueo : MonoBehaviour
{
    public GameObject gekitui_player; // gekitui_player オブジェクトを追加
    public VideoPlayer videoPlayer; // VideoPlayer を追加

    private bool isGameOver = false; // ゲームオーバー状態を管理

    void Start()
    {
            videoPlayer.Play(); // 初期状態で再生を停止
            videoPlayer.loopPointReached += OnVideoEnd; // ビデオ終了時に呼び出されるイベントを追加
    }
    // ビデオが終了したら呼び出されるメソッド
    void OnVideoEnd(VideoPlayer vp)
    {
        gameObject.SetActive (false);
    }

}
