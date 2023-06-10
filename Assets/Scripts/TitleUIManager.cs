using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUIManager : MonoBehaviour
{
    [SerializeField] private GameObject instructionPanelUI;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Stage_1");
    }

    public void GameInstructions()
    {
        instructionPanelUI.SetActive(true);
    }

    public void LeaveInstruction()
    {
        instructionPanelUI.SetActive(false);
    }
}

