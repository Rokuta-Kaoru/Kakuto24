using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // SceneManagerを使用するために必要

public class systems : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // エスケープキーが押されたら
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // タイトル画面に戻る（ここでは"TitleScene"というシーンに戻る例）
            SceneManager.LoadScene("TitleScene");
        }
    }
}
