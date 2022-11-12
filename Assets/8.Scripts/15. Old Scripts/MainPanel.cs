using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanel : MonoBehaviour
{

    [Header("Panels")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject instructionPanel;
    [SerializeField] private GameObject levelSelectionPanel;
    [SerializeField] private GameObject creditosPanel;
    
    private void Start()
    {
        mainPanel.SetActive(true);
    }

    public void OpenPanel(GameObject panel)
    {
        mainPanel.SetActive(false);
        optionsPanel.SetActive(false);
        instructionPanel.SetActive(false);
        levelSelectionPanel.SetActive(false);
        creditosPanel.SetActive(false);

        panel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
