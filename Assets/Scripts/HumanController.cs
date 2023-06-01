using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SocialPlatforms;

public class HumanController : MonoBehaviour
{
    Vector3 initialHumanPosition;
    public static Animator humanAnimator;
    public static float humanSpeed = -1.0f;
    public Transform playerPosition;

    private float timeCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        //initialHumanPosition = this.transform.position;
        humanAnimator = this.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(0, 0, humanSpeed) * Time.deltaTime;

        //Debug.Log(this.transform.position.z);

        
        if (playerPosition.transform.position.z - 3 > this.transform.position.z)
        {
            Destroy(this.gameObject);
        }

        if(Player.isAnimated == false)
        {
            humanAnimator.enabled = false;
            //humanAnimator.speed = 0;
            humanSpeed = -5.0f;
        }
        else
        {
            humanAnimator.enabled = true;
            //humanAnimator.speed = 1;
            humanSpeed = -1.0f;
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Collision_Check")
        {
            if(other.gameObject.name == "CollisionCheck1")
            {
                Debug.Log("Human collision1!");
                transform.DOMove(this.transform.position + new Vector3(2, 0, 0), 1f);
            }

            if(other.gameObject.name == "CollisionCheck2")
            {
                Debug.Log("Human collision2!");
                transform.DOMove(this.transform.position + new Vector3(-2, 0, 0), 1f);
            }

            if(other.gameObject.name == "CollisionCheck3")
            {
                transform.DOJump(new Vector3(0, 0, 2.5f), 2, 1, 2);
            }
        }
    }
    


}
