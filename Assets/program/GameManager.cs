using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform player1; // プレイヤー1のTransform
    public Transform player2; // プレイヤー2のTransform

    // 範囲外とみなされる範囲
    public float overXmax=13f;
    public float overYmax=6f;
    public float overXmin=-13f;
    public float overYmin=-6f;

    public Text resultText; // 結果表示用のUI Text

    private void Update()
    {
        CheckOutOfBound();
    }

    private void CheckOutOfBound()
    {
        // プレイヤー1の位置チェック
        if (Mathf.Abs(player1.position.x)>overXmax||Mathf.Abs(player1.position.y)>overYmax||Mathf.Abs(player1.position.x)<overXmin||Mathf.Abs(player1.position.y)<overYmin)
        {
            // プレイヤー1が範囲外の場合
            SceneManager.LoadScene("2PWinScene");
        }

        // プレイヤー2の位置チェック
        if (Mathf.Abs(player2.position.x)>overXmax||Mathf.Abs(player2.position.y)>overYmax||Mathf.Abs(player2.position.x)<overXmin||Mathf.Abs(player2.position.y)<overYmin)
        {
            // プレイヤー2が範囲外の場合
            SceneManager.LoadScene("1PWinScene");
        }
    }
}
