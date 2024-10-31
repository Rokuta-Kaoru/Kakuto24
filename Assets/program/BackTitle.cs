using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class BackTitle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frames
    void Update()
    {
        var gamepad = Gamepad.all[0]; // 1P用のゲームパッドを取得
        if (gamepad == null) return; // ゲームパッドが接続されていない場合は何もしない
        if(gamepad.buttonWest.wasPressedThisFrame)
        {
            SceneManager.LoadScene("TitleScene");
        }

    }
}
