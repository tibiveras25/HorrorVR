using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    [Header("Components")]
    public MeshRenderer sphereRenderer;

    [Header("Timing Settings")]
    public float fadeDuration = 1.5f;      // How long the fade takes
    public float stayBlackDuration = 2.0f; // How long it stays dark before loading

    private void Start()
    {
        // On start, ensure we fade from black to clear so the player "wakes up" in the maze
        if (sphereRenderer != null)
        {
            StartCoroutine(Fade(1, 0));
        }
    }

    public void FadeAndExit(string sceneName)
    {
        StopAllCoroutines(); // Prevent double-fading if they hit the box twice
        StartCoroutine(FadeAndLoad(sceneName));
    }

    private IEnumerator FadeAndLoad(string sceneName)
    {
        // 1. Fade to total black
        yield return Fade(0, 1);

        // 2. Stay black for a moment
        Debug.Log("Holding black screen for " + stayBlackDuration + " seconds...");
        yield return new WaitForSeconds(stayBlackDuration);

        // 3. Load the next scene
        Debug.Log("Loading Scene: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float timer = 0;
        Material mat = sphereRenderer.material;

        // Ensure we are working with a black base color
        Color color = new Color(0, 0, 0, startAlpha);
        mat.color = color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, timer / fadeDuration);

            // Apply the new alpha to the material
            color.a = alpha;
            mat.color = color;

            yield return null;
        }

        // Snap to final alpha to ensure it's perfect
        color.a = endAlpha;
        mat.color = color;
    }
}