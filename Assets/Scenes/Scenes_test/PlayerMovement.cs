using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator; // Animatorコンポーネントをドラッグ＆ドロップで割り当てます。
    public float movementThreshold = 0.1f; // スティックの入力しきい値
    private SpriteRenderer renderer;


void Start(){
    animator = GetComponent<Animator>();
    renderer = GetComponent<SpriteRenderer>();
}

void Update()
{
     var gamepad = Gamepad.all[0]; // 1P用のゲームパッドを取得
    if (gamepad == null) return; // ゲームパッドが接続されていない場合は何もしない
    // スプライトのスケール値を取得
    Vector2 position = transform.position;

    // 方向キーの横入力で左右移動
    float moveInput = gamepad.dpad.x.ReadValue();
    
    if(moveInput==-1){
        animator.SetBool("isWalking", true);
        renderer.flipX = false;
    }
    else if(moveInput==1){
        animator.SetBool("isWalking",true);
        renderer.flipX = true;
        Debug.Log("true");
    }
    else{
        animator.SetBool("isWalking",false);
    }
    transform.position = position;
}

}
