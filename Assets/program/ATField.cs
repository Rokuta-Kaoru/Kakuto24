using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATField : MonoBehaviour
{
    public float pushForce = 20.0f;  // 吹き飛ばす力
    public List<GameObject> excludedObjects; // 除外するオブジェクトのリスト
    public float detectionRadius = 5.0f; // 範囲半径

    void Update()
    {
        // 範囲内のオブジェクトを取得
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

        foreach (Collider2D collider in colliders)
        {
            // 除外するオブジェクトがリストに含まれているか確認
            if (excludedObjects.Contains(collider.gameObject))
            {
                continue; // 除外するオブジェクトの場合はスキップ
            }

            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // 吹き飛ばす方向を計算
                Vector2 pushDirection = (collider.transform.position - transform.position).normalized;

                // 吹き飛ばす力を適用
                rb.velocity = Vector2.zero; // 現在の速度をリセット
                rb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);

                // デバッグログを表示
                Debug.Log($"Pushed {collider.name} in direction {pushDirection} with force {pushForce}");
            }
        }
    }

    // 範囲の可視化（オプション）
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
