using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanGenerator : MonoBehaviour
{
    public GameObject human; //敵のオブジェクト
    public float interval = 3; //何秒に一回敵を発生させるか
    float timer = 0; //タイマー
    public static GameObject newHuman;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime; //タイマーを減らす
        if (timer < 0)
        {
            //タイマーがゼロより小さくなったら
            Spawn(); // Spawnメソッドを呼ぶ
            timer = interval; // タイマーをリセットする
        }
    }

    // 敵を生成するメソッド
    void Spawn()
    {
        float x_position = Random.Range(-1.8f, 1.8f);
        Vector3 spawnPosition = new Vector3(x_position, this.transform.position.y, this.transform.position.z);
        Instantiate(human, spawnPosition, Quaternion.Euler(0, 90, 0));
    }
    

}





 
