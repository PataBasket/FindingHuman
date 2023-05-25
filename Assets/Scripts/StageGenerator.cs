using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    public GameObject player;
    public GameObject[] LStages;
    public GameObject[] RStages;

    Vector3 generatingPosition_L;
    Vector3 generatingPosition_R;
    int stageCount = 3;
    int nextPosition = 0;
    public static bool _check = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
    void Update()
    {
        if(_check == true)
        {
            Debug.Log("Generate!");
            int Ltype = Random.Range(0, 3);
            int Rtype = Random.Range(0, 3);
            nextPosition = 15 * stageCount;

            generatingPosition_L = new Vector3(0, 0, nextPosition);
            generatingPosition_R = new Vector3(60, 0, nextPosition);
            Debug.Log(nextPosition);
            Instantiate(LStages[Ltype], generatingPosition_L, Quaternion.identity);
            Instantiate(RStages[Rtype], generatingPosition_R, Quaternion.identity);

            stageCount++;
            _check = false;
        }

    }
    /*
    public void StageGenerate()
    {
        
    }
    */
}
