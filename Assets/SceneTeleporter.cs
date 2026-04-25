using UnityEngine;
using UnityEngine.SceneManagement; // Required for changing scenes

public class SceneTeleporter : MonoBehaviour
{
    [Header("Settings")]
    public string sceneToLoad; // Type the name of your next scene here

    public ScreenFader fader; // Drag your FadeSphere here in the Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Tell the fader to start. It will handle the scene change.
            fader.FadeAndExit(sceneToLoad);
        }
    }
}