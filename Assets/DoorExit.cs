using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorExit : MonoBehaviour
{
    [Header("Settings")]
    public string menuSceneName = "Main Menu";

    // This runs when something enters the door's trigger zone
    private void OnTriggerEnter(Collider other)
    {
        // Check if the thing that touched the door is the Player
        // Make sure your VR Player (XR Origin) has the Tag "Player"
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(menuSceneName);
        }
    }
}