using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesignCanvasController : MonoBehaviour
{
    public Text _scoreText;
    [SerializeField] private GameObject[] _instruction;
    [SerializeField] private GameObject instructionPanelUI;

    private int screenCounter = 0;
    private bool isReading = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _scoreText.text = Player.score.ToString("f0");

        /*
        if (_instructions.activeSelf == false && Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("posed");
            Time.timeScale = 0;
            _instructions.SetActive(true);

        }

        else if (_instructions.activeSelf == true && Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("stop pose");
            Time.timeScale = 1;
            _instructions.SetActive(false);
        }
        */

        if(isReading == false && Input.GetKeyDown(KeyCode.P))
        {
            GameInstructions();
        }

        if (screenCounter == 0 && Input.GetKeyDown(KeyCode.P))
        {
            _instruction[0].SetActive(true);
            screenCounter++;
        }
        else if (screenCounter == 1 && Input.GetKeyDown(KeyCode.P))
        {
            _instruction[0].SetActive(false);
            _instruction[1].SetActive(true);
            screenCounter++;
        }
        else if (screenCounter == 2 && Input.GetKeyDown(KeyCode.P))
        {
            _instruction[1].SetActive(false);
            _instruction[2].SetActive(true);
            screenCounter++;
        }
        else if (screenCounter == 3 && Input.GetKeyDown(KeyCode.P))
        {
            _instruction[2].SetActive(false);
            _instruction[3].SetActive(true);
            screenCounter++;
        }
        else if (screenCounter == 4 && Input.GetKeyDown(KeyCode.P))
        {
            _instruction[3].SetActive(false);
            _instruction[0].SetActive(true);
            screenCounter = 0;
            LeaveInstruction();
        }
    }

    
    public void GameInstructions()
    {
        instructionPanelUI.SetActive(true);
        isReading = true;
        Time.timeScale = 0;
    }

    public void LeaveInstruction()
    {
        instructionPanelUI.SetActive(false);
        isReading = false;
        Time.timeScale = 1;
    }

}
