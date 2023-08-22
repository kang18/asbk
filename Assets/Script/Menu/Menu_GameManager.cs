using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class Menu_GameManager : MonoBehaviour
{
    public SoundManager soundManager;
    public GameObject mainPanel;
    public GameObject soundPanel;
    public GameObject stagePanel;


    public void Update()
    {


        if (Input.GetButtonDown("Cancel"))
        {
            if(soundPanel.activeSelf)
            {
                soundPanel.SetActive(false);
            }

            if (stagePanel.activeSelf)
            {
                stagePanel.SetActive(false);
                mainPanel.SetActive(true);
            }
        }
    }




    public void SceneToPlay() // Main Menu�� ���� ���� ��ư�� ����Ǿ� ����
    {
        soundManager.SaveSoundSettings();
        SceneManager.LoadScene("Play");
    }

    public void GameStartButton()
    {
        if (!soundPanel.activeSelf)
        {
            mainPanel.SetActive(false);
            stagePanel.SetActive(true);
        }
    }

    public void ToggleUIPanel()
    {
        if(!soundPanel.activeSelf)
        {
            soundPanel.SetActive(!soundPanel.activeSelf);
        }

    }

    public void QuitGame() // ���� ���� ��ư Ŭ��(���� ����)
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

}

