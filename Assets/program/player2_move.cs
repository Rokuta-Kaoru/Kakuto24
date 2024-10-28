using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // 新しいInput Systemを使用するために追加

public class player2_move : MonoBehaviour
{
    Rigidbody2D rb;
    float speed = 10.0f;
    float jumpForce = 8.0f;

    public GameObject huttobasi;
    private bool canUseWKey = true; // Wキーの使用可否を管理
    private int jumpCount = 0;      // 現在のジャンプ回数
    private int maxJumps = 2;       // 最大ジャンプ回数（地上1回 + 空中1回）

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // ゲーム開始時に「huttobasi」を非表示にする
        if (huttobasi != null)
        {
            huttobasi.SetActive(false);
        }
    }

    void Update()
    {
        var gamepad = Gamepad.all[1]; // 2P用のゲームパッドを取得
        if (gamepad == null) return; // ゲームパッドが接続されていない場合は何もしない

        // 方向キーの横入力で左右移動
        float moveInput = gamepad.dpad.x.ReadValue();
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // ジャンプ（Xボタン） - ジャンプ回数が最大回数未満の場合のみジャンプ
        if (gamepad.buttonWest.wasPressedThisFrame && jumpCount < maxJumps)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jumpCount++;
        }

        // Bボタンで「huttobasi」オブジェクトを一時表示
        if (gamepad.buttonNorth.wasPressedThisFrame && canUseWKey && huttobasi != null)
        {
            StartCoroutine(ShowHuttobasiTemporarily());
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 地面に接触した場合、ジャンプ回数をリセット
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
        }
    }

    IEnumerator ShowHuttobasiTemporarily()
    {
        canUseWKey = false; // Wキーを一時的に無効化

        huttobasi.SetActive(true); // 「huttobasi」を表示
        yield return new WaitForSeconds(0.2f); // 0.5秒待機
        huttobasi.SetActive(false); // 「huttobasi」を非表示

        yield return new WaitForSeconds(1.0f); // クールダウン時間（1.0秒）
        canUseWKey = true; // Wキーを再度有効化
    }
}
