using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ready : MonoBehaviour
{
    public AudioSource audioSource;
    public MonoBehaviour player1ControlScript; // 1Pプレイヤーの制御スクリプト
    public MonoBehaviour player2ControlScript; // 2Pプレイヤーの制御スクリプト
    private List<Rigidbody2D> allRigidbodies;

    void Start()
    {
        // Scene内のすべてのRigidbody2Dを取得
        allRigidbodies = new List<Rigidbody2D>(FindObjectsOfType<Rigidbody2D>());

        // 再生開始時に動作を停止
        StartCoroutine(PauseObjectsDuringAudio());
    }

    private IEnumerator PauseObjectsDuringAudio()
    {
        // AudioSourceが再生されるまで待つ
        yield return new WaitUntil(() => audioSource.isPlaying);

        // すべてのオブジェクトの動きを停止
        foreach (var rb in allRigidbodies)
        {
            rb.velocity = Vector2.zero;
            rb.simulated = false; // Rigidbody2Dの物理演算を停止
        }

        // 1Pと2Pの操作スクリプトを無効化
        if (player1ControlScript != null) player1ControlScript.enabled = false;
        if (player2ControlScript != null) player2ControlScript.enabled = false;

        // 再生開始から3秒待つ
        yield return new WaitForSeconds(2.5f);

        // すべてのオブジェクトの動きを再開
        foreach (var rb in allRigidbodies)
        {
            rb.simulated = true; // Rigidbody2Dの物理演算を再開
        }

        // 1Pと2Pの操作スクリプトを有効化
        if (player1ControlScript != null) player1ControlScript.enabled = true;
        if (player2ControlScript != null) player2ControlScript.enabled = true;
    }
}
