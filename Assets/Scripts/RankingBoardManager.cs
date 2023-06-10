using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RankingBoardManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Retry()
    {
        Player.score = 0;
        Player.isAnimated = true;
        SceneManager.LoadScene("Stage_1");
    }

    public void BackTitle()
    {
        Player.score = 0;
        Player.isAnimated = true;
        SceneManager.LoadScene("Title");
    }
}
