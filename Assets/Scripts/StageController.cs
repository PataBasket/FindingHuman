using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    public float Pspeed = -5.0f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //前に進む
        transform.position += new Vector3(0, 0, Pspeed) * Time.deltaTime;

    }
}
