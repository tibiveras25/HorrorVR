using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Scene Names")]
    public string gameSceneName = "Maze 1";
    public string creditsSceneName = "Credits";
    public string menuSceneName = "Main Menu";

    // 1. CALL THIS ON THE 'START' BUTTON
    public void PlayGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    // 2. CALL THIS ON THE 'CREDITS' BUTTON
    public void OpenCredits()
    {
        // Loads the separate Credits scene you built
        SceneManager.LoadScene(creditsSceneName);
    }

    // 3. CALL THIS ON THE 'EXIT' BUTTON (Main Menu)
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting...");
    }

    // 4. CALL THIS ON THE 'BACK' BUTTON (Inside the Credits Scene)
    public void BackToMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }
}