using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HokoraManager : MonoBehaviour
{
    public int hitCountToDestroy = 10;     // 消滅するまでのヒット回数
    private int currentHitCount = 0;      // 現在のヒット回数
    void Start(){

    }

    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision){
        // ターゲットのタグと一致するか確認
        if (collision.gameObject.CompareTag("Player"))
        {
            currentHitCount++;
            Debug.Log("Hit detected. Current hit count: " + currentHitCount);

            // ヒット回数が指定数に達したらオブジェクトを消滅
            if (currentHitCount >= hitCountToDestroy)
            {
                Destroy(gameObject);
            }
        }
    }
}
