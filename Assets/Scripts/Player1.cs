using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player1 : MonoBehaviour
{

    public float speed = 5.0f;
    public float slideSpeed = 2.0f;
    public int jumpCount = 1;

    int defaultJumpCount;

    //アニメーション
    Animator animator;
    //UIを管理するスクリプト
    UIManager uiscript;
    //上半身のコライダー用
    GameObject headCollider;

    Rigidbody playerRigidbody;

    //無限生成用
    public GameObject[] stages;
    Vector3 generatingPosition;
    int stageCount = 3;
    int nextPosition = 0;
    bool isGenerated = false;


    void Start()
    {
        //変数に必要なデータを格納
        animator = GetComponent<Animator>();
        uiscript = GameObject.Find("Canvas").GetComponent<UIManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        headCollider = GameObject.Find("HeadCollider");

        //設定したジャンプできる回数を保存
        defaultJumpCount = jumpCount;

    }



    void Update()
    {
        /*
        //ストップ
        if (Input.GetKey(KeyCode.Space))
        {
            playerRigidbody.velocity = new Vector3(0, 0, 0);
            animator.SetBool("Pose", true);
            playerRigidbody.isKinematic = true;
        }

        
        if (!Input.GetKey(KeyCode.Space))
        {
            playerRigidbody.isKinematic = false;
            animator.SetBool("Pose", false);
            //前に進む
            transform.position += new Vector3(0, 0, speed) * Time.deltaTime;
        }
        */

        //前に進む
        transform.position += new Vector3(0, 0, speed) * Time.deltaTime;

        //現在のX軸の位置を取得
        float posX = transform.position.x;

        //右アローキーを押した時
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (posX < 1.8f)
            {
                transform.position += new Vector3(slideSpeed, 0, 0) * Time.deltaTime;
            }
        }

        //左アローキーを押した時
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (posX > -1.8f)
            {
                transform.position -= new Vector3(slideSpeed, 0, 0) * Time.deltaTime;
            }
        }

        

        //ジャンプ
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCount > 0)
        {
            //速度を初期化
            playerRigidbody.velocity = new Vector3(0, 0, 0);

            //上方向に力を加える
            playerRigidbody.AddForce(new Vector3(0, 6, 0), ForceMode.Impulse);


            //残りのジャンプ回数を減らす
            jumpCount--;
        }



    }

    // checkにぶつかったとき
    void OnTriggerExit(Collider collider)
    {

        if(collider.gameObject.tag == "Check")
        {
            Debug.Log("Generate!");
            int type = Random.Range(0, 3);
            nextPosition = 15 * stageCount;

            generatingPosition = new Vector3(0, 0, nextPosition);
            Debug.Log(nextPosition);
            Instantiate(stages[type], generatingPosition, Quaternion.identity);

            stageCount++;
            //isGenerated = true;
        }
    }


    //Triggerでない障害物にぶつかったとき
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Barrier")
        {
            //速度を0にして進むのを止める
            speed = 0;

            //横移動する速度を0にして左右移動できなくする
            slideSpeed = 0;

            uiscript.Gameover();
        }

        if (collision.gameObject.tag == "Ground")
        {
            jumpCount = defaultJumpCount;
        }
    }

}

