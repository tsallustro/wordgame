using UnityEngine;

public class MainMenuPanelController : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuPanel;

    [SerializeField]
    private GameObject newGamePanel;

    [SerializeField]
    private GameObject loadGamePanel;

    [SerializeField]
    private GameObject optionsPanel;

    public void SetMainMenu()
    {
        mainMenuPanel.SetActive(true);
        newGamePanel.SetActive(false);
        loadGamePanel.SetActive(false);
        optionsPanel.SetActive(false);
    }

    public void SetNewGameMenu()
    {
        mainMenuPanel.SetActive(false);
        newGamePanel.SetActive(true);
        loadGamePanel.SetActive(false);
        optionsPanel.SetActive(false);
    }

    public void SetLoadGameMenu()
    {
        mainMenuPanel.SetActive(false);
        newGamePanel.SetActive(false);
        loadGamePanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void SetOptionsMenu()
    {
        mainMenuPanel.SetActive(false);
        newGamePanel.SetActive(false);
        loadGamePanel.SetActive(false);
        optionsPanel.SetActive(true);
    }
}
