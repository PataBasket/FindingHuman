using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject[] LStages;
    [SerializeField] public GameObject[] RStages;

    Vector3 generatingPosition_L;
    Vector3 generatingPosition_R;
    public static bool _check = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
    void Update()
    {
        if (_check == true)
        {
            Debug.Log("Generate!");
            int Ltype = Random.Range(0, 3);
            int Rtype = Random.Range(0, 3);

            generatingPosition_L = new Vector3(0, 0, 59.9f);
            generatingPosition_R = new Vector3(60, 0, 59.9f);
            Instantiate(LStages[Ltype], generatingPosition_L, Quaternion.identity);
            Instantiate(RStages[Rtype], generatingPosition_R, Quaternion.identity);

            _check = false;
        }

    }
    
}
