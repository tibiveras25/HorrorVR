using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class KeyLockDetector : MonoBehaviour
{
    [Header("Settings")]
    public string keyTag = "Key"; // Make sure your Key object has this Tag!
    public float openDistance = 1.5f;

    [Header("References")]
    public XRGrabInteractable keyInteractable; // Drag the Key here

    void Update()
    {
        if (keyInteractable == null) return;

        // 1. Check if the key is being held by the player
        if (keyInteractable.isSelected)
        {
            // 2. Check the distance between the Key and this Wall
            float distance = Vector3.Distance(keyInteractable.transform.position, transform.position);

            if (distance <= openDistance)
            {
                OpenWall();
            }
        }
    }

    void OpenWall()
    {
        Debug.Log("Wall Unlocked!");
        // You could play a sound here!
        gameObject.SetActive(false);

        // Optional: Destroy the key so the player isn't holding trash
        Destroy(keyInteractable.gameObject);
    }
}