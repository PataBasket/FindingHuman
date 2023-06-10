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
    private float timeCounter_doubleScore = 10f;
    private float timeCounter_transparent = 10f;
    private float timeCounter_timeStop = 10f;
    private bool doublePoints = false;
    private bool transparent = false;
    private bool timeStop = false;

    public static bool isAnimated = true;
    private int unit = 1;

    [SerializeField] private Slider _itemSlider;
    [SerializeField] private GameObject itemSliderPosition;
    [SerializeField] private Camera targetLCamera;
    [SerializeField] private Camera targetRCamera;

    //やられた時の音
    [SerializeField] private AudioClip[] screamAudios;
    [SerializeField] private AudioClip teleportAudio;
    private AudioSource audioSource;
    private int i;


    void Start()
    {
        //変数に必要なデータを格納
        animator = GetComponent<Animator>();
        uiscript = GameObject.Find("Canvas").GetComponent<UIManager>();
        playerRigidbody = GetComponent<Rigidbody>();


        //設定したジャンプできる回数を保存
        defaultJumpCount = jumpCount;

        audioSource = this.GetComponent<AudioSource>();

        
    }



    void Update()
    {
        
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

            audioSource.PlayOneShot(teleportAudio);
        }

        //アニメーション



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
            playerRigidbody.AddForce(new Vector3(0, 6.2f, 0), ForceMode.Impulse);

            
            //残りのジャンプ回数を減らす
            jumpCount--;
        }


        

        //落下時のGameOver判定
        if (transform.position.y <= -3)
        {
            uiscript.Gameover();
            animator.SetBool("Dead", true);
        }


        //アイテム
        /*
        var sliderWorldPos = this.transform.position;
        
        if(posX < 30)
        {
            var sliderScreenPos = targetLCamera.WorldToScreenPoint(sliderWorldPos) + new Vector3(0, 1, 0);
        }
        
        else if(posX >= 30)
        {
            var sliderScreenPos = targetRCamera.WorldToScreenPoint(sliderWorldPos) + new Vector3(0, 1, 0);
        }
        */
        

        //スコア倍増
        if (doublePoints == true)
        {
            //他のアイテムをリセット
            if(timeCounter_doubleScore > timeCounter_transparent)
            {
                timeCounter_transparent = 0;
            }
            if(timeCounter_doubleScore > timeCounter_timeStop)
            {
                timeCounter_timeStop = 0;
            }
            
            timeCounter_doubleScore -= Time.deltaTime;
            if(timeCounter_doubleScore > 0)
            {
                unit = 2;
                _itemSlider.value = timeCounter_doubleScore/10;
                
            }
            else if (timeCounter_doubleScore <= 0)
            {
                _itemSlider.value = 100;
                unit = 1;
                doublePoints = false;
                timeCounter_doubleScore = 10f;
                
            }
        }
        else
        {
            timeCounter_doubleScore = 10f;
        }

        //壁すり抜け
        if(transparent == true)
        {
            //他のアイテムをリセット
            if(timeCounter_transparent > timeCounter_timeStop)
            {
                timeCounter_timeStop = 0;
            }
            if(timeCounter_transparent > timeCounter_doubleScore)
            {
                timeCounter_doubleScore = 0;
            }
            
            timeCounter_transparent -= Time.deltaTime;
            if(timeCounter_transparent > 0)
            {
                playerRigidbody.isKinematic = true;
                _itemSlider.value = timeCounter_transparent / 10;
                
            }
            else if (timeCounter_transparent <= 0)
            {
                _itemSlider.value = 100;
                playerRigidbody.isKinematic = false;
                transparent = false;
                timeCounter_transparent = 10f;
                
            }
        }
        else
        {
            timeCounter_transparent = 10f;
        }

        //金縛り
        if (timeStop == true)
        {
            //他のアイテムをリセット
            if(timeCounter_timeStop > timeCounter_transparent)
            {
                timeCounter_transparent = 0;
            }
            if (timeCounter_timeStop > timeCounter_doubleScore)
            {
                timeCounter_doubleScore = 0;
            }

            

            timeCounter_timeStop -= Time.deltaTime;
            if (timeCounter_timeStop > 0)
            {
                isAnimated = false;
                _itemSlider.value = timeCounter_timeStop / 10;
               
            }
            else if (timeCounter_timeStop <= 0)
            {
                _itemSlider.value = 1;
                timeStop = false;
                isAnimated = true;
                timeCounter_timeStop = 10f;
            }
        }
        else
        {
            timeCounter_timeStop = 10f;
        }

        //お化け界にいる時のスコア
        if (posX > 30f && score >= 0)
        {
            score -= Time.deltaTime*0.3f;
        }

    }


    private void OnTriggerEnter(Collider collider)
    {
        //アイテムとぶつかったとき
        if(collider.gameObject.tag == "doublePointsItem")
        {
            Debug.Log("Hit Item1!");
            doublePoints = true;
            timeCounter_doubleScore = 10f;
            Destroy(collider.gameObject);
        }

        if(collider.gameObject.tag == "transparentItem")
        {
            Debug.Log("Hit Item2!");
            transparent = true;
            timeCounter_transparent = 10f;
            Destroy(collider.gameObject);
        }

        if(collider.gameObject.tag == "timeStopItem")
        {
            Debug.Log("Hit Item3!");
            timeStop = true;
            timeCounter_timeStop = 10f;

            Destroy(collider.gameObject);
        }

        
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Check")
        {
            StageGenerator._check = true;
            
        }

        //Humanとぶつかったとき
        if (collider.gameObject.tag == "Human")
        {
            Destroy(collider.gameObject);
            score += unit;

            if (screamAudios != null)
            {
                i = Random.Range(0, screamAudios.Length);
            }
            audioSource.PlayOneShot(screamAudios[i]);
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

            //UIの表示
            uiscript.Gameover();
        }

        if (collision.gameObject.tag == "Ground")
        {
            jumpCount = defaultJumpCount;
        }
    }


}

