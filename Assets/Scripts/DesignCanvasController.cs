using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesignCanvasController : MonoBehaviour
{
    public Text _scoreText;
    public GameObject _instructions;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _scoreText.text = Player.score.ToString("f0");

        if (_instructions.activeSelf == false && Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("posed");
            Time.timeScale = 0;
            _instructions.SetActive(true);

        }

        else if (_instructions.activeSelf == true && Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("stop pose");
            Time.timeScale = 1;
            _instructions.SetActive(false);
        }
    }


    
}
