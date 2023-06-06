using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float Pspeed = 5.0f;
    [SerializeField] public float slideSpeed = 2.0f;
    [SerializeField] public int jumpCount = 1;

    int defaultJumpCount;

    //アニメーション
    Animator animator;
    //UIを管理するスクリプト
    UIManager uiscript;
    //上半身のコライダー用
    GameObject headCollider;

    Rigidbody playerRigidbody;

    //無限生成用
    //public GameObject[] stages;
    Vector3 generatingPosition;
    int nextPosition = 0;
    public static float score = 0;

    //アイテム
    private float timeCounter = 0f;
    bool doublePoints = false;
    bool transparent = false;
    public static bool timeStop = false;
    public static bool isAnimated = true;
    int unit = 1;


    void Start()
    {
        //変数に必要なデータを格納
        animator = GetComponent<Animator>();
        uiscript = GameObject.Find("Canvas").GetComponent<UIManager>();
        playerRigidbody = GetComponent<Rigidbody>();

        //headCollider = GameObject.Find("HeadCollider");


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
            playerRigidbody.AddForce(new Vector3(0, 7, 0), ForceMode.Impulse);

            //ジャンプするアニメーションを再生
            animator.SetTrigger("Jump");

            //残りのジャンプ回数を減らす
            jumpCount--;
        }


        //スライディングしていたら頭の判定をなくす
        /*
        if (isSlide == true)
        {
            headCollider.SetActive(false);
        }
        else
        {
            headCollider.SetActive(true);
        }
        */

        //落下時のGameOver判定
        if (transform.position.y <= -3)
        {
            uiscript.Gameover();
            animator.SetBool("Dead", true);
        }


        //アイテム
        //スコア倍増
        if(doublePoints == true)
        {
            timeCounter += Time.deltaTime;
            if(timeCounter < 10)
            {
                unit = 2;
            }
            else
            {
                unit = 1;
                doublePoints = false;
                timeCounter = 0;
            }
        }

        //壁すり抜け
        if(transparent == true)
        {
            timeCounter += Time.deltaTime;
            if(timeCounter < 10)
            {
                playerRigidbody.isKinematic = true;
            }
            else
            {
                playerRigidbody.isKinematic = false;
                transparent = false;
                timeCounter = 0;
            }
        }

        //金縛り
        if (timeStop == true)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter <= 10)
            {
                //Debug.Log(timeCounter);
                isAnimated = false;
            }
            else if (timeCounter > 10)
            {
                Debug.Log("else");
                timeStop = false;
                isAnimated = true;
                timeCounter = 0;
            }
        }

        //お化け界にいる時のスコア
        if (posX > 30f && score >= 0)
        {
            score -= Time.deltaTime;
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit Item!");
        if(other.gameObject.name == "DoublePointsItem" || other.gameObject.name == "DoublePointsItem(Clone)")
        {
            Debug.Log("Hit Item1!");
            doublePoints = true;
        }

        if(other.gameObject.name == "TransparentItem" || other.gameObject.name == "TransparentItem(Clone)")
        {
            Debug.Log("Hit Item2!");
            transparent = true;
        }

        if(other.gameObject.name == "TimeStopItem" || other.gameObject.name == "TimeStopItem(Clone)")
        {
            Debug.Log("Hit Item3!");
            timeStop = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {

        if(collider.gameObject.tag == "Human")
        {
            Destroy(collider.gameObject);
            score += unit;
        }

        if (collider.gameObject.tag == "Check")
        {
            StageGenerator._check = true;
            
        }
    }


    //Triggerでない障害物にぶつかったとき
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Barrier")
        {
            //速度を0にして進むのを止める
            Pspeed = 0;

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

