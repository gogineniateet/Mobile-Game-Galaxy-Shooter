using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public GameObject instructionPanel;
    public GameObject settingsPanel;
    //public GameObject menuPanel;
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void InstructionPanel()
    {
        instructionPanel.SetActive(true);
    }
    public void SettingPanel()
    {
        settingsPanel.SetActive(true);
    }
    public void BackButton()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
