using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_move : MonoBehaviour
{
    Rigidbody2D rb;
    float speed = 5.0f;
    float jumpForce = 5.0f;

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
        // 横移動（左右キー）
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        // ジャンプ（スペースキー） - ジャンプ回数が最大回数未満の場合のみジャンプ
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        {
            //rb.velocity = new Vector2(rb.velocity.x, 0); // Y方向の速度をリセットしてジャンプ
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jumpCount++;
        }

        // Wキーで「huttobasi」オブジェクトを一時表示
        if (Input.GetKeyDown(KeyCode.W) && canUseWKey && huttobasi != null)
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
        yield return new WaitForSeconds(0.5f); // 0.5秒待機
        huttobasi.SetActive(false); // 「huttobasi」を非表示

        yield return new WaitForSeconds(1.0f); // クールダウン時間（1.5秒）
        canUseWKey = true; // Wキーを再度有効化
    }
}
