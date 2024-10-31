using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SelManagerScript : MonoBehaviour
{
    //イメージにアタッチしたアニメーターコントローラ
    [SerializeField]
    private Animator[] animator;

    //連続入力回避用フラグ
    private bool isSel;
    //選択中番号
    private int selCnt;

    private void Start()
    {
        //変数初期化
        selCnt = 0;
        animator[selCnt].SetBool("isSel", true);
    }

    private void Update()
    {
        //ゲームパッド設定以下
        var gamepad = Gamepad.all[0]; // 1P用のゲームパッドを取得
        if (gamepad == null) return; // ゲームパッドが接続されていない場合は何もしない
        // 方向キーの横入力で左右移動
        float moveInput = gamepad.dpad.x.ReadValue();

        //float input = Input.GetAxis("Vertical");
        //Debug.Log($"Input: {input}, selCnt: {selCnt}, isSel: {isSel}");

        // スティックの入力チェック
        if (moveInput >= 0.5f && !isSel)
        {
            isSel = true;
            ChangeSelection(1);
        }
        else if (moveInput <= -0.5f && !isSel)
        {
            isSel = true;
            ChangeSelection(-1);
        }

        // 長押し回避
        if (moveInput >= -0.1f && moveInput <= 0.1f)
        {
            isSel = false;
        }

        // 決定
        if (gamepad.buttonWest.wasPressedThisFrame)
        {
            switch (selCnt)
            {
                case 0:
                    Debug.Log("０を選択");
                    SceneManager.LoadScene("SampleScene"); // シーン名を指定
                    break;
                case 1:
                    Debug.Log("１を選択");
                    SceneManager.LoadScene("Stage2Scene"); // シーン名を指定
                    break;
                case 2:
                    Debug.Log("２を選択");
                    SceneManager.LoadScene("Stage3Scene"); // シーン名を指定
                    break;
                default:
                    break;
            }
        }
    }
private void ChangeSelection(int direction)
{
    // 現在の選択を解除
    if (animator.Length > 0)
    {
        animator[selCnt].SetBool("isSel", false); // 現在の選択を非選択にする
    }

    // 新しい選択を更新
    selCnt += direction;

    // サイクルするための範囲チェック
    if (selCnt >= animator.Length)
    {
        selCnt = 0;  // 循環
    }
    else if (selCnt < 0)
    {
        selCnt = animator.Length - 1;  // 循環
    }

    // 新しい選択を設定
    if (animator.Length > 0)
    {
        animator[selCnt].SetBool("isSel", true); // 新しい選択を選択状態にする
    }
}


}