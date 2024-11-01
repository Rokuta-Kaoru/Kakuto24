using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tumi_bousi : MonoBehaviour
{
    // 外に押し出す距離
    public float pushBackDistance = 1f;

    // 衝突判定用のメソッド
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 貫通して入ったオブジェクトに対して
        if (collision.gameObject.CompareTag("Pushable")) // "Pushable"タグのオブジェクトのみ対象
        {
            // 押し戻す方向ベクトルを計算
            Vector2 pushDirection = collision.transform.position - transform.position;
            pushDirection.Normalize();

            // 押し戻した新しい位置を計算
            Vector2 newPosition = (Vector2)transform.position + pushDirection * pushBackDistance;

            // オブジェクトを新しい位置に設定
            collision.transform.position = newPosition;
        }
    }
}
