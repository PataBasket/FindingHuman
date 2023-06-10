using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class TitleUIManager : MonoBehaviour
{
    [SerializeField] private GameObject instructionPanelUI;
    [SerializeField] private Text titleText;

    // Start is called before the first frame update
    void Start()
    {
        
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

