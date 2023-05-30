using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HumanController : MonoBehaviour
{
    Vector3 initialHumanPosition;

    // Start is called before the first frame update
    void Start()
    {
        //initialHumanPosition = this.transform.position;
  
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(0, 0, -1.0f) * Time.deltaTime;

        
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
