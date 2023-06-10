using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallRanking : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking((int)Player.score);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
