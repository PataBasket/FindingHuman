using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SocialPlatforms;
using static UnityEngine.UI.CanvasScaler;
using UnityEngine.SocialPlatforms.Impl;

public class HumanController : MonoBehaviour
{
    Vector3 initialHumanPosition;
    public static Animator humanAnimator;
    public static float humanSpeed = -2.0f;
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

        
        if(humanAnimator != null)
        {
            if (Player.isAnimated == false)
            {
                //humanAnimator.enabled = false;
                humanAnimator.speed = 0;
                humanSpeed = -5.0f;
            }
            else if (Player.isAnimated == true)
            {
                //humanAnimator.enabled = true;
                humanAnimator.speed = 1;
                humanSpeed = -2.0f;
            }
        }
        else if(humanAnimator == null)
        {
            Debug.Log("is null");
        }
        
        if (playerPosition.transform.position.z - 3 > this.transform.position.z)
        {
            Destroy(this.gameObject);
        }

    }

    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Collision_Check")
        {
            if(other.gameObject.name == "CollisionCheck1")
            {
                transform.DOMove(this.transform.position + new Vector3(1.2f, 0, 0), 1f).SetLink(this.gameObject);
            }

            if(other.gameObject.name == "CollisionCheck2")
            {
                transform.DOMove(this.transform.position + new Vector3(-1.2f, 0, 0), 1f).SetLink(this.gameObject);
            }

            if(other.gameObject.name == "CollisionCheck3")
            {
                transform.DOJump(this.transform.position, 1, 1, 1.5f).SetLink(this.gameObject);
            }

            if (other.gameObject.name == "CollisionCheck4")
            {
                transform.DOMove(this.transform.position + new Vector3(1f, 0, 0), 1f).SetLink(this.gameObject);
            }

            if (other.gameObject.name == "CollisionCheck5")
            {
                transform.DOMove(this.transform.position + new Vector3(-1.3f, 0, 0), 1f).SetLink(this.gameObject);
            }

            if (other.gameObject.name == "CollisionCheck6")
            {
                int RightorLeft = Random.Range(0, 2);
                if(RightorLeft == 0)
                {
                    float moveLeft = Random.Range(-1.6f, -0.5f);
                    transform.DOMove(this.transform.position + new Vector3(moveLeft, 0, 0), 1f).SetLink(this.gameObject);
                }
                else if(RightorLeft == 1)
                {
                    float moveRight = Random.Range(0.5f, 1.6f);
                    transform.DOMove(this.transform.position + new Vector3(moveRight, 0, 0), 1f).SetLink(this.gameObject);
                }
                
            }
        }
    }
    


}
