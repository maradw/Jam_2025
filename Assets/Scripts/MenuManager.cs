using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Scene Settings")]
    public string gameplaySceneName = "GamePlay";

    [Header("Main Menu")]
    public GameObject mainMenuPanel;

    [Header("Pop-up Panels")]
    public GameObject optionsPopup;
    public GameObject creditsPopup;


    void Start()
    {
        if (mainMenuPanel)
        {
            mainMenuPanel.SetActive(true);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(gameplaySceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void OpenOptionsPopup()
    {
        ShowPopup(optionsPopup);
    }

    public void OpenCreditsPopup()
    {
        ShowPopup(creditsPopup);
    }

    void ShowPopup(GameObject popup)
    {
       
        if (popup)
        {
            popup.SetActive(true);
        }
    }
}