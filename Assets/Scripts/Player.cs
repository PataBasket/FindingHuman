﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
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
        float posZ = transform.position.z;

        //右アローキーを押した時
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (posX < 1.8f)
            {
                transform.position += new Vector3(slideSpeed, 0, 0) * Time.deltaTime;
            }

            else if(posX > 30f && posX < 61.8f)
            {
                transform.position += new Vector3(slideSpeed, 0, 0) * Time.deltaTime;
            }
        }

        //左アローキーを押した時
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (posX > -1.8f && posX < 30f)
            {
                transform.position -= new Vector3(slideSpeed, 0, 0) * Time.deltaTime;
            }

            else if (posX > 58.2f)
            {
                transform.position -= new Vector3(slideSpeed, 0, 0) * Time.deltaTime;
            }
        }

        //瞬間移動
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (posX < 30f)
            {
                transform.position = new Vector3(posX + 60, 0, posZ);
            }

            else if(posX > 30f)
            {
                transform.position = new Vector3(posX - 60, 0, posZ);
            }
        }

        //アニメーション
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            animator.SetBool("Slide", true);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            animator.SetBool("Slide", false);
        }

        //現在再生されているアニメーション情報を取得
        var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        //取得したアニメーションの名前が一致指定ればtrue
        bool isJump = stateInfo.IsName("Base Layer.Jump");
        bool isSlide = stateInfo.IsName("Base Layer.Slide");

        //ジャンプ
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCount > 0)
        {
            //速度を初期化
            playerRigidbody.velocity = new Vector3(0, 0, 0);

            //上方向に力を加える
            playerRigidbody.AddForce(new Vector3(0, 6, 0), ForceMode.Impulse);

            //ジャンプするアニメーションを再生
            animator.SetTrigger("Jump");

            //残りのジャンプ回数を減らす
            jumpCount--;
        }


        //スライディングしていたら頭の判定をなくす
        if (isSlide == true)
        {
            headCollider.SetActive(false);
        }
        else
        {
            headCollider.SetActive(true);
        }

        //落下時のGameOver判定
        if (transform.position.y <= -3)
        {
            uiscript.Gameover();
            animator.SetBool("Dead", true);
        }


    }

    // checkにぶつかったとき
    /*
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
    */


    //Triggerでない障害物にぶつかったとき
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Barrier")
        {
            //速度を0にして進むのを止める
            speed = 0;

            //横移動する速度を0にして左右移動できなくする
            slideSpeed = 0;

            animator.SetBool("Dead", true);
            //UIの表示
            uiscript.Gameover();
        }

        if (collision.gameObject.tag == "Ground")
        {
            jumpCount = defaultJumpCount;
        }
    }

}

