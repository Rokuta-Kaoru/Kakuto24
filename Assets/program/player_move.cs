using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // 新しいInput Systemを使用するために追加

public class player_move : MonoBehaviour
{
    Rigidbody2D rb;
    float speed = 10.0f;
    float jumpForce = 8.0f;

    public GameObject huttobasi;
    public GameObject behind_blue; // behind_blue オブジェクトを追加
    private bool canUseWKey = true; // Wキーの使用可否を管理
    private bool canUseRBButton = true; // RBボタンの使用可否を管理
    private int jumpCount = 0;      // 現在のジャンプ回数
    private int maxJumps = 2;       // 最大ジャンプ回数（地上1回 + 空中1回）

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // ゲーム開始時に「huttobasi」と「behind_blue」を非表示にする
        if (huttobasi != null)
        {
            huttobasi.SetActive(false);
        }
        if (behind_blue != null)
        {
            behind_blue.SetActive(false);
        }
    }

    void Update()
    {
        var gamepad = Gamepad.all[0]; // 1P用のゲームパッドを取得
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

        // RBボタンで「behind_blue」オブジェクトを一時表示
        if (gamepad.rightShoulder.wasPressedThisFrame && canUseRBButton && behind_blue != null)
        {
            StartCoroutine(ShowBehindBlueTemporarily());
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

IEnumerator ShowBehindBlueTemporarily()
{
    canUseRBButton = false; // RBボタンを一時的に無効化

    // プレイヤーの進行方向に応じたオフセットを計算
    Vector3 offset = rb.velocity.normalized * 1.27f;

    // 進行方向に基づき回転角度を設定
    float rotationZ = 0f;
    if (Mathf.Abs(rb.velocity.x) > Mathf.Abs(rb.velocity.y))
    {
        // 左右方向に進んでいる場合、z軸回転を-90度に
        rotationZ = -90f;
    }
    else
    {
        // 上下方向に進んでいる場合、z軸回転を0度に
        rotationZ = 0f;
    }

    // behind_blueの位置と回転を設定し表示
    behind_blue.transform.position = transform.position + offset;
    behind_blue.transform.rotation = Quaternion.Euler(0, 0, rotationZ);
    behind_blue.SetActive(true);

    yield return new WaitForSeconds(1.5f); // 1.5秒待機
    behind_blue.SetActive(false); // 「behind_blue」を非表示

    yield return new WaitForSeconds(1.0f); // クールダウン時間（1.0秒）
    canUseRBButton = true; // RBボタンを再度有効化
}
}
