using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using static UnityEditor.Progress;

public class TitleUIManager : MonoBehaviour
{
    [SerializeField] private GameObject instructionPanelUI;
    [SerializeField] private GameObject[] _instruction;
    [SerializeField] private Text titleText;

    private int screenCounter = 0;
    private bool isReading = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isReading == true && screenCounter == 0)
        {
            _instruction[0].SetActive(true);
            screenCounter++;
        }
        else if ((isReading == true && screenCounter == 1) && Input.GetKeyDown(KeyCode.Return))
        {
            _instruction[0].SetActive(false);
            _instruction[1].SetActive(true);
            screenCounter++;
        }
        else if ((isReading == true && screenCounter == 2) && Input.GetKeyDown(KeyCode.Return))
        {
            _instruction[1].SetActive(false);
            _instruction[0].SetActive(true);
            screenCounter = 0;
            LeaveInstruction();
        }

    }

    public void StartGame()
    {
        SceneManager.LoadScene("Stage_1");
    }

    public void GameInstructions()
    {
        instructionPanelUI.SetActive(true);
        isReading = true;
    }

    public void LeaveInstruction()
    {
        instructionPanelUI.SetActive(false);
        isReading = false;
    }
}

