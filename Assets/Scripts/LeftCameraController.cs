using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftCameraController : MonoBehaviour
{
    GameObject target;
    bool gameOver;

    public static Vector3 leftCameraPosition;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Ghost_White");
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            if(target.transform.position.x < 30f)
            {
                this.transform.position = target.transform.position + new Vector3(0, 7, -7);
                leftCameraPosition = this.transform.position;
            }

            else if(target.transform.position.x > 30f)
            {
                this.transform.position = RightCameraController.rightCameraPosition + new Vector3(-60, 0, 0);
            }
        }
    }

    public void SetGameOver()
    {
        gameOver = true;
    }
}
