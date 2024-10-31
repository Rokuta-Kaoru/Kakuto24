using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverChecker : MonoBehaviour
{
    public float overXmax=13f;
    public float overYmax=6f;
    public float overXmin=-13f;
    public float overYmin=-6f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     if(transform.position.x>overXmax||transform.position.y>overYmax||transform.position.x<overXmin||transform.position.y<overYmin){
        SceneManager.LoadScene("ResultScene");
     }   
    }
}
