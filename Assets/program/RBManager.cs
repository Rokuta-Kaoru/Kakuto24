using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBManager : MonoBehaviour
{
    public string targetTag1 = "Blue";
    public string targetTag2 = "Red";
    public float interval = 2f;
    private bool isTag1Active = false;

    // オブジェクトをキャッシュするリスト
    private List<GameObject> objectsTag1 = new List<GameObject>();
    private List<GameObject> objectsTag2 = new List<GameObject>();

void Start()
{
    // 初期状態でオブジェクトをリストに保存
    objectsTag1.AddRange(GameObject.FindGameObjectsWithTag(targetTag1));
    objectsTag2.AddRange(GameObject.FindGameObjectsWithTag(targetTag2));

    // デバッグログでリストの要素数を確認
    Debug.Log("objectsTag1 count: " + objectsTag1.Count);
    Debug.Log("objectsTag2 count: " + objectsTag2.Count);

    // 最初の状態を設定（例：tag1を表示、tag2を非表示）
    SetActiveForList(objectsTag1, true);
    SetActiveForList(objectsTag2, false);

    // タグ切り替えを一定時間ごとに繰り返す
    StartCoroutine(ToggleTagsRoutine());
}


    private IEnumerator ToggleTagsRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            // 現在の状態を基に表示・非表示を切り替え
            isTag1Active = !isTag1Active;

            // リストを使ってアクティブ・非アクティブを設定
            SetActiveForList(objectsTag1, isTag1Active);
            SetActiveForList(objectsTag2, !isTag1Active);
        }
    }

    private void SetActiveForList(List<GameObject> objects, bool isActive)
    {
        foreach (GameObject obj in objects)
        {
            if (obj != null)
            {
                obj.SetActive(isActive);
            }
        }
    }
}
