using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATField: MonoBehaviour
{
    public float pushForce = 10.0f;  // 吹き飛ばす力

    // 他のオブジェクトが接触したときに実行される
    void OnTriggerEnter2D(Collider2D other)
    {
        // Rigidbody2Dを持つオブジェクトのみを対象にする
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // 吹き飛ばす方向を計算
            Vector2 pushDirection = (other.transform.position - transform.position).normalized;

            // Rigidbody2Dに力を加えて吹き飛ばす
            rb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
        }
    }
}
